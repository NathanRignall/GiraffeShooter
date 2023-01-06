using System.Collections.Generic;
using Microsoft.Xna.Framework;

using GiraffeShooterClient.Utility;

namespace GiraffeShooterClient.Entity
{
    class Button : Entity
    {
        public Button(Vector3 position, string buttonText)
        {
            id = System.Guid.NewGuid();
            name = "Button";
            
            Physics physics = new Physics();
            physics.position = position;
            physics.isStatic = true;
            AddComponent(physics);

            Text text = new Text();
            text.String = buttonText;
            AddComponent(text);

        }
    }
}

