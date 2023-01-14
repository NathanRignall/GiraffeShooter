using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Postgrest.Attributes;
using Postgrest.Models;
using Supabase.Realtime;

namespace GiraffeShooterClient.Utility
{
    public static class SupabaseManager
    {
        public static Supabase.Client Client { get; private set; }
        public static Channel channel { get; private set; }
    
        public static void Initialize()
        {
            var url = Environment.GetEnvironmentVariable("SUPABASE_URL");
            var key = Environment.GetEnvironmentVariable("SUPABASE_KEY");
            Client =  new Supabase.Client(url, key, new Supabase.SupabaseOptions { AutoConnectRealtime = true });
            Setup();
        }

        private static async Task Setup()
        {
            await Client.InitializeAsync();
            
            // set up a channel
            channel = Client.Realtime.Channel("realtime", "public", "test");
            
            // capture all events
            channel.OnMessage += (sender, args) => Console.WriteLine("Message received: " + args.Response.Event);
            
            // capture errors
            channel.OnError += (sender, args) => Console.WriteLine("Error");
            
            // subscribe to the channel
            var result = await channel.Subscribe(1000);
            
            // write to console
            Console.WriteLine("Subscribed to channel" + result);
        }
    };

    public class DB
    {
        // test model
        [Table("test")]
        public class Test : BaseModel
        {
            [PrimaryKey("test_id",false)]
            public int TestId { get; set; }

            [Column("text")]
            public string Text { get; set; }

            [Column("created_at")]
            public DateTime CreatedAt { get; set; }
        }
        
        // leaderboard model
        [Table("leaderboard")]
        public class Leaderboard : BaseModel
        {
            [PrimaryKey("user_id",false)]
            public string UserId { get; set; }
            
            [Column("updated_at")]
            public DateTime UpdatedAt { get; set; }

            [Column("wins")]
            public int Wins { get; set; }
        }
    }
    
}

