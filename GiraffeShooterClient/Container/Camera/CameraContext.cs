using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using GiraffeShooterClient.Utility.Input;

namespace GiraffeShooterClient.Container.Camera
{
    public static class CameraContext
    {
        public enum State
        {
            Follow,
            Free
        }

        public static State CurrentState;
        public static Vector2 ScreenSize { get; private set; }

        public static float Zoom { get; private set; }
        public static float MaxZoom { get; private set; }
        public static float MinZoom { get; private set; }

        public static Vector2 Offset { get; private set; }
        public static Vector2 FollowTarget { get; set; }

        private static Vector2 _homePosition;
        private static Vector2 _followOffset;

        private static Vector2 _position;
        private static Vector2 _velocity;
        private static Vector2 _acceleration;

        public static void Initialize(Vector2 screenSize)
        {
            CurrentState = State.Follow;
            ScreenSize = screenSize;

            Zoom = 1f;
            MaxZoom = 4f;
            MinZoom = 0.5f;

            Offset = new Vector2(0, 0);
            FollowTarget = new Vector2(0, 0);

            _homePosition = ScreenSize / 2 / Zoom;
            _followOffset = new Vector2(0, 0);

            _position = _homePosition;
            _velocity = new Vector2(0, 0);
            _acceleration = new Vector2(0, 0);
        }

        public static void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {

            var dt = gameTime.ElapsedGameTime;
            _velocity += _acceleration * (float)dt.TotalSeconds;
            _position += _velocity * (float)dt.TotalSeconds;

            if (_acceleration == Vector2.Zero)
            {
                _velocity = _velocity * 0.9f;
            }

            switch (CurrentState)
            {
                case State.Follow:

                    var _followDiff = FollowTarget - _followOffset;
                    _followOffset += _followDiff * 0.2f;

                    if (_followDiff.Length() > 0.1f)
                    {
                        var _homeDiff = _homePosition - _position;
                        _position += _homeDiff * 0.1f;
                    }

                    Offset = _position - _followOffset;
                    break;
                case State.Free:
                    Offset = _position;
                    break;
            }
        }

        public static void HandleEvents(List<Event> events, float scaleFactor)
        {
            foreach (Event e in events)
            {
                switch (e.Type)
                {
                    case EventType.KeyPress:
                        switch (e.Key)
                        {
                            case Keys.Up:
                                System.Console.WriteLine("Up");
                                break;
                            case Keys.Down:
                                System.Console.WriteLine("Down");
                                break;
                            case Keys.Left:
                                System.Console.WriteLine("Left");
                                break;
                            case Keys.Right:
                                System.Console.WriteLine("Right");
                                break;
                        }
                        break;
                    case EventType.MouseDrag:

                        _velocity += e.MouseDelta * 15 * 1 / scaleFactor;
                        break;

                    case EventType.MouseScroll:

                        Zoom += Zoom * e.MouseScrollDelta / 10000;
                        Zoom = MathHelper.Clamp(Zoom, MinZoom, MaxZoom);

                        var newHomePosition = ScreenSize / 2 / Zoom;
                        _position = _position - _homePosition + newHomePosition;
                        _homePosition = newHomePosition;

                        break;
                }
            }
        }
    }
}