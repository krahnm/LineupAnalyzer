using Postgrest.Attributes;
using Postgrest.Models;
using Swan.Formatters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LineupAnalyzer.Models
{
    [Table("festival_performances_table")]
    public class FestivalPerformancesTable : BaseModel
    {
        [PrimaryKey("performance_id", false)]
        public int PerformanceID { get; set; }

        [Column("artist_id")]
        public string ArtistID { get; set; }

        [Column("festival_id")]
        public int FestivalID { get; set; }

        [Column("date")]
        public DateTime Date { get; set; }

        [Column("popularity")]
        public int Popularity { get; set; }

    }
}
