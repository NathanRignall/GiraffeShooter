using GiraffeShooterClient.Utility;
using Microsoft.Xna.Framework;

namespace GiraffeShooterClient.Entity
{
    class Health : Component
    {
        private HealthBar _healthBar;
        
        public int Value { get; set; } = 75;
        public int MaxValue { get; set; } = 100;
        public bool IsDead { get; set; }


        public Health(HealthBar healthBar = null)
        {
            _healthBar = healthBar;
            
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
            
            if (_healthBar != null)
                _healthBar.SetHealth(Value);
        }
        
        public void ReduceHealth(int amount)
        {
            Value -= amount;
            
            if (_healthBar != null)
                _healthBar.SetHealth(Value);
        }
        
        public override void Deregister()
        {
            HealthSystem.Deregister(this);
        }
    }
}

