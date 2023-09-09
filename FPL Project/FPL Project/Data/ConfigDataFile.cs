using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPL_Project.Data
{
	internal class ConfigDataFile : DataFile
	{

		private Dictionary<string, string> Pairs_ = new();


		public ConfigDataFile() : base( $"Config.csv" )
		{

		}

		public override void ReadFile()
		{
			using StreamReader r = new( fileName );

			string line;
			while ( ( line = r.ReadLine() ) != null )
			{
				var vals = line.Split( ',' );
				Pairs_[ vals[ 0 ] ] = vals[ 1 ];
			}

		}

		public void UpdateValue(string key, string value )
		{
			if(Pairs_.ContainsKey(key))
			{
				NeedsWrite_ = true;
				Pairs_[key] = Pairs_[ value ];
			}
		}

		public void AddKey(string key, string value)
		{
			if(!Pairs_.ContainsKey(key))
			{
				NeedsWrite_ = true;
				Pairs_.Add( key, value );
			}
		}

		public bool ContainsKey(string key)
		{
			return Pairs_.ContainsKey( key );
		}

		public override void WriteToFile()
		{
			using StreamWriter streamWriter = new ( fileName );

			foreach(var key in Pairs_.Keys)
			{
				streamWriter.WriteLine( key + "," + Pairs_[ key ] );
			}
		}
	}
}
