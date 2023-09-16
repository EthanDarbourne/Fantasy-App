using FPL_Project.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPL_Project.Collections
{
	public class GameweekDataCollection : Collection
	{
		private Dictionary<string, GameweekData> Players_ = new();

		private List<List<GameweekData>> PlayersByTeam_ = new();

		private int Gameweek_;

		public GameweekDataCollection( int gameweek )
		{
			Gameweek_ = gameweek;
			for ( int i = 0; i < 20; ++i )
			{
				PlayersByTeam_.Add( new() );
			}
		}

		public int Count => Players_.Count;

		public void AddPlayer( GameweekData player )
		{
			Players_.Add( player.Name, player );
			PlayersByTeam_[ ( int ) player.Team ].Add( player );
			Info_.Add( player ); // need to add this in order of players
		}

		public bool DeletePlayer( string player )
		{
			if ( !Players_.ContainsKey( player ) )
			{
				return false;
			}
			DeleteInfo( info => (info as GameweekData)!.Name == player);
			Players_.Remove( player );
			for ( int i = 0; i < 20; ++i )
			{
				bool found = false;
				for ( int j = 0; j < PlayersByTeam_[ i ].Count; ++j )
				{
					if ( PlayersByTeam_[ i ][ j ].Name == player )
					{
						PlayersByTeam_[ i ].RemoveAt( j );
						return true;
					}
				}
				if ( found ) break;

			}
			return false; // won't get here
		}

		public GameweekData? GetGameweekData( string name )
		{
			return Players_[ name ];
		}


	}
}
