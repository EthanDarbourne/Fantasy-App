using FPL_Project.Players;
using System;
using System.Collections.Generic;
using System.IO.Enumeration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPL_Project.Data
{
	public class GameweekDataFile : DataFile
	{



		public GameweekDataFile( int gameweek, bool create = false) : base( $"Gameweek{gameweek}.csv" )
		{
			if(create && File.Exists(fileName))
			{
				throw new Exception( "File already made for this week" );
			}
		}


		public override Collection ReadFile()
		{
			using StreamReader r = new StreamReader( fileName ) ;
			
			string line;
			while ( ( line = r.ReadLine() ) != null )
			{
				throw new NotImplementedException();
				//Players_.Add( Players.LoadPlayers( line ) );
			}

			throw new NotImplementedException();
		}

		public override void WriteToFile(Collection gameweekData)
		{

			using StreamWriter w = new StreamWriter( fileName );

			foreach (var player in Players_)
			{
				w.WriteLine( player.Stringify() );
			}

		}
	}
}
