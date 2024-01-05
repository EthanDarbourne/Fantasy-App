using FPL_Project.Collections;
using FPL_Project.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPL_Project.DataFiles
{
	public class ConfigDataFile : DataFile<ConfigCollection>
	{
		public ConfigDataFile() : base( $"Data/Config.csv" )
		{

		}

		public override string Header => "Key,Value";

		public override ConfigCollection ReadDataFile()
		{
			ConfigCollection config = new();
			if ( !File.Exists( fileName ) ) return config;

			using StreamReader r = new( fileName );

			string? line = r.ReadLine(); // header
			while ( ( line = r.ReadLine() ) is not null )
			{
				var vals = line.Split( ',' );
				config.AddPair( vals[0], vals[1] );
			}
			r.Close();
			return config;
		}

		public override void WriteToFile( ConfigCollection config )
		{
			using StreamWriter w = new ( fileName );

			w.WriteLine( "Key,Value" );

			foreach(Info key in config)
			{
				w.WriteLine( key.Stringify() );
			}
			w.Close();
		}
	}
}
