using System;
using Microsoft.Xna.Framework;
using GiraffeShooterClient.Utility;

namespace GiraffeShooterClient.Entity
{
    // Inheritance: MetaAmmunition inherits from Meta class
    class MetaAmmunition : Meta
    {
        // Constructor for MetaAmmunition class with a quantity parameter
        public MetaAmmunition(int quantity)
        {
            Quantity = quantity;
        }
 
        // This method is an implementation of the abstract method in the parent class, Meta
        public override void Create(Vector3 position, Vector3 velocity)
        {
            // Creates a new Ammunition object
            new Ammunition(position, velocity, Id,this);
        }
    }

    // Inheritance: Ammunition inherits from Entity class
    class Ammunition : Entity
    {
        MetaAmmunition _meta;
        
        // Constructor for Ammunition class
        public Ammunition(Vector3 position, Vector3 velocity, Guid id = default, Meta meta = null)
        {
            // Assigns Id value or creates a new Guid if Id value is default
            Id = (id == default) ? Guid.NewGuid() : id;
            // Assigns Name value as "Ammunition"
            Name = "Ammunition";
            // Assigns Meta value as meta parameter if not null, otherwise creates a new MetaBullet object
            Meta = (meta == null) ? new MetaBullet() : (MetaGiraffe)meta;
            
            // Assigns _meta value as a new MetaAmmunition object if meta parameter is null, otherwise assigns it as the meta parameter casted to MetaAmmunition type
            if (meta == null)
                _meta = new MetaAmmunition(10);
            else
                _meta = (MetaAmmunition)meta;

            // Creates a new Physics object and assigns the position, velocity and deceleration values
            Physics physics = new Physics();
            physics.Position = position;
            physics.Velocity = velocity;
            physics.deceleration = 0.8f;
            // Adds the Physics component to the Ammunition entity
            AddComponent(physics);
            
            // Creates a new Collider object and assigns a pickupAction method to the Player and Giraffe type responses
            Collider collider = new Collider();
            Action<Entity> pickupAction = (Entity subject) =>
            {
                // Adds the _meta object to the Inventory component of the subject entity and deletes the Ammunition entity if successful
                if (subject.GetComponent<Inventory>().AddItem(_meta))
                    Delete();
            };
            collider.AddResponse<Player>(pickupAction);
            collider.AddResponse<Giraffe>(pickupAction);
            // Adds the Collider component to the Ammunition entity
            AddComponent(collider);

            // Creates a new Sprite object with AssetManager.AmmunitionTexture and adds it to the Ammunition entity
            Sprite sprite = new Sprite(AssetManager.AmmunitionTexture);
            AddComponent(sprite);
        }
    }
}
