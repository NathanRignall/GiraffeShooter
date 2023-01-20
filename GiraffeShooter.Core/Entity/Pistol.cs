using System;

using Microsoft.Xna.Framework;

using GiraffeShooterClient.Utility;

namespace GiraffeShooterClient.Entity
{
    class MetaPistol : Meta
    {

    }

    class Pistol : Entity
    {
        MetaPistol meta;
        
        public Pistol(Vector3 position)
        {
            Id = Guid.NewGuid();
            Name = "Pistol";
            meta = new MetaPistol();
            
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

            Sprite sprite = new Sprite(AssetManager.PistolTexture);
            AddComponent(sprite);
        }
    }
}