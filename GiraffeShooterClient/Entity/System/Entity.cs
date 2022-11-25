using System.Collections.Generic;

namespace GiraffeShooterClient.Entity
{
    class Entity
    {
        public int ID { get; set; }

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
    }
}