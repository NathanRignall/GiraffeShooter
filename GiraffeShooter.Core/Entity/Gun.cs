using System;
using Microsoft.Xna.Framework;

using GiraffeShooterClient.Utility;

namespace GiraffeShooterClient.Entity
{
    
    class MetaGun : Meta
    {
        public Type RequiredAmmo = typeof(MetaAmmunition);
        public int RequiredQuanity = 1;
        
        public override void Create(Vector3 position, Vector3 velocity)
        {
            new Gun(position, velocity, this);
        }

        public override bool Action(Entity subject)
        {
            // get the inventory
            Inventory inventory = subject.GetComponent<Inventory>();
            
            // check if the inventory has the required ammo
            if (inventory.HasItem(RequiredAmmo))
            {
                // remove the ammo from the inventory
                inventory.ReduceItem(RequiredAmmo, RequiredQuanity);
                
                // create a bullet
                //new Bullet(subject.GetComponent<Physics>().Position, subject.GetComponent<Physics>().Velocity);

                // return true
                return true;
            }
            
            return false;
        }
    }
    
    class Gun : Entity
    {
        
        public MetaGun _meta;

        public Gun(Vector3 position, Vector3 velocity, Meta meta = null)
        {
            Id = Guid.NewGuid();
            Name = "Gun";
            
            if (meta == null)
                _meta = new MetaGun();
            else
                _meta = (MetaGun)meta;
            
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
            collider.AddResponse<Giraffe>(pickupAction);
            AddComponent(collider);

            Sprite sprite = new Sprite(AssetManager.PistolTexture);
            AddComponent(sprite);
        }
    }
}

