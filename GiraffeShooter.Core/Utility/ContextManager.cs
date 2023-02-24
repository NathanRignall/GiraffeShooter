
using System;

namespace GiraffeShooterClient.Utility
{
    public static class ContextManager
    {
        public enum State
        {
            SplashScreen,
            Menu,
            Leaderboard,
            World,
            Win
        }

        public static State CurrentState { get; private set; }
        public static State NextState { get; private set; }
        public static bool Paused { get; set; }
        
        public static Container.SplashScreen.SplashScreenContext SplashScreenContext;
        public static Container.Menu.MenuContext MenuContext;
        public static Container.Leaderboard.LeaderboardContext LeaderboardContext;
        public static Container.World.WorldContext WorldContext;
        public static Container.Win.WinContext WinContext;

        public static void Initialize()
        {
            SplashScreenContext = new Container.SplashScreen.SplashScreenContext();
            CurrentState = State.SplashScreen;
            Paused = false;
        }

        public static void SetState(State state)
        {
            NextState = state;

            switch (NextState)
            {
                case State.SplashScreen:
                    SplashScreenContext = new Container.SplashScreen.SplashScreenContext();
                    break;
                case State.Menu:
                    MenuContext = new Container.Menu.MenuContext();
                    break;
                case State.Leaderboard:
                    LeaderboardContext = new Container.Leaderboard.LeaderboardContext();
                    break;
                case State.World:
                    Paused = false;
                    WorldContext = new Container.World.WorldContext();
                    break;
                case State.Win:
                    WinContext = new Container.Win.WinContext();
                    break;
            }
        }
        
        public static void SwitchState()
        {
            if (NextState != CurrentState)
            {
                CurrentState = NextState;
            }
        }
        
        public static void TogglePause()
        {
            Paused = !Paused;
            Console.WriteLine("Paused: " + Paused);
        }
    }
}

