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
        
        public Button(Vector2 offset, Texture2D texture, Action action, ScreenManager.CenterType center = ScreenManager.CenterType.MiddleCenter, Guid id = default)
        {
            Id = (id == default) ? Guid.NewGuid() : id;
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

