using System.Collections.Generic;

using Microsoft.Xna.Framework;

namespace GiraffeShooterClient.Utility
{

    public static class ScreenManager
    {
        public readonly static Dictionary<string, Vector2> Resolutions = new Dictionary<string, Vector2>()
        {
            { "800x600", new Vector2(800, 600) },
            { "1024x768", new Vector2(1024, 768) },
            { "1280x720", new Vector2(1280, 720) },
            { "1280x800", new Vector2(1280, 800) },
            { "1280x1024", new Vector2(1280, 1024) },
            { "1366x768", new Vector2(1366, 768) },
            { "1440x900", new Vector2(1440, 900) },
            { "1600x900", new Vector2(1600, 900) },
            { "1680x1050", new Vector2(1680, 1050) },
            { "1920x1080", new Vector2(1920, 1080) },
            { "1920x1200", new Vector2(1920, 1200) },
            { "2560x1440", new Vector2(2560, 1440) },
            { "2560x1600", new Vector2(2560, 1600) },
            { "3840x2160", new Vector2(3840, 2160) },
        };
            
        public static Vector2 Size;
        
        public static void SetResolution(string resolution)
        {
            Size = Resolutions[resolution];
        }
    }
}