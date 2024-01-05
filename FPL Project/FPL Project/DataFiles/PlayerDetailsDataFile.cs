using FPL_Project.Collections;
using FPL_Project.Data;
using FPL_Project.Players;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPL_Project.DataFiles
{
    public class PlayerDetailsDataFile : DataFile<PlayerDetailsCollection>
	{
		public PlayerDetailsDataFile() : base( "Data/PlayerDetails.csv" )
		{
		}

		public PlayerDetailsDataFile(string fileName) : base( fileName )
		{
		}

		public override string Header => "Id,Name,Team,Position,Price";

		public override PlayerDetailsCollection ReadDataFile()
		{
			PlayerDetailsCollection players = new ();
			if ( !File.Exists( fileName ) ) return players;

			using StreamReader r = new( fileName );
			string? line = r.ReadLine(); // header
			while ( ( line = r.ReadLine() ) is not null )
			{
				var player = new PlayerDetails();
				player.LoadFromLine( line );
				players.AddPlayerDetails( player );
			}
			r.Close();
			return players;
		}

		public override void WriteToFile( PlayerDetailsCollection players)
		{
			using StreamWriter w = new( fileName );

			w.WriteLine( Header );
			foreach (PlayerDetails player in players )
			{
				w.WriteLine( player.Stringify() );
			}

			w.Close();
		}
	}
}
