using GiraffeShooterClient.Utility;
using Microsoft.Xna.Framework;

namespace GiraffeShooterClient.Entity
{
    class Ammunition : Entity
    {
        public Ammunition(Vector3 position)
        {
            id = System.Guid.NewGuid();
            name = "Ammunition";
            
            Physics physics = new Physics();
            physics.Position = position;
            AddComponent(physics);
            
            Collider collider = new Collider();
            AddComponent(collider);
            
            Sprite sprite = new Sprite(AssetManager.AmmunitionSpriteTexture);
            AddComponent(sprite);

        }
    }
}