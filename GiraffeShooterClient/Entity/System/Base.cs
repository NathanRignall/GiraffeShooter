using System.Collections.Generic;

namespace GiraffeShooterClient.Entity.System
{
    class Base<T> where T : Component
    {

        public static List<T> components = new List<T>();

        public static void Register(T component)
        {
            components.Add(component);
        }

        public static void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            foreach (T component in components)
            {
                component.Update(gameTime);
            }
        }

        public static void Draw(Microsoft.Xna.Framework.GameTime gameTime, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            foreach (T component in components)
            {
                component.Draw(gameTime, spriteBatch);
            }
        }

    }

    class TransformSystem : Base<Transform> { }
    class ColliderSystem : Base<Collider> { }
    class SpriteSystem : Base<Sprite> { }
}