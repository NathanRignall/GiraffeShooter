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
            id = System.Guid.NewGuid();
            name = "Button";
            
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
                    case EventType.MouseClick:
                    case EventType.TouchPress:

                        if (GetComponent<Sprite>().Bounds.Contains(e.Position))
                        {
                            _action();
                        }
                        break;
                }
            }
            
            
        }
    }
}

