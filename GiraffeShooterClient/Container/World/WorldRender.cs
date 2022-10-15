using GiraffeShooterClient.Entity.System;

namespace GiraffeShooterClient.Container.World
{

    public class WorldRender
    {

        public void Update(Microsoft.Xna.Framework.GameTime gameTime)
        {
            TransformSystem.Update(gameTime);
            ColliderSystem.Update(gameTime);
            SpriteSystem.Update(gameTime);
        }

        public void Draw(Microsoft.Xna.Framework.GameTime gameTime, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch)
        {
            SpriteSystem.Draw(gameTime, spriteBatch);
        }

    }

}