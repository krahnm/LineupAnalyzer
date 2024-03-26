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
using Microsoft.VisualBasic.FileIO;
using System.Collections.Generic;

/*
 TODO: Add Try/Catch around Spotify requests to handle errors
   
 */

class Program
{
   
    static async Task Main()
    {
        int menu = 0;
        while (menu >= 0)
        {
            switch (menu)
            {
                case 0:
                    menu = MainMenu();
                    break;
                case 1:
                    menu = ViewArtistMenu();
                    break;
                case 2:
                    menu = await ViewFestivalsMenu();
                    break;
                case 3:
                    menu = await AddFestivalMenu();
                    break;


            }
        }

        //FestivalManager fManager = new FestivalManager();
        //await fManager.TestInsertFestival();

        //await fManager.TestSelectFestival();


    }

    private static int MainMenu()
    {

        Console.WriteLine("Welcome to the Festival Lineup Analyzer!\n");
        Console.WriteLine("Select an option from the following list: ");
        Console.WriteLine("\t1) - View Artists");
        Console.WriteLine("\t2) - View Festival");
        Console.WriteLine("\t3) - Add Festival\n");
        Console.Write("Action: ");
        
        switch (Console.ReadLine())
        {
            case "1":
                Console.Clear();
                Console.WriteLine("You want to view artists!\n");
                return 1;
            case "2":
                Console.Clear();
                Console.WriteLine("You want to view festivals!\n");
                return 2;
            case "3":
                Console.Clear();
                Console.WriteLine("You want to add a festival!\n");
                return 3;
            default:
                Console.Clear();
                Console.WriteLine("Incorrect Response! Select an option from the list!\n");
                return 0;
        }
    }

    private static int ViewArtistMenu()
    {

        /* Console.WriteLine("Welcome to the Festival Lineup Analyzer!\n");
         Console.WriteLine("Select an option from the following list: ");
         Console.WriteLine("\t1) - View Artists");
         Console.WriteLine("\t2) - View Festival");
         Console.WriteLine("\t3) - Add Festival\n");
         Console.Write("Action: ");

         switch (Console.ReadLine())
         {
             case "1":
                 Console.Clear();
                 Console.WriteLine("You want to view artists!\n");
                 return 1;
             case "2":
                 Console.Clear();
                 Console.WriteLine("You want to view festivals!\n");
                 return 2;
             case "3":
                 Console.Clear();
                 Console.WriteLine("You want to add a festival!\n");
                 return 3;
             default:
                 Console.Clear();
                 Console.WriteLine("Incorrect Response! Select an option from the list!\n");
                 return 0;
         }*/
        return -1;
    }

    private static async Task<int> ViewFestivalsMenu()
    {
        Console.Clear();

        FestivalManager fManager = new FestivalManager();
        await fManager.PrintFestivals();

        Console.ReadLine();

        return 2;

    }

    private static async Task<int> AddFestivalMenu()
    {
        FestivalManager fManager = new FestivalManager();
        PerformanceManager pManager = new PerformanceManager();
        ArtistManager aManager = new ArtistManager();

        //Get high level Festival info
        fManager.PromptFestivalEntry();

        //Get the lineup artist details from a csv file
        Console.WriteLine("\nPlease provide the name of the .csv file containing the artists on the lineup and date of their performance. This file will be placed in a top level folder named 'Resources'");
        Console.Write("Filename: ");
        string csvFile = Console.ReadLine();

        //ADD CHECK TO SEE IF THERE IS A DUPLICATE ARTIST ENTRY?
        //IF NO DUPLIDCATE, INSERT FESTIVAL AND OBTAIN FESTIVAL ID

        //INSERT FESTIVAL        
        await fManager.InsertFestival();

        //GET FESTIVAL ID
        FestivalTable insertedTable = await fManager.GetLastInsertedFestival();
        int festivalID = insertedTable.FestivalID;
        Console.WriteLine("The Retrieved ID is: " + insertedTable.FestivalID);

       
        //PROMPT FOR A CSV OF ARTISTS PLAYING A FESTIVAL
        SpotifyInfo spotifyInfo = new SpotifyInfo();
        string filePath = "C:/Users/Krahnm/Documents/LineupAnalyzer/Resources/" + csvFile;

        //READ THE CSV AND ADD ARTSIT TO PERFORMANCE LIST
        List<FullArtistPerformanceInfo> performanceList = FileIO.ReadArtistListCSV(filePath);
        Console.WriteLine("Artist list is " + performanceList.Count + " acts long.");
        
        //COLLECT SPOTIFY INFO FOR EACH ARTIST AND ADD TO THE PERFORMANCE INFO
        performanceList = await spotifyInfo.GetFullArtistList(performanceList);

        //ENSURE AUTHENTICATED
        await aManager.Authenticate();
        await pManager.Authenticate();

        for (int i = 0; i < performanceList.Count; i++)
        {
            //ASSIGN FESTIVAL ID TO PERFORMANCE
            performanceList[i].FestivalID = festivalID;

            //INSERT ARTISTS INTO DATABASE TABLE
            await aManager.InsertArtist(performanceList[i]);
            Console.WriteLine("Inserted Artist: " + performanceList[i].Name + " | " + performanceList[i].Popularity);

            //INSERT PERFORMANCES INTO DATABASE TABLE
            await pManager.InsertPerformance(performanceList[i]);
            Console.WriteLine("Inserted Performance: " + performanceList[i].Name + " | " + performanceList[i].Date);

        }


        //------------ After I have the data (eventually encorporate into the fest upload flow) --------
        //Update artist profiles and festival statistics


        Console.ReadLine();

        return 3;
    }


}