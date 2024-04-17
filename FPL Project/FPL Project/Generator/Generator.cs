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

		private static readonly TrainingDataFile TrainingData_;
		private static readonly TestingDataFile TestingData_;

		static Generator()
		{
			TrainingData_ = new();
			TestingData_ = new();
		}

		private static async Task<TrainingDataCollection?> GenerateTrainingDataHidden(int weeks, PlayerDetailsCollection playerData, List<GameweekDataCollection> gameweekDataCollection, FixtureCollection fixtures )
		{
			if ( weeks < 5 ) return null; // need 5 weeks of data

			// make full players
			TrainingDataCollection trainingDataCollection = new();

			// iterate through full players and create data for all groups of 6 weeks
			FullPlayerCollection FullPlayers = new(playerData.Select( player => new FullPlayer( player, new())));

			FixtureCollection nextWeekFixtures = await FantasyApi.GetFixtureWeek( weeks );

			List<Teams> teams = Enum.GetValues( typeof( Teams ) ).Cast<Teams>().ToList();
			List<TeamsHistory> teamHistorys = teams.Select(team => new TeamsHistory(team)).ToList();

			for ( int week = 1; week <= weeks; ++week )
			{
				foreach ( var team in teamHistorys )
				{
					List<Fixture> fixturesInWeek = fixtures.GetFixtures( week, team.Team );
					fixturesInWeek.ForEach( team.AddFixture );
				}

				foreach ( FullPlayer fullPlayer in FullPlayers )
				{
					fullPlayer.GameweekData.Add( gameweekDataCollection[ week - 1 ].GetGameweekData( fullPlayer.PlayerDetails.Name )! );

					var gameweekData = fullPlayer.GameweekData;
					var trainingData = fullPlayer.TrainingData;

					
					
					if ( week > 5 && week != weeks )
					{
						
						//if ( week == weeks) // use next fixture data
						//{
						//	opponent = nextWeekFixtures.GetOpponent( week, trainingData.Team );
						//}
						//else
						//{
						List<Teams> opponents = fixtures.GetOpponents( week, trainingData.Team );
						//}

						foreach(Teams opponent in opponents)
						{
							foreach ( TeamsHistory team in teamHistorys )
							{
								if ( team.Team == opponent ) trainingData.SetOpponentData( team );
							}
						}
						

						// use current weeks points
						trainingData.ActualPoints = gameweekDataCollection[ week ].GetGameweekData( fullPlayer.PlayerDetails.Name )!.Points;

						trainingData.Week = week;
						trainingDataCollection.AddTrainingData( new TrainingData( trainingData ) );
					}

					trainingData.AddGameweekToStats( gameweekData[ week - 1 ] );
					trainingData.AddGameweekToLastFive( gameweekData[ week - 1 ] );
					if ( week > 5 )
					{
						trainingData.RemoveGameweekFromLastFive( gameweekData[ week - 1 - 5 ] ); // remove oldest gameweek
					}
				}
			}
			return trainingDataCollection;
		}

		private static async Task<TestingDataCollection?> GenerateFutureData(int weeks, PlayerDetailsCollection playerData, List<GameweekDataCollection> gameweekDataCollection, FixtureCollection fixtures )
		{
			// get data for all players in the last 5 weeks

			// find future fixtures


			if ( weeks < 5 ) return null; // need 5 weeks of data

			// make full players
			TestingDataCollection testingDataCollection = new();

			// iterate through full players and create data for all groups of 6 weeks
			FullPlayerCollection FullPlayers = new( playerData.Select( player => new FullPlayer( player, new() ) ) );

			FixtureCollection nextWeekFixtures = await FantasyApi.GetFixtureWeek( weeks );

			List<Teams> teams = Enum.GetValues( typeof( Teams ) ).Cast<Teams>().ToList();
			List<TeamsHistory> teamHistorys = teams.Select( team => new TeamsHistory( team ) ).ToList();

			for ( int week = 1; week <= weeks; ++week )
			{
				foreach ( var team in teamHistorys )
				{
					List<Fixture> fixturesInWeek = fixtures.GetFixtures( week, team.Team );
					fixturesInWeek.ForEach( team.AddFixture );
				}

				foreach ( FullPlayer fullPlayer in FullPlayers )
				{
					fullPlayer.GameweekData.Add( gameweekDataCollection[ week - 1 ].GetGameweekData( fullPlayer.PlayerDetails.Name ) );

					var gameweekData = fullPlayer.GameweekData;
					var trainingData = fullPlayer.TrainingData;

					trainingData.AddGameweekToStats( gameweekData[ week - 1 ] );

					if(week >= weeks - 4 )
					{
						trainingData.AddGameweekToLastFive( gameweekData[ week - 1 ] );
					}

					//if ( week > 4 )
					//{
					//	Teams opponent;
					//	if ( week == weeks ) // use next fixture data
					//	{
					//		opponent = nextWeekFixtures.GetOpponent( week, trainingData.Team );
					//	}
					//	else
					//	{
					//		opponent = fixtures.GetOpponent( week, trainingData.Team );
					//	}

					//	foreach ( var team in teamHistorys )
					//	{
					//		if ( team.Team == opponent ) trainingData.SetOpponentData( team );
					//	}

					//	testingDataCollection.AddTrainingData( new TrainingData( trainingData ) );
					//}
				}
			}

			for (int week = weeks + 1; week <= 38; ++week)
			{
				FixtureCollection nextFixtures = await FantasyApi.GetFixtureWeek( week );
				foreach ( FullPlayer player in FullPlayers )
				{
					List<Teams> opponents = nextFixtures.GetOpponents( week, player.TrainingData.Team );

					foreach ( Teams opponent in opponents )
					{
						foreach ( TeamsHistory team in teamHistorys )
						{
							if ( team.Team == opponent ) player.TrainingData.SetOpponentData( team );
						}
					}
					player.TrainingData.Week = week;
					testingDataCollection.AddTestingData( new TestingData( player.TrainingData ) );
				}
			}
			return testingDataCollection;
		}

		// generate the data to train on
		public static async Task GenerateTrainingData( int weeks, PlayerDetailsCollection playerData, List<GameweekDataCollection> gameweekDataCollection, FixtureCollection fixtures )
		{

			TrainingDataCollection? trainingData = await GenerateTrainingDataHidden( weeks, playerData, gameweekDataCollection, fixtures );
			if ( trainingData is not null )
			{
				TrainingData_.WriteToFile( trainingData, "" );
			}

		}


		public static async Task GenerateTrainingDataAboveThreshold( int weeks, PlayerDetailsCollection playerData, List<GameweekDataCollection> gameweekDataCollection, FixtureCollection fixtures, int threshold = 5 )
		{

			TrainingDataCollection? trainingData = await GenerateTrainingDataHidden( weeks, playerData, gameweekDataCollection, fixtures );
			if ( trainingData is not null )
			{
				TrainingDataCollection filteredData = new();
				foreach ( TrainingData data in trainingData )
				{
					if ( data.Points > threshold )
					{
						filteredData.AddTrainingData( data );
					}
				}
				TrainingData_.WriteToFile( trainingData, "-Above" + threshold );
			}

		}

		public static async Task GenerateTrainingDataForPlayers( List<string> names, int weeks, PlayerDetailsCollection playerData, List<GameweekDataCollection> gameweekDataCollection, FixtureCollection fixtures )
		{
			IEnumerable<PlayerDetails> players = playerData.Where( x => names.Contains(x.Name));
			TrainingDataCollection? trainingData = await GenerateTrainingDataHidden( weeks, new PlayerDetailsCollection( players ), gameweekDataCollection, fixtures );
			if ( trainingData is not null )
			{
				TrainingData_.WriteToFile( trainingData, "-" + "BulkCreation" );
			}
		}

		public static async Task GenerateTrainingDataForPlayer( string name, int weeks, PlayerDetailsCollection playerData, List<GameweekDataCollection> gameweekDataCollection, FixtureCollection fixtures )
		{
			IEnumerable<PlayerDetails> players = playerData.Where(x => x.Name == name );
			TrainingDataCollection? trainingData = await GenerateTrainingDataHidden( weeks, new PlayerDetailsCollection(players), gameweekDataCollection, fixtures );
			if ( trainingData is not null )
			{
				TrainingData_.WriteToFile( trainingData, "-" + name );
			}
		}

		public static async Task GeneratePredictionDataForWeek(int week, int weeks, PlayerDetailsCollection playerData, List<GameweekDataCollection> gameweekDataCollection, FixtureCollection fixtures )
		{
			TestingDataCollection? testingData = await GenerateFutureData( weeks, playerData, gameweekDataCollection, fixtures );

			
			if ( testingData is not null )
			{
				TestingDataCollection filteredData = new ();
				foreach ( TestingData data in testingData )
				{
					if(data.Week == week)
					{
						filteredData.AddTestingData( data );
					}
				}
				TestingData_.WriteToFile( filteredData, "-Week" + week );
			}
		}
	}
}
