using FPL_Project.Collections;
using FPL_Project.Constants;
using FPL_Project.Data;
using FPL_Project.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPL_Project.DataFiles
{
	public class FixtureDataFile : DataFile<FixtureCollection>
	{

		public FixtureDataFile() : base( $"{FPLConstants.FolderName}/Fixtures.csv" )
		{
		}

		public override string Header => "Id,Gameweek,Home,Away,HomeGoals,AwayGoals,xHomeGoals,xAwayGoals";

		public override FixtureCollection ReadDataFile()
		{
			FixtureCollection fixtures = new();
			if ( !File.Exists( fileName ) ) return fixtures;

			using StreamReader r = new( fileName );
			string? line = r.ReadLine(); // header
			while ( ( line = r.ReadLine() ) is not null )
			{
				var fixture = new Fixture();
				fixture.LoadFromLine( line );
				fixtures.AddFixture( fixture );
			}
			r.Close();
			return fixtures;
		}

		public override void WriteToFile( FixtureCollection collection )
		{
			using StreamWriter w = new( fileName );

			w.WriteLine( Header );
			foreach ( Fixture fixture in collection )
			{
				w.WriteLine( fixture.Stringify() );
			}

			w.Close();
		}
	}
}
