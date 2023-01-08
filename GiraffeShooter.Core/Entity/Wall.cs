using GiraffeShooterClient.Utility;
using Microsoft.Xna.Framework;

namespace GiraffeShooterClient.Entity
{
    class Wall : Entity
    {
        public Wall(Vector3 position, Vector3 size)
        {
            id = new System.Guid();
            name = "Wall";

            Physics physics = new Physics();
            physics.IsStatic = true;
            physics.Position = position;
            physics.Size = size;
            AddComponent(physics);
            
            Collider collider = new Collider();
            AddComponent(collider);
            
            Sprite sprite = new Sprite(AssetManager.GiraffeTextureTest, new Rectangle(0,0,(int)(size.X*32f),(int)(size.Y*32f)));
            AddComponent(sprite);
        }
    }
}

