using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GiraffeShooterClient.Utility
{
    public static class VirtualManager
    {

        private static Rectangle _leftControlStick;
        private static Rectangle _leftControlBall;
        private static Vector2 _leftControlOffset;
        
        private static Rectangle _rightControlStick;
        private static Rectangle _rightControlBall;
        private static Vector2 _rightControlOffset;
        
        private static Rectangle _leftShootButton;
        private static Rectangle _rightShootButton;

        public static void Initialize()
        {
            // screen size
            var screenScale = ScreenManager.Size.X/ 1280;

            // control stick size
            var controlStickSize = new Vector2(256, 256) * screenScale;
            var controlButtonSize = new Vector2(64, 64) * screenScale;
            
            
            // left control stick position
            var leftControlStickPosition = new Vector2( controlButtonSize.X * 1f, ScreenManager.Size.Y - controlStickSize.Y * 1f - controlButtonSize.Y * 1f);

            // create the left control stick rectangle
            _leftControlStick = new Rectangle((int)leftControlStickPosition.X, (int)leftControlStickPosition.Y, (int)controlStickSize.X, (int)controlStickSize.Y);
            
            // create the left control ball rectangle
            _leftControlBall = new Rectangle((int)leftControlStickPosition.X + (int)controlStickSize.X / 2 - (int)controlButtonSize.X / 2, (int)leftControlStickPosition.Y + (int)controlStickSize.Y / 2 - (int)controlButtonSize.Y / 2, (int)controlButtonSize.X, (int)controlButtonSize.Y);
            
            
            // right control stick position
            var rightControlStickPosition = new Vector2(ScreenManager.Size.X - controlStickSize.X * 1f - controlButtonSize.X * 1f, ScreenManager.Size.Y - controlStickSize.Y * 1f - controlButtonSize.Y * 1f);

            // create the right control stick rectangle
            _rightControlStick = new Rectangle((int)rightControlStickPosition.X, (int)rightControlStickPosition.Y, (int)controlStickSize.X, (int)controlStickSize.Y);
            
            // create the right control ball rectangle
            _rightControlBall = new Rectangle((int)rightControlStickPosition.X + (int)controlStickSize.X / 2 - (int)controlButtonSize.X / 2, (int)rightControlStickPosition.Y + (int)controlStickSize.Y / 2 - (int)controlButtonSize.Y / 2, (int)controlButtonSize.X, (int)controlButtonSize.Y);

            
            // shoot button size
            var shootButtonSize = new Vector2(64, 64) * screenScale;
            
            
            // shoot button position screen on left
            var leftShootButtonPosition = new Vector2(shootButtonSize.X * 5f, ScreenManager.Size.Y  - shootButtonSize.Y * 2f);
            
            // create the left shoot button rectangle
            _leftShootButton = new Rectangle((int)leftShootButtonPosition.X, (int)leftShootButtonPosition.Y, (int)shootButtonSize.X, (int)shootButtonSize.Y);
            
            
            // shoot button position screen on right
            var rightShootButtonPosition = new Vector2(ScreenManager.Size.X - shootButtonSize.X * 6f, ScreenManager.Size.Y - shootButtonSize.Y * 2f);
            
            // create the right shoot button rectangle
            _rightShootButton = new Rectangle((int)rightShootButtonPosition.X, (int)rightShootButtonPosition.Y, (int)shootButtonSize.X, (int)shootButtonSize.Y); 

        }

        public static void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
        }

        public static void HandleEvents(List<Event> events)
        {
            var eventsToRemove = new List<Event>();
            var eventsToAdd = new List<Event>();
            
            foreach (Event e in events)
            {
                switch (e.Type)
                {
                    case EventType.MouseClick:
                    case EventType.TouchPress:
                    case EventType.TouchHold:

                        // if (_arrowUp.Contains(e.Position))
                        // {
                        //     eventsToAdd.Add(new Event(Keys.Up, EventType.KeyHold));
                        //     eventsToRemove.Add(e);
                        // }

                        // check if the press on the left control stick
                        if (_leftControlStick.Contains(e.Position))
                        {
                            // get the new offset
                            _leftControlOffset = e.Position - new Vector2(_leftControlBall.X + _leftControlBall.Width / 2, _leftControlBall.Y + _leftControlBall.Height / 2);
                            
                            // check if the new offset is outside the control stick
                            if (!_leftControlStick.Contains(new Vector2(_leftControlBall.X, _leftControlBall.Y) + _leftControlOffset + new Vector2(_leftControlBall.Width / 2)))
                            {
                                // get the new offset
                                _leftControlOffset = Vector2.Normalize(_leftControlOffset) * (_leftControlStick.Width / 2 - _leftControlStick.Width / 2);
                            }
                            
                            // remove the event
                            eventsToRemove.Add(e);
                        }
                        
                        // check if the press on the right control stick
                        if (_rightControlStick.Contains(e.Position))
                        {
                            // get the new offset
                            _rightControlOffset = e.Position - new Vector2(_rightControlBall.X + _rightControlBall.Width / 2, _rightControlBall.Y + _rightControlBall.Height / 2);
                            
                            // check if the new offset is outside the control stick
                            if (!_rightControlStick.Contains(new Vector2(_rightControlBall.X, _rightControlBall.Y) + _rightControlOffset + new Vector2(_rightControlBall.Width / 2)))
                            {
                                // get the new offset
                                _rightControlOffset = Vector2.Normalize(_rightControlOffset) * (_rightControlStick.Width / 2 - _rightControlStick.Width / 2);
                            }
                            
                            // remove the event
                            eventsToRemove.Add(e);
                        }

                        break;

                    case EventType.MouseDrag:
                    case EventType.TouchDrag:
                        
                        // if (_arrowUp.Contains(e.Position))
                        // {
                        //     eventsToAdd.Add(new Event(Keys.Up, EventType.KeyHold));
                        //     eventsToRemove.Add(e);
                        // }

                        // get the start position of the drag
                        var startPosition = e.Position - e.Delta;
                        
                        // check if the drag started on the left control stick
                        if (_leftControlStick.Contains(startPosition))
                        {
                            // get the new offset
                            _leftControlOffset = e.Position - new Vector2(_leftControlBall.X + _leftControlBall.Width / 2, _leftControlBall.Y + _leftControlBall.Height / 2);
                            
                            // check if the new offset is outside the control stick
                            if (!_leftControlStick.Contains(new Vector2(_leftControlBall.X, _leftControlBall.Y) + _leftControlOffset + new Vector2(_leftControlBall.Width / 2)))
                            {
                                // get the new offset
                                _leftControlOffset = Vector2.Normalize(_leftControlOffset) * (_leftControlStick.Width / 2 - _leftControlStick.Width / 2);
                            }
                            
                            // remove the event
                            eventsToRemove.Add(e);
                        }
                        
                        // check if the drag started on the right control stick
                        if (_rightControlStick.Contains(startPosition))
                        {
                            // get the new offset
                            _rightControlOffset = e.Position - new Vector2(_rightControlBall.X + _rightControlBall.Width / 2, _rightControlBall.Y + _rightControlBall.Height / 2);
                            
                            // check if the new offset is outside the control stick
                            if (!_rightControlStick.Contains(new Vector2(_rightControlBall.X, _rightControlBall.Y) + _rightControlOffset + new Vector2(_rightControlBall.Width / 2)))
                            {
                                // get the new offset
                                _rightControlOffset = Vector2.Normalize(_rightControlOffset) * (_rightControlStick.Width / 2 - _rightControlStick.Width / 2);
                            }
                            
                            // remove the event
                            eventsToRemove.Add(e);
                        }
                        
                        break;

                    case EventType.TouchPinch:
                        
                        // if (_arrowUp.Contains(e.Position) || _arrowUp.Contains(e.Position2))
                        // {
                        //     eventsToAdd.Add(new Event(Keys.Up, EventType.KeyHold));
                        //     eventsToRemove.Add(e);
                        // }

                        // get the start position of the drag
                        var startPosition1 = e.Position - e.Delta;
                        var startPosition2 = e.Position2 - e.Delta2;
                        
                        // check if the drag started on the left control stick (pos1)
                        if (_leftControlStick.Contains(startPosition1))
                        {
                            // get the new offset
                            _leftControlOffset = e.Position - new Vector2(_leftControlBall.X + _leftControlBall.Width / 2, _leftControlBall.Y + _leftControlBall.Height / 2);
                            
                            // check if the new offset is outside the control stick
                            if (!_leftControlStick.Contains(new Vector2(_leftControlBall.X, _leftControlBall.Y) + _leftControlOffset + new Vector2(_leftControlBall.Width / 2)))
                            {
                                // get the new offset
                                _leftControlOffset = Vector2.Normalize(_leftControlOffset) * (_leftControlStick.Width / 2 - _leftControlStick.Width / 2);
                            }
                            
                            // remove the event
                            eventsToRemove.Add(e);
                        }
                        
                        // check if the drag started on the left control stick (pos2)
                        if (_leftControlStick.Contains(startPosition2))
                        {
                            // get the new offset
                            _leftControlOffset = e.Position2 - new Vector2(_leftControlBall.X + _leftControlBall.Width / 2, _leftControlBall.Y + _leftControlBall.Height / 2);
                            
                            // check if the new offset is outside the control stick
                            if (!_leftControlStick.Contains(new Vector2(_leftControlBall.X, _leftControlBall.Y) + _leftControlOffset + new Vector2(_leftControlBall.Width / 2)))
                            {
                                // get the new offset
                                _leftControlOffset = Vector2.Normalize(_leftControlOffset) * (_leftControlStick.Width / 2 - _leftControlBall.Width / 2);
                            }
                            
                            // remove the event
                            eventsToRemove.Add(e);
                        }
                        
                        // check if the drag started on the right control stick (pos1)
                        if (_rightControlStick.Contains(startPosition1))
                        {
                            // get the new offset
                            _rightControlOffset = e.Position - new Vector2(_rightControlBall.X + _rightControlBall.Width / 2, _rightControlBall.Y + _rightControlBall.Height / 2);
                            
                            // check if the new offset is outside the control stick
                            if (!_rightControlStick.Contains(new Vector2(_rightControlBall.X, _rightControlBall.Y) + _rightControlOffset + new Vector2(_rightControlBall.Width / 2)))
                            {
                                // get the new offset
                                _rightControlOffset = Vector2.Normalize(_rightControlOffset) * (_rightControlStick.Width / 2 - _rightControlStick.Width / 2);
                            }
                            
                            // remove the event
                            eventsToRemove.Add(e);
                        }
                        
                        // check if the drag started on the right control stick (pos2)
                        if (_rightControlStick.Contains(startPosition2))
                        {
                            // get the new offset
                            _rightControlOffset = e.Position2 - new Vector2(_rightControlBall.X + _rightControlBall.Width / 2, _rightControlBall.Y + _rightControlBall.Height / 2);
                            
                            // check if the new offset is outside the control stick
                            if (!_rightControlStick.Contains(new Vector2(_rightControlBall.X, _rightControlBall.Y) + _rightControlOffset + new Vector2(_rightControlBall.Width / 2)))
                            {
                                // get the new offset
                                _rightControlOffset = Vector2.Normalize(_rightControlOffset) * (_rightControlStick.Width / 2 - _rightControlStick.Width / 2);
                            }
                            
                            // remove the event
                            eventsToRemove.Add(e);
                        }

                        break;
                }
            }

            // if the left control offset is not zero, add the event
            if (_leftControlOffset != Vector2.Zero)
                eventsToAdd.Add(new Event(_leftControlOffset / 256, EventType.StickLeftMove));

            // if the right control offset is not zero, add the event
            if (_rightControlOffset != Vector2.Zero)
                eventsToAdd.Add(new Event(_rightControlOffset / 256, EventType.StickRightMove));

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

        public static void Draw(Microsoft.Xna.Framework.GameTime gameTime, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            // source rectangle for the control sticks
            var controlStickSource = new Rectangle(0, 0, 128, 128);
            var controlBallSource = new Rectangle(0, 0, 32, 32);
            
            
            // new left control ball position with offset
            var newLeftControlBallPosition = new Vector2(_leftControlBall.X, _leftControlBall.Y) + _leftControlOffset;
            _leftControlOffset *= 0.5f;
            
            // cut off to prevent wasted movement
            if (_leftControlOffset.Length() < 0.1)
                _leftControlOffset = Vector2.Zero;

            // draw the left control stick at center of the rectangle
            spriteBatch.Draw(AssetManager.VirtualControlStickTexture, _leftControlStick, controlStickSource, Color.White);
            spriteBatch.Draw(AssetManager.VirtualControlBallTexture, new Rectangle((int)newLeftControlBallPosition.X, (int)newLeftControlBallPosition.Y, _leftControlBall.Width, _leftControlBall.Height), controlBallSource, Color.White);
            
            
            // new right control ball position with offset
            var newRightControlBallPosition = new Vector2(_rightControlBall.X, _rightControlBall.Y) + _rightControlOffset; 
            _rightControlOffset *= 0.5f;
            
            // cut off to prevent wasted movement
            if (_rightControlOffset.Length() < 0.1)
                _rightControlOffset = Vector2.Zero;
            
            // draw the right control stick at center of the rectangle
            spriteBatch.Draw(AssetManager.VirtualControlStickTexture, _rightControlStick, controlStickSource, Color.White);
            spriteBatch.Draw(AssetManager.VirtualControlBallTexture, new Rectangle((int)newRightControlBallPosition.X, (int)newRightControlBallPosition.Y, _rightControlBall.Width, _rightControlBall.Height), controlBallSource, Color.White);
            
            
            // source rectangle for shoot
            var shootSource = new Rectangle(0, 0, 64, 64);
            
            // draw left shoot button
            spriteBatch.Draw(AssetManager.VirtualControlShootTexture, _leftShootButton, shootSource, Color.White);
            
            // draw right shoot button
            spriteBatch.Draw(AssetManager.VirtualControlShootTexture, _rightShootButton, shootSource, Color.White);
        }
    }
}

