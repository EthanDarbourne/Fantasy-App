using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPL_Project.Players
{
	public class ConfigPair : Info
	{

		private string Key_;
		private string Value_;

		public ConfigPair()
		{

		}

		public ConfigPair(string key, string value)
		{
			Key_ = key;
			Value_ = value;
		}

		public string Key => Key_;
		public string Value => Value_;

		

		public override void LoadFromLine( string line )
		{
			var vals = line.Split( ',' );
			Key_ = vals[ 0 ];
			Value_ = vals[ 1 ];
		}

		public void SetValue(string value)
		{
			Value_ = value;
		}

		public override string Stringify()
		{
			return Key + "," + Value;
		}
	}
}
