using System.Collections.Generic;

using Microsoft.Xna.Framework;

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

        public static FollowContext FollowContext { get; private set; }
        public static FreeContext FreeContext { get; private set; }

        public static void Initialize()
        {
            FollowContext = new FollowContext();
            FreeContext = new FreeContext();

            CurrentState = State.Follow;
        }

        public static void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            switch (CurrentState)
            {
                case State.Follow:
                    FollowContext.Update(gameTime);
                    break;
                case State.Free:
                    FreeContext.Update(gameTime);
                    break;
            }
        }

        public static void HandleEvents(List<Event> events)
        {
            switch (CurrentState)
            {
                case State.Follow:
                    FollowContext.HandleEvents(events);
                    break;
                case State.Free:
                    FreeContext.HandleEvents(events);
                    break;
            }
        }

        public static Vector2 GetPosition()
        {
            switch (CurrentState)
            {
                case State.Follow:
                    return FollowContext.GetPosition();
                case State.Free:
                    return FreeContext.GetPosition();
            }

            return Vector2.Zero;
        }
    }

    public abstract class Camera
    {
        protected Vector2 _position;
        protected Vector2 _velocity;
        protected Vector2 _acceleration;


        public Camera()
        {
            _position = new Vector2(0, 0);
        }

        public void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
           
            var dt = gameTime.ElapsedGameTime;
            _velocity += _acceleration * (float)dt.TotalSeconds;
            _position += _velocity * (float)dt.TotalSeconds;

            if (_acceleration == Vector2.Zero)
            {
                _velocity = _velocity * 0.9f;
            }

        }

        public abstract void HandleEvents(List<Event> events);

        public Vector2 GetPosition()
        {
            return _position;
        }

    }
}