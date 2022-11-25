using Microsoft.Xna.Framework;

using GiraffeShooterClient.Utility.Assets;

namespace GiraffeShooterClient.Entity.System
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

        public void SetAcceleration(Vector3 acceleration)
        {
            Physics physics = GetComponent<Physics>();
            physics.acceleration = acceleration;
        }

        public void Delete()
        {
            RemoveComponent<Physics>();
            RemoveComponent<Sprite>();
        }
    }

}