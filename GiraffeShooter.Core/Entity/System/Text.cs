using System;

using Microsoft.Xna.Framework;

using GiraffeShooterClient.Utility;

namespace GiraffeShooterClient.Entity
{
    class Text : Component
    {
        public string String { get; set; } = "";
        public bool Visible { get; set; } = true;
        public Vector2 Offset;

        public Text()
        {
            TextSystem.Register(this);
        }
        
        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            if (!Visible) return;
            
            // set font
            var font = ScreenManager.GetScaleFactor() == 1 ? AssetManager.Fontx1Normal : AssetManager.Fontx2Normal;
                
            // if has a physics component, use its position
            if (entity.HasComponent<Physics>())
            {
                // calculate the position of the text
                var physics = entity.GetComponent<Physics>();
                var cameraOffset = Camera.Offset;
                var position = (new Vector2(physics.Position.X, physics.Position.Y) * 32f ) + cameraOffset - new Vector2((int)font.MeasureString(String).X / 2, (int)font.MeasureString(String).Y / 2) / ScreenManager.GetScaleFactor();
            
                // draw the text
                spriteBatch.DrawString(font, String, position * Camera.Zoom, Color.White);
            }
            
            // if has a screen component, use its position
            if (entity.HasComponent<Screen>())
            {
                // calculate the position of the text
                var screen = entity.GetComponent<Screen>();
                var basePosition = ScreenManager.GetCenter(screen.Center) / ScreenManager.GetScaleFactor();
                var position = basePosition - screen.Offset * 32f + Offset -  new Vector2((int)font.MeasureString(String).X / 2, (int)font.MeasureString(String).Y / 2) / ScreenManager.GetScaleFactor();
                
                // draw the text
                spriteBatch.DrawString(font, String, position * (float)ScreenManager.GetScaleFactor(), Color.White);
            }
            

        }
        
        public override void Deregister()
        {
            TextSystem.Deregister(this);
        }
            
    }
}