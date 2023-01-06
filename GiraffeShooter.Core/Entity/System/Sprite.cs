using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using GiraffeShooterClient.Utility;

namespace GiraffeShooterClient.Entity
{
    class Sprite : Component
    {
        public Texture2D texture;
        public Rectangle Bounds { get; set; }

        public Sprite()
        {
            SpriteSystem.Register(this);
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            // calculate the position of the sprite
            Physics physics = entity.GetComponent<Physics>();
            Vector2 cameraOffset = Camera.Offset;
            Vector2 position = (new Vector2(physics.position.X, physics.position.Y) * 1000f / 32f  ) + cameraOffset - new Vector2(texture.Width / 2, texture.Height / 2);

            // draw the sprite
            spriteBatch.Draw(texture, position, Color.White);
            
            // update bounds
            Bounds = new Rectangle((int)position.X, (int)position.Y, texture.Width, texture.Height);
        }

        public override void Deregister()
        {
            SpriteSystem.Deregister(this);
        }
    }
}