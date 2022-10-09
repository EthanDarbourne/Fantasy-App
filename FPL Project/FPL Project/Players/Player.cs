using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPL_Project.Players
{
    public class Player
    {

        public string Name_;
        public Positions Position;
        public Teams Team_;
        public double Price;
        public int Points;
        public int Goals;
        public int Assists;
        public int CleanSheets;
        public int GoalsConceded;

        public static Player LoadPlayerFromLine(string line)
        {
            var ret = new Player();

            var vals = line.Split(',');

            Debug.Assert(vals.Length == 9);


            ret.Name_ = vals[0];
            if (!Enum.TryParse(vals[1], out ret.Position)) throw new Exception("Invalid Position");
            if (!Enum.TryParse(vals[2].Remove(' '), out ret.Team_)) throw new Exception("Invalid Team");
            ret.Price = double.Parse(vals[3]);
            ret.Points = int.Parse(vals[4]);
            ret.Goals = int.Parse(vals[5]);
            ret.Assists = int.Parse(vals[6]);
            ret.CleanSheets = int.Parse(vals[7]);
            ret.GoalsConceded = int.Parse(vals[8]);

            return ret;
        }

        public void Print()
        {

        }
    }
}
