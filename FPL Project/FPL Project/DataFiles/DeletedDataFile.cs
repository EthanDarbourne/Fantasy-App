using FPL_Project.Collections;
using FPL_Project.Constants;
using FPL_Project.Data;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPL_Project.DataFiles
{
	public class DeletedDataFile : DataFile<DeletedCollection>
	{
		public DeletedDataFile() : base( $"{FPLConstants.FolderName}/DeletedData.csv" )
		{
		}

		public override string Header => "";

		public override DeletedCollection ReadDataFile()
		{
			throw new NotImplementedException(); // why are we reading this
		}

		public override void WriteToFile( DeletedCollection collection )
		{
			using StreamWriter w = new( fileName, true );
			int i = 0;
			foreach(Info info in collection)
			{
				w.Write( collection.GetReason( i ) + ',');
				w.WriteLine( info.Stringify() );
				++i;
			}
		}
	}
}
