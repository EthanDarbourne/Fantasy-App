using FPL_Project.Api;
using FPL_Project.Collections;
using FPL_Project.Data;
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
		public static async void GenerateTrainingData( int weeks, PlayerDetailsCollection playerData, List<GameweekDataCollection> gameweekDataCollection, FixtureCollection fixtures )
		{
			if ( weeks < 5 ) return; // need 5 weeks of data

			// make full players
			var trainingDataCollection = new TrainingDataCollection();

			// iterate through full players and create data for all groups of 6 weeks
			var FullPlayers = new FullPlayerCollection();

			foreach ( PlayerDetails player in playerData )
			{
				FullPlayers.AddPlayer( new FullPlayer( player, new() ) );
			}

			var nextWeekFixtures = await FantasyApi.GetFixtureWeek(weeks);

			var teamHistorys = new List<TeamsHistory>();
			var teams = Enum.GetValues( typeof( Teams ) ).Cast<Teams>().ToList();
			foreach (var team in teams )
			{
				teamHistorys.Add( new TeamsHistory( team ) );
			}

			for (int week = 1; week <= weeks; ++week)
			{
				foreach ( var team in teamHistorys )
				{
					var fixturesInWeek = fixtures.GetFixtures( week, team.Team );
					fixturesInWeek.ForEach( team.AddFixture );
					var playerWith90Mins = FullPlayers.
				}

				foreach ( FullPlayer fullPlayer in FullPlayers)
				{
					fullPlayer.GameweekData.Add( gameweekDataCollection[ week ].GetGameweekData( fullPlayer.PlayerDetails.Name ) );

					var gameweekData = fullPlayer.GameweekData;
					var trainingData = fullPlayer.TrainingData;

					trainingData.AddGameweekToStats( gameweekData[ week ] );
					trainingData.AddGameweekToLastFive( gameweekData[ week ] );
					trainingData.ActualPoints = gameweekData[ week ].Points;
					if ( week > 5 )
					{
						trainingData.RemoveGameweekFromLastFive( gameweekData[ week - 5 ] ); // remove oldest gameweek
					}
					if ( week > 4 )
					{

						Teams opponent;
						if(week == weeks) // use next fixture data
						{
							opponent = nextWeekFixtures.GetOpponent( week, trainingData.Team );
						}
						else
						{
							opponent = fixtures.GetOpponent( week, trainingData.Team );
						}

						foreach ( var team in teamHistorys )
						{
							if ( team.Team == opponent ) trainingData.SetOpponentData( team );
						}

						trainingDataCollection.AddTrainingData( new TrainingData( trainingData ) );
					}
				}
			}
			
			

			TrainingData_.WriteToFile( trainingDataCollection );

		}

		// generate the data to predict on 
		public static void GenerateTestingData( int weeks, PlayerDetailsCollection playerData, List<GameweekDataCollection> gameweekData )
		{
			// make full players

			// iterate through full players and create data for last of 5 weeks 

			var trainingDataCollection = new TrainingDataCollection();

			foreach ( PlayerDetails player in playerData )
			{
				var playerGameweek = new List<GameweekData>();
				foreach ( var gameweek in gameweekData )
				{
					playerGameweek.Add( gameweek.GetGameweekData( player.Name ) );
				}
				var fullPlayer = new FullPlayer( player, playerGameweek );

				var trainingData = new TrainingData( player );
				for ( int i = 0; i < weeks; ++i )
				{
					trainingData.AddGameweekToStats( playerGameweek[ i ] );
					trainingData.AddGameweekToLastFive( playerGameweek[ i ] );
					trainingData.ActualPoints = playerGameweek[ i ].Points;
					if ( i >= 5 )
					{
						trainingData.RemoveGameweekFromLastFive( playerGameweek[ i - 5 ] ); // remove oldest gameweek
					}
					if ( i >= 4 )
					{
						trainingDataCollection.AddTrainingData( new TrainingData( trainingData ) );
					}

				}

			}

			TestingData_.WriteToFile( trainingDataCollection );
		}
	}
}
