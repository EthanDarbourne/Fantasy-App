using FPL_Project.Collections;
using FPL_Project.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPL_Project.DataFiles
{
    public abstract class DataFile<T> 
	{

		protected string fileName;
		protected bool NeedsWrite_ = false;

		public DataFile( string fileName )
		{
			this.fileName = "../../../../../" + fileName;
		}

		public abstract string Header { get; }

		public abstract T ReadDataFile();

		public abstract void WriteToFile( T collection );

		//public Collection ReadFile()
		//{
		//	if ( File.Exists( fileName ) )
		//	{
		//		return ReadDataFile();
		//	}
		//	throw new FileNotFoundException();
		//}
	}
}
