using  Microsoft.Xna.Framework;

namespace GiraffeShooterClient.Entity.System
{
    class Transform : Component
    {
        public Vector3 position = Vector3.Zero;
        public float layerDepth = 0;
        public float rotation = 0;

        public Transform()
        {
            TransformSystem.Register(this);
        }
    }
}