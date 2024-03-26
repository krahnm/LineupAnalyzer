using LineupAnalyzer.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LineupAnalyzer.DataAccess;
using Supabase;
using Supabase.Gotrue;
using Swan;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Globalization;
using Newtonsoft.Json;
using System.Linq.Expressions;
using System.Data;
using LineupAnalyzer.Helpers;
using LineupAnalyzer.Services;


namespace LineupAnalyzer.DataAccess;

internal class PerformanceManager
{

    public DatabaseManager dbManager;
    public string ArtistID { get; set; }
    public int FestivalID { get; set; }
    public DateTime Date { get; set; }
    public int Popularity { get; set; }
    

    // Constructor
    public PerformanceManager()
    {
        dbManager = new DatabaseManager();
    }

    public async Task Authenticate()
    {
        //TODO: Check current status of session before creating a new session

        //DatabaseManager dbManager = new DatabaseManager();
        await dbManager.InitializeAsync();

        // Obtain a user session
        await dbManager.CreateUserSession();

    }

    public async Task TestInsertPerformance()
    {
        FestivalPerformancesTable data = new FestivalPerformancesTable
        {
            ArtistID = "6FQqZYVfTNQ1pCqfkwVFEa", //Foals
            FestivalID = 1,
            Date = DateTime.Parse("08-06-2023"),
            Popularity = 62,

        };

        await dbManager.InsertPerformance(data);
    }

    public async Task InsertPerformance(FullArtistPerformanceInfo performance)
    {
        FestivalPerformancesTable data = new FestivalPerformancesTable
        {
            ArtistID = performance.ArtistID,
            FestivalID = performance.FestivalID,
            Date = performance.Date,
            Popularity = performance.Popularity,

        };

        await dbManager.InsertPerformance(data);
    }

}

