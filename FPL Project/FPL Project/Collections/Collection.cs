using FPL_Project.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace FPL_Project.Collections
{
	public abstract class Collection : IEnumerator, IEnumerable
	{
		protected List<Info> Info_ = new();

		private int Index_ = -1;

		public object Current => Info_[ Index_ ];

		public bool MoveNext()
		{

			if ( Index_ >= Info_.Count - 1 ) return false;
			++Index_;
			return true;
		}

		public void Reset()
		{
			Index_ = -1;
		}

		public IEnumerator GetEnumerator()
		{
			return this;
		}

		protected void AddInfo(Info info)
		{
			Info_.Add( info );
		}

		protected bool DeleteInfo(Func<Info, bool> filter)
		{
			for(int i = 0; i < Info_.Count; ++i)
			{
				if(filter(Info_[i]))
				{
					Info_.RemoveAt( i );
					return true;
				}
			}
			return false;
		}
	}
}
