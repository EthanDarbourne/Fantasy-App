// See https://aka.ms/new-console-template for more information
using FPL_Project.Collections;
using FPL_Project.Data;
using FPL_Project.Players;
using FPL_Project.DataFiles;
using System.Text;
using FPL_Project.Api;
using FPL_Project.Generator;

var ConfigData = new ConfigDataFile();

//var Players = new PlayerCollection(); // Data
PlayerDetailsDataFile PlayerDetailsData = new();
FixtureDataFile FixtureData = new();
DeletedDataFile DeletedData = new();
List<GameweekDataFile> GameweekData = new(); // one for each gameweek that has passed

ConfigCollection ConfigCollection = ConfigData.ReadDataFile();
PlayerDetailsCollection PlayerCollection = PlayerDetailsData.ReadDataFile();
FixtureCollection FixturesCollection = FixtureData.ReadDataFile();
DeletedCollection DeletedCollection = new (); // not loaded, only appended to file
List<GameweekDataCollection> GameweeksCollection = new ();

bool success = int.TryParse( ConfigCollection.GetValue( "Gameweek" ), out var TotalWeeks);

for ( int week = 1; week <= TotalWeeks; ++week )
{
	GameweekData.Add( new GameweekDataFile( week ) );
	GameweeksCollection.Add( GameweekData[ week - 1 ].ReadDataFile() as GameweekDataCollection );
}

void VerifyData()
{
	bool missingData = false;
	if ( TotalWeeks != GameweeksCollection.Count )
	{
		missingData = true;
		Console.WriteLine( "Warning: TotalWeeks is not equal to number of Gameweeks" );
	}
	for ( int i = 0; i < GameweeksCollection.Count; ++i )
	{
		if ( PlayerCollection.Count != GameweeksCollection[ i ].Count )
		{
			missingData = true;
			Console.WriteLine( $"Warning: Player count not equal to gameweek count for week {i}" );
		}
	}
	if ( missingData ) FindMissingData();
}
void FindMissingData()
{
	// make sure we all gameweek datas have players, and players have gameweek data. List all players with missing gameweek data for user to fill in (todo);
}


void PrintDataInfo()
{
	Console.WriteLine( $"Total Weeks: {TotalWeeks}" );
	Console.WriteLine( $"Player Count: {PlayerCollection.Count}" );
}

GameweekData GetGameweekData( string name, int week )
{
	GameweekData ret = new ( week );

	string[] attributes = GameweekData[ 0 ].Header.Split( ',' );

	StringBuilder line = new ();
	line.Append( $"{name},{week}," );

	var player = PlayerCollection.GetPlayer( name );

	Console.WriteLine( $"Enter data for {name} of {player.Team}" );


	var input = Console.ReadLine()!.Trim();

	if ( input == "0" )
	{
		line.Append( ",0,0,0,0,0,0,0,0,0,0,0,0" );
	}
	else
	{
		string[] given = input.Split( '\t' ).Skip( 3 ).ToArray();

		line.AppendJoin( ',', new string[] { given[ 0 ], given[ 2 ], given[ 3 ], given[ 4 ], given[ 5 ], given[ 6 ], given[ 8 ], given[ 9 ], given[ 10 ], given[ 16 ], given[ 17 ], given[ 18 ] } );
	}

	ret.LoadFromLine( line.ToString() );

	Console.WriteLine( GameweekData[ 0 ].Header );
	Console.WriteLine( ret.Stringify() );

	Console.WriteLine( "Confirm Values (y/n) : " );
	//var c = Console.ReadKey();
	//if ( c.KeyChar == 'n' ) return GetGameweekData( name, week );
	return ret;
}

void EnterNewWeek()
{
	// create a new csv file (soon: database table) for the week
	// ask for the stats of all players

	int newWeek = TotalWeeks + 1;

	GameweekData.Add( new GameweekDataFile( newWeek, true ) );
	GameweekDataCollection file = new ( newWeek );

	Console.WriteLine( $"Entering data for gameweek {newWeek}" );
	var players = PlayerCollection!.PlayersByTeam;

	foreach ( var team in players )
	{
		foreach ( var player in team )
		{
			var data = GetGameweekData( player.Name, newWeek );

			file.AddPlayer( data );
		}
	}
	++TotalWeeks;
}

