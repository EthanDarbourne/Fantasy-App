using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FPL_Project.Data;

namespace FPL_Project.Players
{
	public class PlayerDetails : Info
	{


		private string Name_;
		private Teams Team_;
		private Positions Position_;
		private double Price_;

		public PlayerDetails()
		{

		}

		public PlayerDetails(string name, Teams team, Positions position, double price )
		{
			Name_ = name;
			Team_ = team;
			Position_ = position;
			Price_ = price;
		}

		public string Name => Name_;
		public Teams Team => Team_;
		public Positions Position => Position_;
		public double Price => Price_;


		public override void LoadFromLine(string line)
		{
			var vals = line.Split( ',' );

			//Debug.Assert( vals.Length == 3 );

			Name_ = vals[ 0 ];
			Team_ = TeamReader.ReadTeam( vals[ 1 ] );
			Position_ = PositionReader.ReadPosition( vals[ 2 ] );
			Price_ = double.Parse( vals[ 3 ] );
		}


		public override string Stringify()
		{
			var str = new StringBuilder();

			str.AppendJoin( ',', new string[] { Name, Team.ToString(), Position.ToString(), Price_.ToString() } );

			return str.ToString();
		}
	}
}
