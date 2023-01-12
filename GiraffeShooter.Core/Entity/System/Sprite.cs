using System;

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
        
        private Vector2 _offset;

        public Sprite(Texture2D texture, Vector2 offset = default(Vector2))
        {
            Texture = texture;
            SourceRectangle = new Rectangle(0, 0, texture.Width, texture.Height);
            _offset = offset;
            SpriteSystem.Register(this);
        }
        
        public Sprite(Texture2D texture, Rectangle sourceRectangle, Vector2 offset = default(Vector2))
        {
            Texture = texture;
            SourceRectangle = sourceRectangle;
            _offset = offset;
            SpriteSystem.Register(this);
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            // calculate the position of the sprite
            var physics = entity.GetComponent<Physics>();
            var cameraOffset = Camera.Offset;
            var position = new Vector2(physics.Position.X, physics.Position.Y) * 32f  + cameraOffset + _offset - new Vector2(SourceRectangle.Width / 2, SourceRectangle.Height / 2);
            var destinationRectangle = new Rectangle((int)Math.Ceiling(position.X * Camera.Zoom), (int)Math.Ceiling(position.Y * Camera.Zoom), 
                (int)Math.Ceiling(SourceRectangle.Width * Camera.Zoom), (int)Math.Ceiling(SourceRectangle.Height * Camera.Zoom));

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