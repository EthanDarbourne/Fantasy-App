using FPL_Project.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace FPL_Project.Data
{ 
	public class Fixture : Info
	{
		public int Gameweek;
		public Teams Home;
		public Teams Away;
		public int HomeGoals;
		public int AwayGoals;

		public Fixture()
		{

		}

		public Fixture(int week, Teams home, Teams away, int homeGoals, int awayGoals )
		{
			Gameweek = week;
			Home = home;
			Away = away;
			HomeGoals = homeGoals;
			AwayGoals = awayGoals;
		}

		public override void LoadFromLine( string line )
		{
			var vals = line.Split( ',' );


			Gameweek = int.Parse( vals[ 0 ] );
			Home = TeamReader.ReadTeam( vals[ 1 ] );
			Away = TeamReader.ReadTeam( vals[ 2 ] );
			HomeGoals = int.Parse( vals[ 3 ] );
			AwayGoals = int.Parse( vals[ 4 ] );
		}

		public override string Stringify()
		{
			var str = new StringBuilder();

			str.AppendJoin( ',', new string[] { Gameweek.ToString(), Home.ToString(), Away.ToString(), HomeGoals.ToString(), AwayGoals.ToString() } );

			return str.ToString();
		}
	}
}
