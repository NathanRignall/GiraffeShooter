using GiraffeShooterClient.Utility;
using Microsoft.Xna.Framework;

namespace GiraffeShooterClient.Entity
{
    class Screen : Component
    {

        public Vector2 Offset;
        public ScreenManager.CenterType Center;

        public Screen(Vector2 offset, ScreenManager.CenterType center = ScreenManager.CenterType.TopLeft)
        {
            Offset = offset;
            Center = center;
            ScreenSystem.Register(this);
        }

        public override void Deregister()
        {
            ScreenSystem.Deregister(this);
        }
    }
}

