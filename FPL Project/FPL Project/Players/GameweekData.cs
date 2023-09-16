using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FPL_Project.Data;

namespace FPL_Project.Players
{
	public class GameweekData : Info
	{
		private string Name_;
		private Teams Team_;
		private Positions Position_;
		private int Week_;
		private int Points_;
		private int MinutesPlayed_;
		private int Goals_;
		private int Assists_;
		private double xGoals_;
		private double xAssists_;
		private int CleanSheets_;
		private int GoalsConceded_;
		private double xGoalsConceded_;
		private int Saves_;
		private int BonusPoints_;
		private int BonusPointsRating_;

		public string Name => Name_;
		public Teams Team => Team_;
		public Positions Position => Position_;
		public int Week => Week_;
		public int Points => Points_;
		public int MinutesPlayed => MinutesPlayed_;
		public int Goals => Goals_;
		public int Assists => Assists_;
		public double xGoals => xGoals_;
		public double xAssists => xAssists_;
		public int CleanSheets => CleanSheets_;
		public int GoalsConceded => GoalsConceded_;
		public double xGoalsConceded => xGoalsConceded_;
		public int Saves => Saves_;
		public int BonusPoints => BonusPoints_;
		public int BonusPointsRating => BonusPointsRating_;

		public GameweekData()
		{

		}

		public GameweekData(int week)
		{
			Week_ = week;
		}

		// public static GameweekData LoadPlayer(string line)
		public override void LoadFromLine(string line)
		{

			var vals = line.Split( ',' );

			Debug.Assert(vals.Length == 14);

			Name_ = vals[ 0 ];
			Week_ = int.Parse( vals[ 1 ] );
			Points_ = int.Parse( vals[ 2 ] );
			MinutesPlayed_ = int.Parse( vals[ 3 ] );
			Goals_ = int.Parse( vals[ 4 ] );
			Assists_ = int.Parse( vals[ 5 ] );
			xGoals_ = double.Parse( vals[ 6 ] );
			xAssists_ = double.Parse( vals[ 7 ] );
			CleanSheets_ = int.Parse( vals[ 8 ] );
			GoalsConceded_ = int.Parse( vals[ 9 ] );
			xGoalsConceded_ = double.Parse( vals[ 10 ] );
			Saves_ = int.Parse( vals[ 11 ] );
			BonusPoints_ = int.Parse( vals[ 12 ] );
			BonusPointsRating_ = int.Parse( vals[ 13 ] );

		}

		public override string Stringify()
		{
			var str = new StringBuilder();

			str.Append( Name + ',' );

			str.AppendJoin( ',', new double[]{ Week, Points, MinutesPlayed, Goals, Assists, xGoals, xAssists,
				CleanSheets, GoalsConceded, xGoalsConceded, Saves, BonusPoints, BonusPointsRating } );

			return str.ToString();
		}
	}
}
