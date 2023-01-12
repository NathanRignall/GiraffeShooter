using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using GiraffeShooterClient.Utility;
using Microsoft.Xna.Framework.Input;

namespace GiraffeShooterClient.Entity
{
    class TextInput : Entity
    {
        public string String { get; set; } = "";
        public int MaxLength { get; set; } = 32;
        public int CursorPosition { get; set; } = 0;
        public bool IsSelected { get; set; } = false;
        
        private bool _open = false;

        public TextInput(Vector3 position)
        {
            id = Guid.NewGuid();
            name = "TextInput";

            Physics physics = new Physics();
            physics.Position = position;
            physics.IsStatic = true;
            AddComponent(physics);

            Sprite sprite = new Sprite(AssetManager.PlayerTexture);
            AddComponent(sprite);

            Text text = new Text();
            text.String = String;
            AddComponent(text);
        }

        private void AddCharacter(char c)
        {
            if (String.Length < MaxLength)
            {
                String = String.Insert(CursorPosition, c.ToString());
                CursorPosition++;
            }
        }

        private void RemoveCharacter()
        {
            if (CursorPosition > 0)
            {
                String = String.Remove(CursorPosition - 1, 1);
                CursorPosition--;
            }
        }

        public void ResetString()
        {
            String = "";
            CursorPosition = 0;
        }

        public override void HandleEvents(List<Event> events)
        {
            
            // get the keyboard state
            var keyboardState = InputManager.CurrentKeyboardState;

            // get the keys that are pressed
            foreach (Event e in events)
            {
                switch (e.Type)
                {
                    
                    case EventType.TouchPress:

                        if (GetComponent<Sprite>().Bounds.Contains(e.Position / ScreenManager.GetScaleFactor()))
                        {
#if __IOS__
                            if (!_open)
                                KeyboardPopup();
#endif
                        }
                        
                        break;
                    
                    case EventType.MouseClick:
                        
                        if (GetComponent<Sprite>().Bounds.Contains(e.Position / ScreenManager.GetScaleFactor()))
                        {
                            IsSelected = true;
                        }
                        else
                        {
                            IsSelected = false;
                        }

                        break;

                    case EventType.KeyPress:

                        // only if the entity is selected
                        if (IsSelected)
                        {
                            // check if shift is down
                            if (keyboardState.IsKeyDown(Keys.LeftShift) || keyboardState.IsKeyDown(Keys.RightShift))
                            {
                                // check if the key is a letter
                                if (e.Key.ToString().Length == 1)
                                {
                                    // add the letter to the text
                                    AddCharacter(e.Key.ToString().ToUpper()[0]);
                                }
                            }
                            else
                            {
                                // check if the key is a letter
                                if (e.Key.ToString().Length == 1)
                                {
                                    // add the letter to the text
                                    AddCharacter(e.Key.ToString().ToLower()[0]);
                                }
                            }

                            // check if the key is a number
                            if (e.Key.ToString().Length == 1 && e.Key.ToString()[0] >= '0' &&
                                e.Key.ToString()[0] <= '9')
                            {
                                // add the number to the text
                                AddCharacter(e.Key.ToString()[0]);
                            }

                            // check if the key is at symbol
                            if (e.Key == Keys.F1)
                            {
                                // add the at symbol to the text
                                AddCharacter('@');
                            }

                            // check if key is a dot
                            if (e.Key == Keys.OemPeriod)
                            {
                                // add the dot to the text
                                AddCharacter('.');
                            }

                            // check if the key is a space
                            if (e.Key == Keys.Space)
                            {
                                // add a space to the text
                                AddCharacter(' ');
                            }

                            // check if the key is a backspace
                            if (e.Key == Keys.Back)
                            {
                                RemoveCharacter();
                            }

                            // check if the key is a left arrow
                            if (e.Key == Keys.Left)
                            {
                                if (CursorPosition > 0)
                                {
                                    CursorPosition--;
                                }
                            }

                            // check if the key is a right arrow
                            if (e.Key == Keys.Right)
                            {
                                if (CursorPosition < String.Length)
                                {
                                    CursorPosition++;
                                }
                            }
                        }

                        break;
                }
            }

            // form the display string
            var displayString = String;
            if (IsSelected)
            {
                displayString = displayString.Insert(CursorPosition, "|");
                displayString = displayString.Insert(0, " ");
            }
            else
            {
                displayString = displayString.Insert(CursorPosition, " ");
                displayString = displayString.Insert(0, " ");
            }

            // update the text
            GetComponent<Text>().String = displayString;
        }
        
#if __IOS__
        private async Task<int> KeyboardPopup()
        {
            // prevent multiple popups
            _open = true;

            try
            {
                // get the result
                var result = await Microsoft.Xna.Framework.Input.KeyboardInput.Show("Input", "Giraffe Shooter", "Enter your name", false);
                
                // set the string
                String = result;

                // stop open
                _open = false;
            }
            catch (Exception e)
            {
                // log the error
                Console.WriteLine(e);

                // stop open
                _open = false;
            }

            return 1;
        }
#endif
        
    }
}