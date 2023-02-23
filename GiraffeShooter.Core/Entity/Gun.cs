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
            
            // get the aim direction
            Aim aim = subject.GetComponent<Aim>();
            
            // get the rotation
            var rotation = aim.Rotation;
            
            // check if the inventory has the required ammo
            if (inventory.HasItem(RequiredAmmo))
            {
                // remove the ammo from the inventory
                inventory.ReduceItem(RequiredAmmo, RequiredQuanity);
                
                // create a position using subject position
                var position = subject.GetComponent<Physics>().Position + new Vector3(0, -0.7f, 0);
                
                // create a velocity vector using the rotation
                var velocity = new Vector3((float)Math.Cos(rotation), (float)Math.Sin(rotation), 0) * 50;

                // create a bullet
                new Bullet(position, velocity);

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

