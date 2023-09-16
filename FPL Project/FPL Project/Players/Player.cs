using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace FPL_Project.Players
{
    public class Player
	{

        private string Name_ = "";
		private Positions Position_;
		private Teams Team_;
		private double Price_;
		private List<int> Points_ = new();
		private int GoalsTotal_;
		private List<int> Goals_ = new();
		private int AssistsTotal_;
		private List<int> Assists_ = new();
		private int CleanSheetsTotal_;
		private List<int> CleanSheets_ = new();
		private int GoalsConcededTotal_;
		private List<int> GoalsConceded_ = new();

        public string Name => Name_;
        public Positions Position => Position_;
        public Teams Team => Team_;
        public double Price => Price_;
        public List<int> Points => Points_;
        public int GoalsTotal => GoalsTotal_;
        public List<int> Goals => Goals_;
        public int AssistsTotal => AssistsTotal_;
        public List<int> Assists => Assists_;
        public int CleanSheetsTotal => CleanSheetsTotal_;
        public List<int> CleanSheets => CleanSheets_;
        public int GoalsConcededTotal => GoalsConcededTotal_;
        public List<int> GoalsConceded => GoalsConceded_;


		// line format: name, team, position, price, goals, assists, clean sheets, goals conceded, points (list)
		public static Player LoadPlayerFromLine(string line)
        {
            var ret = new Player();

            var vals = line.Split(',');

            // Debug.Assert(vals.Length == 9);


            ret.Name_ = vals[0];
			ret.Team_ = TeamReader.ReadTeam( vals[ 1 ] );
            ret.Position_ = PositionReader.ReadPosition( vals[ 2 ] );
            ret.Price_ = double.Parse(vals[3]);
            
            ret.GoalsTotal_ = int.Parse(vals[4]);
            ret.AssistsTotal_ = int.Parse(vals[5]);
            ret.CleanSheetsTotal_ = int.Parse(vals[6]);
            ret.GoalsConcededTotal_ = int.Parse(vals[7]);

            for(int i = 8; i < vals.Length; ++i)
            {
                ret.Points_.Add( int.Parse( vals[ i ] ) );
            }
			return ret;
        }

        public void Print()
        {

        }
    }
}
