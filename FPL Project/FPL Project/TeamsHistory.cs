using FPL_Project.Data;
using FPL_Project.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPL_Project
{
	public class TeamsHistory
	{

		public int GoalsScored = 0;
		public int GoalsConceded = 0;
		public int GoalsScoredInLastFive = 0;
		public int GoalsConcededInLastFive = 0;
		public double xGoalsScored = 0;
		public double xGoalsConceded = 0;
		public double xGoalsScoredInLastFive = 0;
		public double xGoalsConcededInLastFive = 0;
		public Teams Team;

		private List<int> LastFiveScored = new();
		private List<int> LastFiveConceded = new();
		private List<double> LastFivexScored = new();
		private List<double> LastFivexConceded = new();

		public TeamsHistory( Teams team )
		{
			Team = team;
		}

		public void AddFixture( Fixture fixture )
		{
			int goalsScored, goalsConceded;
			double xgoalsScored, xgoalsConceded;
			if ( fixture.Home == Team )
			{
				goalsScored = fixture.HomeGoals;
				goalsConceded = fixture.AwayGoals;
				xgoalsScored = fixture.xHomeGoals;
				xgoalsConceded = fixture.xAwayGoals;
			}
			else
			{
				goalsScored = fixture.AwayGoals;
				goalsConceded = fixture.HomeGoals;
				xgoalsScored = fixture.xAwayGoals;
				xgoalsConceded = fixture.xHomeGoals;
			}

			if ( LastFiveScored.Count == 5 )
			{
				GoalsScored -= LastFiveScored[ 0 ];
				GoalsConceded -= LastFiveConceded[ 0 ];
				xGoalsScored -= LastFivexScored[ 0 ];
				xGoalsConceded -= LastFivexConceded[ 0 ];
				LastFiveScored.RemoveAt( 0 );
				LastFiveConceded.RemoveAt( 0 );
				LastFivexScored.RemoveAt( 0 );
				LastFivexConceded.RemoveAt( 0 );
			}
			LastFiveScored.Add( goalsScored );
			LastFiveConceded.Add( goalsConceded );
			GoalsScored += goalsScored;
			GoalsConceded += goalsConceded;
			LastFivexScored.Add( xgoalsScored );
			LastFivexConceded.Add( xgoalsConceded );
			xGoalsScored += xgoalsScored;
			xGoalsConceded += xgoalsConceded;
		}
	}
}
