using System;
using System.Collections.Generic;

using GiraffeShooterClient.Utility;

namespace GiraffeShooterClient.Entity
{
    class Entity
    {
        public Guid id { get; protected set; }
        public string name { get; protected set; }

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

        public virtual void HandleEvents(List<Event> events)
        {

        }
    }
}