using Microsoft.Xna.Framework;

namespace GiraffeShooterClient.Entity
{
    class Physics : Component
    {
        public bool isStatic = false;

        public Vector3 position = Vector3.Zero;
        public Vector3 velocity = Vector3.Zero;
        public Vector3 acceleration = Vector3.Zero;
        public float deceleration = 0.99f;

        public Vector3 size = Vector3.Zero;
        public float mass = 1.0f;

        public Physics()
        {
            PhysicsSystem.Register(this);
        }

        public override void Update(GameTime gameTime)
        {

            if (isStatic) return;
            
            var dt = gameTime.ElapsedGameTime;

            velocity += acceleration * (float)dt.TotalSeconds;;
            position += velocity * (float)dt.TotalSeconds;
            
            velocity *= deceleration;
        }

        public override void Deregister()
        {
            PhysicsSystem.Deregister(this);
        }
    }
}