// See https://aka.ms/new-console-template for more information
using FPL_Project.Collections;
using FPL_Project.Data;
using FPL_Project.Players;
using FPL_Project.DataFiles;
using System.Text;

var ConfigData = new ConfigDataFile();

//var Players = new PlayerCollection(); // Data
var PlayerDetailsData = new PlayerDetailsDataFile();
var GameweekData = new List<GameweekDataFile>(); // one for each gameweek that has passed
var DeletedData = new DeletedDataFile();


var ConfigCollection = ConfigData.ReadDataFile() as ConfigCollection;
var PlayerCollection = PlayerDetailsData.ReadDataFile() as PlayerDetailsCollection;
var GameweeksCollection = new List<GameweekDataCollection>();
var DeletedCollection = new DeletedCollection();

var TotalWeeks = int.Parse( ConfigCollection.GetValue( "Gameweek" ) );

for (int week = 1; week <= TotalWeeks; ++week)
{
    GameweekData.Add( new GameweekDataFile( week ) );
	GameweeksCollection.Add( GameweekData[ week - 1 ].ReadDataFile() as GameweekDataCollection );
} 


void VerifyData()
{
    bool missingData = false;
    if(TotalWeeks != GameweeksCollection.Count)
    {
        missingData = true;
		Console.WriteLine( "Warning: TotalWeeks is not equal to number of Gameweeks" );
    }
    for(int i = 0; i < GameweeksCollection.Count; ++i)
    {
        if(PlayerCollection.Count != GameweeksCollection[i].Count)
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

GameweekData GetGameweekData(string name, int week)
{
	var ret = new GameweekData(week);

	var attributes = GameweekData[0].Header.Split(',');

	var line = new StringBuilder(); 
    line.Append($"{name},{week},");

    var player = PlayerCollection.GetPlayer( name );

    Console.WriteLine( $"Enter data for {name} of {player.Team}" );


    var input = Console.ReadLine().Trim();

    var given = input.Split( '\t' ).Skip(3).ToArray();

    line.AppendJoin( ',', new string[] { given[ 0 ], given[ 2 ], given[ 3 ], given[ 4 ], given[ 5 ], given[ 6 ], given[ 8 ], given[ 9 ], given[ 10 ], given[16], given[ 17 ], given[ 18 ] } );

 //   for(int i = 2; i < attributes.Length; ++i)
 //   {
	//	Console.Write( $"Enter {attributes[i]}: " );
 //       var input = Console.ReadLine().Trim();
	//	line += ',' + input;
 //       if ( input == "redo" ) return GetGameweekData( name, week );
	//}

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

    var newWeek = TotalWeeks + 1;

	GameweekData.Add( new GameweekDataFile( newWeek, true) );
	var file = new GameweekDataCollection( newWeek );

    Console.WriteLine( $"Entering data for gameweek {newWeek}" );
    var players = PlayerCollection!.PlayersByTeam;
    
	foreach (var team in players)
    {
		foreach (var player in team)
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
    for(int i = 0; i < weeks; ++i )
    {
		GameweekData.Add( new GameweekDataFile( TotalWeeks + i + 1, true ) );
		newDatas.Add( new GameweekDataCollection( TotalWeeks + i + 1) );
    }

	var players = PlayerCollection!.PlayersByTeam;

	foreach ( var team in players )
	{
		foreach ( var player in team )
		{
            for (var week = weeks - 1; week >= 0; --week)
            {
				var data = GetGameweekData( player.Name, TotalWeeks + week + 1 );
                newDatas[week].AddPlayer( data );
			}
		}
	}

	for ( int i = 0; i < weeks; ++i )
	{
		GameweeksCollection.Add( newDatas[i] );
	}

	TotalWeeks += weeks;
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
        GameweeksCollection[week].AddPlayer( data );
	}

	return true;
}

void AddPlayersUntilQuit()
{
    while( AddNewPlayer() )
    {
	}
    
}

void AddPlayersFromFile()
{
    Console.Write( "Enter file name : " );
    var filename = Console.ReadLine();

    var playerDetails = new PlayerDetailsDataFile( filename );

    var players = playerDetails.ReadDataFile() as PlayerDetailsCollection;
    foreach(PlayerDetails player in players)
    {
        PlayerCollection.AddPlayerDetails( player );
	}
}

void DeletePlayer()
{
	Console.Write( "Enter player name : " );
	var playername = Console.ReadLine();
    var player = PlayerCollection.GetPlayer( playername );
    if(player is null)
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

    if(key.KeyChar == 'y')
    {
        var deleted = true;
        DeletedCollection.AddInfo( PlayerCollection.GetPlayer( playername ), "DeletePlayer" );
		deleted &= PlayerCollection.DeletePlayer( playername );
		for ( int i = 0; i < GameweeksCollection.Count ; ++i)
        {
			DeletedCollection.AddInfo( GameweeksCollection[ i ].GetGameweekData( playername ), "DeletePlayer" );
			GameweeksCollection[ i ].DeletePlayer( playername ); // might not have player data across every week
		}
		if(deleted) Console.WriteLine( "Player deleted" );
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
        Console.WriteLine( $"Header: {GameweekData[0].Header}" );
		for ( int i = 0; i < GameweeksCollection.Count; ++i )
		{
            Console.WriteLine($"Gameweek {i + 1} : {GameweeksCollection[ i ].GetGameweekData( playername ).Stringify()}");
		}
	}
}

void WriteToAllFiles()
{
    ConfigCollection.UpdateValue( "Gameweek", TotalWeeks.ToString() );

	ConfigData.WriteToFile( ConfigCollection );
    PlayerDetailsData.WriteToFile( PlayerCollection );
    if(GameweekData.Count != GameweeksCollection.Count)
    {
        throw new Exception( "Gameweek data is mismatched" );
    }
    for(int week = 1; week <= GameweekData.Count; ++week)
    {
        if ( GameweeksCollection[ week - 1].Count > PlayerCollection.Count)
        {
            Console.WriteLine( $"Warning: Have gameweek {week} data for player that doesn't exist" );
        }
		if ( GameweeksCollection[ week - 1 ].Count < PlayerCollection.Count )
		{
			Console.WriteLine( $"Warning: Missing gameweek {week} data for player" );
		}
		GameweekData[ week - 1 ].WriteToFile( GameweeksCollection[ week - 1 ] );
    }
    DeletedData.WriteToFile( DeletedCollection );
}

bool quit = false;

try
{


    while ( !quit )
    {
        Console.Write( "Enter an instruction : " );
        string instr = Console.ReadLine()!.Trim();

        switch ( instr.ToLower() )
        {
            case "getdatainfo":
                PrintDataInfo();
                break;
            case "enternewweek":
                EnterNewWeek();
                break;
            case "entermultipleweeks":
                EnterMultipleWeeks();
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
            case "reloadplayerdetails":
                break;
            case "lookupplayer":
                LookupPlayer();
                break;
            case "generatedata":
                
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
    }


}
catch(Exception e)
{
    WriteToAllFiles();
    Console.WriteLine( e.ToString() );
    return;
}
