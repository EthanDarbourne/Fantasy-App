﻿using FPL_Project.Collections;
using FPL_Project.Data;
using FPL_Project.Players;
using Newtonsoft.Json.Linq;
using System.Globalization;

namespace FPL_Project.Api
{
	public static class FantasyApi
	{
		private static string getAllData_ = "https://fantasy.premierleague.com/api/bootstrap-static/";
		private static string apiStartString = "https://fantasy.premierleague.com/api/";
		private static string getPlayerData_ = "https://fantasy.premierleague.com/api/element-summary/{0}/";
		private static string getGameweekData_ = "https://fantasy.premierleague.com/api/event/{0}/live/";
		private static string getFixtureData_ = "https://fantasy.premierleague.com/api/fixtures/";

		static HttpClient client = new(); 

		// need to map some players as they have the same names
		private static Dictionary<Tuple<string, Teams>, string> playerMap = new()
		{
			{ Tuple.Create("Martinez", Teams.ManUtd), "L.Martinez" },
			{ Tuple.Create("Johnson", Teams.Spurs), "B.Johnson" },
			{ Tuple.Create("Onana", Teams.ManUtd), "A.Onana" },
			{ Tuple.Create("Neto", Teams.Arsenal), "N.Neto" },
			{ Tuple.Create("Wilson", Teams.Fulham), "H.Wilson" },
			{ Tuple.Create("Taylor", Teams.Ipswich), "J.Taylor" },
			//{ Tuple.Create("Thomas", Teams.SheffieldUtd), "L.Thomas" },

		};

		private static char[,] characterMap = new char[,]{{ 'Ø', 'O' }, { 'ß', 'b' }, { 'ć', 'c' }, { 'á', 'a' }, { 'í', 'i' }, { 'ğ', 'g' }, { 'ú', 'u' }, { 'ž', 'z' }, { 'ł', 'l' }, { 'ö', 'o' },
				{ 'ü', 'u' }, { 'ø', 'o' }, { 'ä', 'a' }, { 'š', 's' }, { 'ó', 'o' }, { 'é', 'e' }, { 'Á', 'A' }, { 'ï', 'i' }, { 'ñ', 'n' }, { 'ã', 'a' }, { 'ş', 's' }, { 'Š', 'S' }};

		private static string PlayerMap( string player, Teams team )
		{
			playerMap.TryGetValue( Tuple.Create( player, team ), out var val );
			return val is null ? player : val;

		}

		private static string CharacterMap( string name )
		{
			for ( int i = 0; i < characterMap.Length / 2; ++i )
			{
				name = name.Replace( characterMap[ i, 0 ], characterMap[ i, 1 ] );
			}
			return name;
		}

		private static Teams TeamsMap(int team)
		{
			return TeamReader.ReadTeam( TeamReader.TeamsAsStrings[ team - 1 ]);
		}

		public static async Task<FixtureCollection> LoadFixtureDetails()
		{
			var request = getFixtureData_;
			var response = await client.GetAsync( request );
			var dataObjects = await response.Content.ReadAsStringAsync();
			var fixtures = JArray.Parse( dataObjects );

			var ret = new FixtureCollection();

			foreach(var fixture in fixtures)
			{
				if ( !(bool)fixture[ "finished" ]! ) continue;

				var id = ( int ) fixture[ "id" ];
				var week = ( int ) fixture[ "event" ];
				var homeTeam = TeamsMap( (int)fixture[ "team_h" ] );
				var awayTeam = TeamsMap( (int) fixture[ "team_a" ] );
				var homeScored = ( int ) fixture[ "team_h_score" ];
				var awayScored = ( int ) fixture[ "team_a_score" ];

				ret.AddFixture( new Fixture( id, week, homeTeam, awayTeam, homeScored, awayScored ) );
			}

			return ret;
		}

		public static async Task<FixtureCollection> GetFixtureWeek( int gameweek )
		{
			var request = getFixtureData_;
			var response = await client.GetAsync( request );
			var dataObjects = await response.Content.ReadAsStringAsync();
			var fixtures = JArray.Parse( dataObjects );

			var ret = new FixtureCollection();

			foreach ( var fixture in fixtures )
			{
				var events = fixture ["event"];
				if ( events.Type == JTokenType.Null ) continue;
				var id = ( int ) fixture[ "id" ];
				var week = ( int ) fixture[ "event" ];
				if ( week != gameweek ) continue;
				var homeTeam = TeamsMap( ( int ) fixture[ "team_h" ] );
				var awayTeam = TeamsMap( ( int ) fixture[ "team_a" ] );

				var tok = fixture[ "team_h_score" ];
				int homeScored = tok!.Type == JTokenType.Null ? 0 : ( int ) tok;
				tok = fixture[ "team_a_score" ];
				int awayScored = tok!.Type == JTokenType.Null ? 0 : ( int ) tok;
				//int awayScored = (int)(fixture[ "team_a_score" ] ?? 0);

				ret.AddFixture( new Fixture( id, week, homeTeam, awayTeam, homeScored, awayScored ) );
			}

			return ret;
		}

