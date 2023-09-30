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
using System.Xml.Linq;

namespace FPL_Project.Collections
{
	public class PlayerDetailsCollection : Collection
	{

		private Dictionary<string, PlayerDetails> Players_ = new();
		private Dictionary<int, string> PlayersById_ = new();

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
			PlayersById_.Add( player.Id, player.Name);
			Players_.Add( player.Name, player );
			PlayersByTeam_[ ( int ) player.Team - 1].Add( player );
			AddInfo( player );
		}

		public bool DeletePlayer( string player )
		{
			if ( !Players_.ContainsKey( player ) )
			{
				return false;
			}
			DeleteInfo( info => ( info as PlayerDetails )!.Name == player );
			var playerDetail = Players_[ player ];
			PlayersById_.Remove( playerDetail.Id);
			Players_.Remove( player );
			for ( int i = 0; i < 20; ++i )
			{
				for ( int j = 0; j < PlayersByTeam_[ i ].Count; ++j )
				{
					if ( PlayersByTeam_[ i ][ j ].Name == player )
					{
						PlayersByTeam_[ i ].RemoveAt( j );
						return true;
					}
				}
			}
			return false; // won't get here
		}

		public PlayerDetails? GetPlayer( string name )
		{
			Players_.TryGetValue( name, out var player );
			return player;
		}

		public bool ContainsPlayerId(int playerId)
		{
			return PlayersById_.ContainsKey( playerId );
		}

		public string? GetPlayerNameById(int playerId)
		{
			PlayersById_.TryGetValue( playerId, out var player );
			return player;
		}
	}
}
