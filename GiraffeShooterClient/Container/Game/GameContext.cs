
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
        public static State NextState;
        public static SplashScreen.SplashScreenContext SplashScreenContext;
        public static World.WorldContext WorldContext;

        public static void Initialize()
        {
            SplashScreenContext = new SplashScreen.SplashScreenContext();
            CurrentState = State.SplashScreen;
        }

        public static void SetState(State state)
        {
            NextState = state;

            switch (state)
            {
                case State.SplashScreen:
                    SplashScreenContext = new SplashScreen.SplashScreenContext();
                    break;
                case State.World:
                    WorldContext = new World.WorldContext();
                    break;
            }
        }
    }
}