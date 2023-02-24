using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using GiraffeShooterClient.Utility;

namespace GiraffeShooterClient.Entity
{
    public class Sprite : Component
    {
        public Texture2D Texture { get; set; }
        public int Scale { get; set; } = 1;
        public Rectangle SourceRectangle { get; set; }
        public Rectangle Bounds { get; private set; }
        public bool Centered { get; set; } = true;
        public float Rotation { get; set; }
        public bool Visible { get; set; } = true;
        
        public Vector2 Offset;

        public Sprite(Texture2D texture, Vector2 offset = default(Vector2))
        {
            Texture = texture;
            SourceRectangle = new Rectangle(0, 0, texture.Width, texture.Height);
            Offset = offset;
            SpriteSystem.Register(this);
        }
        
        public Sprite(Texture2D texture, Rectangle sourceRectangle, Vector2 offset = default(Vector2))
        {
            Texture = texture;
            SourceRectangle = sourceRectangle;
            Offset = offset;
            SpriteSystem.Register(this);
        }
        
        public void UpdateSprite(Texture2D texture, Vector2 offset = default(Vector2))
        {
            Texture = texture;
            SourceRectangle = new Rectangle(0, 0, texture.Width, texture.Height);
            Offset = offset;
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            if (!Visible) return;
            
            var destinationRectangle = new Rectangle();
            
            // if has a physics component, use its position
            if (entity.HasComponent<Physics>())
            {
                // calculate the position of the sprite
                var physics = entity.GetComponent<Physics>();
                var cameraOffset = Camera.Offset;
                var position = new Vector2(physics.Position.X, physics.Position.Y) * 32f  + cameraOffset + Offset - new Vector2(SourceRectangle.Width / 2, SourceRectangle.Height / 2);
                
                if (!Centered)
                    position = new Vector2(physics.Position.X, physics.Position.Y) * 32f + cameraOffset + Offset;
                
                // create the destination rectangle
                destinationRectangle = new Rectangle((int)Math.Ceiling(position.X * Camera.Zoom), (int)Math.Ceiling(position.Y * Camera.Zoom), 
                    (int)Math.Ceiling(SourceRectangle.Width * Camera.Zoom), (int)Math.Ceiling(SourceRectangle.Height * Camera.Zoom));
                
                // update bounds
                Bounds = new Rectangle((int)position.X, (int)position.Y, SourceRectangle.Width, SourceRectangle.Height);
            }
            
            // if has a screen component, use its position
            if (entity.HasComponent<Screen>())
            {
                var screen = entity.GetComponent<Screen>();
                var basePosition = ScreenManager.GetCenter(screen.Center) / ScreenManager.GetScaleFactor();
                var position = basePosition - screen.Offset * 32f + Offset - new Vector2(SourceRectangle.Width / 2 * Scale, SourceRectangle.Height / 2 * Scale);
                
                if (!Centered)
                    position = basePosition - screen.Offset * 32f + Offset;
                
                // create the destination rectangle
                destinationRectangle = new Rectangle((int)Math.Ceiling(position.X * ScreenManager.GetScaleFactor()), (int)Math.Ceiling(position.Y * ScreenManager.GetScaleFactor()), 
                    (int)Math.Ceiling(SourceRectangle.Width * (float)ScreenManager.GetScaleFactor() * Scale), (int)Math.Ceiling(SourceRectangle.Height * (float)ScreenManager.GetScaleFactor() * Scale));
                
                // update bounds
                Bounds = new Rectangle((int)position.X, (int)position.Y, SourceRectangle.Width * Scale, SourceRectangle.Height * Scale);
            }

            // draw the sprite
            spriteBatch.Draw(Texture, destinationRectangle, SourceRectangle, Color.White, Rotation, Vector2.Zero, SpriteEffects.None, 0f);
            
            
        }

        public override void Deregister()
        {
            SpriteSystem.Deregister(this);
        }
    }
}