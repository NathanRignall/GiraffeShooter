using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;

using GiraffeShooterClient.Utility;

namespace GiraffeShooterClient.Entity
{
    public class TextInput : Entity
    {

        public TextInput(Vector2 offset, string placeholder, string popupText, bool IsPassword = false, ScreenManager.CenterType center = ScreenManager.CenterType.MiddleCenter)
        {
            Id = Guid.NewGuid();
            Name = "TextInput";
        
            Screen screen = new Screen(offset, center);
            AddComponent(screen);
        
            Sprite sprite = new Sprite(AssetManager.InputTexture, new Rectangle(0,0,512,64));
            AddComponent(sprite);
        
            Text text = new Text();
            text.String = "";
            AddComponent(text);
            
            Input input = new Input();
            AddComponent(input);
        }
        
        public override void HandleEvents(List<Event> events)
        {
            GetComponent<Input>().HandleEvents(events);
        }

        public string GetString()
        {
            return GetComponent<Input>().String;
        }
        
        public void ResetString()
        {
            GetComponent<Input>().ResetString();
        }

    }
}

