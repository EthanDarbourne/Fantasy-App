using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPL_Project.Players
{
	public abstract class Collection : IEnumerator, IEnumerable
	{
		protected List<Data>

		public abstract object Current { get; }

		public abstract IEnumerator GetEnumerator();
		public abstract bool MoveNext();
		public abstract void Reset();
	}
}
