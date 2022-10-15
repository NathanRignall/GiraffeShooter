using Microsoft.Xna.Framework;

using GiraffeShooterClient.Utility.Assets;

namespace GiraffeShooterClient.Entity.System
{
    class Giraffe : Entity
    {
        public Giraffe()
        {
            Transform transform = new Transform();
            transform.position = new Vector3(100, 0, 0);
            AddComponent(transform);

            Sprite sprite = new Sprite();
            sprite.texture = AssetManager.GiraffeTextureTest;
            AddComponent(sprite);
        }

        public void SetPosition(Vector3 position)
        {
            GetComponent<Transform>().position = position;
        }

        public void Delete()
        {
            RemoveComponent<Transform>();
            RemoveComponent<Sprite>();
        }
    }

}