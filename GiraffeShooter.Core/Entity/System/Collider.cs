using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;

namespace GiraffeShooterClient.Entity
{
    class Collider : Component
    {
        
        // dictionary of component types and their respective collision responses
        private Dictionary<Type, Action> _responses = new Dictionary<Type, Action>();

        public Collider()
        {
            ColliderSystem.Register(this);
        }
        
        public void AddResponse<T>(Action response) where T : Entity
        {
            _responses.Add(typeof(T), response);
        }

        public void RemoveResponse<T>() where T : Entity
        {
            _responses.Remove(typeof(T));
        }

        public override void Update(GameTime gameTime)
        {

            foreach (Collider collider in ColliderSystem.components)
            {
                
                if (collider != this)
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
                        if (_responses.ContainsKey(collider.entity.GetType()))
                        {
                            Console.WriteLine("Collision detected between " + entity.name + " and " + collider.entity.name);
                            _responses[collider.entity.GetType()]();
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
                            entity.GetComponent<Physics>().Position = entity.GetComponent<Physics>().Position + entity.GetComponent<Physics>().Velocity;

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