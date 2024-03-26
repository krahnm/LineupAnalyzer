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

internal class FestivalManager
{

    public DatabaseManager dbManager;
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
        dbManager = new DatabaseManager();
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

    public async Task<int> TestInsertFestival()
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

        return data.FestivalID;
    }

    public async Task InsertFestival()
    {
       
        FestivalTable data = new FestivalTable
        {
            FestivalName = this.FestivalName,
            Year = this.Dates[0],
            Dates = JsonConvert.SerializeObject(this.Dates),
            City = this.City,
            Province = this.Province,
            Country = this.Country,
            PopularityStats = "{}",
            GenreStats = "{}",

        };

        if (!dbManager.ActiveSession())
        {
            Console.WriteLine("Session Not Active, Re-initialize");
            await dbManager.InitializeAsync();

            // Obtain a database user session
            await dbManager.CreateUserSession();
        }

        //DatabaseManager dbManager = new DatabaseManager();
        //await dbManager.InitializeAsync();

        // Obtain a user session
        //await dbManager.CreateUserSession();

        await dbManager.InsertFestival(data);
    }

    public async Task<int> PrintFestivals()
    {
        // Initialize connection to database
        //DatabaseManager dbManager = new DatabaseManager();
        
        if(!dbManager.ActiveSession())
        {
            Console.WriteLine("Session Not Active, Re-initialize");
            await dbManager.InitializeAsync();

            // Obtain a database user session
            await dbManager.CreateUserSession();
        }
        
        List<FestivalTable> festivalList = await dbManager.GetFestivalsList();

        foreach (var festival in festivalList)
        {
            
            try
            {
                List<DateTime> dates = JsonConvert.DeserializeObject<List<DateTime>>(festival.Dates);

                Console.WriteLine("Name: " + festival.FestivalName + " " + festival.Year.Year);
                Console.WriteLine("Location: " + festival.City + ", " + festival.Province + ", " + festival.Country);
                Console.WriteLine("Dates: " + dates[0].ToString("MMMM dd yyyy") + " - " + dates[dates.Count-1].ToString("MMMM dd yyyy\n"));

            }
            catch (Exception e)
            {
                Console.WriteLine("There is an error with festival information");
                Console.WriteLine(e.ToString());
                return -1;
            }
            

        }

        return 2;
    }

    public async Task<FestivalTable> GetLastInsertedFestival()
    {
        if (!dbManager.ActiveSession())
        {
            Console.WriteLine("Session Not Active, Re-initialize");
            await dbManager.InitializeAsync();

            // Obtain a database user session
            await dbManager.CreateUserSession();
        }

        return await dbManager.GetLastInsertedFestival();
    }

        public int PromptFestivalEntry()
    {
        Console.WriteLine("Please enter the following information:");

        Console.Write("Festival Name: ");
        string festivalName = Console.ReadLine();
        
        Console.Write("Number of Days: ");
        int numDays = Int32.Parse(Console.ReadLine());

        List<DateTime> dates = new List<DateTime>();
        
        for (int i = 0; i<numDays; i++)
        {
            Console.Write("Enter the date for day " + (i+1) + ": ");
            dates.Add(DateTime.Parse(Console.ReadLine()));
        }

        Console.Write("City: ");
        string city = Console.ReadLine();

        Console.Write("State/Province: ");
        string province = Console.ReadLine();

        Console.Write("Country: ");
        string country = Console.ReadLine();

        //Move into the above statements?
        this.FestivalName = festivalName;
        this.Dates = dates;
        this.City = city;
        this.Province = province;
        this.Country = country;

        return -1;
    }

}

