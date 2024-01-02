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
		public int Id;
		public int Gameweek;
		public Teams Home;
		public Teams Away;
		public int HomeGoals;
		public int AwayGoals;
		public double xHomeGoals;
		public double xAwayGoals;

		public Fixture()
		{

		}

		public Fixture(int id, int week, Teams home, Teams away, int homeGoals, int awayGoals )
		{
			Id = id;
			Gameweek = week;
			Home = home;
			Away = away;
			HomeGoals = homeGoals;
			AwayGoals = awayGoals;
		}

		public void SetExpectedData(Teams team, double xGoalsScored, double xGoalsConceded )
		{
			if(team == Away)
			{
				double tmp = xGoalsScored;
				xGoalsScored = xGoalsConceded;
				xGoalsConceded = tmp;
			}
			if(team != Home)
			{
				throw new Exception( "Team is not playing in this fixture" );
			}
			xHomeGoals = xGoalsScored;
			xAwayGoals = xGoalsConceded;
		}

		public override void LoadFromLine( string line )
		{
			var vals = line.Split( ',' );

			Id = int.Parse( vals[ 0 ] );
			Gameweek = int.Parse( vals[ 1 ] );
			Home = TeamReader.ReadTeam( vals[ 2 ] );
			Away = TeamReader.ReadTeam( vals[ 3 ] );
			HomeGoals = int.Parse( vals[ 4 ] );
			AwayGoals = int.Parse( vals[ 5 ] );
			xHomeGoals = double.Parse( vals[ 6 ] );
			xAwayGoals = double.Parse( vals[ 7 ] );
		}

		public override string Stringify()
		{
			var str = new StringBuilder();

			str.AppendJoin( ',', new string[] { Id.ToString(), Gameweek.ToString(), Home.ToString(), Away.ToString(), HomeGoals.ToString(), AwayGoals.ToString(), xHomeGoals.ToString(), xAwayGoals.ToString() } );

			return str.ToString();
		}
	}
}
