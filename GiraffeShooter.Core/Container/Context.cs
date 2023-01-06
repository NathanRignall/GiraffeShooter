using System.Collections.Generic;

using GiraffeShooterClient.Utility;

namespace GiraffeShooterClient.Container
{
    public abstract class Context
    {
        public abstract void HandleEvents(List<Event> events);
        public abstract void Update(Microsoft.Xna.Framework.GameTime gameTime);
        public abstract void Draw(Microsoft.Xna.Framework.GameTime gameTime, Microsoft.Xna.Framework.Graphics.SpriteBatch spriteBatch);
    }
}

