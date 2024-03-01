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


namespace LineupAnalyzer.DataAccess;

internal class FestivalManager
{
    public string FestivalName { get; set; }
    public DateTime Year { get; set; }
    public List<DateTime> Dates { get; set; }
    public string City { get; set; }
    public string Province { get; set; }
    public string Country { get; set; }
    public Dictionary<string, string> PopularityStats { get; set; }
    public Dictionary<string, string> GenreStats { get; set; }

    // Constructor
    public FestivalManager()
    {

    }
    /*public Festival(string name, DateTime year, List<DateTime> dates,
        string city, string province, string country)
    {
        FestivalName = name;
        Year = year;
        Dates = dates;
        City = city;
        Province = province;
        Country = country;
        PopularityStats = new Dictionary<string, string>();
        GenreStats = new Dictionary<string, string>();
    }*/

    public async Task TestInsertFestival()
    {
        FestivalTable data = new FestivalTable
        {
            FestivalName = "Lollapalooza Chicago",
            Year = DateTime.Parse("08-03-2023"),
            Dates = new List<DateTime>
                {
                     DateTime.Parse("08-03-2023"),
                     DateTime.Parse("08-04-2023"),
                     DateTime.Parse("08-05-2023")
                }.ToJson(),
            City = "Chicago",
            Province = "Illinois",
            Country = "USA",
            PopularityStats = "{}",
            GenreStats = "{}",

        };

        DatabaseManager dbManager = new DatabaseManager();
        await dbManager.InitializeAsync();

        // Obtain a user session
        await dbManager.CreateUserSession();

        await dbManager.InsertFestival(data);
    }

}

