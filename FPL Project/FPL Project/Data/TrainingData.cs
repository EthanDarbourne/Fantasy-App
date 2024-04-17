using FPL_Project.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPL_Project.Data
{
	public class TrainingData : TestingData
	{
		public int ActualPoints = 0;

		public TrainingData(PlayerDetails player)
			:base(player)
		{
			LoadPlayerDetails(player);
		}

		public TrainingData(TrainingData data)
			: base(data)
		{
			ActualPoints = data.ActualPoints;
		}

		public override void LoadFromLine( string line )
		{
			throw new NotImplementedException(); // not needed yet
		}

		public override string Stringify()
		{
			return base.Stringify() + ',' + ActualPoints.ToString();
		}

		

	}
}
