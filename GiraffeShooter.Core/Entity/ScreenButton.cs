using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using GiraffeShooterClient.Utility;

namespace GiraffeShooterClient.Entity
{
    class ScreenButton : Entity
    {
        private Action _action;
        
        public ScreenButton(Vector2 offset, Texture2D texture, Action action, ScreenManager.CenterType center = ScreenManager.CenterType.MiddleCenter)
        {
            Id = Guid.NewGuid();
            Name = "ScreenButton";
            
            _action = action;

            Screen screen = new Screen(offset, center);
            AddComponent(screen);

            Sprite sprite = new Sprite(texture);
            sprite.zOrder = 9;
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

                        if (GetComponent<Sprite>().Bounds.Contains(e.Position / ScreenManager.GetScaleFactor()))
                        {
                            _action();
                        }
                        break;
                }
            }
            
            
        }
    }
}