using FPL_Project.Players;
using FPL_Project.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPL_Project.Collections
{
    public class PlayerCollection : Collection<Player>
    {


        private Dictionary<string, Player> Players_ = new();

        private List<List<Player>> PlayersByTeam_ = new();

        public PlayerCollection()
        {
            for(int i = 0; i < 20; ++i)
            {
				PlayersByTeam_.Add( new() );
			}
        }

        public void AddPlayer(Player player)
        {
            Players_.Add(player.Name, player);
            PlayersByTeam_[ ( int ) player.Team ].Add( player );
			AddItem( player );
		}

        public Player? GetPlayer(string name)
        {
            return Players_[name];
        }

        public int Count => Players_.Count;

        public List<List<Player>> PlayersByTeam => PlayersByTeam_;
    }
}
