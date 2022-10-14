using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using GiraffeShooterClient.Utility.Assets;

namespace GiraffeShooterClient.Entity.System
{
    class Sprite : Component
    {
        public Texture2D texture;

        public Sprite()
        {
            SpriteSystem.Register(this);
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {

            Transform transform = entity.GetComponent<Transform>();

            spriteBatch.Draw(texture, new Vector2(260, 30), Color.White);

        }
    }
}