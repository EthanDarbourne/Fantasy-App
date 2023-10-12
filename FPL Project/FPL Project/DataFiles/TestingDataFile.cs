using FPL_Project.Collections;
using FPL_Project.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPL_Project.DataFiles
{
	public class TestingDataFile : DataFile
	{
		public TestingDataFile() : base( "Data/TestingData.csv" )
		{
		}

		public override string Header => throw new NotImplementedException();

		public override Collection ReadDataFile()
		{
			throw new NotImplementedException();
		}

		public override void WriteToFile( Collection collection )
		{
			throw new NotImplementedException();
		}
	}
}
