using FPL_Project.Collections;
using FPL_Project.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPL_Project.DataFiles
{
	public class TestingDataFile : DataFile<TestingDataCollection>
	{
		public TestingDataFile() : base( "Data/TestingData{0}.csv" )
		{
		}

		public override string Header => "Id,Name,Team,Position,Price,Week,Points,MinutesPlayed,Goals,Assists,xGoals,xAssists,CleanSheets,GoalsConceded,xGoalsConceded,Saves,BonusPoints,BonusPointRating,PointsInLastFive,MinutesInLastFive,GoalsInLastFive,xGoalsInLastFive,xAssistsInLastFive,CleanSheetsInLastFive,GoalsConcededInLastFive,xGoalsConcededInLastFive,SavesInLastFive,BonusPointsInLastFive,BonusPointsRatingInLastFive,Opponent,OpponentGoalsScored,OpponentGoalsConceded,OpponentGoalsScoredInLastFive,OpponentGoalsConcededInLastFive";

		public override TestingDataCollection ReadDataFile()
		{
			throw new NotImplementedException();
		}

		public void WriteToFile( TestingDataCollection collection, string append )
		{
			string original = fileName;
			fileName = string.Format( fileName, append );
			WriteToFile( collection );
			fileName = original;
		}

		public override void WriteToFile( TestingDataCollection collection )
		{
			string fullFileName = string.Format( fileName );
			using StreamWriter w = new( fullFileName );
			w.WriteLine( Header );

			foreach ( TestingData data in collection )
			{
				w.WriteLine( data.Stringify() );
			}
			w.Close();
		}
	}
}
