using System;

using Microsoft.Xna.Framework;

using GiraffeShooterClient.Utility;

namespace GiraffeShooterClient.Entity
{
    class Ammunition : Entity
    {
        public Ammunition(Vector3 position)
        {
            Id = Guid.NewGuid();
            Name = "Ammunition";
            
            Physics physics = new Physics();
            physics.Position = position;
            AddComponent(physics);
            
            Collider collider = new Collider();
            Action pickupAction = Delete;
            collider.AddResponse<Player>(pickupAction);
            AddComponent(collider);

            Sprite sprite = new Sprite(AssetManager.AmmunitionSpriteTexture);
            AddComponent(sprite);

        }
    }
}