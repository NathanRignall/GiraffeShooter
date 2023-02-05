using System;
using System.Collections.Generic;
using GiraffeShooterClient.Utility;
using Microsoft.Xna.Framework;

namespace GiraffeShooterClient.Entity
{
    class Shoot : Entity
    {
        private TimeSpan _pressedTime;

        public Shoot()
        {
            Id = Guid.NewGuid();
            Name = "Shoot";

            Physics physics = new Physics();
            physics.Position = new Vector3(0.5f, 0.5f, 0);
            physics.IsStatic = true;
            physics.IsKinematic = true;
            AddComponent(physics);
            
            Sprite sprite = new Sprite(AssetManager.ShootSpriteTexture);
            sprite.Centered = false;
            sprite.Visible = false;
            AddComponent(sprite);
        }

        public void SetPosition(Vector3 position)
        {
            Physics physics = GetComponent<Physics>();
            physics.Position = position;
        }
        
        public void SetRotation(float rotation)
        {
            Sprite sprite = GetComponent<Sprite>();
            sprite.Rotation = rotation;
        }
        
        public float GetRotation()
        {
            Sprite sprite = GetComponent<Sprite>();
            return sprite.Rotation;
        }
        
        public override void Update(GameTime gameTime)
        {
            
            // if no touch screen
            if (!InputManager.TouchConnected && !ContextManager.Paused)
            {
                // get the mouse position
                Vector2 mousePosition = InputManager.CurrentMouseState.Position.ToVector2();
            
                // use the mouse position to calculate the rotation from 0,0
                Vector2 delta = mousePosition - ScreenManager.Size / 2;
                SetRotation((float)Math.Atan2(delta.Y, delta.X));
            }

            // hide shoot button after 1 second
            if (gameTime.TotalGameTime - _pressedTime > TimeSpan.FromSeconds(1))
            {
                Sprite sprite = GetComponent<Sprite>();
                sprite.Visible = true;
                sprite.zOrder = 6;
            }
        }

        public override void HandleEvents(List<Event> events)
        {
            if (ContextManager.Paused)
                return;
            
            foreach (Event e in events)
            {
                switch (e.Type)
                {
                    case EventType.StickRightMove:
                        
                        // update pressed time
                        _pressedTime = e.Time;

                        // use delta to calculate rotation
                        Vector2 delta = e.Delta;
                        
                        // set rotation
                        SetRotation((float)Math.Atan2(delta.Y, delta.X));
                        
                        // set visibility
                        Sprite sprite = GetComponent<Sprite>();
                        sprite.Visible = true;

                        break;
                }
            }
        } 

    }
}

