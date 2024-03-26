using LineupAnalyzer.Models;
using SpotifyAPI.Web;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineupAnalyzer.Helpers;

internal class FileIO
{
    /*
     Read a CSV containing 2 columns, Artist and date performing
     */
    public static List<FullArtistPerformanceInfo> ReadArtistListCSV(string filePath) 
    {
        List<FullArtistPerformanceInfo> artistPerformanceList = new List<FullArtistPerformanceInfo>();
        StreamReader reader = new StreamReader(filePath);
        string line;
        string[] values;

        while (!reader.EndOfStream)
        {
            FullArtistPerformanceInfo performance = new FullArtistPerformanceInfo();

            line = reader.ReadLine();
            values = line.Split(',');

            performance.Name = values[0];
            performance.Date = DateTime.Parse(values[1]);

            Console.WriteLine("Adding: " + performance.Name + " on " + performance.Date);
            artistPerformanceList.Add(performance);
        }

        return artistPerformanceList;
    }
}
