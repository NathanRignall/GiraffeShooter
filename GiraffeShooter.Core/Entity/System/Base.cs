using System;
using System.Collections.Generic;

namespace GiraffeShooterClient.Entity
{
    class BaseComponent<T> where T : Component
    {

        public static List<T> components = new List<T>();

        public static void Register(T component)
        {
            components.Add(component);
        }

        public static void Deregister(T component)
        {
            components.Remove(component);
        }

        public static void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            foreach (var component in components)
            {
                component.Update(gameTime);
            }
        }

        public static void Draw(Microsoft.Xna.Framework.GameTime gameTime, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            foreach (var component in components)
            {
                component.Draw(gameTime, spriteBatch);
            }
        }

    }

    class PhysicsSystem : BaseComponent<Physics> { }
    class ColliderSystem : BaseComponent<Collider> { }
    class ControlSystem : BaseComponent<Control> { }
    class TiledSystem : BaseComponent<Tiled> { }
    class SpriteSystem : BaseComponent<Sprite> { }
    class TextSystem : BaseComponent<Text> { }
    
    class Base
    {
        public static void Clear()
        {
            PhysicsSystem.components.Clear();
            ColliderSystem.components.Clear();
            ControlSystem.components.Clear();
            TiledSystem.components.Clear();
            SpriteSystem.components.Clear();
            TextSystem.components.Clear();
        }
    }
}