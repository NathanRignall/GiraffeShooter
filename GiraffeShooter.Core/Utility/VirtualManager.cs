using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GiraffeShooterClient.Utility
{
    public static class VirtualManager
    {
        private static Rectangle _arrowUp;
        private static Rectangle _arrowDown;
        private static Rectangle _arrowLeft;
        private static Rectangle _arrowRight;
        
        private static Rectangle _controlStick;
        private static Rectangle _controlBall;
        private static Vector2 _controlOffset;
        
        private static Rectangle _shootButton;

        public static void Initialize()
        {
            var screenScale = ScreenManager.Size.X/ 1280;

            // button size
            var buttonSize = new Vector2(128, 128) * screenScale;

            // base position of the buttons
            var leftBasePosition = new Vector2(buttonSize.X * 1.5f, ScreenManager.Size.Y - buttonSize.Y * 1.5f);
            
            // button positions
            var upPosition = new Vector2(leftBasePosition.X, leftBasePosition.Y - buttonSize.Y);
            var downPosition = new Vector2(leftBasePosition.X, leftBasePosition.Y);
            var leftPosition = new Vector2(leftBasePosition.X - buttonSize.X, leftBasePosition.Y);
            var rightPosition = new Vector2(leftBasePosition.X + buttonSize.X, leftBasePosition.Y);

            // create the button rectangles
            _arrowUp = new Rectangle((int)upPosition.X, (int)upPosition.Y, (int)buttonSize.X, (int)buttonSize.Y);
            _arrowDown = new Rectangle((int)downPosition.X, (int)downPosition.Y, (int)buttonSize.X, (int)buttonSize.Y);
            _arrowLeft = new Rectangle((int)leftPosition.X, (int)leftPosition.Y, (int)buttonSize.X, (int)buttonSize.Y);
            _arrowRight = new Rectangle((int)rightPosition.X, (int)rightPosition.Y, (int)buttonSize.X, (int)buttonSize.Y);

            
            // control stick size
            var controlStickSize = new Vector2(256, 256) * screenScale;
            var controlButtonSize = new Vector2(64, 64) * screenScale;
            
            // control stick position
            var controlStickPosition = new Vector2(ScreenManager.Size.X - controlStickSize.X * 1f - controlButtonSize.X * 1f, ScreenManager.Size.Y - controlStickSize.Y * 1f - controlButtonSize.Y * 1f);

            // create the control stick rectangle
            _controlStick = new Rectangle((int)controlStickPosition.X, (int)controlStickPosition.Y, (int)controlStickSize.X, (int)controlStickSize.Y);
            
            // create the control ball rectangle
            _controlBall = new Rectangle((int)controlStickPosition.X + (int)controlStickSize.X / 2 - (int)controlButtonSize.X / 2, (int)controlStickPosition.Y + (int)controlStickSize.Y / 2 - (int)controlButtonSize.Y / 2, (int)controlButtonSize.X, (int)controlButtonSize.Y);
            
            
            // shoot button size
            var shootButtonSize = new Vector2(64, 64) * screenScale;
            
            // shoot button position half way up the screen on left
            var shootButtonPosition = new Vector2(shootButtonSize.X * 1.5f, ScreenManager.Size.Y / 2 - shootButtonSize.Y / 2);
            
            // create the shoot button rectangle
            _shootButton = new Rectangle((int)shootButtonPosition.X, (int)shootButtonPosition.Y, (int)shootButtonSize.X, (int)shootButtonSize.Y);

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

                        if (_arrowUp.Contains(e.Position))
                        {
                            eventsToAdd.Add(new Event(Keys.Up, EventType.KeyHold));
                            eventsToRemove.Add(e);
                        }
                        
                        if (_arrowDown.Contains(e.Position))
                        {
                            eventsToAdd.Add(new Event(Keys.Down, EventType.KeyHold));
                            eventsToRemove.Add(e);
                        }
                        
                        if (_arrowLeft.Contains(e.Position))
                        {
                            eventsToAdd.Add(new Event(Keys.Left, EventType.KeyHold));
                            eventsToRemove.Add(e);
                        }
                        
                        if (_arrowRight.Contains(e.Position))
                        {
                            eventsToAdd.Add(new Event(Keys.Right, EventType.KeyHold));
                            eventsToRemove.Add(e);
                        }
                        
                        // check if the press on the control stick
                        if (_controlStick.Contains(e.Position))
                        {
                            // get the new offset
                            _controlOffset = e.Position - new Vector2(_controlBall.X + _controlBall.Width / 2, _controlBall.Y + _controlBall.Height / 2);
                            
                            // check if the new offset is outside the control stick
                            if (!_controlStick.Contains(new Vector2(_controlBall.X, _controlBall.Y) + _controlOffset + new Vector2(_controlBall.Width / 2)))
                            {

                                // get the new offset
                                _controlOffset = Vector2.Normalize(_controlOffset) * (_controlStick.Width / 2 - _controlBall.Width / 2);
                            }
                            
                            // remove the event
                            eventsToRemove.Add(e);
                        }

                        break;

                    case EventType.MouseDrag:
                    case EventType.TouchDrag:
                        
                        if (_arrowUp.Contains(e.Position))
                        {
                            eventsToAdd.Add(new Event(Keys.Up, EventType.KeyHold));
                            eventsToRemove.Add(e);
                        }
                        
                        if (_arrowDown.Contains(e.Position))
                        {
                            eventsToAdd.Add(new Event(Keys.Down, EventType.KeyHold));
                            eventsToRemove.Add(e);
                        }
                        
                        if (_arrowLeft.Contains(e.Position))
                        {
                            eventsToAdd.Add(new Event(Keys.Left, EventType.KeyHold));
                            eventsToRemove.Add(e);
                        }
                        
                        if (_arrowRight.Contains(e.Position))
                        {
                            eventsToAdd.Add(new Event(Keys.Right, EventType.KeyHold));
                            eventsToRemove.Add(e);
                        }
                        
                        // get the start position of the drag
                        var startPosition = e.Position - e.Delta;
                        
                        // check if the drag started on the control stick
                        if (_controlStick.Contains(startPosition))
                        {
                            // get the new offset
                            _controlOffset = e.Position - new Vector2(_controlBall.X + _controlBall.Width / 2, _controlBall.Y + _controlBall.Height / 2);
                            
                            // check if the new offset is outside the control stick
                            if (!_controlStick.Contains(new Vector2(_controlBall.X, _controlBall.Y) + _controlOffset + new Vector2(_controlBall.Width / 2)))
                            {
                                // get the new offset
                                _controlOffset = Vector2.Normalize(_controlOffset) * (_controlStick.Width / 2 - _controlBall.Width / 2);
                            }
                            
                            // remove the event
                            eventsToRemove.Add(e);
                        }
                        
                        break;

                    case EventType.TouchPinch:
                        
                        if (_arrowUp.Contains(e.Position) || _arrowUp.Contains(e.Position2))
                        {
                            eventsToAdd.Add(new Event(Keys.Up, EventType.KeyHold));
                            eventsToRemove.Add(e);
                        }
                        
                        if (_arrowDown.Contains(e.Position) || _arrowDown.Contains(e.Position2))
                        {
                            eventsToAdd.Add(new Event(Keys.Down, EventType.KeyHold));
                            eventsToRemove.Add(e);
                        }
                        
                        if (_arrowLeft.Contains(e.Position) || _arrowLeft.Contains(e.Position2))
                        {
                            eventsToAdd.Add(new Event(Keys.Left, EventType.KeyHold));
                            eventsToRemove.Add(e);
                        }
                        
                        if (_arrowRight.Contains(e.Position) || _arrowRight.Contains(e.Position2))
                        {
                            eventsToAdd.Add(new Event(Keys.Right, EventType.KeyHold));
                            eventsToRemove.Add(e);
                        }
                        
                        // get the start position of the drag
                        var startPosition1 = e.Position - e.Delta;
                        var startPosition2 = e.Position2 - e.Delta2;
                        
                        // check if the drag started on the control stick (pos1)
                        if (_controlStick.Contains(startPosition1))
                        {
                            // get the new offset
                            _controlOffset = e.Position - new Vector2(_controlBall.X + _controlBall.Width / 2, _controlBall.Y + _controlBall.Height / 2);
                            
                            // check if the new offset is outside the control stick
                            if (!_controlStick.Contains(new Vector2(_controlBall.X, _controlBall.Y) + _controlOffset + new Vector2(_controlBall.Width / 2)))
                            {
                                // get the new offset
                                _controlOffset = Vector2.Normalize(_controlOffset) * (_controlStick.Width / 2 - _controlBall.Width / 2);
                            }
                            
                            // remove the event
                            eventsToRemove.Add(e);
                        }
                        
                        // check if the drag started on the control stick (pos2)
                        if (_controlStick.Contains(startPosition2))
                        {
                            // get the new offset
                            _controlOffset = e.Position2 - new Vector2(_controlBall.X + _controlBall.Width / 2, _controlBall.Y + _controlBall.Height / 2);
                            
                            // check if the new offset is outside the control stick
                            if (!_controlStick.Contains(new Vector2(_controlBall.X, _controlBall.Y) + _controlOffset + new Vector2(_controlBall.Width / 2)))
                            {
                                // get the new offset
                                _controlOffset = Vector2.Normalize(_controlOffset) * (_controlStick.Width / 2 - _controlBall.Width / 2);
                            }
                            
                            // remove the event
                            eventsToRemove.Add(e);
                        }

                        break;
                }
            }
            
            // if the control offset is not zero, add the event
            if (_controlOffset != Vector2.Zero)
            {
                Console.WriteLine(_controlOffset);
                eventsToAdd.Add(new Event(_controlOffset, EventType.StickMove));
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

        public static void Draw(Microsoft.Xna.Framework.GameTime gameTime, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            // source rectangle for the buttons
            var arrowSource = new Rectangle(0, 0, 64, 64);

            // draw the buttons
            spriteBatch.Draw(AssetManager.VirtualControlArrowTexture, _arrowUp, arrowSource, Color.White);
            spriteBatch.Draw(AssetManager.VirtualControlArrowTexture, _arrowDown, arrowSource, Color.White);
            spriteBatch.Draw(AssetManager.VirtualControlArrowTexture, _arrowLeft, arrowSource, Color.White);
            spriteBatch.Draw(AssetManager.VirtualControlArrowTexture, _arrowRight, arrowSource, Color.White);
            
            // source rectangle for the control stick
            var controlStickSource = new Rectangle(0, 0, 128, 128);
            var controlBallSource = new Rectangle(0, 0, 32, 32);
            
            // new control ball position with offset
            var newControlBallPosition = new Vector2(_controlBall.X, _controlBall.Y) + _controlOffset;
            _controlOffset *= 0.9f;

            // draw the control stick at center of the rectangle
            spriteBatch.Draw(AssetManager.VirtualControlStickTexture, _controlStick, controlStickSource, Color.White);
            spriteBatch.Draw(AssetManager.VirtualControlBallTexture, new Rectangle((int)newControlBallPosition.X, (int)newControlBallPosition.Y, _controlBall.Width, _controlBall.Height), controlBallSource, Color.White);
            
            // source rectangle for shoot
            var shootSource = new Rectangle(0, 0, 64, 64);
            
            // draw shoot button
            spriteBatch.Draw(AssetManager.VirtualControlShootTexture, _shootButton, shootSource, Color.White);
        }
    }
}

