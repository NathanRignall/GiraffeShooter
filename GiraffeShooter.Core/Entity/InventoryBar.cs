using System;
using GiraffeShooterClient.Utility;
using Microsoft.Xna.Framework;

namespace GiraffeShooterClient.Entity
{
    class InventoryBar : Entity
    {
        public InventoryBar()
        {
            Id = Guid.NewGuid();
            Name = "InventoryBar";

            Screen screen = new Screen();
            screen.Center = ScreenManager.CenterType.BottomCenter;
            screen.Offset = new Vector2(0f, 3.5f);
            AddComponent(screen);

            Sprite sprite = new Sprite(AssetManager.InventoryBarTexture, new Rectangle(0, 0, 242, 64));
            sprite.Scale = 2;
            AddComponent(sprite);
        }
    }
}
