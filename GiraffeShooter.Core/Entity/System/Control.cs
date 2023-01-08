using Microsoft.Xna.Framework;

namespace GiraffeShooterClient.Entity
{
    class Control : Component
    {
        public enum Direction { up, down, left, right };
        private int speed = 10;

        public Control()
        {
            ControlSystem.Register(this);
        }

        public void Move(Direction direction) {
            Physics physics = entity.GetComponent<Physics>();

            switch (direction) {
                case Direction.up:
                    physics.Velocity.Y = -speed;
                    break;
                case Direction.down:
                    physics.Velocity.Y = speed;
                    break;
                case Direction.left:
                    physics.Velocity.X = -speed;
                    break;
                case Direction.right:
                    physics.Velocity.X = speed;
                    break;
            }
        }

        public override void Deregister()
        {
            ControlSystem.Deregister(this);
        }
    }
}