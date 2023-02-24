using Microsoft.Xna.Framework;

using GiraffeShooterClient.Utility;

namespace GiraffeShooterClient.Entity
{
    public class PausedCover : Entity
    {
        public PausedCover(Vector2 offset, ScreenManager.CenterType center = ScreenManager.CenterType.MiddleCenter)
        {
            Id = System.Guid.NewGuid();
            Name = "PausedCover";
            
            Screen screen = new Screen(offset, center);
            AddComponent(screen);
            
            Sprite sprite = new Sprite(AssetManager.PausedCoverTexture);
            sprite.zOrder = 8;
            sprite.Scale = 10;
            AddComponent(sprite);
        }
    }
}

