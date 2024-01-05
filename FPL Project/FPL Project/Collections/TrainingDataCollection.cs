﻿using FPL_Project.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPL_Project.Collections
{
	public class TrainingDataCollection : Collection<TrainingData>
	{


		public void AddTrainingData(TrainingData data )
		{
			Items_.Add( data );
		}
	}
}
