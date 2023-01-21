using System;

using Microsoft.Xna.Framework;

using GiraffeShooterClient.Utility;

namespace GiraffeShooterClient.Entity
{
    class MetaPistol : Meta
    {
        public override void Create(Vector3 position, Vector3 velocity)
        {
            new Pistol(position, velocity, this);
        }
    }

    class Pistol : Entity
    {
        MetaPistol _meta;
        
        public Pistol(Vector3 position, Vector3 velocity, Meta meta = null)
        {
            Id = Guid.NewGuid();
            Name = "Pistol";
            
            if (meta == null)
                _meta = new MetaPistol();
            else
                _meta = (MetaPistol)meta;
            
            Physics physics = new Physics();
            physics.Position = position;
            physics.Velocity = velocity;
            physics.deceleration = 0.8f;
            AddComponent(physics);
            
            Collider collider = new Collider();
            Action<Entity> pickupAction = (Entity subject) =>
            {
                if (subject.GetComponent<Inventory>().AddItem(_meta))
                    Delete();
            };
            collider.AddResponse<Player>(pickupAction);
            AddComponent(collider);

            Sprite sprite = new Sprite(AssetManager.PistolTexture);
            AddComponent(sprite);
        }
    }
}