using System;
using System.Net.Security;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SpotifyAPI.Web;
using SpotifyAPI.Web.Http;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LineupAnalyzer.Spotify
{
    public class SpotifyInfo
    {
        /*  Create only one HttpClient to share across project  */
        //private static readonly HttpClient client = new HttpClient();

        private static SpotifyClient spotify;

        private static bool authenticated;

        public SpotifyInfo() 
        {
            authenticated = false;
        }

        private static void InitializeSpotifyClient()
        {
            // Retrieve Spotify Secrets
            var appConfig = new ConfigurationBuilder()
                .AddUserSecrets<Program>()
                .Build();

            var config = SpotifyClientConfig
                .CreateDefault()
                .WithAuthenticator(new ClientCredentialsAuthenticator(appConfig["SpotifyClientID"], appConfig["SpotifyClientSecret"]));

            spotify = new SpotifyClient(config);

            authenticated = true;

        }

        /*
         Function to return an artists spotifyID given their name
         */
        private async Task<string> GetArtistID(string artistName)
        {
            string artistID = null;
 
            // Create search request
            SearchRequest search = new SearchRequest(SearchRequest.Types.Artist,artistName);

            // Search using the created request
            SearchResponse searchResult = await spotify.Search.Item(search);

            // Verify that the search returned a result
            if (searchResult.Artists.Items.Count > 0)
            {
                artistID = searchResult.Artists.Items[0].Id;
                Console.WriteLine($"Artist ID obtained");
            }
            else
            {
                Console.WriteLine($"Artist '{artistName}' could not be found.");
            }

            return artistID;
        }
        
        /*
         Function to return an artists profile given their name
         */
        public async Task<FullArtist> GetFullArtist(string artistName)
        {
            if(!authenticated)
            {
                InitializeSpotifyClient();
            }

            string artistID = await GetArtistID(artistName);

            FullArtist artist = await spotify.Artists.Get(artistID);

            return artist;
        }

        public async Task<List<FullArtist>> GetFullArtistList(List<string> artistList)
        {
            if (!authenticated)
            {
                InitializeSpotifyClient();
            }

            List<FullArtist> lineup = new List<FullArtist> { };
            FullArtist buffer = new FullArtist();
            
            for (int i = 0; i < artistList.Count; i++)
            {
                Console.WriteLine(artistList[i]);
                buffer = await GetFullArtist(artistList[i]);
                lineup.Add(buffer);
            }

            return lineup;
        }

    }
}
