using GiraffeShooterClient.Utility;
using Microsoft.Xna.Framework;

namespace GiraffeShooterClient.Entity
{
    class Screen : Component
    {

        public Vector2 Offset = Vector2.Zero; 
        public ScreenManager.CenterType Center = ScreenManager.CenterType.TopLeft;

        public Screen()
        {
            ScreenSystem.Register(this);
        }

        public override void Deregister()
        {
            ScreenSystem.Deregister(this);
        }
    }
}

