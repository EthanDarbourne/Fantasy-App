using FPL_Project.Data;
using FPL_Project.DataFiles;
using FPL_Project.Players;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPL_Project.Collections
{
	public class PlayerDetailsCollection : Collection
	{

		private Dictionary<string, PlayerDetails> Players_ = new();

		private List<List<PlayerDetails>> PlayersByTeam_ = new();
		public int Team_ = 0;
		public int Player_ = 0;

		public PlayerDetailsCollection()
		{
			for ( int i = 0; i < 20; ++i )
			{
				PlayersByTeam_.Add( new() );
			}
		}

		public int Count => Players_.Count;

		public List<List<PlayerDetails>> PlayersByTeam => PlayersByTeam_;

		//public override object Current => PlayersByTeam_[ Team_ ][ Player_ ];

		public void AddPlayerDetails( PlayerDetails player )
		{
			if(Players_.ContainsKey( player.Name ) )
			{
				return;
			}
			Players_.Add( player.Name, player );
			PlayersByTeam_[ ( int ) player.Team - 1].Add( player );
			Info_.Add( player );
		}

		public bool DeletePlayer(string player)
		{
			if ( !Players_.ContainsKey( player ) )
			{
				return false;
			}
			DeleteInfo( info => ( info as PlayerDetails )!.Name == player );
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

		public PlayerDetails? GetPlayer( string name )
		{
			Players_.TryGetValue( name, out var player );
			return player;
		}

		public static PlayerDetailsCollection LoadFromDataFile()
		{
			var players = new PlayerDetailsCollection();

			var playerData = new PlayerDetailsDataFile();

			return players;
		}

		//public override bool MoveNext()
		//{
		//	if ( Team_ >= PlayersByTeam_.Count ) return false;
		//	if ( Player_ == PlayersByTeam_[ Team_ ].Count - 1 ) ++Team_;
		//	if ( Team_ == PlayersByTeam_.Count ) return false;
		//	++Player_;
		//	return true;
		//}

		//public override void Reset()
		//{
		//	Team_ = 0;
		//	Player_ = 0;
		//}

		//public override IEnumerator GetEnumerator()
		//{
		//	return ( IEnumerator ) this;
		//}
	}
}
