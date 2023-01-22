using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Microsoft.Xna.Framework;

namespace GiraffeShooterClient.Android
{
    [Activity(
        Label = "Giraffe Royale",
        MainLauncher = true,
        AlwaysRetainTaskState = true,
        LaunchMode = LaunchMode.SingleInstance,
        ScreenOrientation = ScreenOrientation.SensorLandscape,
        ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.Keyboard | ConfigChanges.KeyboardHidden,
        Immersive = true,
        MaxAspectRatio = 4.0f
    )]
    public class Activity1 : AndroidGameActivity
    {
        private GiraffeShooter _game;
        private View _view;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            
            Window.AddFlags(WindowManagerFlags.Fullscreen);
            Window.SetDecorFitsSystemWindows(false);

            _game = new GiraffeShooter();
            _view = _game.Services.GetService(typeof(View)) as View;

            SetContentView(_view);
            _game.Run();
        }
    }
}
