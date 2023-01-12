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
            // calculate the position of the text
            Physics physics = entity.GetComponent<Physics>();
            Vector2 cameraOffset = Camera.Offset;
            Vector2 position = (new Vector2(physics.Position.X, physics.Position.Y) * 32f ) + cameraOffset - new Vector2((int)AssetManager.Font.MeasureString(String).X / 2, (int)AssetManager.Font.MeasureString(String).Y / 2);
            
            // draw the text
            spriteBatch.DrawString(AssetManager.Font, String, position * Camera.Zoom, Color.White);

        }
        
        public override void Deregister()
        {
            TextSystem.Deregister(this);
        }
            
    }
}