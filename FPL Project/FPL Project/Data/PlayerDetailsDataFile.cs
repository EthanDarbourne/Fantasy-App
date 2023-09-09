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

		private PlayerDetailsCollection PlayerDetails_ = new();

		

		public PlayerDetailsDataFile() : base("PlayerDetails")
		{
			
		}

		public override void ReadFile()
		{

			using StreamReader r = new StreamReader( fileName );
			string line;
			while ( ( line = r.ReadLine() ) != null )
			{
				PlayerDetails_.AddPlayerDetails( PlayerDetails.LoadPlayerDetails( line ) );
			}

		}

		public void AddPlayerDetails( PlayerDetails playerDetails )
		{
			NeedsWrite_ = true;
			PlayerDetails_.AddPlayerDetails( playerDetails );
		}

		public override void WriteToFile()
		{
			using StreamWriter w = new StreamWriter( fileName );
				
			foreach (PlayerDetails player in PlayerDetails_)
			{
				w.WriteLine( player.Stringify() );
			}
				
			
		}
	}
}
