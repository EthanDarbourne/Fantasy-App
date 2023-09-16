using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPL_Project.Players
{
	public class DeletedCollection : Collection
	{
		private List<string> Reasons_ = new();

		public void AddInfo(Info info, string reason)
		{
			Reasons_.Add( reason );
			Info_.Add( info );
		}

		public string GetReason(int i)
		{
			return Reasons_[ i ];
		}
	}
}
