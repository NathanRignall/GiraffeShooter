using System;
using Microsoft.Xna.Framework;
using GiraffeShooterClient.Utility;

namespace GiraffeShooterClient.Entity
{
    // Inheritance: MetaBullet inherits from Meta class
    class MetaBullet : Meta
    {
        // How much damage the bullet does
        public int Damage = 1;
        
        // This method is an implementation of the abstract method in the parent class, Meta
        public override void Create(Vector3 position, Vector3 velocity)
        {
            // Creates a new Bullet object
            new Bullet(position, velocity, Damage, Id,this);
        }
    }

    // Inheritance: Bullet inherits from Entity class
    class Bullet : Entity
    {
        // how much damage the bullet does
        int Damage;
        
        // Constructor for Bullet class
        public Bullet(Vector3 position = default, Vector3 velocity = default, int damage = 1, Guid id = default, Meta meta = null)
        {
            // Assigns Id value or creates a new Guid if Id value is default
            Id = (id == default) ? Guid.NewGuid() : id;
            // Assigns Name value as "Bullet
            Name = "Bullet";
            // Assigns Meta value as meta parameter if not null, otherwise creates a new MetaBullet object
            MetaBullet Meta = (meta == null) ? new MetaBullet() : (MetaBullet)meta;
            
            // updates the damage value
            Damage = damage;
            Meta.Damage = damage;

            // Creates a new Physics object and assigns the position, velocity and deceleration values
            Physics physics = new Physics();
            physics.Position = position;
            physics.Velocity = velocity;
            physics.deceleration = 1f;
            AddComponent(physics);

            // Creates a new Collider object and assigns a hitAction method to the Giraffe type response
            Collider collider = new Collider();
            Action<Entity> hitAction = (Entity subject) =>
            {
                // get the subjects health
                Health health = subject.GetComponent<Health>();
                
                // reduce the health by the damage
                health.ReduceHealth(Damage);

                // delete the bullet
                Delete();
            };
            collider.AddResponse<Giraffe>(hitAction);
            AddComponent(collider);

            // Creates a new Sprite object with AssetManager.AmmunitionFireTexture and adds it to the Bullet entity
            Sprite sprite = new Sprite(AssetManager.AmmunitionFireTexture);
            AddComponent(sprite);

            // Creates a new Cleaner object with a time span of 2 seconds and adds it to the Bullet entity
            Cleaner cleaner = new Cleaner(TimeSpan.FromSeconds(2));
            AddComponent(cleaner);
        }
    }
}
