using System;
using Microsoft.Xna.Framework;
using GiraffeShooterClient.Utility;

namespace GiraffeShooterClient.Entity
{
    class HealthBar : Entity
    {
        public HealthBar()
        {
            Id = Guid.NewGuid();
            Name = "HealthBar";

            Screen screen = new Screen(new Vector2(0f, -3.0f), ScreenManager.CenterType.TopCenter);
            AddComponent(screen);

            Sprite sprite = new Sprite(AssetManager.HealthBarTexture, new Rectangle(0, 0, 260, 21));
            sprite.Scale = 2;
            sprite.zOrder = 7;
            AddComponent(sprite);
        }
        
        public void SetHealth(int health)
        {
            Sprite sprite = GetComponent<Sprite>();
            sprite.SourceRectangle = new Rectangle(0, 0, 260 * health / 100, 21);
        }
    }
}

