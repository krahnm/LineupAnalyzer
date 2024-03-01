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
    [Table("festival_table")]
    public class FestivalTable : BaseModel
    {
        [PrimaryKey("festival_id", false)]
        public int FestivalID { get; set; }

        [Column("festival_name")]
        public string FestivalName { get; set; }

        [Column("year")]
        public DateTime Year { get; set; }

        [Column("dates")]
        public string Dates { get; set; }

        [Column("city")]
        public string City { get; set; }

        [Column("province")]
        public string Province { get; set; }

        [Column("country")]
        public string Country { get; set; }

        [Column("popularity_stats")]
        public string PopularityStats { get; set; }

        [Column("genre_stats")]
        public string GenreStats { get; set; }
    }
}
