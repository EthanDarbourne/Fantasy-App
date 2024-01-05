using FPL_Project.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace FPL_Project.Collections
{
	public abstract class Collection<T> : IEnumerable<T> where T : Info// IQueryable
	{
		protected readonly List<T> Items_ = new();

		//public abstract Type ElementType { get; }

		//public Expression Expression => throw new NotImplementedException();

		//public IQueryProvider Provider => throw new NotImplementedException();

		IEnumerator IEnumerable.GetEnumerator()
		{
			//foreach(Info info in Info_)
			//{
			//	yield return info;
			//}
			return new CollectionEnumerator( Items_ );
		}

		public IEnumerator<T> GetEnumerator()
		{
			return new CollectionEnumerator( Items_ );
		}

		protected void AddItem( T info )
		{
			Items_.Add( info );
		}

		protected bool DeleteItem( Func<T, bool> filter )
		{
			for ( int i = 0; i < Items_.Count; ++i )
			{
				if ( filter( Items_[ i ] ) )
				{
					Items_.RemoveAt( i );
					return true;
				}
			}
			return false;
		}

		private class CollectionEnumerator : IEnumerator<T>
		{

			private readonly List<T> Item;

			private int Index_ = -1;


			public CollectionEnumerator( List<T> item )
			{
				Item = item;
			}

			public object Current => this.Current;

			T IEnumerator<T>.Current => Item[ Index_ ];

			public void Dispose()
			{

			}

			public bool MoveNext()
			{
				if ( Index_ >= Item.Count - 1 ) return false;
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
