using System;
using Microsoft.Xna.Framework;

namespace GiraffeShooterClient.Entity
{
    class Control : Component
    {
        public enum Direction { up, down, left, right };
        public int Speed { get; set; } = 10;

        public Control()
        {
            ControlSystem.Register(this);
        }

        public void Move(Direction direction) {
            Physics physics = entity.GetComponent<Physics>();

            switch (direction) {
                case Direction.up:
                    physics.Velocity.Y = -Speed;
                    break;
                case Direction.down:
                    physics.Velocity.Y = Speed;
                    break;
                case Direction.left:
                    physics.Velocity.X = -Speed;
                    break;
                case Direction.right:
                    physics.Velocity.X = Speed;
                    break;
            }
        }

        public void Move(float angle, float speedFactor)
        {
            Physics physics = entity.GetComponent<Physics>();
            physics.Velocity.X = (float)Math.Cos(angle) * Speed * speedFactor;
            physics.Velocity.Y = (float)Math.Sin(angle) * Speed * speedFactor;
        }

        public override void Deregister()
        {
            ControlSystem.Deregister(this);
        }
    }
}