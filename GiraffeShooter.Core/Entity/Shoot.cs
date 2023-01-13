using System;
using System.Collections.Generic;
using GiraffeShooterClient.Utility;
using Microsoft.Xna.Framework;

namespace GiraffeShooterClient.Entity
{
    class Shoot : Entity
    {
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

        public override void HandleEvents(List<Event> events)
        {
            foreach (Event e in events)
            {
                switch (e.Type)
                {
                    case EventType.StickMove:

                        // use delta to calculate rotation
                        Vector2 delta = e.Delta;
                        
                        // set rotation
                        SetRotation((float)Math.Atan2(delta.Y, delta.X));

                        break;
                }
            }
        } 

    }
}

