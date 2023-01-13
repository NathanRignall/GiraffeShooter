using System;

using Microsoft.Xna.Framework;

namespace GiraffeShooterClient.Entity
{
    class TextDisplay : Entity
    {

        public TextDisplay(Vector3 position, string message)
        {
            Id = Guid.NewGuid();
            Name = "Text";

            Physics physics = new Physics();
            physics.Position = position;
            physics.IsStatic = true;
            AddComponent(physics);
            
            Text text = new Text();
            text.String = message;
            AddComponent(text);

        }
        
    }
}