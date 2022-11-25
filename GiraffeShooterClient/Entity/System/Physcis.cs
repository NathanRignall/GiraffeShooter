using Microsoft.Xna.Framework;

namespace GiraffeShooterClient.Entity.System
{
    class Physics : Component
    {
        public Vector3 position = Vector3.Zero;
        public Vector3 velocity = Vector3.Zero;
        public Vector3 acceleration = Vector3.Zero;

        public Physics()
        {
            PhysicsSystem.Register(this);
        }

        public override void Update(GameTime gameTime)
        {
            var dt = gameTime.ElapsedGameTime;

            velocity += acceleration * (float)dt.TotalSeconds;;
            position += velocity * (float)dt.TotalSeconds;
            
            velocity *= 0.99f;
        }

        public override void Deregister()
        {
            PhysicsSystem.Deregister(this);
        }
    }
}