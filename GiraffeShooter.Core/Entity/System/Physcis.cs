using Microsoft.Xna.Framework;

namespace GiraffeShooterClient.Entity
{
    class Physics : Component
    {
        public bool IsStatic = false;
        public bool IsKinematic = false;

        public Vector3 Position = Vector3.Zero;
        public Vector3 Velocity = Vector3.Zero;
        public Vector3 Acceleration = Vector3.Zero;
        public float deceleration = 0.99f;

        public Vector3 Size = Vector3.Zero;
        public float mass = 1.0f;

        public BoundingBox BoundingBox
        {
            get
            {
                return new BoundingBox(Position - Size / 2, Position + Size / 2);
            }
        }

        public Physics()
        {
            PhysicsSystem.Register(this);
        }

        public override void Update(GameTime gameTime)
        {

            if (IsStatic) return;
            
            var dt = gameTime.ElapsedGameTime;

            Velocity += Acceleration * (float)dt.TotalSeconds;;
            Position += Velocity * (float)dt.TotalSeconds;
            
            Velocity *= deceleration;
        }

        public override void Deregister()
        {
            PhysicsSystem.Deregister(this);
        }
    }
}