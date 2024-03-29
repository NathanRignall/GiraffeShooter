using System;

using Microsoft.Xna.Framework;

namespace GiraffeShooterClient.Entity
{
    public class Wall : Entity
    {
        public Wall(Vector3 position, Vector3 size)
        {
            Id = Guid.NewGuid();
            Name = "Wall";

            Physics physics = new Physics();
            physics.IsStatic = true;
            physics.Position = position;
            physics.Size = size;
            AddComponent(physics);
            
            Collider collider = new Collider();
            AddComponent(collider);
        }
    }
}

