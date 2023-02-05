using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using GiraffeShooterClient.Utility;

namespace GiraffeShooterClient.Entity
{
    class Button : Entity
    {
        private Action _action;
        
        public Button(Vector3 position, Texture2D texture, Action action)
        {
            Id = Guid.NewGuid();
            Name = "Button";
            
            _action = action;
            
            Physics physics = new Physics();
            physics.Position = position;
            physics.IsStatic = true;
            AddComponent(physics);

            Sprite sprite = new Sprite(texture);
            AddComponent(sprite);

        }

        public override void HandleEvents(List<Event> events)
        {
            foreach (Event e in events)
            {
                switch (e.Type)
                {
                    case EventType.MouseRelease:
                    case EventType.TouchPress:
                        
                        var sprite = GetComponent<Sprite>();
                        
                        // if not visible, don't handle event
                        if (sprite == null || !sprite.Visible)
                            break;

                        // check if button was pressed
                        if (sprite.Bounds.Contains(e.Position / ScreenManager.GetScaleFactor()))
                            _action();

                        break;
                }
            }
            
            
        }
    }
}

