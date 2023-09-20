using FPL_Project.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPL_Project.Collections
{
	public class FullPlayerCollection : Collection
	{
		// ignores info


		public void AddPlayer(FullPlayer player)
		{
			AddInfo( player );
		}

		//public List<FullPlayer> Players => Info_.Cast<FullPlayer>().ToList();
	}
}
