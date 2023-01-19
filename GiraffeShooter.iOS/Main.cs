using System;
using Foundation;
using UIKit;

namespace GiraffeShooterClient.iOS
{
    [Register("AppDelegate")]
    internal class Program : UIApplicationDelegate
    {
        private static GiraffeShooter game;

        internal static void RunGame()
        {
            game = new GiraffeShooter();
            game.Run();
        }

        static void Main(string[] args)
        {
            UIApplication.Main(args, null, typeof(Program));
        }

        public override void FinishedLaunching(UIApplication app)
        {
            RunGame();
        }
    }
}