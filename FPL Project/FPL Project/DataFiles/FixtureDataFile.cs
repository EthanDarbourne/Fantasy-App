using FPL_Project.Collections;
using FPL_Project.Data;
using FPL_Project.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPL_Project.DataFiles
{
	public class FixtureDataFile : DataFile
	{

		public FixtureDataFile() : base( "Data/Fixtures.csv" )
		{
		}

		public override string Header => "Gameweek,Home,Away,HomeScored,AwayScored";

		public override Collection ReadDataFile()
		{
			var fixtures = new FixtureCollection();
			if ( !File.Exists( fileName ) ) return fixtures;

			using StreamReader r = new StreamReader( fileName );
			string line = r.ReadLine(); // header
			while ( ( line = r.ReadLine() ) != null )
			{
				var fixture = new Fixture();
				fixture.LoadFromLine( line );
				fixtures.AddFixture( fixture );
			}
			r.Close();
			return fixtures;
		}

		public override void WriteToFile( Collection collection )
		{
			using StreamWriter w = new StreamWriter( fileName );

			w.WriteLine( Header );
			foreach ( Fixture fixture in collection )
			{
				w.WriteLine( fixture.Stringify() );
			}

			w.Close();
		}
	}
}
