using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;

namespace GiraffeShooterClient.Entity
{

    public enum MetaType
    {
        Player,
        Weapon,
        Ammunition,
        None
    }
    
    public class Meta
    {
        private static readonly Dictionary<Type, Type> _typeMap = new Dictionary<Type, Type>
        {
            {typeof(MetaAmmunition), typeof(Ammunition)},
            {typeof(MetaGun), typeof(Gun)},
            {typeof(MetaMachineGun), typeof(MachineGun)},
        };
        
        public static Meta GetEntity(String name)
        {
            return (Meta)Activator.CreateInstance(_typeMap[Type.GetType(name)]);
        }

        public Guid Id { get; private set; }
        public MetaType MetaType { get; set; } = MetaType.None;
        
        public int Quantity { get; set; } = 0;
        public int MaxQuantity { get; set; } = 256;
        
        public int Health { get; set; } = 100;
        public int MaxHealth { get; set; } = 100;

        public Meta()
        {
            Id = Guid.NewGuid();
        }

        public virtual void Create(Vector3 position, Vector3 velocity) { }
        
        public virtual bool Action(TimeSpan time, Entity entity) { return false; }
    }
}