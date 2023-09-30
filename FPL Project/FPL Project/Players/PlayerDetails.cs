using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FPL_Project.Data;
using Newtonsoft.Json;

namespace FPL_Project.Players
{
	public class PlayerDetails : Info
	{


		private string Name_;
		private int Id_;
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

		[JsonConstructor]
		public PlayerDetails( int id, string web_name, double now_cost)
		{
			Id_ = id;
			Name_ = web_name;
			Price_ = now_cost / 10;
		}

		public string Name => Name_;
		public int Id => Id_;
		public Teams Team => Team_;
		public Positions Position => Position_;
		public double Price => Price_;


		public void SetTeamAndPosition(Teams team, Positions pos)
		{
			Team_ = team;
			Position_ = pos;
		}

		public void SetName(string name)
		{
			Name_ = name;
		}

		public override void LoadFromLine(string line)
		{
			var vals = line.Split( ',' );

			//Debug.Assert( vals.Length == 3 );

			Id_ = int.Parse( vals[ 0 ] );
			Name_ = vals[ 1 ];
			Team_ = TeamReader.ReadTeam( vals[ 2 ] );
			Position_ = PositionReader.ReadPosition( vals[ 3 ] );
			Price_ = double.Parse( vals[ 4 ] );
		}


		public override string Stringify()
		{
			var str = new StringBuilder();

			str.AppendJoin( ',', new string[] { Id.ToString(), Name, Team.ToString(), Position.ToString(), Price_.ToString() } );

			return str.ToString();
		}
	}
}
