using LineupAnalyzer.Models;
using Microsoft.Extensions.Configuration;
using SpotifyAPI.Web;
using Supabase;
using Supabase.Gotrue;
using Supabase.Realtime.PostgresChanges;
using Swan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LineupAnalyzer.DataAccess;

internal class DatabaseManager
{
    private readonly Supabase.Client client;
    //private IConfigurationRoot appConfig;
    private string email;
    private string password;

    public DatabaseManager()//string url, string key)
    {
        // Retrieve Supabase Secrets
        IConfigurationRoot appConfig = new ConfigurationBuilder()
            .AddUserSecrets<Program>()
            .Build();

        var url = appConfig["SupabaseURL"];
        var key = appConfig["SupabaseKey"];

        // Used to Authenticate User
        email = appConfig["SupabaseEmail"];
        password = appConfig["SupabasePass"];

        var options = new Supabase.SupabaseOptions
        {
            AutoConnectRealtime = true,
            AutoRefreshToken = true
        };

        client = new Supabase.Client(url, key, options);
    }

    public async Task InitializeAsync()
    {
        await client.InitializeAsync();
        Console.WriteLine("Supabase Client Initialized");
    }

    public async Task CreateUserSession()//string email, string password)
    {
        await client.Auth.SignIn(email, password);
        Console.WriteLine("Supabase user sessiong created");
    }

    public async Task InsertFestival(FestivalTable data)
    {
        await client.From<FestivalTable>().Insert(data);
        Console.WriteLine("Festival inserted");
    }

    public async Task<List<FestivalTable>> TestSelectFestival()
    {
        Postgrest.Responses.ModeledResponse<FestivalTable> result;
        try
        {
            result= await client
            .From<FestivalTable>()
            .Order("year", Postgrest.Constants.Ordering.Ascending)
            .Get();

        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            result = null;
        }
        

        return result.Models;
    }

    
}

