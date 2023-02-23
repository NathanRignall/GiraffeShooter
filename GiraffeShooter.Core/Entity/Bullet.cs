using System;
using Microsoft.Xna.Framework;
using GiraffeShooterClient.Utility;

namespace GiraffeShooterClient.Entity
{
    // Inheritance: MetaBullet inherits from Meta class
    class MetaBullet : Meta
    {
        // This method is an implementation of the abstract method in the parent class, Meta
        public override void Create(Vector3 position, Vector3 velocity)
        {
            // Creates a new Bullet object
            new Bullet(position, velocity, Id,this);
        }
    }

    // Inheritance: Bullet inherits from Entity class
    class Bullet : Entity
    {
        // Constructor for Bullet class
        public Bullet(Vector3 position = default, Vector3 velocity = default, Guid id = default, Meta meta = null)
        {
            // Assigns Id value or creates a new Guid if Id value is default
            Id = (id == default) ? Guid.NewGuid() : id;
            // Assigns Name value as "Bullet
            Name = "Bullet";
            // Assigns Meta value as meta parameter if not null, otherwise creates a new MetaBullet object
            Meta = (meta == null) ? new MetaBullet() : (MetaGiraffe)meta;

            // Creates a new Physics object and assigns the position, velocity and deceleration values
            Physics physics = new Physics();
            physics.Position = position;
            physics.Velocity = velocity;
            physics.deceleration = 1f;
            // Adds the Physics component to the Bullet entity
            AddComponent(physics);

            // Creates a new Collider object and assigns a hitAction method to the Giraffe type response
            Collider collider = new Collider();
            Action<Entity> hitAction = (Entity subject) =>
            {
                subject.Delete();
                Delete();
            };
            collider.AddResponse<Giraffe>(hitAction);
            // Adds the Collider component to the Bullet entity
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
