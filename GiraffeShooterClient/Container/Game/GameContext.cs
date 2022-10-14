
namespace GiraffeShooterClient.Container.Game
{
    public static class GameContext
    {
        public enum State
        {
            SplashScreen,
            World
        }

        public static State CurrentState;

        public static SplashScreen.SplashScreenContext SplashScreenContext;

        public static void Initialize()
        {
            SplashScreenContext = new SplashScreen.SplashScreenContext();
            CurrentState = State.SplashScreen;
        }
    }
}