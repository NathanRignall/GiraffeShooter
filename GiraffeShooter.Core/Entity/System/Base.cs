using System.Collections.Generic;

namespace GiraffeShooterClient.Entity
{
    public class BaseComponent<T> where T : Component
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
            
            // loop through -10 to 10
            for (int i = -10; i <= 10; i++)
            {
                foreach (var component in components)
                {
                    if (component.zOrder == i)
                    {
                        component.Draw(gameTime, spriteBatch);
                    }
                }
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

    public class ScreenSystem : BaseComponent<Screen> { }
    public class PhysicsSystem : BaseComponent<Physics> { }
    public class ColliderSystem : BaseComponent<Collider> { }
    public class ControlSystem : BaseComponent<Control> { }
    public class TiledSystem : BaseComponent<Tiled> { }
    public class AnimationSystem : BaseComponent<Animation> { }
    public class SpriteSystem : BaseComponent<Sprite> { }
    public class TextSystem : BaseComponent<Text> { }
    public class InputSystem : BaseComponent<Input> { }
    public class InventorySystem : BaseComponent<Inventory> { }
    public class AimSystem : BaseComponent<Aim> { }
    public class HealthSystem : BaseComponent<Health> { }
    public class CleanerSystem : BaseComponent<Cleaner> { }
    public class BotSystem : BaseComponent<Bot> { }
    
    public class Base
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
            InputSystem.components.Clear();
            InventorySystem.components.Clear();
            AimSystem.components.Clear();
            HealthSystem.components.Clear();
            CleanerSystem.components.Clear();
            BotSystem.components.Clear();
        }
    }
}