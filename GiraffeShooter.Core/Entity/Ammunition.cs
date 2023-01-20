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
    }

    class Ammunition : Entity
    {
        MetaAmmunition meta;
        
        public Ammunition(Vector3 position)
        {
            Id = Guid.NewGuid();
            Name = "Ammunition";
            meta = new MetaAmmunition(10);
            
            Physics physics = new Physics();
            physics.Position = position;
            AddComponent(physics);
            
            Collider collider = new Collider();
            Action<Entity> pickupAction = (Entity subject) =>
            {
                if (subject.GetComponent<Inventory>().AddItem(meta))
                    Delete();
            };
            collider.AddResponse<Player>(pickupAction);
            AddComponent(collider);

            Sprite sprite = new Sprite(AssetManager.AmmunitionTexture);
            AddComponent(sprite);
        }
    }
}