using FPL_Project.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPL_Project.Collections
{
	public class ConfigCollection : Collection
	{

		private Dictionary<string, ConfigPair> Pairs_ = new();


		public void UpdateValue( string key, string value )
		{
			if ( Pairs_.ContainsKey( key ) )
			{
				Pairs_[ key ].SetValue( value );
			}
		}

		public void AddPair( string key, string value )
		{
			if ( !Pairs_.ContainsKey( key ) )
			{
				var config = new ConfigPair( key, value );
				Pairs_.Add( key, config );
				Info_.Add( config );
			}
		}

		public bool ContainsKey( string key )
		{
			return Pairs_.ContainsKey( key );
		}

		public string GetValue(string key)
		{
			if(ContainsKey(key))
			{
				return Pairs_[ key ].Value;
			}
			return "";
		}

	}
}
