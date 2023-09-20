using FPL_Project.Collections;
using FPL_Project.DataFiles;
using FPL_Project.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPL_Project.Generator
{
	public static class Generator
	{

		private static TrainingDataFile TrainingData_;
		private static TestingDataFile TestingData_;

		static Generator()
		{
			TrainingData_ = new TrainingDataFile();
			TestingData_ = new TestingDataFile();
		}

		// generate the data to train on
		public static void GenerateTrainingData( int weeks, PlayerDetailsCollection playerData, List<GameweekDataCollection> gameweekData )
		{
			// make full players

			var trainingdatacollection = new TrainingDataCollection();

			// iterate through full players and create data for all groups of 6 weeks
			var FullPlayers = new FullPlayerCollection();
			foreach(PlayerDetails player in playerData)
			{
				var playerGameweek = new List<GameweekData>();
				foreach(var gameweek in gameweekData)
				{
					playerGameweek.Add( gameweek.GetGameweekData( player.Name ) );
				}
				var fullPlayer = new FullPlayer( player, playerGameweek );


				for(int i = 0; i < weeks; ++i)
				{
					
				}
			}

		}



		// generate the data to predict on 
		public static void GeneratePredictionData( PlayerDetailsCollection playerData, List<GameweekDataCollection> gameweekData )
		{
			// make full players

			// iterate through full players and create data for last of 5 weeks 

		}
	}
}
