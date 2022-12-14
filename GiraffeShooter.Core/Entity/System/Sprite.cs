using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using GiraffeShooterClient.Utility;

namespace GiraffeShooterClient.Entity
{
    class Sprite : Component
    {
        public Texture2D Texture { get; private set; }
        public Rectangle SourceRectangle;
        
        public Rectangle Bounds { get; private set; }

        public Sprite(Texture2D texture)
        {
            Texture = texture;
            SourceRectangle = new Rectangle(0, 0, texture.Width, texture.Height);
            SpriteSystem.Register(this);
        }
        
        public Sprite(Texture2D texture, Rectangle sourceRectangle)
        {
            Texture = texture;
            SourceRectangle = sourceRectangle;
            SpriteSystem.Register(this);
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            // calculate the position of the sprite
            var physics = entity.GetComponent<Physics>();
            var cameraOffset = Camera.Offset;
            var position = (new Vector2(physics.Position.X, physics.Position.Y) * 32f ) + cameraOffset - (new Vector2(SourceRectangle.Width / 2, SourceRectangle.Height / 2));
            var destinationRectangle = new Rectangle((int)position.X, (int)position.Y, SourceRectangle.Width, SourceRectangle.Height);

            // draw the sprite
            spriteBatch.Draw(Texture, destinationRectangle, SourceRectangle, Color.White);
            
            // update bounds
            Bounds = new Rectangle((int)position.X, (int)position.Y, SourceRectangle.Width, SourceRectangle.Height);
        }

        public override void Deregister()
        {
            SpriteSystem.Deregister(this);
        }
    }
}