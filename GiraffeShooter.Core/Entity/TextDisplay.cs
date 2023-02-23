using System;

using Microsoft.Xna.Framework;

using GiraffeShooterClient.Utility;

namespace GiraffeShooterClient.Entity
{
    class TextDisplay : Entity
    {

        public TextDisplay(Vector2 offset, string message, ScreenManager.CenterType center = ScreenManager.CenterType.MiddleCenter)
        {
            Id = Guid.NewGuid();
            Name = "Text";

            Screen screen = new Screen(offset, center);
            AddComponent(screen);
            
            Text text = new Text();
            text.String = message;
            AddComponent(text);

        }
        
    }
}