using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Channels;
using System.Threading.Tasks;
using GiraffeShooterClient.Entity;
using Newtonsoft.Json;
using Postgrest.Attributes;
using Postgrest.Models;
using Supabase.Realtime;
using Supabase.Realtime.Models;

namespace GiraffeShooterClient.Utility
{
    public static class SupabaseManager
    {
        public static Supabase.Client Client { get; private set; }
        public static RealtimeChannel Channel { get; private set; }
        public static RealtimeBroadcast<DB.EntityBroadcast> EntityBroadcast { get; private set; }

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
            
            // write to console when connected
            Console.WriteLine("Connected to Supabase");
            
            // set up a channel
            Channel = Client.Realtime.Channel("player");
            
            Channel.OnMessage += (sender, args) =>
            {
                Console.WriteLine(args);
            };

            // set up broadcast
            EntityBroadcast = Channel.Register<DB.EntityBroadcast>(false, true);

            // test broadcast
            EntityBroadcast.OnBroadcast += (sender, args) =>
            {
                var state = EntityBroadcast.Current();
                
                // // if in game, update mouse position
                // if (ContextManager.WorldContext != null)
                //     ContextManager.WorldContext.HandleBroadcastedEntity(state.Payload);
            };

            // sub to channel
            await Channel.Subscribe(1000);
            
            Console.WriteLine("Connected to Supabase Realtime");
        }
        
        public static async Task BroadcastEntities(List<DB.Entity> status)
        {
            var data = new DB.EntityBroadcast() { Event = "entity", Payload = status };
            await EntityBroadcast.Send("entity", data);
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
            [PrimaryKey("id",false)]
            public string Id { get; set; }
            
            [Column("updated_at")]
            public DateTime UpdatedAt { get; set; }

            [Column("wins")]
            public int Wins { get; set; }
        }
        
        // leaderboard model
        [Table("main_leaderboard")]
        public class MainLeaderboard : BaseModel
        {
            [Column("wins")]
            public int Wins { get; set; }
            
            [Column("username")]
            public string Username { get; set; }
        }
        
        public class Vector3
        {
            [JsonProperty("x")]
            public float X { get; set; }

            [JsonProperty("y")]
            public float Y { get; set; }

            [JsonProperty("z")]
            public float Z { get; set; }
            
            public Vector3(float x, float y, float z)
            {
                X = x;
                Y = y;
                Z = z;
            }
        }
        
        public class Entity : BaseBroadcast
        {
            [JsonProperty("id")]
            public string Id { get; set; }
            
            [JsonProperty("type")]
            public string Type { get; set; }
            
            [JsonProperty("position")]
            public Vector3 Position { get; set; }
            
            [JsonProperty("velocity")]
            public Vector3 Velocity { get; set; }
        }
        
        public class EntityBroadcast : BaseBroadcast
        {
            [JsonProperty("payload")]
            public List<Entity> Payload { get; set; }
        }
    }
}

