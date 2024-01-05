using FPL_Project.Collections;
using FPL_Project.Players;
using System;
using System.Collections.Generic;
using System.IO.Enumeration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPL_Project.DataFiles
{
	public class GameweekDataFile : DataFile<GameweekDataCollection>
	{

		private int Gameweek_;

		public GameweekDataFile( int gameweek, bool create = false ) : base( $"Data/Gameweek{gameweek}.csv" )
		{
			Gameweek_ = gameweek;
			if ( create && File.Exists( fileName ) )
			{
				throw new Exception( "File already made for this week" );
			}
		}

		public override string Header => "Name,Week,FixtureIds,Points,MinutesPlayed,Goals,Assists,xGoals,xAssists,CleanSheets,GoalsConceded,xGoalsConceded,Saves,BonusPoints,BonusPointsRating";

		public override GameweekDataCollection ReadDataFile()
		{
			GameweekDataCollection data = new( Gameweek_ );
			if ( !File.Exists( fileName ) ) return data;

			using StreamReader r = new( fileName );

			string? line = r.ReadLine(); // header
			while ( ( line = r.ReadLine() ) is not null )
			{
				var gameweekData = new GameweekData();
				gameweekData.LoadFromLine( line );
				data.AddPlayer( gameweekData );
			}
			r.Close();
			return data;
		}

		public override void WriteToFile( GameweekDataCollection gameweekData )
		{

			using StreamWriter w = new( fileName );
			w.WriteLine( Header );

			foreach ( GameweekData player in gameweekData )
			{
				w.WriteLine( player.Stringify() );
			}
			w.Close();
		}
	}
}
