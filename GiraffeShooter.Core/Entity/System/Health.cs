using GiraffeShooterClient.Utility;
using Microsoft.Xna.Framework;

namespace GiraffeShooterClient.Entity
{
    class Health : Component
    {
        public int Value { get; set; } = 100;
        public int MaxValue { get; set; } = 100;
        public bool IsDead { get; set; }


        public Health()
        {
            HealthSystem.Register(this);
        }
        
        public override void Update(GameTime gameTime)
        {
            if (ContextManager.Paused)
                return;
            
            if (Value <= 0) 
                IsDead = true;
                
            if (IsDead)
                entity.Delete();
        }
        
        public void ReduceHealth(int amount)
        {
            Value -= amount;
        }
        
        public override void Deregister()
        {
            HealthSystem.Deregister(this);
        }
    }
}

