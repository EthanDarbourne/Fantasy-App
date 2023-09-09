using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPL_Project.Players
{
    public enum Positions
    {
        Goalkeeper = 1,
        Defender = 2,
        Midfielder = 3,
        Forward = 4,
    }

	public static class PositionReader
	{
		public static Positions ReadPosition( string s )
		{
			if ( !Enum.TryParse( s, out Positions pos ) ) throw new Exception( "Invalid Position" );
			return pos;
		}
	}
}
