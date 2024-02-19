﻿using System;
using System.Net.Security;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SpotifyAPI.Web;
using SpotifyAPI.Web.Http;

namespace LineupAnalyzer.Spotify
{
    class SpotifyAuthResponse
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }
        [JsonProperty("token_type")]
        public string TokenType { get; set; }
    }

    public class SpotifyInfo
    {
        /*  Create only one HttpClient to share across project  */
        private static readonly HttpClient client = new HttpClient();

        private static SpotifyClient spotify;

        private bool authenticated = false;

        //public SpotifyAPI
        private static async Task<string> RequestSpotifyToken()
        {
            // Retrieve Spotify Secrets
            var appConfig = new ConfigurationBuilder()
            .AddUserSecrets<Program>()
            .Build();

            var mySpotifyClientID = appConfig["SpotifyClientID"];
            var mySpotifyCllientSecret = appConfig["SpotifyClientSecret"];

            // Set HttpRequest Authorization Header
            var authenticationString = $"{mySpotifyClientID}:{mySpotifyCllientSecret}";
            var base64EncodedAuthenticationString = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(authenticationString));

            client.DefaultRequestHeaders.Add("Authorization", "Basic " + base64EncodedAuthenticationString);

            // Set HttpRequest Body
            var values = new Dictionary<string, string>
            {
                { "grant_type", "client_credentials" }
            };

            var content = new FormUrlEncodedContent(values);

            //TODO: Implement error handling!
            // Request Spotify API Access Token
            HttpResponseMessage response = await client.PostAsync("https://accounts.spotify.com/api/token", content);
            response.EnsureSuccessStatusCode();

            // Deserialize HttpRequest to access information
            var responseString = await response.Content.ReadAsStringAsync();
            SpotifyAuthResponse authResponse = JsonConvert.DeserializeObject<SpotifyAuthResponse>(responseString);

            return authResponse.AccessToken;
        }

        private static async Task ConfigureSpotifyClient()
        {
            //TODO: Add check to see if we need a new token or not
            // Request Spotify access token
            string accessToken = await RequestSpotifyToken();

            // Configure SpotifyAPI client
            SpotifyClientConfig config = SpotifyClientConfig
                .CreateDefault(accessToken)
                .WithHTTPLogger(new SimpleConsoleHTTPLogger());

            spotify = new SpotifyClient(config);

        }
    
        // Temp Method to keep tested functionality
        public async Task PrintArtistInfoAsync()
        {
            //TODO: This is a fake authentication check. Implement something proper
            if(!authenticated)
            {
               
                await ConfigureSpotifyClient();

                authenticated = true;

            }

            // Optional query/body parameter
            FullTrack track = await spotify.Tracks.Get("1s6ux0lNiTziSrd7iUAADH", new TrackRequest
            {
                Market = "DE"
            });

            FullArtist artist = await spotify.Artists.Get("4Z8W4fKeB5YxbusRsdQVPb");

            //SearchClient search = await spotify.Search. .Search. .Humanize .Browse. .GetCategories() Search Get("Beyonce");

            // Sometimes, query/body parameters are also required!
            // var tracks = await spotify.Tracks.GetSeveral(new TracksRequest(new List<string> {
            // "1s6ux0lNiTziSrd7iUAADH",
            // "6YlOxoHWLjH6uVQvxUIUug"
            // }));

            // Print data of interest
            Console.WriteLine(track.Name);
            Console.WriteLine(artist.Name);
            Console.WriteLine(artist.Followers.Total);
            Console.WriteLine(artist.Popularity);
            Console.WriteLine(artist.Genres[0]);
        }
    
    }
}
