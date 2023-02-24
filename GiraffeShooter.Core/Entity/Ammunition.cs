using System;
using Microsoft.Xna.Framework;
using GiraffeShooterClient.Utility;

namespace GiraffeShooterClient.Entity
{
    // Inheritance: MetaAmmunition inherits from Meta class
    public class MetaAmmunition : Meta
    {
        // Constructor for MetaAmmunition class with a quantity parameter
        public MetaAmmunition(int quantity)
        {
            MetaType = MetaType.Ammunition;
            Quantity = quantity;
            MaxQuantity = 100;
        }
 
        // This method is an implementation of the abstract method in the parent class, Meta
        public override void Create(Vector3 position, Vector3 velocity)
        {
            // Creates a new Ammunition object
            new Ammunition(position, velocity, Id,this);
        }
    }

    // Inheritance: Ammunition inherits from Entity class
    public class Ammunition : Entity
    {
        // Constructor for Ammunition class
        public Ammunition(Vector3 position, Vector3 velocity, Guid id = default, Meta meta = null)
        {
            Id = (id == default) ? Guid.NewGuid() : id;
            Name = "Ammunition";

            if (meta == null)
                Meta = new MetaAmmunition(10);
            else
                Meta = (MetaAmmunition)meta;

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
            collider.AddResponse<Player>(pickupAction);
            collider.AddResponse<Giraffe>(pickupAction);
            AddComponent(collider);
            
            Sprite sprite = new Sprite(AssetManager.AmmunitionTexture);
            AddComponent(sprite);
        }
    }
}
