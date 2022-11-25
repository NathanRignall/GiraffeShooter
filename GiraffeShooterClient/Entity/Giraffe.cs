using Microsoft.Xna.Framework;

using GiraffeShooterClient.Utility;

namespace GiraffeShooterClient.Entity
{
    class Giraffe : Entity
    {
        public Giraffe()
        {
            Physics physics = new Physics();
            physics.position = new Vector3(100, 0, 0);
            physics.velocity = new Vector3(0, 0, 0);
            AddComponent(physics);

            Sprite sprite = new Sprite();
            sprite.texture = AssetManager.GiraffeTextureTest;
            AddComponent(sprite);
        }

        public void Delete()
        {
            RemoveComponent<Physics>();
            RemoveComponent<Sprite>();
        }
    }

}