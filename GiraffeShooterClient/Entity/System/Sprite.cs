using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using GiraffeShooterClient.Container.Camera;
using GiraffeShooterClient.Utility.Assets;


namespace GiraffeShooterClient.Entity.System
{
    class Sprite : Component
    {
        public Texture2D texture;

        public Sprite()
        {
            SpriteSystem.Register(this);
        }

        public override void Draw(Microsoft.Xna.Framework.GameTime gameTime, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {

            Physics physics = entity.GetComponent<Physics>();

            // get the current camera position
            Vector2 cameraPosition = CameraContext.GetPosition();

            Vector2 position = new Vector2(physics.position.X, physics.position.Y) + cameraPosition;

            spriteBatch.Draw(texture, new Vector2(position.X, position.Y), Color.White);

        }

        public override void Deregister()
        {
            SpriteSystem.Deregister(this);
        }
    }
}