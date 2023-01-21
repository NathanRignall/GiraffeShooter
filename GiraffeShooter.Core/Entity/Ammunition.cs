using System;

using Microsoft.Xna.Framework;

using GiraffeShooterClient.Utility;

namespace GiraffeShooterClient.Entity
{
    class MetaAmmunition : Meta
    {
        public MetaAmmunition(int quantity)
        {
            Quantity = quantity;
        }
 
        public override void Create(Vector3 position, Vector3 velocity)
        {
            new Ammunition(position, velocity, this);
        }
    }

    class Ammunition : Entity
    {
        MetaAmmunition _meta;
        
        public Ammunition(Vector3 position, Vector3 velocity, Meta meta = null)
        {
            Id = Guid.NewGuid();
            Name = "Ammunition";
            
            if (meta == null)
                _meta = new MetaAmmunition(10);
            else
                _meta = (MetaAmmunition)meta;

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

            Sprite sprite = new Sprite(AssetManager.AmmunitionTexture);
            AddComponent(sprite);
        }
    }
}