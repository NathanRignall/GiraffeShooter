using System;
using System.Collections.Generic;

namespace GiraffeShooterClient.Entity
{

    class Meta
    {
        private static Dictionary<Type, Type> _typeMap = new Dictionary<Type, Type>
        {
            {typeof(MetaAmmunition), typeof(Ammunition)},
            {typeof(MetaPistol), typeof(Pistol)},
        };

        public Guid Id { get; protected set; }
        public int Quantity { get; set; } = 0;
        public int MaxQuantity { get; set; } = 69;
        
        public Meta()
        {
            Id = Guid.NewGuid();
        }
    }
}