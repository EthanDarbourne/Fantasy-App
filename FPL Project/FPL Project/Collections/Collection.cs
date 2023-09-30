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
	public abstract class Collection : IEnumerable
	{
		protected List<Info> Info_ = new();

		public IEnumerator GetEnumerator()
		{
			return new CollectionEnumerator(Info_);
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

		private class CollectionEnumerator : IEnumerator
		{

			private List<Info> Info_;

			private int Index_ = -1;


			public CollectionEnumerator(List<Info> info)
			{
				Info_ = info;
			}

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
		}
	}
}
