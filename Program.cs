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

/*
 TODO: Add Try/Catch around Spotify requests to handle errors
   
 */

class Program
{

    static async Task Main()
    {

        SpotifyInfo spotifyInfo = new SpotifyInfo();

        await spotifyInfo.PrintArtistInfoAsync();

    }
}