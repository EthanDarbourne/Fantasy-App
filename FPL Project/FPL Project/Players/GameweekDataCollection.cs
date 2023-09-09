using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPL_Project.Players
{
	public class GameweekDataCollection
	{
		private Dictionary<string, GameweekData> Players_ = new();

		private List<List<GameweekData>> PlayersByTeam_ = new();

		public GameweekDataCollection()
		{
			for ( int i = 0; i < 20; ++i )
			{
				PlayersByTeam_.Add( new() );
			}
		}

		public void AddPlayer( GameweekData player )
		{
			Players_.Add( player.Name, player );
			PlayersByTeam_[ ( int ) player.Team ].Add( player );
		}

		public GameweekData? GetGameweekData( string name )
		{
			return Players_[ name ];
		}

		public int Count => Players_.Count;
	}
}
