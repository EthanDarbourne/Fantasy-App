using FPL_Project.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPL_Project.DataFiles
{
    public abstract class DataFile
	{

		protected string fileName;
		protected bool NeedsWrite_ = false;

		public DataFile( string fileName )
		{
			this.fileName = "../../../../../" + fileName;
		}

		public abstract string Header { get; }

		public abstract Collection ReadDataFile();

		public abstract void WriteToFile( Collection collection );

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
