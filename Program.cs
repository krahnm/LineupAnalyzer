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
        FestivalManager fManager = new FestivalManager();
        await fManager.TestInsertFestival();

    }
}