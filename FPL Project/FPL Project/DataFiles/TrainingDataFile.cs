using FPL_Project.Collections;
using FPL_Project.Data;
using FPL_Project.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPL_Project.DataFiles
{
	public class TrainingDataFile : DataFile
	{
		public TrainingDataFile() : base( "Data/TrainingData.csv" )
		{
		}

		public override string Header => "Id,Name,Team,Position,Price,Points,MinutesPlayed,Goals,Assists,xGoals,xAssists,CleanSheets,GoalsConceded,xGoalsConceded,Saves,BonusPoints,BonusPointRating,PointsInLastFive,MinutesInLastFive,GoalsInLastFive,xGoalsInLastFive,xAssistsInLastFive,CleanSheetsInLastFive,GoalsConcededInLastFive,xGoalsConcededInLastFive,SavesInLastFive,BonusPointsInLastFive,BonusPointsRatingInLastFive,Opponent,OpponentGoalsScored,OpponentGoalsConceded,OpponentGoalsScoredInLastFive,OpponentGoalsConcededInLastFive,ActualPoints";

		public override Collection ReadDataFile()
		{
			throw new NotImplementedException();
		}

		public override void WriteToFile( Collection collection )
		{

			using StreamWriter w = new StreamWriter( fileName );
			w.WriteLine( Header );

			foreach ( TrainingData data in collection )
			{
				w.WriteLine( data.Stringify() );
			}
			w.Close();
		}
	}
}
