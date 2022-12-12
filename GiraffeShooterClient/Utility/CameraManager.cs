using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GiraffeShooterClient.Utility
{
    public static class Camera
    {
        public enum State
        {
            Follow,
            Free
        }

        public static State CurrentState;

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

        public static void Initialize()
        {
            CurrentState = State.Follow;

            Zoom = 3f;
            MaxZoom = 4f;
            MinZoom = 0.5f;

            Offset = new Vector2(0, 0);
            FollowTarget = new Vector2(0, 0);

            _homePosition = ScreenManager.Size / 2 / Zoom;
            _followOffset = new Vector2(0, 0);

            _position = _homePosition;
            _velocity = new Vector2(0, 0);
            _acceleration = new Vector2(0, 0);
        }

        public static void Reset()
        {
            Zoom = 3f;
            Offset = new Vector2(0, 0);
            FollowTarget = new Vector2(0, 0);

            _homePosition = ScreenManager.Size / 2 / Zoom;
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
                    _followOffset += _followDiff * 0.05f;

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

        public static void HandleEvents(List<Event> events)
        {
            foreach (Event e in events)
            {
                switch (e.Type)
                {
                    case EventType.MouseDrag:

                        _velocity += e.MouseDelta * 15 * 1 / Zoom;
                        break;

                    case EventType.MouseScroll:

                        Zoom += Zoom * e.MouseScrollDelta / 10000;
                        Zoom = MathHelper.Clamp(Zoom, MinZoom, MaxZoom);

                        var newHomePosition = ScreenManager.Size / 2 / Zoom;
                        _position = _position - _homePosition + newHomePosition;
                        _homePosition = newHomePosition;

                        break;
                }
            }
        }
    }
}