void EnterMultipleWeeks()
{
	Console.Write( "Enter how many weeks you want to input : " );
	var weeks = int.Parse( Console.ReadLine() );

	// will enter this many lines at a time
	// weeks are entered highest first

	var newDatas = new List<GameweekDataCollection>();
	for ( int i = 0; i < weeks; ++i )
	{
		GameweekData.Add( new GameweekDataFile( TotalWeeks + i + 1, true ) );
		newDatas.Add( new GameweekDataCollection( TotalWeeks + i + 1 ) );
	}

	var players = PlayerCollection!.PlayersByTeam;

	foreach ( var team in players )
	{
		foreach ( var player in team )
		{
			for ( var week = weeks - 1; week >= 0; --week )
			{
				var data = GetGameweekData( player.Name, TotalWeeks + week + 1 );
				newDatas[ week ].AddPlayer( data );
			}
		}
	}

	for ( int i = 0; i < weeks; ++i )
	{
		GameweeksCollection.Add( newDatas[ i ] );
	}

	TotalWeeks += weeks;
}

// one-off
async Task LoadFixtureData()
{
	FixturesCollection = await FantasyApi.LoadFixtureDetails();
}

bool AddNewPlayer()
{
	Console.Write( "Enter player name: " );
	string name = Console.ReadLine()!;
	if ( name.ToLower() == "q" || name.ToLower() == "quit" ) return false;
	Console.Write( "Enter player team: " );
	Teams team = TeamReader.ReadTeam( Console.ReadLine()! );
	Console.Write( "Enter player position: " );
	Positions pos = PositionReader.ReadPosition( Console.ReadLine()! );
	Console.Write( "Enter player price: " );
	int price = int.Parse( Console.ReadLine()! );
	var details = new PlayerDetails( name, team, pos, price );
	PlayerCollection.AddPlayerDetails( details );

	for ( int week = 1; week <= TotalWeeks; ++week )
	{
		Console.WriteLine( $"Enter data for gameweek {week}" );
		var data = GetGameweekData( name, week );
		GameweeksCollection[ week ].AddPlayer( data );
	}

	return true;
}

void AddPlayersUntilQuit()
{
	while ( AddNewPlayer() )
	{
	}

}

void AddPlayersFromFile()
{
	Console.Write( "Enter file name : " );
	var filename = Console.ReadLine();

	var playerDetails = new PlayerDetailsDataFile( filename );

	var players = playerDetails.ReadDataFile();
	foreach ( PlayerDetails player in players )
	{
		PlayerCollection.AddPlayerDetails( player );
	}
}

void AddGameweekData()
{
	Console.Write( "Enter player name : " );
	var name = Console.ReadLine();

	for ( int week = 1; week <= TotalWeeks; ++week )
	{
		Console.WriteLine( $"Enter data for gameweek {week}" );
		var data = GetGameweekData( name, week );
		GameweeksCollection[ week ].UpdatePlayer( data );
	}
}

void DeletePlayer()
{
	Console.Write( "Enter player name : " );
	var playername = Console.ReadLine();
	var player = PlayerCollection.GetPlayer( playername );
	if ( player is null )
	{
		Console.WriteLine( "Player does not exist" );
		return;
	}
	Console.WriteLine( $"Player name: {player.Name}" );
	Console.WriteLine( $"Team: {player.Team}" );
	Console.WriteLine( $"Position: {player.Position}" );
	Console.Write( "Are you sure you want to delete this player? (Y/N) : " );
	var key = Console.ReadKey();
	Console.WriteLine();

	if ( key.KeyChar == 'y' )
	{
		var deleted = true;
		DeletedCollection.AddInfo( PlayerCollection.GetPlayer( playername ), "DeletePlayer" );
		deleted &= PlayerCollection.DeletePlayer( playername );
		for ( int i = 0; i < GameweeksCollection.Count; ++i )
		{
			DeletedCollection.AddInfo( GameweeksCollection[ i ].GetGameweekData( playername ), "DeletePlayer" );
			GameweeksCollection[ i ].DeletePlayer( playername ); // might not have player data across every week
		}
		if ( deleted ) Console.WriteLine( "Player deleted" );
	}

}

void LookupPlayer()
{
	Console.Write( "Enter player name : " );
	var playername = Console.ReadLine();
	var player = PlayerCollection.GetPlayer( playername );
	if ( player is null )
	{
		Console.WriteLine( "Player does not exist" );
		return;
	}
	Console.WriteLine( $"Player name: {player.Name}" );
	Console.WriteLine( $"Team: {player.Team}" );
	Console.WriteLine( $"Position: {player.Position}" );
	Console.Write( "Would you like gameweek information (Y/N) : " );
	var key = Console.ReadKey();
	Console.WriteLine();

	if ( key.KeyChar == 'y' )
	{
		Console.WriteLine( $"Header: {GameweekData[ 0 ].Header}" );
		for ( int i = 0; i < GameweeksCollection.Count; ++i )
		{
			Console.WriteLine( $"Gameweek {i + 1} : {GameweeksCollection[ i ].GetGameweekData( playername )?.Stringify()}" );
		}
	}
}

