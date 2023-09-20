using FPL_Project.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPL_Project.Data
{
	public class TrainingData : Info
	{
		public string Name;
		public Teams Team;
		public Positions Position;
		public double Price;
		public int Points;
		public int MinutesPlayed;
		public int Goals;
		public int Assists;
		public double xGoals;
		public double xAssists;
		public int CleanSheets;
		public int GoalsConceded;
		public double xGoalsConceded;
		public int Saves;
		public int BonusPoints;
		public int BonusPointsRating;
		public int PointsInLastFive;
		public int MinutesInLastFive;
		public int GoalsInLastFive;
		public int AssistsInLastFive;
		public double xGoalsInLastFive;
		public double xAssistsInLastFive;
		public int CleanSheetsInLastFive;
		public int GoalsConcededInLastFive;
		public double xGoalsConcededInLastFive;
		public int SavesInLastFive;
		public int BonusPointsInLastFive;
		public int BonusPointsRatingInLastFive;
		public int ActualPoints;

		public TrainingData()
		{

		}

		public TrainingData(TrainingData data)
		{
			Points = data.Points;
			MinutesPlayed = data.MinutesPlayed;
			Goals = data.Goals;
			Assists = data.Assists;
			xGoals = data.xGoals;
			xAssists = data.xAssists;
			CleanSheets = data.CleanSheets;
			GoalsConceded = data.GoalsConceded;
			xGoalsConceded = data.xGoalsConceded;
			Saves = data.Saves;
			BonusPoints = data.BonusPoints;
			BonusPointsRating = data.BonusPointsRating;
			PointsInLastFive = data.Points;
			MinutesInLastFive = data.MinutesPlayed;
			GoalsInLastFive = data.Goals;
			AssistsInLastFive = data.Assists;
			xGoalsInLastFive = data.xGoals;
			xAssistsInLastFive = data.xAssists;
			CleanSheetsInLastFive = data.CleanSheets;
			GoalsConcededInLastFive = data.GoalsConceded;
			xGoalsConcededInLastFive = data.xGoalsConceded;
			SavesInLastFive = data.Saves;
			BonusPointsInLastFive = data.BonusPoints;
			BonusPointsRatingInLastFive = data.BonusPointsRating;
		}

		public override void LoadFromLine( string line )
		{
			throw new NotImplementedException(); // not needed yet
		}

		public override string Stringify()
		{
			var str = new StringBuilder();

			str.Append( $"{Name},{Team},{Position}," );
			str.AppendJoin( ',', new double[] {Price, Points, MinutesPlayed, Goals, Assists, xGoals, xAssists, 
				CleanSheets, GoalsConceded, xGoalsConceded,Saves, BonusPoints, BonusPointsRating,
			PointsInLastFive, MinutesInLastFive,AssistsInLastFive,xGoalsInLastFive,xAssistsInLastFive,
				CleanSheetsInLastFive,GoalsConcededInLastFive,xGoalsConcededInLastFive,SavesInLastFive,
				BonusPointsInLastFive,BonusPointsRatingInLastFive} );

			return str.ToString();
		}

		public void LoadPlayerDetails(PlayerDetails playerDetails)
		{
			Name = playerDetails.Name;
			Team = playerDetails.Team;
			Position = playerDetails.Position;
			Price = playerDetails.Price;
		}

		public void AddGameweekFromLastFive(GameweekData gameweek)
		{
			PointsInLastFive += gameweek.Points;
			MinutesInLastFive += gameweek.MinutesPlayed;
			GoalsInLastFive += gameweek.Goals;
			AssistsInLastFive += gameweek.Assists;
			xGoalsInLastFive += gameweek.xGoals;
			xAssistsInLastFive += gameweek.xAssists;
			CleanSheetsInLastFive += gameweek.CleanSheets;
			GoalsConcededInLastFive += gameweek.GoalsConceded;
			xGoalsConcededInLastFive += gameweek.xGoalsConceded;
			SavesInLastFive += gameweek.Saves;
			BonusPointsInLastFive += gameweek.BonusPoints;
			BonusPointsRatingInLastFive += gameweek.BonusPointsRating;
		}

		public void RemoveGameweekFromLastFive(GameweekData gameweek)
		{
			PointsInLastFive -= gameweek.Points;
			MinutesInLastFive -= gameweek.MinutesPlayed;
			GoalsInLastFive -= gameweek.Goals;
			AssistsInLastFive -= gameweek.Assists;
			xGoalsInLastFive -= gameweek.xGoals;
			xAssistsInLastFive -= gameweek.xAssists;
			CleanSheetsInLastFive -= gameweek.CleanSheets;
			GoalsConcededInLastFive -= gameweek.GoalsConceded;
			xGoalsConcededInLastFive -= gameweek.xGoalsConceded;
			SavesInLastFive -= gameweek.Saves;
			BonusPointsInLastFive -= gameweek.BonusPoints;
			BonusPointsRatingInLastFive -= gameweek.BonusPointsRating;
		}

		public void AddGameweekToStats(GameweekData gameweek)
		{
			Points += gameweek.Points;
			MinutesPlayed += gameweek.MinutesPlayed;
			Goals += gameweek.Goals;
			Assists += gameweek.Assists;
			xGoals += gameweek.xGoals;
			xAssists += gameweek.xAssists;
			CleanSheets += gameweek.CleanSheets;
			GoalsConceded += gameweek.GoalsConceded;
			xGoalsConceded += gameweek.xGoalsConceded;
			Saves += gameweek.Saves;
			BonusPoints += gameweek.BonusPoints;
			BonusPointsRating += gameweek.BonusPointsRating;
		}

	}
}
