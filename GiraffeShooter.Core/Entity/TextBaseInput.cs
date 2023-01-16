using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;

using GiraffeShooterClient.Utility;

namespace GiraffeShooterClient.Entity
{
    class TextBaseInput : Entity
    {

        public TextBaseInput(Vector3 position)
        {
            Id = Guid.NewGuid();
            Name = "TextInput";
        
            Physics physics = new Physics();
            physics.Position = position;
            physics.IsStatic = true;
            AddComponent(physics);
        
            Sprite sprite = new Sprite(AssetManager.InputTexture, new Rectangle(0,0,512,64));
            AddComponent(sprite);
        
            Text text = new Text();
            text.String = "";
            AddComponent(text);
            
            TextInput textInput = new TextInput();
            AddComponent(textInput);
        }
        
        public override void HandleEvents(List<Event> events)
        {
            GetComponent<TextInput>().HandleEvents(events);
        }

        public string GetString()
        {
            return GetComponent<TextInput>().String;
        }
        
        public void ResetString()
        {
            GetComponent<TextInput>().ResetString();
        }

    }
}

