using System;

using Microsoft.Xna.Framework;

using GiraffeShooterClient.Utility;

namespace GiraffeShooterClient.Entity
{
    public class Cleaner : Component
    {
        public TimeSpan TimeToDestroy;
        
        
        public Cleaner(TimeSpan timeToDestroy)
        {
            TimeToDestroy = timeToDestroy;
            CleanerSystem.Register(this);
        }

        public override void Update(GameTime gameTime)
        {
            if (ContextManager.Paused)
                return;
            
            if (TimeToDestroy.TotalSeconds <= 0)
                entity.Delete();
            else
                TimeToDestroy -= gameTime.ElapsedGameTime;
        }
    }
}

