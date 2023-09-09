using FPL_Project.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPL_Project.Data
{
	public abstract class DataFile
	{

		protected string fileName;
		protected bool NeedsWrite_ = false;

		public DataFile( string fileName )
		{
			this.fileName = "Data/" + fileName;
			if ( File.Exists( fileName ) )
			{
				ReadFile();
			}
		}


		public abstract Collection ReadFile();

		public abstract void WriteToFile();

		~DataFile()
		{
			WriteToFile();
		}
	}
}
