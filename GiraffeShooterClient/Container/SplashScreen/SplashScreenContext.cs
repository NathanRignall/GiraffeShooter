using GiraffeShooterClient.Container.Game;

using GiraffeShooterClient.Utility;

namespace GiraffeShooterClient.Container.SplashScreen
{

    public class SplashScreenContext
    {

        public SplashScreenContext()
        {

        }

        public void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {

            if (gameTime.TotalGameTime.TotalSeconds > 3)
            {
                GameContext.SetState(Game.GameContext.State.World);
            }

        }

        public void Draw(Microsoft.Xna.Framework.GameTime gameTime, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(GiraffeShooterClient.Utility.AssetManager.GiraffeTextureTest, new Microsoft.Xna.Framework.Rectangle(0, 0, (int)ScreenManager.Size.X, (int)ScreenManager.Size.Y), Microsoft.Xna.Framework.Color.White);

        }
    }

}