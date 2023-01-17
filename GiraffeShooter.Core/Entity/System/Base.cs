using System.Collections.Generic;

namespace GiraffeShooterClient.Entity
{
    class BaseComponent<T> where T : Component
    {

        public static List<T> components = new List<T>();
        public static List<T> componentsToRemove = new List<T>();

        public static void Register(T component)
        {
            components.Add(component);
        }

        public static void Deregister(T component)
        {
            componentsToRemove.Add(component);
        }

        public static void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            CleanUp();
            
            foreach (var component in components)
            {
                component.Update(gameTime);
            }
        }

        public static void Draw(Microsoft.Xna.Framework.GameTime gameTime, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            CleanUp();
            
            foreach (var component in components)
            {
                component.Draw(gameTime, spriteBatch);
            }
        }
        
        private static void CleanUp()
        {
            // remove all components that have been marked for removal
            foreach (var component in componentsToRemove)
            {
                components.Remove(component);
            }
            
            /// clear the list of components to remove
            componentsToRemove.Clear();
        }

    }

    class ScreenSystem : BaseComponent<Screen> { }
    class PhysicsSystem : BaseComponent<Physics> { }
    class ColliderSystem : BaseComponent<Collider> { }
    class ControlSystem : BaseComponent<Control> { }
    class TiledSystem : BaseComponent<Tiled> { }
    class AnimationSystem : BaseComponent<Animation> { }
    class SpriteSystem : BaseComponent<Sprite> { }
    class TextSystem : BaseComponent<Text> { }
    class TextInputSystem : BaseComponent<TextInput> { }
    
    class Base
    {
        public static void Clear()
        {
            ScreenSystem.components.Clear();
            PhysicsSystem.components.Clear();
            ColliderSystem.components.Clear();
            ControlSystem.components.Clear();
            TiledSystem.components.Clear();
            AnimationSystem.components.Clear();
            SpriteSystem.components.Clear();
            TextSystem.components.Clear();
            TextInputSystem.components.Clear();
        }
    }
}