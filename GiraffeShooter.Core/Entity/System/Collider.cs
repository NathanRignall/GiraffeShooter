using Microsoft.Xna.Framework;
namespace GiraffeShooterClient.Entity
{
    class Collider : Component
    {

        public Collider()
        {
            ColliderSystem.Register(this);
        }

        public override void Update(GameTime gameTime)
        {

            // foreach (Collider collider in ColliderSystem.components)
            // {
            //     if (collider != this)
            //     {   
                    
            //         // check if the two cubes are intersecting
            //         if (collider.entity.GetComponent<Physics>().BoundingBox.Intersects(entity.GetComponent<Physics>().BoundingBox))
            //         {
            //             // write the collision to the console
            //             System.Console.WriteLine("Collision detected between " + entity.name + " and " + collider.entity.name);

            //             // write bounding box information to the console
            //             System.Console.WriteLine("Entity 1: " + entity.GetComponent<Physics>().BoundingBox.ToString());
            //             System.Console.WriteLine("Entity 2: " + collider.entity.GetComponent<Physics>().BoundingBox.ToString());

            //             // calculate the objects momentum
            //             Vector3 entityMomentum = entity.GetComponent<Physics>().velocity * entity.GetComponent<Physics>().mass;

            //             // calculate the colliders momentum
            //             Vector3 colliderMomentum = collider.entity.GetComponent<Physics>().velocity * collider.entity.GetComponent<Physics>().mass;

            //             // calculate the total momentum
            //             Vector3 totalMomentum = entityMomentum + colliderMomentum;

            //             // calculate the total mass
            //             float totalMass = entity.GetComponent<Physics>().mass + collider.entity.GetComponent<Physics>().mass;
                        

            //         }

            //     }
            // }

        }

        public override void Deregister()
        {
            ColliderSystem.Deregister(this);
        }

    }
}