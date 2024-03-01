using System;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

using Swan;
using Swan.Logging;

using SpotifyAPI;
using LineupAnalyzer.Models;
using SpotifyAPI.Web;
using LineupAnalyzer.Helpers;
using Postgrest.Attributes;
using Postgrest.Models;
using System.Reflection;
using static System.Runtime.InteropServices.JavaScript.JSType;
using LineupAnalyzer.Services;
using LineupAnalyzer.DataAccess;

/*
 TODO: Add Try/Catch around Spotify requests to handle errors
   
 */

class Program
{
   
    static async Task Main()
    {

        //Console.WriteLine("Welcome to the Lineup Analyzer");
        //SpotifyInfo spotifyInfo = new SpotifyInfo();
        //Console.WriteLine("Created SpotifyInfo");

        // FestivalInfo festival = new FestivalInfo();
        //Console.WriteLine("Created FestivalInfo");
        //festival.TestFestivalInsert();
        //Console.WriteLine("Inserted Test Festival");

        FestivalTable data = new FestivalTable
        {
            FestivalName = "Lollapalooza Chicago",
            Year = DateTime.Parse("08-03-2023"),
            Dates = "{\"Dates\": [08-03-2023,08-04-2023,08-05-2023]}",
            City = "Chicago",
            Province = "Illinois",
            Country = "USA",
            PopularityStats = "{}",
            GenreStats = "{}",

        };

        // Retrieve Supabase Secrets
        IConfigurationRoot appConfig = new ConfigurationBuilder()
            .AddUserSecrets<Program>()
            .Build();

        // Used to connect to Supabase project
        var url = appConfig["SupabaseURL"];
        var key = appConfig["SupabaseKey"];

        var options = new Supabase.SupabaseOptions
        {
            AutoConnectRealtime = true,
            AutoRefreshToken = true
        };

        // Used to Authenticate User
        var email = appConfig["SupabaseEmail"];
        var password = appConfig["SupabasePass"];

        // Initialize the Supabase client
        var client = new Supabase.Client(url, key, options);
        await client.InitializeAsync();
        

        Console.WriteLine("Supabase Client Initialized");

        if (client == null)
        {
            Console.WriteLine("supabase is null here");
        }
        // Obtain a user session
        await client.Auth.SignIn(email, password);

        await client.From<FestivalTable>().Insert(data);


        /*
        FullArtist artist = await spotifyInfo.GetFullArtist("Foals");

        Console.WriteLine(artist.Name);
        Console.WriteLine(artist.Followers);
        Console.WriteLine(artist.Followers.Total);
        Console.WriteLine(artist.Popularity);

        for(int i = 0; i < artist.Genres.Count; i++)
        {
            Console.WriteLine(artist.Genres[i]);
        }*/

        //TODO: Make a lineup class?
        /* string filePath = "C:/Users/Krahnm/Documents/LineupAnalyzer/Resources/Osheaga2023Lineup.csv";
         List<string> artistList = FileIO.ReadArtistListCSV(filePath);
         Console.WriteLine("Artist list is " + artistList.Count + " acts long.");
         List<FullArtist> lineup = await spotifyInfo.GetFullArtistList(artistList);

         for (int i = 0; i < lineup.Count; i++)
         {
             Console.WriteLine(lineup[i].Popularity);

         }*/

    }
}