using FPL_Project.Players;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPL_Project.Data
{
	public class DeletedDataFile : DataFile
	{
		public DeletedDataFile() : base( "Data/DeletedData.csv" )
		{
		}

		public override string Header => "";

		public override Collection ReadDataFile()
		{
			throw new NotImplementedException(); // why are we reading this
		}

		public override void WriteToFile( Collection collection )
		{
			var collect = collection as DeletedCollection;
			using var w = new StreamWriter( fileName, true );
			int i = 0;
			foreach(Info info in collection)
			{
				w.Write( collect!.GetReason( i ) + ',');
				w.WriteLine( info.Stringify() );
				++i;
			}
		}
	}
}
