using System;
using System.Net.Security;
using LineupAnalyzer.Models;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SpotifyAPI.Web;
using SpotifyAPI.Web.Http;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LineupAnalyzer.Services;

internal class SpotifyInfo
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
        SearchRequest search = new SearchRequest(SearchRequest.Types.Artist, artistName);

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
        if (!authenticated)
        {
            InitializeSpotifyClient();
        }

        string artistID = await GetArtistID(artistName);

        FullArtist artist = await spotify.Artists.Get(artistID);

        return artist;
    }

    /*
     Get all the spotify information for all artists in a provided lineup
     */
    public async Task<List<FullArtistPerformanceInfo>> GetFullArtistList(List<FullArtistPerformanceInfo> performanceList)
    {
        if (!authenticated)
        {
            InitializeSpotifyClient();
        }

        FullArtist buffer = new FullArtist();

        for (int i = 0; i < performanceList.Count; i++)
        {
                        
            buffer = await GetFullArtist(performanceList[i].Name);
            Console.WriteLine("Get full Artist List: " + performanceList[i].Name);

            performanceList[i].ExternalUrls = buffer.ExternalUrls;
            performanceList[i].Followers = buffer.Followers;
            performanceList[i].Genres = buffer.Genres;
            performanceList[i].Href = buffer.Href;
            performanceList[i].Id = buffer.Id;
            performanceList[i].ArtistID = buffer.Id;
            performanceList[i].Images = buffer.Images;
            performanceList[i].Name = buffer.Name; //Minimize issues around accents and capitals by using the spotify name
            performanceList[i].Popularity = buffer.Popularity;
            performanceList[i].Type = buffer.Type;
            performanceList[i].Uri = buffer.Uri;
        }

        return performanceList;
    }



}
