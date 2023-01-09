using System;
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
            physics.Position = position;
            physics.IsStatic = true;
            AddComponent(physics);

            Sprite sprite = new Sprite(AssetManager.PlayButtonTexture);
            AddComponent(sprite);

        }

        public override void HandleEvents(List<Event> events)
        {
            foreach (Event e in events)
            {
                switch (e.Type)
                {
                    case EventType.MouseClick:

                        if (GetComponent<Sprite>().Bounds.Contains(e.MousePosition))
                        {
                            ContextManager.SetState(ContextManager.State.World);
                        }
                        break;
                }
            }
            
            
        }
    }
}