void WriteToAllFiles()
{
	ConfigCollection.UpdateValue( "Gameweek", TotalWeeks.ToString() );

	ConfigData.WriteToFile( ConfigCollection );
	PlayerDetailsData.WriteToFile( PlayerCollection );
	if ( GameweekData.Count != GameweeksCollection.Count )
	{
		throw new Exception( "Gameweek data is mismatched" );
	}
	for ( int week = 1; week <= GameweekData.Count; ++week )
	{
		if ( GameweeksCollection[ week - 1 ].Count > PlayerCollection.Count )
		{
			Console.WriteLine( $"Warning: Have gameweek {week} data for player that doesn't exist" );
		}
		if ( GameweeksCollection[ week - 1 ].Count < PlayerCollection.Count )
		{
			Console.WriteLine( $"Warning: Missing gameweek {week} data for player" );
		}
		GameweekData[ week - 1 ].WriteToFile( GameweeksCollection[ week - 1 ] );
	}
	FixtureData.WriteToFile( FixturesCollection );

	DeletedData.WriteToFile( DeletedCollection );
}


async Task LoadAllData( bool reload = false )
{
	TotalWeeks = await FantasyApi.GetWeeksPlayed();


	if ( reload ) PlayerCollection = await FantasyApi.LoadNewPlayerDetails( PlayerCollection );


	GameweeksCollection = new();

	for ( int week = 1; week <= TotalWeeks; ++week )
	{
		var gameweekData = await FantasyApi.GetFullGameweekData( week, PlayerCollection );
		foreach ( PlayerDetails player in PlayerCollection )
		{
			var data = gameweekData.GetGameweekData( player.Name );
			if ( data is null )
			{
				var gwData = new GameweekData();
				gwData.SetExtraInfo( player.Id, player.Name, player.Team, gameweekData.Week, new() );
				gameweekData.AddPlayer( gwData );
				Console.WriteLine( $"Added missing Gameweek data for {player.Name}, {player.Team}" );
			}
		}
		GameweeksCollection.Add( gameweekData );
	}

	while ( GameweekData.Count < GameweeksCollection.Count )
	{
		GameweekData.Add( new GameweekDataFile( GameweekData.Count + 1 ) );
	}

	FixturesCollection = await FantasyApi.LoadFixtureDetails();

	double Get90MinutesConceded( List<GameweekData> teamData )
	{
		foreach ( var data in teamData )
		{
			if ( data.MinutesPlayed == 90 )
			{
				return data.xGoalsConceded;
			}
		}
		return 0; // will never get here
	}

	foreach ( Fixture fixture in FixturesCollection )
	{
		if ( fixture.Gameweek > TotalWeeks ) continue;
		var gameweekData = GameweeksCollection[ fixture.Gameweek - 1 ];
		var homeTeamData = gameweekData.GetTeamData( fixture.Home );
		var awayTeamData = gameweekData.GetTeamData( fixture.Away );
		double xGoalsScored = Get90MinutesConceded( awayTeamData );
		double xGoalsConceded = Get90MinutesConceded( homeTeamData );

		fixture.SetExpectedData( fixture.Home, xGoalsScored, xGoalsConceded );

	}
}

void LoadSpecialData()
{

	var fullPlayers = new FullPlayerCollection();

	var playerStrings = new List<string>();

	foreach ( PlayerDetails player in PlayerCollection )
	{
		var sb = new StringBuilder( player.Name );
		var gwData = new List<GameweekData>();
		int goals = 0;
		foreach ( var gameweek in GameweeksCollection )
		{
			var data = gameweek.GetGameweekData( player.Name );
			sb.Append( $",{data.Goals}" );
			goals += data.Goals;

		}

		if ( goals > 0 ) playerStrings.Add( sb.ToString() );
	}
	using var w = new StreamWriter( "../../../../../Data/GoalsScored.csv" );
	w.WriteLine( "Name,Week1,Week2,Week3,Week4,Week5,Week6" );
	foreach ( var s in playerStrings )
	{
		w.WriteLine( s );
	}
	w.Close();
}


