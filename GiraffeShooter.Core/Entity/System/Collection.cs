using System;
using System.Collections.Generic;

using GiraffeShooterClient.Utility;

namespace GiraffeShooterClient.Entity
{
    class Collection
    {
        public List<Entity> entities = new List<Entity>();
        public List<Entity> entitiesToRemove = new List<Entity>();

        public Guid AddEntity(Entity entity)
        {
            entities.Add(entity);
            return entity.Id;
        }

        public void RemoveEntity(Entity entity)
        {
            entitiesToRemove.Add(entity);
        }

        public Entity GetEntity(Guid id)
        {
            foreach (Entity entity in entities)
            {
                if (entity.Id == id)
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
        
        public void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            CleanUp();
            
            foreach (var entity in entities)
            {
                entity.Update(gameTime);
            }
        }

        public void HandleEvents(List<Event> events)
        {
            CleanUp();
            
            foreach (var entity in entities)
            {
                entity.HandleEvents(events);
            }
        }

        private void CleanUp()
        {
            // check if any entities are marked for deletion
            foreach (Entity entity in entities)
            {
                if (entity.IsDeleted)
                {
                    entitiesToRemove.Add(entity);
                }
            }

            // remove all entities marked for deletion
            foreach (Entity entity in entitiesToRemove)
            {
                entities.Remove(entity);
            }

            // clear the list of entities to remove
            entitiesToRemove.Clear();
        }
    }
}