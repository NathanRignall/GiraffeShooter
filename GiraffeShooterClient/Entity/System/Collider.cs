
namespace GiraffeShooterClient.Entity.System
{
    class Collider : Component
    {
        // implementation of a Collider component

        public Collider()
        {
            ColliderSystem.Register(this);
        }

    }
}