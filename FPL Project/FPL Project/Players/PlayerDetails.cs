using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPL_Project.Players
{
	public class PlayerDetails
	{


		private string Name_;
		private Teams Team_;
		private Positions Position_;


		public PlayerDetails()
		{

		}

		public PlayerDetails(string name, Teams team, Positions position )
		{
			Name_ = name;
			Team_ = team;
			Position_ = position;
		}

		public string Name => Name_;
		public Teams Team => Team_;
		public Positions Position => Position_;
		


		public static PlayerDetails LoadPlayerDetails(string line)
		{
			var ret = new PlayerDetails();

			var vals = line.Split( ',' );

			Debug.Assert( vals.Length == 3 );

			ret.Name_ = vals[ 0 ];
			ret.Position_ = PositionReader.ReadPosition( vals[ 1 ] );
			ret.Team_ = TeamReader.ReadTeam( vals[ 2 ] );

			return ret;
		}


		public string Stringify()
		{
			var str = new StringBuilder();

			str.AppendJoin( ',', new string[] { Name, Team.ToString(), Position.ToString() } );

			return str.ToString();
		}
	}
}
