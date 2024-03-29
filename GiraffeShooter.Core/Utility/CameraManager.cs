using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;

namespace GiraffeShooterClient.Utility
{
    public static class Camera
    {
        public enum State
        {
            Follow,
            Free,
            Frozen,
        }

        public static State CurrentState;

        public static float Zoom { get; private set; }
        public static float DefaultZoom { get; private set; }
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

            var screenScale = ScreenManager.Size.Y/ 720f;

            Zoom = DefaultZoom = 2f * screenScale;
            MaxZoom = 4f * screenScale;
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
            Zoom = DefaultZoom;
            Offset = new Vector2(0, 0);
            FollowTarget = new Vector2(0, 0);

            _homePosition = ScreenManager.Size / 2 / Zoom;
            _followOffset = new Vector2(0, 0);

            _position = _homePosition;
            _velocity = new Vector2(0, 0);
            _acceleration = new Vector2(0, 0);
        }
        
        public static void Reset(float zoom)
        {
            Zoom = zoom;
            Offset = new Vector2(0, 0);
            FollowTarget = new Vector2(0, 0);

            _homePosition = ScreenManager.Size / 2 / Zoom;
            _followOffset = new Vector2(0, 0);

            _position = _homePosition;
            _velocity = new Vector2(0, 0);
            _acceleration = new Vector2(0, 0);
        }
        
        public static void Snap()
        {
            _followOffset = FollowTarget;
        }
        
        public static void ToggleState()
        {
            switch (CurrentState)
            {
                case State.Follow:
                    CurrentState = State.Free;
                    break;
                case State.Free:
                    CurrentState = State.Follow;
                    break;
            }
            
            // log state change
            Console.WriteLine("Camera state changed to " + CurrentState + ".");
        }

        public static void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {

            switch (CurrentState)
            {
                case State.Follow:
                case State.Free:
                    // update camera position
                    var dt = gameTime.ElapsedGameTime;
                    _velocity += _acceleration * (float)dt.TotalSeconds;
                    _position += _velocity * (float)dt.TotalSeconds;

                    // decelerate
                    if (_acceleration == Vector2.Zero)
                    {
                        _velocity = _velocity * 0.9f;
                    }
                    
                    // calculate offset
                    var _followDiff = FollowTarget - _followOffset;
                    _followOffset += _followDiff * 0.1f;

                    
                    if (_followDiff.Length() > 0.1f)
                    {
                        var _homeDiff = _homePosition - _position;
                        _position += _homeDiff * 0.1f;
                    }
                    
                    // cut off velocity
                    if (_velocity.Length() < 0.1f)
                    {
                        _velocity = Vector2.Zero;
                    }

                    Offset = _position - _followOffset;
                    break;
                
                case State.Frozen:
                    Offset = _position;
                    break;
            }
        }

        public static void HandleEvents(List<Event> events)
        {
            var eventsToRemove = new List<Event>();
            var eventsToAdd = new List<Event>();
            
            foreach (Event e in events)
            {
                switch (e.Type)
                {
                    case EventType.MouseDrag:
                        switch (CurrentState)
                        {
                            case State.Free:
                                _velocity += e.Delta * 15 * 1 / Zoom;
                                
                                // remove the event
                                eventsToRemove.Add(e);
                                break;
                        }
                        break;
                    
                    case EventType.TouchDrag:
                        switch (CurrentState)
                        {
                            case State.Free:
                                _velocity += e.Delta * 13 * 1 / Zoom;
                                
                                // remove the event
                                eventsToRemove.Add(e);
                                break;
                        }
                        break;

                    case EventType.MouseScroll:

                        switch (CurrentState)
                        {
                            case State.Free:
                                // calculate zoom
                                Zoom += Zoom * e.ScrollDelta / 10000;
                                Zoom = MathHelper.Clamp(Zoom, MinZoom, MaxZoom);

                                // update home position
                                var newHomePosition = ScreenManager.Size / 2 / Zoom;
                                _position = _position - _homePosition + newHomePosition;
                                _homePosition = newHomePosition;
                                
                                // remove the event
                                eventsToRemove.Add(e);
                                break;
                        }

                        break;
                    
                    case EventType.TouchPinch:
                        
                        switch (CurrentState)
                        {
                            case State.Free:
                                // calculate distances
                                var dist = Vector2.Distance(e.Position, e.Position2);
                                var distOld = Vector2.Distance(e.Position - e.Delta, e.Position2 - e.Delta2);

                                // calculate zoom
                                Zoom += Zoom * (dist - distOld)/ 500;
                                Zoom = MathHelper.Clamp(Zoom, MinZoom, MaxZoom);

                                // update home position
                                var newHomePosition = ScreenManager.Size / 2 / Zoom;
                                _position = _position - _homePosition + newHomePosition;
                                _homePosition = newHomePosition;
                                
                                // remove the event
                                eventsToRemove.Add(e);
                                break;
                        }
                        
                        break;
                }
            }
            
            // remove the events that we handled
            foreach (var e in eventsToRemove)
            {
                events.Remove(e);
            }
            
            // add the new events
            foreach (var e in eventsToAdd)
            {
                events.Add(e);
            }
        }
    }
}