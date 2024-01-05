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
					fullPlayer.GameweekData.Add( gameweekDataCollection[ week - 1 ].GetGameweekData( fullPlayer.PlayerDetails.Name ) );

					var gameweekData = fullPlayer.GameweekData;
					var trainingData = fullPlayer.TrainingData;

					trainingData.AddGameweekToStats( gameweekData[ week - 1 ] );
					trainingData.AddGameweekToLastFive( gameweekData[ week - 1 ] );
					trainingData.ActualPoints = gameweekData[ week - 1 ].Points;
					if ( week > 5 )
					{
						trainingData.RemoveGameweekFromLastFive( gameweekData[ week - 1 - 5 ] ); // remove oldest gameweek
					}
					if ( week > 4 )
					{
						Teams opponent;
						if ( week == weeks ) // use next fixture data
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
			return trainingDataCollection;
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

		public static async Task GenerateTrainingDataForPlayer( string name, int weeks, PlayerDetailsCollection playerData, List<GameweekDataCollection> gameweekDataCollection, FixtureCollection fixtures )
		{
			IEnumerable<PlayerDetails> players = playerData.Where(x => (x as PlayerDetails)!.Name == name ).Cast<PlayerDetails>();
			TrainingDataCollection? trainingData = await GenerateTrainingDataHidden( weeks, new PlayerDetailsCollection(players), gameweekDataCollection, fixtures );
			if ( trainingData is not null )
			{
				TrainingData_.WriteToFile( trainingData, "-" + name );
			}
		}

		// generate the data to predict on 
		public static void GenerateTestingData( int weeks, PlayerDetailsCollection playerData, List<GameweekDataCollection> gameweekData )
		{
			// make full players

			// iterate through full players and create data for last of 5 weeks 

			var testingDataCollection = new TrainingDataCollection();

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
						testingDataCollection.AddTrainingData( new TrainingData( trainingData ) );
					}

				}

			}

			//TestingData_.WriteToFile( trainingDataCollection );
		}
	}
}
