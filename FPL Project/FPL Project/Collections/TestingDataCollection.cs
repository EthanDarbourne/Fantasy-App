using FPL_Project.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPL_Project.Collections
{
	public class TestingDataCollection : Collection<TestingData>
	{


		public void AddTestingData( TestingData data )
		{
			Items_.Add( data );
		}
	}
}
