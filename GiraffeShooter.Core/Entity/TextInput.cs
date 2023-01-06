using System.Collections.Generic;
using Microsoft.Xna.Framework;

using GiraffeShooterClient.Utility;
using Microsoft.Xna.Framework.Input;

namespace GiraffeShooterClient.Entity
{
    class TextInput : Entity
    {
        string textInput = "";

        public TextInput(Vector3 position)
        {
            id = System.Guid.NewGuid();
            name = "TextInput";
            
            Physics physics = new Physics();
            physics.position = position;
            physics.isStatic = true;
            AddComponent(physics);
            
            Text text = new Text();
            text.String = textInput;
            AddComponent(text);
            
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
                    case EventType.KeyPress:
                        
                        // check if shift is down
                        if (keyboardState.IsKeyDown(Keys.LeftShift) || keyboardState.IsKeyDown(Keys.RightShift))
                        {
                            // check if the key is a letter
                            if (e.Key.ToString().Length == 1)
                            {
                                // add the letter to the text
                                textInput += e.Key.ToString().ToUpper();
                            }
                        }
                        else
                        {
                            // check if the key is a letter
                            if (e.Key.ToString().Length == 1)
                            {
                                // add the letter to the text
                                textInput += e.Key.ToString().ToLower();
                            }
                        }
                        
                        // check if the key is a number
                        if (e.Key.ToString().Length == 1 && e.Key.ToString()[0] >= '0' && e.Key.ToString()[0] <= '9')
                        {
                            // add the number to the text
                            textInput += e.Key.ToString();
                        }
                        
                        // check if the key is a space
                        if (e.Key == Keys.Space)
                        {
                            // add a space to the text
                            textInput += " ";
                        }
                        
                        // check if the key is a backspace
                        if (e.Key == Keys.Back)
                        {
                            // remove the last character from the text
                            if (textInput.Length > 0)
                            {
                                textInput = textInput.Substring(0, textInput.Length - 1);
                            }
                        }
                        
                        break;
                }
            }
            
            // update the text
            GetComponent<Text>().String = textInput;
            
        }
    }
}