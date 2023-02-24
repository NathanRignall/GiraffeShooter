using System;
using System.Collections.Generic;
using GiraffeShooterClient.Utility;

namespace GiraffeShooterClient.Container.Win
{
    public class WinContext : Context
    {
        private bool _isInitialized = false;
        TimeSpan _timeSpan;
        
        public WinContext()
        {
        }
        
        public override void HandleEvents(List<Event> events)
        {
            
        }
        
        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            // if is not initialized, set the time span
            if (!_isInitialized)
            {
                _timeSpan = gameTime.TotalGameTime;
                _isInitialized = true;
            }

            // wait 3 seconds before going back to the main menu
            if (gameTime.TotalGameTime - _timeSpan > TimeSpan.FromSeconds(3))
            {
                ContextManager.SetState(ContextManager.State.Menu);
            }

        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            // set font
            var font = ScreenManager.GetScaleFactor() == 1 ? AssetManager.Fontx1Title : AssetManager.Fontx2Title;

            var source = new Microsoft.Xna.Framework.Rectangle(32, 0, 32, 64);
            
            // use the height of the screen to determine the scale of the giraffe
            var scale = (int)(ScreenManager.Size.Y / 64f);
            
            // destination in the middle of the screen
            var destination = new Microsoft.Xna.Framework.Rectangle((int)(ScreenManager.Size.X / 2f) - (32 * scale / 2), (int)(ScreenManager.Size.Y / 2f) - (64 * scale / 2), 32 * scale, 64 * scale);
            
            spriteBatch.Draw(AssetManager.GiraffeSpriteTexture, destination, source, Microsoft.Xna.Framework.Color.White);
            
            var textLine1 = "You";
            var textLine2 = "Win!";
            
            var textLine1Size = font.MeasureString(textLine1);
            var textLine2Size = font.MeasureString(textLine2);
            
            // draw the text to the left of the giraffe
            spriteBatch.DrawString(font, textLine1, new Microsoft.Xna.Framework.Vector2(destination.X - textLine1Size.X - 10, destination.Y + (destination.Height / 2f) - (textLine1Size.Y / 2f)), Microsoft.Xna.Framework.Color.White);
            spriteBatch.DrawString(font, textLine2, new Microsoft.Xna.Framework.Vector2(destination.X - textLine2Size.X - 10, destination.Y + (destination.Height / 2f) - (textLine2Size.Y / 2f) + textLine1Size.Y), Microsoft.Xna.Framework.Color.White);

        }
    }
}