		public static async Task<PlayerDetailsCollection> LoadNewPlayerDetails( PlayerDetailsCollection cur )
		{
			var request = getAllData_;
			var response = await client.GetAsync( request );
			var dataObjects = await response.Content.ReadAsStringAsync();
			var joResponse = JObject.Parse( dataObjects );
			var players = ( JArray ) joResponse[ "elements" ]!;

			var ret = new PlayerDetailsCollection();

			foreach ( JObject player in players )
			{
				var stat = player.ToObject<PlayerDetails>();

				var name = PlayerMap( CharacterMap( stat.Name ), TeamsMap( (int)player["team"] ) );


				var replacePlayer = cur.GetPlayer( name );
				if ( replacePlayer is null )
				{
                    Console.WriteLine( $"Couldn't add team and position for {stat.Name}" );
                    //if ( ( int ) player[ "total_points" ] > 13 || ( int ) player[ "minutes" ] > 152 )
                    //{
                    //	stat.SetTeamAndPosition( Teams.Burnley, Positions.Defender );
                    //	ret.AddPlayerDetails( stat );
                    //	Console.WriteLine( $"Couldn't add team and position for {stat.Name}" );
                    //}
                    continue;
				}
				else
				{
					stat.SetName( name );
					stat.SetTeamAndPosition( replacePlayer.Team, replacePlayer.Position );
					ret.AddPlayerDetails( stat! );
				}

			}

			foreach ( PlayerDetails player in cur )
			{
				if ( ret.GetPlayer( player.Name ) is null )
				{
					Console.WriteLine( $"Didn't find player {player.Name}, {player.Team}" );
				}
			}

			return ret;
		}

        public static async Task<List<GameweekData>> GetPlayerGameweekData( int playerId )
		{
			var request = string.Format( getPlayerData_, playerId );
			var response = await client.GetAsync( request );
			var dataObjects = await response.Content.ReadAsStringAsync();
			var joResponse = JObject.Parse( dataObjects );
			var history = ( JArray ) joResponse[ "history" ]!;
			var fixtures = ( JArray ) joResponse[ "fixtures" ]!;
			var historyPast = ( JArray ) joResponse[ "history_past" ]!;

			var stats = new List<GameweekData>();

			//var pastPlayers = history.ToObject<List<GameweekData>>();
			foreach ( var obj in history )
			{
				Console.WriteLine( obj );
				var stat = obj.ToObject<GameweekData>();
				stats.Add( stat! );
			}
			return stats;
		}

		public static async Task<GameweekDataCollection> GetFullGameweekData( int gameweek, PlayerDetailsCollection players )
		{
			var request = string.Format( getGameweekData_, gameweek );
			var response = await client.GetAsync( request );
			var dataObjects = await response.Content.ReadAsStringAsync();
			var joResponse = JObject.Parse( dataObjects );
			var newPlayers = ( JArray ) joResponse[ "elements" ]!;

			var gameweekData = new GameweekDataCollection( gameweek );

			foreach ( var player in newPlayers )
			{
				var playerId = ( int ) player[ "id" ]; // check if we want this player id

				if ( !players.ContainsPlayerId( playerId ) ) continue;

				var playerDetails = players.GetPlayer( players.GetPlayerNameById( playerId )! );

				var play = player[ "stats" ]!.ToObject<GameweekData>();

				var fixtures = player[ "explain" ];
				var ids = new List<int>();
				foreach(var fixture in fixtures)
				{
					ids.Add( ( int ) fixture[ "fixture" ] );
				}
				play.SetExtraInfo( playerId, playerDetails.Name, playerDetails.Team, gameweek, ids );
				gameweekData.AddPlayer( play! );
			}


			return gameweekData;
		}

		public static async Task<int> GetWeeksPlayed()
		{
			var request = getAllData_;
			var response = await client.GetAsync( request );
			var dataObjects = await response.Content.ReadAsStringAsync();
			var joResponse = JObject.Parse( dataObjects );
			var weeks = ( JArray ) joResponse[ "events" ]!;

			int count = 0;

			foreach(var week in weeks)
			{
				if ( ( bool ) week[ "finished" ] ) ++count;
			}

			return count;
		}
	}
}
