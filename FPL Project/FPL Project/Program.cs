// See https://aka.ms/new-console-template for more information
using FPL_Project.Data;
using FPL_Project.Players;

Console.WriteLine("Hello, World!");

var Config = new ConfigDataFile();
var Players = new PlayerCollection();
var playerDetails = new PlayerDetailsDataFile();
var gameweekData = new List<GameweekDataFile>(); // one for each gameweek that has passed



void LoadConfigData()
{
    
}


void ReadAllPlayers()
{

}



void EnterNewWeek()
{
    // create a new csv file (soon: database table) for the week
    // ask for the stats of all players
    Console.Write( "What is the next week? : " );
    var week = int.Parse(Console.ReadLine()!);
    var file = new GameweekDataFile(week, true);


    var players = Players.PlayersByTeam;

	foreach (var team in players)
    {
        foreach(var player in team)
        {
            Console.WriteLine("")
        }
    }

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
	var details = new PlayerDetails( name, team, pos );
	playerDetails.AddPlayerDetails( details );
    return true;
}

void AddPlayersUntilQuit()
{
    while( AddNewPlayer() )
    {
	}
    
}




bool quit = false;





LoadConfigData();

ReadAllPlayers();

while (!quit)
{
    string instr = Console.ReadLine()!.Trim();

    switch(instr)
    {
        case "LoadPlayerData":
            // load player data from csv files

            break;
		case "EnterNewWeek":
            EnterNewWeek();
			break;
        case "AddNewPlayer":
            AddNewPlayer();
            break;
        case "AddPlayersUntilQuit":
            AddPlayersUntilQuit();
            break;
		case "PrintPlayer":
            string name = Console.ReadLine()!;
            Players.GetPlayer( name )?.Print();
            break;
        
		case "quit":
        case "Quit":
        case "Q":
            quit = true;
            break;
        case "help":
        default:
            Console.WriteLine("Showing Possible Commands");
            Console.WriteLine("LoadPlayerData: Load player data from spreadsheet");
            Console.WriteLine("Quit: Exit the program");
            break;
    }
}



Console.ReadKey();