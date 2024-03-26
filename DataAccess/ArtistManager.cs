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
using Microsoft.IdentityModel.Tokens;


namespace LineupAnalyzer.DataAccess;

internal class ArtistManager
{

    public DatabaseManager dbManager;
    public string ArtistID { get; set; }
    public string ArtistName { get; set; }
    public string MusicGenres { get; set; }
    public string Image { get; set; }
    

    // Constructor
    public ArtistManager()
    {
        dbManager = new DatabaseManager();
    }
   
    public async Task Authenticate()
    {
        //TODO: Check current status of session before creating a new session

        //DatabaseManager dbManager = new DatabaseManager();
        await dbManager.InitializeAsync();

        // Obtain a user session
        await dbManager.CreateUserSession();

    }

    public async Task TestInsertArtist()
    {
        string images = "[{\"url\": \"https://i.scdn.co/image/ab6761610000e5eb5f0109249ffd70b2d85a9467\", \"height\": 640,\"width\": 640},{\"url\": \"https://i.scdn.co/image/ab676161000051745f0109249ffd70b2d85a9467\", \"height\": 320,\"width\": 320},{\"url\": \"https://i.scdn.co/image/ab6761610000f1785f0109249ffd70b2d85a9467\",\"height\": 160,\"width\": 160}]";
        string genres = "[\"alternative dance\",\"garage rock\",\"indie rock\",\"indietronica\",\"modern alternative rock\",\"modern rock\",\"neo-synthpop\",\"oxford indie\",\"shimmer pop\"]";
        ArtistTable data = new ArtistTable
        {
            ArtistID = "6FQqZYVfTNQ1pCqfkwVFEa", //Foals
            ArtistName = "Foals",
            MusicGenres = genres,
            Image = images,

        };

        await dbManager.InsertArtist(data);
    }

    public async Task InsertArtist(FullArtistPerformanceInfo artist)
    {
        ArtistTable data = new ArtistTable
        {
            ArtistID = artist.ArtistID,
            ArtistName = artist.Name,
            MusicGenres = JsonConvert.SerializeObject(artist.Genres),
            Image = JsonConvert.SerializeObject(artist.Images),

        };

        await dbManager.InsertArtist(data);
    }

}

