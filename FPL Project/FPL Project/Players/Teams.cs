using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPL_Project.Players
{
    public enum Teams
    {
        ManCity = 1,
        Arsenal = 2,
		ManUtd = 3,
		Newcastle = 4,
		Liverpool = 5,
		Brighton = 6,
		AstonVilla = 7,
		Spurs = 8,
		Brentford = 9,
		Fulham = 10,
		CrystalPalace = 11,
		Chelsea = 12,
        Wolves = 13,
		WestHam = 14,
		Bournemouth = 15,
		NottmForest = 16,
        Everton = 17,
        Burnley = 18,
		SheffieldUtd = 19,
		Luton = 20,
	}

	public static class TeamReader
	{

		private static List<string> TeamsAsStrings = new() { "Arsenal", "AstonVilla", "Bournemouth", "Brentford", "Brighton", "Burnley", "Chelsea", "CrystalPalace", "Everton", "Fulham", "Liverpool", "Luton",
															"ManCity", "ManUtd", "Newcastle", "NottmForest", "SheffieldUtd", "Spurs", "WestHam", "Wolves", };

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
				throw new Exception( "Invalid Team" );

			}
			return team;
		}
		
		public static Teams ReadTeam(int i)
		{
			return (Teams)i;
		}
	}
}
