using Postgrest.Attributes;
using Postgrest.Models;
using SpotifyAPI.Web;
using Swan.Formatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LineupAnalyzer.Models
{
    
    public class FullArtistPerformanceInfo : BaseModel
    {
        
        public int PerformanceID { get; set; }

        public string ArtistID { get; set; }

        public int FestivalID { get; set; }

        public DateTime Date { get; set; }

        public Dictionary<string, string> ExternalUrls { get; set; }

        public Followers Followers { get; set; }

        public List<string> Genres { get; set; }

        public string Href { get; set; }

        public string Id { get; set; }

        public List<Image> Images { get; set; }

        public string Name { get; set; }

        public int Popularity { get; set; }

        public string Type { get; set; }

        public string Uri { get; set; }

    }
}
