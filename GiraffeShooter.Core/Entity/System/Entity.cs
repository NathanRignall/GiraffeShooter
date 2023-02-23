using System;
using System.Collections.Generic;

using GiraffeShooterClient.Utility;

namespace GiraffeShooterClient.Entity
{
    class Entity
    {
        public Guid Id { get; protected set; }
        public string Name { get; protected set; }
        public bool IsDeleted { get; protected set; }
        public Meta Meta { get; protected set; }

        List<Component> _components = new List<Component>();

        public void AddComponent(Component component)
        {
            _components.Add(component);
            component.entity = this;
        }

        public T GetComponent<T>() where T : Component
        {
            foreach (Component component in _components)
            {
                if (component.GetType().Equals(typeof(T)))
                {
                    return (T)component;
                }
            }
            
            return null;
        }

        public void RemoveComponent<T>() where T : Component
        {
            foreach (Component component in _components)
            {
                if (component.GetType().Equals(typeof(T)))
                {
                    component.Deregister();
                    _components.Remove(component);

                    break;
                }
            }
        }
        
        public bool HasComponent<T>() where T : Component
        {
            foreach (Component component in _components)
            {
                if (component.GetType().Equals(typeof(T)))
                {
                    return true;
                }
            }
            
            return false;
        }
        
        public virtual void Update(Microsoft.Xna.Framework.GameTime gameTime) { }

        public virtual void HandleEvents(List<Event> events) { }

        public void Delete()
        {
            foreach (Component component in _components)
            {
                component.Deregister();
            }

            IsDeleted = true;
        }
    }
}