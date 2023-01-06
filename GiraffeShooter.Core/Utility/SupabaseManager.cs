using System;

namespace GiraffeShooterClient.Utility
{
    public static class SupabaseManager
    {
        public static Supabase.Client Client { get; private set; }
    
        public static void Initialize()
        {
            var url = Environment.GetEnvironmentVariable("SUPABASE_URL");
            var key = Environment.GetEnvironmentVariable("SUPABASE_KEY");
            Client =  new Supabase.Client(url, key, new Supabase.SupabaseOptions { AutoConnectRealtime = true });
            Client.InitializeAsync();
        }
    }
    
}

