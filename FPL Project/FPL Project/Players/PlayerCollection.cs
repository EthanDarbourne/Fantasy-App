using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPL_Project.Players
{
    public class PlayerCollection
    {


        private Dictionary<string, Player> Players_ = new();

        public PlayerCollection()
        {

        }

        public void AddPlayer(Player player)
        {
            Players_.Add(player.Name_, player);
        }

        public Player? GetPlayer(string name)
        {
            return Players_[name];
        }

        public int Count => Players_.Count;
    }
}
