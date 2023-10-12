using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;
using FPL_Project.Data;
using Newtonsoft.Json;

namespace FPL_Project.Players
{
	public class GameweekData : Info
	{
		private string Name_;
		private int Id_;
		private Teams Team_;
		private int Week_;
		private List<int> FixtureIds_;
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
		public int Id => Id_;
		public Teams Team => Team_;
		public int Week => Week_;
		public List<int> FixtureIds => FixtureIds_;
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

		[JsonConstructor]
		public GameweekData(int total_points, int minutes, int goals_scored, int assists, int clean_sheets, int goals_conceded, int saves, int bonus, int bps,
			double expected_goals, double expected_assists, double expected_goals_conceded)
		{
			Points_ = total_points;
			MinutesPlayed_ = minutes;
			Goals_ = goals_scored;
			Assists_ = assists;
			xGoals_ = expected_goals;
			xAssists_ = expected_assists;
			CleanSheets_ = clean_sheets;
			GoalsConceded_ = goals_conceded;
			xGoalsConceded_ = expected_goals_conceded;
			Saves_ = saves;
			BonusPoints_ = bonus;
			BonusPointsRating_ = bps;
		}


		// gameweek data json doesn't contain name or id
		public void SetExtraInfo(int id, string name, Teams team, int week, List<int> fixtureIds)
		{
			Id_ = id;
			Name_ = name;
			Team_ = team;
			Week_ = week;
			FixtureIds_ = fixtureIds;
		}

		// public static GameweekData LoadPlayer(string line)
		public override void LoadFromLine(string line)
		{

			var vals = line.Split( ',' );

			Debug.Assert(vals.Length == 15);

			Name_ = vals[ 0 ];
			Week_ = int.Parse( vals[ 1 ] );
			FixtureIds_ = vals[ 2 ].Split( ';', StringSplitOptions.RemoveEmptyEntries ).Select( x => int.Parse( x ) ).ToList();
			Points_ = int.Parse( vals[ 3 ] );
			MinutesPlayed_ = int.Parse( vals[ 4 ] );
			Goals_ = int.Parse( vals[ 5 ] );
			Assists_ = int.Parse( vals[ 6 ] );
			xGoals_ = double.Parse( vals[ 7 ] );
			xAssists_ = double.Parse( vals[ 8 ] );
			CleanSheets_ = int.Parse( vals[ 9 ] );
			GoalsConceded_ = int.Parse( vals[ 10 ] );
			xGoalsConceded_ = double.Parse( vals[ 11 ] );
			Saves_ = int.Parse( vals[ 12 ] );
			BonusPoints_ = int.Parse( vals[ 13 ] );
			BonusPointsRating_ = int.Parse( vals[ 14 ] );

		}

		public override string Stringify()
		{
			var str = new StringBuilder();


			var s = string.Join( ";", FixtureIds.Select( x => x.ToString() ));
			str.Append( $"{Name},{Week},{s}," );

			str.AppendJoin( ',', new double[]{ Points, MinutesPlayed, Goals, Assists, xGoals, xAssists,
				CleanSheets, GoalsConceded, xGoalsConceded, Saves, BonusPoints, BonusPointsRating } );

			return str.ToString();
		}
	}
}
