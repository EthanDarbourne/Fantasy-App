using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPL_Project.Players
{
    public enum Teams
    {
        Arsenal,
		AstonVilla,
		Bournemouth,
		Brentford,
		Brighton,
		Chelsea,
		CrystalPalace,
        Everton,
		Fulham,
		Ipswich,
        Leicester,
		Liverpool,
        ManCity,
		ManUtd,
		Newcastle,
		NottmForest,
		Southampton,
		Spurs,
		WestHam,
        Wolves,
	}

	public static class TeamReader
	{

		public static readonly List<string> TeamsAsStrings = new() { "Arsenal", "AstonVilla", "Bournemouth", "Brentford", "Brighton", "Chelsea", "CrystalPalace", "Everton", "Fulham", "Ipswich", "Leicester", "Liverpool", 
															"ManCity", "ManUtd", "Newcastle", "NottmForest", "Southampton", "Spurs", "WestHam", "Wolves", };

		public static Teams ReadTeam(string s)
		{
			if(int.TryParse(s, out var i))
			{
				return ( Teams ) i;
			}
			if ( !Enum.TryParse( s.Replace( " ", "" ), out Teams team ) )
			{
				foreach(var t in TeamsAsStrings )
				{
					if(t.ToLower() == s.ToLower())
					{
						return ReadTeam( t );
					}
				}
				throw new Exception( $"Invalid Team: {s}" );

			}
			return team;
		}
		
		public static Teams ReadTeam(int i)
		{
			return (Teams)i;
		}
	}
}
