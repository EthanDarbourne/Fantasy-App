using FPL_Project.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPL_Project.Collections
{
	public class FullPlayerCollection : Collection<FullPlayer>
	{

		public FullPlayerCollection()
		{

		}

		public FullPlayerCollection(IEnumerable<FullPlayer> fullPlayers)
		{
			foreach ( FullPlayer player in fullPlayers ) 
			{
				AddPlayer( player );
			}
		}

		public void AddPlayer(FullPlayer player)
		{
			AddItem( player );
		}

		//public List<FullPlayer> Players => Info_.Cast<FullPlayer>().ToList();
	}
}
