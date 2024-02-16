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
		public int Id;
		public string Name;
		public Teams Team;
		public Positions Position;
		public double Price = 0;
		public int Week = 0;
		public int Points = 0;
		public int MinutesPlayed = 0;
		public int Goals = 0;
		public int Assists = 0;
		public double xGoals = 0;
		public double xAssists = 0;
		public int CleanSheets = 0;
		public int GoalsConceded = 0;
		public double xGoalsConceded = 0;
		public int Saves = 0;
		public int BonusPoints = 0;
		public int BonusPointsRating = 0;
		public int PointsInLastFive = 0;
		public int MinutesInLastFive = 0;
		public int GoalsInLastFive = 0;
		public int AssistsInLastFive = 0;
		public double xGoalsInLastFive = 0;
		public double xAssistsInLastFive = 0;
		public int CleanSheetsInLastFive = 0;
		public int GoalsConcededInLastFive = 0;
		public double xGoalsConcededInLastFive = 0;
		public int SavesInLastFive = 0;
		public int BonusPointsInLastFive = 0;
		public int BonusPointsRatingInLastFive = 0;
		public Teams Opponent;
		public int OppenentGoalsScored = 0;
		public int OpponentGoalsConceded = 0;
		public int OpponentGoalsScoredInLastFive = 0;
		public int OpponentGoalsConcededInLastFive = 0;
		public int ActualPoints = 0;

		// Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
#pragma warning disable CS8618
		public TrainingData(PlayerDetails player)
#pragma warning restore CS8618
		{
			LoadPlayerDetails(player);
		}

		public TrainingData(TrainingData data)
		{
			Id = data.Id;
			Name = data.Name;
			Team = data.Team;
			Position = data.Position;
			Price = data.Price;
			Week = data.Week;
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
			PointsInLastFive = data.PointsInLastFive;
			MinutesInLastFive = data.MinutesInLastFive;
			GoalsInLastFive = data.GoalsInLastFive;
			AssistsInLastFive = data.AssistsInLastFive;
			xGoalsInLastFive = data.xGoalsInLastFive;
			xAssistsInLastFive = data.xAssistsInLastFive;
			CleanSheetsInLastFive = data.CleanSheetsInLastFive;
			GoalsConcededInLastFive = data.GoalsConcededInLastFive;
			xGoalsConcededInLastFive = data.xGoalsConcededInLastFive;
			SavesInLastFive = data.SavesInLastFive;
			BonusPointsInLastFive = data.BonusPointsInLastFive;
			BonusPointsRatingInLastFive = data.BonusPointsRatingInLastFive;
			ActualPoints = data.ActualPoints;
			Opponent = data.Opponent;
			OppenentGoalsScored = data.OppenentGoalsScored;
			OpponentGoalsConceded = data.OpponentGoalsConceded;
			OpponentGoalsScoredInLastFive = data.OpponentGoalsScoredInLastFive;
			OpponentGoalsConcededInLastFive = data.OpponentGoalsConcededInLastFive;
		}

		public override void LoadFromLine( string line )
		{
			throw new NotImplementedException(); // not needed yet
		}

		public override string Stringify()
		{
			var str = new StringBuilder();

			str.Append( $"{Id},{Name},{Team},{Position}," );
			str.AppendJoin( ',', new double[] {Price, Week, Points, MinutesPlayed, Goals, Assists, xGoals, xAssists, 
				CleanSheets, GoalsConceded, xGoalsConceded,Saves, BonusPoints, BonusPointsRating,
			PointsInLastFive, MinutesInLastFive,AssistsInLastFive,xGoalsInLastFive,xAssistsInLastFive,
				CleanSheetsInLastFive,GoalsConcededInLastFive,xGoalsConcededInLastFive,SavesInLastFive,
				BonusPointsInLastFive,BonusPointsRatingInLastFive} );

			str.Append( $",{Opponent}," );
			str.AppendJoin( ',', new double[] { OppenentGoalsScored, OpponentGoalsConceded, OpponentGoalsScoredInLastFive, OpponentGoalsConcededInLastFive, ActualPoints } );

			return str.ToString();
		}

		public void SetOpponentData(TeamsHistory history)
		{
			Opponent = history.Team;
			OppenentGoalsScored = history.GoalsScored;
			OpponentGoalsConceded = history.GoalsConceded;
			OpponentGoalsScoredInLastFive = history.GoalsScoredInLastFive;
			OpponentGoalsConcededInLastFive = history.GoalsConcededInLastFive;
		}

		public void LoadPlayerDetails(PlayerDetails playerDetails)
		{
			Id = playerDetails.Id;
			Name = playerDetails.Name;
			Team = playerDetails.Team;
			Position = playerDetails.Position;
			Price = playerDetails.Price;
		}

		public void AddGameweekToLastFive(GameweekData gameweek)
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
			Week = gameweek.Week;
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
