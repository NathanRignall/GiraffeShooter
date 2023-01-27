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
        private static bool _leftShootButtonPressed;
        private static TimeSpan _leftShootButtonPressedTime;
        
        private static Rectangle _rightShootButton;
        private static bool _rightShootButtonPressed;
        private static TimeSpan _rightShootButtonPressedTime;
        
        private static Rectangle _dropButton;
        private static bool _dropButtonPressed;
        private static TimeSpan _dropButtonPressedTime;

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
            var leftShootButtonPosition = new Vector2(shootButtonSize.X * 5f, ScreenManager.Size.Y - shootButtonSize.Y * 2f);
            
            // create the left shoot button rectangle
            _leftShootButton = new Rectangle((int)leftShootButtonPosition.X, (int)leftShootButtonPosition.Y, (int)shootButtonSize.X, (int)shootButtonSize.Y);
            
            // set left pressed state
            _leftShootButtonPressed = false;
            
            
            // shoot button position screen on right
            var rightShootButtonPosition = new Vector2(ScreenManager.Size.X - shootButtonSize.X * 6f, ScreenManager.Size.Y - shootButtonSize.Y * 2f);
            
            // create the right shoot button rectangle
            _rightShootButton = new Rectangle((int)rightShootButtonPosition.X, (int)rightShootButtonPosition.Y, (int)shootButtonSize.X, (int)shootButtonSize.Y);
            
            // set right pressed state
            _rightShootButtonPressed = false;
            
            
            // drop button size
            var dropButtonSize = new Vector2(64, 64) * screenScale;
            
            
            // drop button position screen on left
            var dropButtonPosition = new Vector2(dropButtonSize.X * 1.5f, ScreenManager.Size.Y - dropButtonSize.Y * 6.5f);
            
            // create the drop button rectangle
            _dropButton = new Rectangle((int)dropButtonPosition.X, (int)dropButtonPosition.Y, (int)dropButtonSize.X, (int)dropButtonSize.Y);
            
            // set drop pressed state
            _dropButtonPressed = false;

        }

        public static void Update(GameTime gameTime)
        {
        }

        public static void HandleEvents(List<Event> events, GameTime gameTime)
        {
            var eventsToRemove = new List<Event>();
            var eventsToAdd = new List<Event>();
            
            foreach (Event e in events)
            {
                switch (e.Type)
                {
                    case EventType.MouseRelease:
                    case EventType.TouchPress:
                    case EventType.TouchHold:

                        // check if the left shoot button is pressed
                        if (_leftShootButton.Contains(e.Position))
                        { 
                            _leftShootButtonPressed = true;
                            _leftShootButtonPressedTime = e.Time;
                        } 
                        
                        // check if the right shoot button is pressed
                        if (_rightShootButton.Contains(e.Position))
                        { 
                            _rightShootButtonPressed = true;
                            _rightShootButtonPressedTime = e.Time;
                        }
                        
                        // check if the drop button is pressed
                        if (_dropButton.Contains(e.Position))
                        { 
                            _dropButtonPressed = true;
                            _dropButtonPressedTime = e.Time;
                        }

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
                eventsToAdd.Add(new Event(_leftControlOffset / 256, EventType.StickLeftMove, gameTime.TotalGameTime));

            // if the right control offset is not zero, add the event
            if (_rightControlOffset != Vector2.Zero)
                eventsToAdd.Add(new Event(_rightControlOffset / 256, EventType.StickRightMove, gameTime.TotalGameTime));
            
            // if either the left or right shoot button is pressed, add the event
            if (_leftShootButtonPressed || _rightShootButtonPressed)
                eventsToAdd.Add(new Event(Keys.Space, EventType.KeyPress, gameTime.TotalGameTime));
            
            // if drop button is pressed, add the event
            if (_dropButtonPressed)
                eventsToAdd.Add(new Event(Keys.Q, EventType.KeyPress, gameTime.TotalGameTime));

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
            var controlStickSource = new Rectangle(0, 0, 256, 256);
            var controlBallSource = new Rectangle(0, 0, 64, 64);
            
            
            // new left control ball position with offset
            var newLeftControlBallPosition = new Vector2(_leftControlBall.X, _leftControlBall.Y) + _leftControlOffset;
            _leftControlOffset *= 0.5f;
            
            // cut off to prevent wasted movement
            if (_leftControlOffset.Length() < 0.1)
                _leftControlOffset = Vector2.Zero;

            // draw the left control stick at center of the rectangle
            spriteBatch.Draw(AssetManager.VirtualControlStickTexture, _leftControlStick, controlStickSource, new Color(Color.White, 0.1f));
            spriteBatch.Draw(AssetManager.VirtualControlBallTexture, new Rectangle((int)newLeftControlBallPosition.X, (int)newLeftControlBallPosition.Y, _leftControlBall.Width, _leftControlBall.Height), controlBallSource, Color.White);
            
            
            // new right control ball position with offset
            var newRightControlBallPosition = new Vector2(_rightControlBall.X, _rightControlBall.Y) + _rightControlOffset; 
            _rightControlOffset *= 0.5f;
            
            // cut off to prevent wasted movement
            if (_rightControlOffset.Length() < 0.1)
                _rightControlOffset = Vector2.Zero;
            
            // draw the right control stick at center of the rectangle
            spriteBatch.Draw(AssetManager.VirtualControlStickTexture, _rightControlStick, controlStickSource, new Color(Color.White, 0.1f));
            spriteBatch.Draw(AssetManager.VirtualControlBallTexture, new Rectangle((int)newRightControlBallPosition.X, (int)newRightControlBallPosition.Y, _rightControlBall.Width, _rightControlBall.Height), controlBallSource, Color.White);
            
            
            // default source rectangle for the buttons
            var defaultShootSource = new Rectangle(0, 0, 128, 128);

            // red source rectangle for the buttons
            var redShootSource = new Rectangle(128, 0, 128, 128);
            
            // set the source rectangle for the shoot buttons
            var leftShootSource = _leftShootButtonPressed ? redShootSource : defaultShootSource;
            var rightShootSource = _rightShootButtonPressed ? redShootSource : defaultShootSource;
            
            // draw shoot buttons
            spriteBatch.Draw(AssetManager.VirtualControlShootTexture, _leftShootButton, leftShootSource, Color.White);
            spriteBatch.Draw(AssetManager.VirtualControlShootTexture, _rightShootButton, rightShootSource, Color.White);
            
            // reset the shoot button pressed if time is up
            if (gameTime.TotalGameTime - _leftShootButtonPressedTime > TimeSpan.FromMilliseconds(100))
                _leftShootButtonPressed = false;
            
            if (gameTime.TotalGameTime - _rightShootButtonPressedTime > TimeSpan.FromMilliseconds(100))
                _rightShootButtonPressed = false;
            
            
            // default source rectangle for the buttons
            var defaultDropButtonSource = new Rectangle(0, 0, 128, 128);
            
            // pressed source rectangle for the buttons
            var pressedDropButtonSource = new Rectangle(0, 0, 128, 128);
            
            // set the source rectangle for the buttons
            var dropButtonSource = _dropButtonPressed ? defaultDropButtonSource : pressedDropButtonSource;
            
            // draw the drop button
            spriteBatch.Draw(AssetManager.VirtualControlDropTexture, _dropButton, dropButtonSource, Color.White);
            
            // reset the drop button pressed if time is up
            if (gameTime.TotalGameTime - _dropButtonPressedTime > TimeSpan.FromMilliseconds(100))
                _dropButtonPressed = false;
            
        }
    }
}

