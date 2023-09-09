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

        public string Name_ = "";
        public Positions Position;
        public Teams Team;
        public double Price;
        public List<int> Points = new();
        public int GoalsTotal;
        public List<int> Goals = new();
        public int AssistsTotal;
        public List<int> Assists = new();
        public int CleanSheetsTotal;
        public List<int> CleanSheets = new();
        public int GoalsConcededTotal;
        public List<int> GoalsConceded = new();

        // line format: name, position, team, price, goals, assists, clean sheets, goals conceded, points (list)
        public static Player LoadPlayerFromLine(string line)
        {
            var ret = new Player();

            var vals = line.Split(',');

            // Debug.Assert(vals.Length == 9);


            ret.Name_ = vals[0];
            ret.Position = PositionReader.ReadPosition( vals[ 1 ] );
			ret.Team = TeamReader.ReadTeam( vals[ 2 ] );
            ret.Price = double.Parse(vals[3]);
            
            ret.GoalsTotal = int.Parse(vals[4]);
            ret.AssistsTotal = int.Parse(vals[5]);
            ret.CleanSheetsTotal = int.Parse(vals[6]);
            ret.GoalsConcededTotal = int.Parse(vals[7]);

            for(int i = 8; i < vals.Length; ++i)
            {
                ret.Points.Add( int.Parse( vals[ i ] ) );
            }
			return ret;
        }

        public void Print()
        {

        }
    }
}