async Task LoadNewData()
{
	TotalWeeks = await FantasyApi.GetWeeksPlayed();

	int curWeeks = GameweeksCollection!.Count;


	for ( int i = curWeeks + 1; i <= TotalWeeks; ++i )
	{
		var data = await FantasyApi.GetFullGameweekData( i, PlayerCollection );
		GameweekData.Add( new GameweekDataFile( i ) );
		GameweeksCollection.Add( data );
	}

	FixturesCollection = await FantasyApi.LoadFixtureDetails();
}

bool quit = false;
string? lastInstruction = null;

async Task RunInstruction( string? instruction = null )
{
	if ( instruction is null )
	{
		Console.Write( "Enter an instruction : " );
		instruction = Console.ReadLine()!.Trim();
	}

	switch ( instruction.ToLower() )
	{
		case "incrementweeks":
			++TotalWeeks;
			break;
		case "printweeks":
			Console.WriteLine( $"Total Weeks: {TotalWeeks}" );
			break;
		case "v":
			LoadSpecialData();
			break;
		case "getdatainfo":
			PrintDataInfo();
			break;
		case "enternewweek":
			EnterNewWeek();
			break;
		case "entermultipleweeks":
			EnterMultipleWeeks();
			break;
		case "loadfixturedata":
			await LoadFixtureData();
			break;
		case "addgameweekdata":
			AddGameweekData();
			break;
		case "addnewplayer":
			AddNewPlayer();
			break;
		case "addplayersuntilquit":
			AddPlayersUntilQuit();
			break;
		case "addplayersfromfile":
			AddPlayersFromFile();
			break;
		case "deleteplayer":
			DeletePlayer();
			break;
		case "loadalldata":
			await LoadAllData();
			break;
		case "reloadalldata":
		case "u":
			await LoadAllData( true );
			break;
		case "lookupplayer":
			LookupPlayer();
			break;
		case "generatetrainingdata":
			await Generator.GenerateTrainingData( TotalWeeks, PlayerCollection, GameweeksCollection, FixturesCollection );
			break;
		case "generatetrainingdataforplayer":
			Console.Write( "Enter a player name: " );
			string player = Console.ReadLine()!;
			await Generator.GenerateTrainingDataForPlayer( player, TotalWeeks, PlayerCollection, GameweeksCollection, FixturesCollection );
			break;
		case "generatedataforweek":
			Console.Write( "Enter week number: " );
			int week = int.Parse(Console.ReadLine()!);
			await Generator.GeneratePredictionDataForWeek( week, TotalWeeks, PlayerCollection, GameweeksCollection, FixturesCollection );
			break;
		case "generatedataabovethreshold":
			Console.Write( "Enter threshold number: " );
			int threshold = int.Parse( Console.ReadLine()! );
			await Generator.GenerateTrainingDataAboveThreshold( TotalWeeks, PlayerCollection, GameweeksCollection, FixturesCollection, threshold );
			break;
		case "backupdata":
			break;
		case "reportonteam":
			// report on the team created by the ML model
			break;
		case "repeat":
			if ( lastInstruction is not null )
			{
				await RunInstruction( lastInstruction );
			}
			break;
		case "quit":
		case "q":
			WriteToAllFiles();
			quit = true;
			break;
		case "help":
		default:
			Console.WriteLine( "Showing Possible Commands" );
			Console.WriteLine( "EnterNewWeek: Enter a new week of player data" );
			Console.WriteLine( "AddNewPlayer: Add a new player" );
			Console.WriteLine( "AddPlayersUntilQuit: Add new players until you quit" );
			Console.WriteLine( "AddPlayersFromFile: Add players from a csv file" );
			Console.WriteLine( "DeletePlayer: Delete a player" );
			Console.WriteLine( "ReloadPlayerDetails: Reload all player details" );
			Console.WriteLine( "LookupPlayer: Find player by name and print data on the player" );
			Console.WriteLine( "Quit: Exit the program" );
			break;
	}

	lastInstruction = instruction;

}

try
{
	while ( !quit )
	{
		await RunInstruction();
	}
}
catch ( Exception e )
{
	Console.WriteLine( e.ToString() );
	Console.WriteLine( "Should we write to files? (Y/N):" );
	var key = Console.ReadKey();
	if ( key.KeyChar == 'y' ) WriteToAllFiles();
	return;
}


// need to save all player names we want to a different file that never gets changed so we know what players to grab from fantasy api