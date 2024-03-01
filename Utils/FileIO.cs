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
    public static List<string> ReadArtistListCSV(string filePath) 
    {
        List<string> artistList = new List<string>();
        StreamReader reader = new StreamReader(filePath);
        while (!reader.EndOfStream)
        {
            string line = reader.ReadLine();
            string[] values = line.Split(',');

            artistList.Add(values[0]);
            //listB.Add(values[1]);
        }


        return artistList;
    }
}
