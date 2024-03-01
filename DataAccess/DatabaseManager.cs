using LineupAnalyzer.Models;
using Microsoft.Extensions.Configuration;
using SpotifyAPI.Web;
using Supabase;
using Supabase.Gotrue;
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
    /*private static string url;
    private static string key;
    private static string email;
    private static string password;
    private static SupabaseOptions options;
    private static Session session;*/
    //public static Supabase.Client  client;


    // public class DatabaseManager()
    // {
    /*// Retrieve Supabase Secrets
    IConfigurationRoot appConfig = new ConfigurationBuilder()
        .AddUserSecrets<Program>()
        .Build();

    // Used to connect to Supabase project
    url = appConfig["SupabaseURL"];
    key = appConfig["SupabaseKey"];

    options = new Supabase.SupabaseOptions
    {
        AutoConnectRealtime = true,
        AutoRefreshToken = true
    };

    // Used to Authenticate User
    email = appConfig["SupabaseEmail"];
    password = appConfig["SupabasePass"];
    Console.WriteLine("Completed SupabaseDatabase constructor");*/

    //}

    /*public async Task InitializeSupabase()
    /*public async Task InitializeSupabase()
    {
        // Retrieve Supabase Secrets
        IConfigurationRoot appConfig = new ConfigurationBuilder()
            .AddUserSecrets<Program>()
            .Build();

        // Used to connect to Supabase project
        var url = appConfig["SupabaseURL"];
        var key = appConfig["SupabaseKey"];

        var options = new Supabase.SupabaseOptions
        {
            AutoConnectRealtime = true,
            AutoRefreshToken = true
        };

        // Used to Authenticate User
        var email = appConfig["SupabaseEmail"];
        var password = appConfig["SupabasePass"];

        // Initialize the Supabase client
        client = new Supabase.Client(url, key, options);
        await client.InitializeAsync();


        Console.WriteLine("Supabase Client Initialized");

        if (client == null)
        {
            Console.WriteLine("supabase is null here");
        }
        // Obtain a user session
        await client.Auth.SignIn(email, password);

        if(client == null)
        {
            Console.WriteLine("Client is NULL after initializing");
        }
    }

    public async Task InsertTable(FestivalTable data)
    {
        Console.WriteLine($"Insert table {data.TableName}");
        if (client == null)
        {
            Console.WriteLine("Client is NULL before inserting");
        }
        await client.From<FestivalTable>().Insert(data);
        Console.WriteLine("Everything done correctly");
    }*/
    /*public async Task InitializeSupabaseConnectionAsync()
    {
        try
        {
            await InitializeSupabaseClientAsync();
        }
        catch (Exception e) 
        {
            Console.WriteLine(e.Message);
        }

        try
        {
            await InitializeSupabaseSessionAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }

    }

    public async Task<Supabase.Client> InitializeSupabaseClientAsync()
    {
        // Initialize the Supabase client
        try
        {
            client = new Supabase.Client(url, key, options);
            await client.InitializeAsync();
        } catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
        
        Console.WriteLine("Supabase Client Initialized");
        return client;
    }

    public async Task<Session> InitializeSupabaseSessionAsync()
    {
        if(client == null)
        {
            Console.WriteLine("supabase is null here");
        }
        // Obtain a user session
        session = await client.Auth.SignIn(email, password);

        return session;
    }

    public async Task<int> SupabaseFestivalTableInsert(FestivalTable data)
    {
        // Initialize Supabase Client if not initialized already
        if (client == null)
        {
            Console.WriteLine("supabase Is Null");
            await InitializeSupabaseClientAsync();

            Console.WriteLine("Initialized Supabase Session");
        }
        if (session == null)
        {
            Console.WriteLine("Session Is Null");
            await InitializeSupabaseSessionAsync();

            Console.WriteLine("Initialized Supabase Session");
        }

        try
        {
            Console.WriteLine("Signed in!");

            await client.From<FestivalTable>().Insert(data);

        } catch (Exception e)
        {
            Console.WriteLine(e.Message);
            return -1;
        }

        return 1;

    }*/
}

