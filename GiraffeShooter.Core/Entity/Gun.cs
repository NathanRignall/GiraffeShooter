using System;
using Microsoft.Xna.Framework;

using GiraffeShooterClient.Utility;
using Microsoft.Xna.Framework.Audio;

namespace GiraffeShooterClient.Entity
{
    
    public class MetaGun : Meta
    {
        public Type RequiredAmmo = typeof(MetaAmmunition);
        protected int RequiredQuanity = 1;

        public int Damage = 10;
        
        public TimeSpan TimeDelay = TimeSpan.FromSeconds(1);
        public TimeSpan PreviousShoot;
        
        public SoundEffect GunSound;
        
        public MetaGun()
        {
            MetaType = MetaType.Weapon;
            MaxQuantity = 0;
            GunSound = AssetManager.GunEffect;
        }
        
        public override void Create(Vector3 position, Vector3 velocity)
        {
            new Gun(position, velocity, Id, this);
        }

        public override bool Action(TimeSpan time, Entity subject)
        {
            // check if the previous shoot is greater than the time delay
            if (time - PreviousShoot < TimeDelay)
                return false;
            
            // set the previous shoot to the current time
            PreviousShoot = time;
            
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
                new Bullet(position, velocity, Damage);
                
                // play the gun sound
                GunSound.Play();

                // return true
                return true;
            }
            
            return false;
        }
    }
    
    public class Gun : Entity
    {
        public Gun(Vector3 position, Vector3 velocity, Guid id = default, Meta meta = null)
        {
            Id = (id == default) ? Guid.NewGuid() : id;
            Name = "Gun";

            if (meta == null)
                Meta = new MetaGun();
            else
                Meta = (MetaGun)meta;

            Physics physics = new Physics();
            physics.Position = position;
            physics.Velocity = velocity;
            physics.deceleration = 0.8f;
            AddComponent(physics);
            
            Collider collider = new Collider();
            Action<Entity> pickupAction = (Entity subject) =>
            {
                if (subject.GetComponent<Inventory>().AddItem(Meta))
                    Delete();
            };
            Action<Entity> pickupAction2 = (Entity subject) =>
            {
                Delete();
            };
            collider.AddResponse<Player>(pickupAction);
            collider.AddResponse<Giraffe>(pickupAction);
            collider.AddResponse<Wall>(pickupAction2);
            AddComponent(collider);

            Sprite sprite = new Sprite(AssetManager.PistolTexture);
            AddComponent(sprite);
        }
    }
}

