using FPL_Project.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPL_Project.Players
{
	public class FullPlayer : Info
	{

		private PlayerDetails PlayerDetails_;
		private List<GameweekData> GameweekData_;

		public FullPlayer(PlayerDetails playerDetails, List<GameweekData> gameweekData )
		{
			PlayerDetails_ = playerDetails;
			GameweekData_ = gameweekData;
		}

		public PlayerDetails PlayerDetails => PlayerDetails_;
		public List<GameweekData> GameweekData => GameweekData_;

		public override void LoadFromLine( string line )
		{
			throw new NotImplementedException(); // can't load from line, too much info
		}

		public override string Stringify()
		{
			throw new NotImplementedException(); // can't stringify, too much info
		}
	}
}
