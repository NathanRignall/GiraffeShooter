
namespace GiraffeShooterClient.Entity.System
{
    class Component
    {
        public Entity entity;
        public virtual void Update(Microsoft.Xna.Framework.GameTime gameTime) { }
        public virtual void Draw(Microsoft.Xna.Framework.GameTime gameTime, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch) { }

    }
}