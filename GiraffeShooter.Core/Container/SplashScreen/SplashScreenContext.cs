using System.Collections.Generic;

using GiraffeShooterClient.Utility;

namespace GiraffeShooterClient.Container.SplashScreen
{

    public class SplashScreenContext : Context
    {

        public SplashScreenContext()
        {

        }

        public override void HandleEvents(List<Event> events)
        {
            
        }
        
        public override void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {

            if (gameTime.TotalGameTime.TotalSeconds > 3)
            {
                ContextManager.SetState(ContextManager.State.Menu);
            }

        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(GiraffeShooterClient.Utility.AssetManager.GiraffeTextureTest, new Microsoft.Xna.Framework.Rectangle(0, 0, (int)ScreenManager.Size.X, (int)ScreenManager.Size.Y), Microsoft.Xna.Framework.Color.White);

        }
    }

}