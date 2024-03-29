using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;

using GiraffeShooterClient.Utility;

namespace GiraffeShooterClient.Entity
{
    public class Collider : Component
    {
        
        // dictionary of component types and their respective collision responses
        private Dictionary<Type, Action<Entity>> _responses = new Dictionary<Type, Action<Entity>>();

        public Collider()
        {
            ColliderSystem.Register(this);
        }
        
        public void AddResponse<T>(Action<Entity> response) where T : Entity
        {
            _responses.Add(typeof(T), response);
        }

        public void RemoveResponse<T>() where T : Entity
        {
            _responses.Remove(typeof(T));
        }

        public override void Update(GameTime gameTime)
        {
            if (ContextManager.Paused)
                return;
            
            // // temporary code to only check for collisions with a moving entity
            // if (entity.GetComponent<Physics>().Velocity == Vector3.Zero)
            //     return;

            foreach (Collider collider in ColliderSystem.components)
            {
                
                if (collider != this && collider.entity.IsDeleted == false)
                {   
                    
                    // check if the two cubes are intersecting
                    if (collider.entity.GetComponent<Physics>().BoundingBox.Intersects(entity.GetComponent<Physics>().BoundingBox))
                    {

                        // // write the collision to the console
                        // System.Console.WriteLine("Collision detected between " + entity.name + " and " + collider.entity.name);
                        //
                        // // write bounding box information to the console
                        // System.Console.WriteLine("Entity 1: " + entity.GetComponent<Physics>().BoundingBox.ToString());
                        // System.Console.WriteLine("Entity 2: " + collider.entity.GetComponent<Physics>().BoundingBox.ToString());
                        
                        // call the collision response
                        // if (_responses.ContainsKey(entity.GetType()))
                        // {
                        //     _responses[collider.entity.GetType()](collider.entity);
                        // }
                        
                        // call the collision response from the other entity
                        if (collider._responses.ContainsKey(entity.GetType()))
                        {
                            collider._responses[entity.GetType()](entity);
                        }

                        // no physics on kinematic objects
                        if (entity.GetComponent<Physics>().IsKinematic || collider.entity.GetComponent<Physics>().IsKinematic)
                        {
                            continue;
                        }
                        
                        // no collision between two static objects
                        if (entity.GetComponent<Physics>().IsStatic && collider.entity.GetComponent<Physics>().IsStatic)
                        {
                            continue;
                        }
                        
                        // if the second entity is static
                        if (collider.entity.GetComponent<Physics>().IsStatic)
                        {
                            // move the first entity out of the second entity (TEST CODE DOES NOT WORK)
                            entity.GetComponent<Physics>().Position = entity.GetComponent<Physics>().Position - entity.GetComponent<Physics>().Velocity / 2;
                            
                            // invert the velocity of the first entity
                            entity.GetComponent<Physics>().Velocity = -entity.GetComponent<Physics>().Velocity * 2;

                        }

                        // calculate the objects momentum
                        // Vector3 entityMomentum = entity.GetComponent<Physics>().velocity * entity.GetComponent<Physics>().mass;
                        //
                        // // calculate the colliders momentum
                        // Vector3 colliderMomentum = collider.entity.GetComponent<Physics>().velocity * collider.entity.GetComponent<Physics>().mass;
                        //
                        // // calculate the total momentum
                        // Vector3 totalMomentum = entityMomentum + colliderMomentum;
                        //
                        // // calculate the total mass
                        // float totalMass = entity.GetComponent<Physics>().mass + collider.entity.GetComponent<Physics>().mass;
                        
                    }

                }
            }

        }

        public override void Deregister()
        {
            ColliderSystem.Deregister(this);
        }

    }
}