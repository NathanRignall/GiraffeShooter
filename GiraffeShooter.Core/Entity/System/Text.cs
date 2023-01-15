using System;

using Microsoft.Xna.Framework;

using GiraffeShooterClient.Utility;

namespace GiraffeShooterClient.Entity
{
    class Text : Component
    {
        public string String { get; set; }

        public Text()
        {
            TextSystem.Register(this);
        }
        
        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            // set font
            var font = ScreenManager.GetScaleFactor() == 1 ? AssetManager.Fontx1Normal : AssetManager.Fontx2Normal;
            
            // calculate the position of the text
            Physics physics = entity.GetComponent<Physics>();
            Vector2 cameraOffset = Camera.Offset;
            Vector2 position = (new Vector2(physics.Position.X, physics.Position.Y) * 32f ) + cameraOffset - new Vector2((int)font.MeasureString(String).X / 2, (int)font.MeasureString(String).Y / 2) / ScreenManager.GetScaleFactor();
            
            // draw the text
            spriteBatch.DrawString(font, String, position * Camera.Zoom, Color.White);

        }
        
        public override void Deregister()
        {
            TextSystem.Deregister(this);
        }
            
    }
}