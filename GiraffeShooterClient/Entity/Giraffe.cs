using Microsoft.Xna.Framework;

using GiraffeShooterClient.Utility.Assets;

namespace GiraffeShooterClient.Entity.System
{
    class Giraffe : Entity
    {
        public Giraffe(/* Texture2D tex */)
        {
            Transform transform = new Transform();
            transform.position = new Vector3(100, 100, 0);
            AddComponent(transform);

            Sprite sprite = new Sprite();
            sprite.texture = AssetManager.GiraffeTextureTest;
            AddComponent(sprite);
        }
    }

}