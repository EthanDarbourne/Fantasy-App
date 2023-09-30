using FPL_Project.Data;
using FPL_Project.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPL_Project.Collections
{
	public class FixtureCollection : Collection
	{
		private List<List<Fixture>> Fixtures_ = new();

		public FixtureCollection()
		{
			for(int i = 0; i <= 38; ++i )
			{
				Fixtures_.Add( new List<Fixture>() );
			}
		}


		public void AddFixture(Fixture fixture)
		{
			Info_.Add( fixture );
			Fixtures_[ fixture.Gameweek ].Add( fixture );
		}

		// problem: when playing two games
		public Teams GetOpponent(int gameweek, Teams team)
		{
			foreach(var fixture in Fixtures_[gameweek] )
			{
				if ( fixture.Home == team ) return fixture.Away;
				if ( fixture.Away == team ) return fixture.Home;
			}
			return team; // did not play in the gameweek
		}

		public bool IsHome(int gameweek, Teams team)
		{
			foreach ( var fixture in Fixtures_[ gameweek ] )
			{
				if ( fixture.Home == team ) return true;
				if ( fixture.Away == team ) return false;
			}
			return false; // did not play in the gameweek
		}
	}
}
