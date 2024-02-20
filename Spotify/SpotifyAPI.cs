using System;
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

        private static async Task InitializeSpotifyClient()
        {
            // Retrieve Spotify Secrets
            var appConfig = new ConfigurationBuilder()
                .AddUserSecrets<Program>()
                .Build();

            var config = SpotifyClientConfig
                .CreateDefault()
                .WithAuthenticator(new ClientCredentialsAuthenticator(appConfig["SpotifyClientID"], appConfig["SpotifyClientSecret"]));

            spotify = new SpotifyClient(config);

        }
        //TODO: Add check to see if we need a new token or not
        // Temp Method to keep tested functionality
        public async Task PrintArtistInfoAsync()
        {
            //TODO: This is a fake authentication check. Implement something proper
            if(!authenticated)
            {
               
                await InitializeSpotifyClient();

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
    
        public async Task<string> GetArtistID(string artistName)
        {
            string artistID = "Test";

           // SimpleArtist artist = await spotify.Search();


            return artistID;
        }

    }
}
