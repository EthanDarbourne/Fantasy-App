using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

		// public static GameweekData LoadPlayer(string line)
		public override void LoadFromLine(string line)
		{
			var ret = new GameweekData();

			var vals = line.Split( ',' );

			Debug.Assert(vals.Length == 14);

			ret.Name_ = vals[ 0 ];
			ret.Week_ = int.Parse( vals[ 1 ] );
			ret.Points_ = int.Parse( vals[ 2 ] );
			ret.MinutesPlayed_ = int.Parse( vals[ 3 ] );
			ret.Goals_ = int.Parse( vals[ 4 ] );
			ret.Assists_ = int.Parse( vals[ 5 ] );
			ret.xGoals_ = int.Parse( vals[ 6 ] );
			ret.xAssists_ = double.Parse( vals[ 7 ] );
			ret.CleanSheets_ = int.Parse( vals[ 8 ] );
			ret.GoalsConceded_ = int.Parse( vals[ 9 ] );
			ret.xGoalsConceded_ = double.Parse( vals[ 10 ] );
			ret.Saves_ = int.Parse( vals[ 11 ] );
			ret.BonusPoints_ = int.Parse( vals[ 12 ] );
			ret.BonusPointsRating_ = int.Parse( vals[ 13 ] );

			

		}

		public override string Stringify()
		{
			var str = new StringBuilder();

			str.AppendJoin( ',', new double[]{ Week, Points, MinutesPlayed, Goals, Assists, xGoals, xAssists,
				CleanSheets, GoalsConceded, xGoalsConceded, Saves, BonusPoints, BonusPointsRating } );

			return str.ToString();
		}
	}
}
