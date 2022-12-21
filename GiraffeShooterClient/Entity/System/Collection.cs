using System;
using System.Collections.Generic;

using GiraffeShooterClient.Utility;

namespace GiraffeShooterClient.Entity
{
    class Collection
    {
        public List<Entity> entities = new List<Entity>();

        public Guid AddEntity(Entity entity)
        {
            entities.Add(entity);
            return entity.id;
        }

        public void RemoveEntity(Entity entity)
        {
            entities.Remove(entity);
        }

        public Entity GetEntity(Guid id)
        {
            foreach (Entity entity in entities)
            {
                if (entity.id == id)
                {
                    return entity;
                }
            }
            return null;
        }

        public Entity GetRandomEntity()
        {
            Random random = new Random();
            return entities[random.Next(entities.Count)];
        }

        public List<Entity> GetEntities()
        {
            return entities;
        }

        public void HandleEvents(List<Event> events)
        {
            foreach (Entity entity in entities)
            {
                entity.HandleEvents(events);
            }
        }
    }
}