// See https://aka.ms/new-console-template for more information
using FPL_Project.Players;

Console.WriteLine("Hello, World!");




bool quit = false;

var Players = new PlayerCollection();


while(!quit)
{
    string instr = Console.ReadLine()!.Trim();

    switch(instr)
    {
        case "LoadPlayerData":


            break;
        case "PrintPlayer":
            string name = Console.ReadLine()!;
            Players.GetPlayer(name).Print();
            break;
        case "quit":
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