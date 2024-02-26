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
using LineupAnalyzer.Spotify;
using SpotifyAPI.Web;
using LineupAnalyzer.Helpers;

/*
 TODO: Add Try/Catch around Spotify requests to handle errors
   
 */

class Program
{

    static async Task Main()
    {

        SpotifyInfo spotifyInfo = new SpotifyInfo();
        
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
        string filePath = "C:/Users/Krahnm/Documents/LineupAnalyzer/Resources/Osheaga2023Lineup.csv";
        List<string> artistList = FileIO.ReadArtistListCSV(filePath);
        Console.WriteLine("Artist list is " + artistList.Count + " acts long.");
        List<FullArtist> lineup = await spotifyInfo.GetFullArtistList(artistList);

        for (int i = 0; i < lineup.Count; i++)
        {
            Console.WriteLine(lineup[i].Popularity);
    
        }
    }
}