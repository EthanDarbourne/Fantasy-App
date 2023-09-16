using FPL_Project.Players;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPL_Project.Data
{
	public class PlayerDetailsDataFile : DataFile
	{
		

		public PlayerDetailsDataFile() : base( "Data/PlayerDetails.csv" )
		{
			
		}

		public PlayerDetailsDataFile(string fileName) : base( fileName )
		{

		}

		public override string Header => "Name,Team,Position,Price";

		public override Collection ReadDataFile()
		{
			var players = new PlayerDetailsCollection();
			if ( !File.Exists( fileName ) ) return players;

			using StreamReader r = new StreamReader( fileName );
			string line = r.ReadLine(); // header
			while ( ( line = r.ReadLine() ) != null )
			{
				var player = new PlayerDetails();
				player.LoadFromLine( line );
				players.AddPlayerDetails( player );
			}
			r.Close();
			return players;
		}

		public override void WriteToFile(Collection players)
		{
			using StreamWriter w = new StreamWriter( fileName );

			w.WriteLine( Header );
			foreach (PlayerDetails player in players )
			{
				w.WriteLine( player.Stringify() );
			}

			w.Close();
		}
	}
}
