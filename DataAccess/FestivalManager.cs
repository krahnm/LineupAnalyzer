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
        List<DateTime> dates = new List<DateTime>
        {
            DateTime.Parse("08-03-2023"),
            DateTime.Parse("08-04-2023"),
            DateTime.Parse("08-05-2023")
        };

        FestivalTable data = new FestivalTable
        {
            FestivalName = "Lollapalooza Chicago",
            Year = DateTime.Parse("08-03-2023"),
            Dates = JsonConvert.SerializeObject(dates),
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

    public async Task TestSelectFestival()
    {
        DatabaseManager dbManager = new DatabaseManager();
        await dbManager.InitializeAsync();

        // Obtain a user session
        await dbManager.CreateUserSession();

        List<FestivalTable> festivalList = await dbManager.TestSelectFestival();

        foreach (var festival in festivalList)
        {
            Console.WriteLine("Name: " + festival.FestivalName + " " + festival.Year.Year);
            Console.WriteLine("Location: " + festival.City + ", " + festival.Province + ", " + festival.Country);
            Console.WriteLine("Dates:");
            try
            {
                List<DateTime> dates = JsonConvert.DeserializeObject<List<DateTime>>(festival.Dates);
                Console.WriteLine("\t" + dates[0].ToString("MMMM dd yyyy"));
                Console.WriteLine("\t" + dates[1].ToString("MMMM dd yyyy"));
                Console.WriteLine("\t" + dates[2].ToString("MMMM dd yyyy"));
                Console.WriteLine(dates.Count() + " days of music \n");
            }
            catch (Exception e)
            {
                Console.WriteLine("There are not dates available");
                Console.WriteLine(e.ToString());
            }
            

        }

        Console.WriteLine("Festivals selected");
    }

}

