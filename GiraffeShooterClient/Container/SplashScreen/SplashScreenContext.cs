using GiraffeShooterClient.Container.Game;

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

            spriteBatch.Draw(GiraffeShooterClient.Utility.Assets.AssetManager.GiraffeTextureTest, new Microsoft.Xna.Framework.Rectangle(0, 0, 800, 480), Microsoft.Xna.Framework.Color.White);
            
        }
    }

}