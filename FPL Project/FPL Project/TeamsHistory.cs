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
		public Teams Team;

		private List<int> LastFiveScored = new();
		private List<int> LastFiveConceded = new();

		public TeamsHistory( Teams team )
		{
			Team = team;
		}

		public void AddFixture( Fixture fixture )
		{
			int goalsScored, goalsConceded;
			if ( fixture.Home == Team )
			{
				goalsScored = fixture.HomeGoals;
				goalsConceded = fixture.AwayGoals;
			}
			else
			{
				goalsScored = fixture.AwayGoals;
				goalsConceded = fixture.HomeGoals;
			}

			if ( LastFiveScored.Count == 5 )
			{
				GoalsScored -= LastFiveScored[ 0 ];
				GoalsConceded -= LastFiveConceded[ 0 ];
				LastFiveScored.RemoveAt( 0 );
				LastFiveConceded.RemoveAt( 0 );
			}
			LastFiveScored.Add( goalsScored );
			LastFiveConceded.Add( goalsConceded );
			GoalsScored += goalsScored;
			GoalsConceded += goalsConceded;
		}
	}
}
