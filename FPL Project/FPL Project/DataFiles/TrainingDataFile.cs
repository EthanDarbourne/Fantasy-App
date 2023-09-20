using FPL_Project.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPL_Project.DataFiles
{
	public class TrainingDataFile : DataFile
	{
		public TrainingDataFile() : base( "Data/TrainingData.csv" )
		{
		}

		public override string Header => "Name,Team,Position,"

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
