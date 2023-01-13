
using Microsoft.Xna.Framework;

namespace GiraffeShooterClient.Entity
{
    class TextDisplay : Entity
    {

        public TextDisplay(Vector3 position, string message)
        {
            id = System.Guid.NewGuid();
            name = "Text";

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