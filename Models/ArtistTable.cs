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
    [Table("artist_table")]
    public class ArtistTable : BaseModel
    {
        [PrimaryKey("artist_id", true)]
        public string ArtistID { get; set; }

        [Column("artist_name")]
        public string ArtistName { get; set; }

        [Column("music_genres")]
        public string MusicGenres { get; set; }

        [Column("image")]
        public string Image { get; set; }

    }
}
