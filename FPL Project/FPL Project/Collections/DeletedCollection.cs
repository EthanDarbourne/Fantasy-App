﻿using FPL_Project.Data;
using FPL_Project.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPL_Project.Collections
{
	public class DeletedCollection : Collection
	{
		private List<string> Reasons_ = new();

		public void AddInfo(Info info, string reason)
		{
			Reasons_.Add( reason );
			AddInfo( info );
		}

		public string GetReason(int i)
		{
			return Reasons_[ i ];
		}
	}
}
