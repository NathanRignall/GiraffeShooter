
namespace GiraffeShooterClient.Container.SplashScreen
{

    public class SplashScreenContext
    {
        public readonly SplashScreenRender SplashScreenRender;

        public SplashScreenContext()
        {
            SplashScreenRender = new SplashScreenRender();
        }

        public void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            SplashScreenRender.Update(gameTime);
        }
    }

}