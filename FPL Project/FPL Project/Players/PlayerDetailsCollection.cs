using FPL_Project.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPL_Project.Players
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

		public override object Current => PlayersByTeam_[ Team_ ][ Player_ ];

		public void AddPlayerDetails( PlayerDetails player )
		{
			Players_.Add( player.Name, player );
			PlayersByTeam_[ ( int ) player.Team ].Add( player );
		}

		public PlayerDetails? GetPlayer( string name )
		{
			return Players_[ name ];
		}

		public static PlayerDetailsCollection LoadFromDataFile()
		{
			var players = new PlayerDetailsCollection();

			var playerData = new PlayerDetailsDataFile();

			return players;
		}

		public override bool MoveNext()
		{
			if ( Team_ >= PlayersByTeam_.Count ) return false;
			if ( Player_ == PlayersByTeam_[ Team_ ].Count - 1 ) ++Team_;
			if ( Team_ == PlayersByTeam_.Count ) return false;
			++Player_;
			return true;
		}

		public override void Reset()
		{
			Team_ = 0;
			Player_ = 0;
		}

		public override IEnumerator GetEnumerator()
		{
			return ( IEnumerator ) this;
		}
	}
}
