
namespace GiraffeShooterClient.Entity.System
{
    class Collider : Component
    {

        public Collider()
        {
            ColliderSystem.Register(this);
        }

        public override void Deregister()
        {
            ColliderSystem.Deregister(this);
        }

    }
}