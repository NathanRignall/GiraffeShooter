using System;
using Microsoft.Xna.Framework;
using GiraffeShooterClient.Utility;

namespace GiraffeShooterClient.Entity
{
    public class MetaMachineGun : MetaGun
    {
        public MetaMachineGun()
        {
            MetaType = MetaType.Weapon;

            TimeDelay = TimeSpan.FromSeconds(0.1);
            RequiredQuanity = 1;
            Damage = 1;
        }
        
        public override void Create(Vector3 position, Vector3 velocity)
        {
            new MachineGun(position, velocity, Id, this);
        }
    }
    
    public class MachineGun : Gun
    {
        public MachineGun(Vector3 position, Vector3 velocity, Guid id = default, Meta meta = null) : base(position, velocity, id, meta)
        {
            if (meta == null)
                Meta = new MetaMachineGun();
            else
                Meta = (MetaMachineGun)meta;

            // get the sprite component
            var sprite = GetComponent<Sprite>();
            sprite.UpdateSprite(AssetManager.MachineGunTexture);
        }
    }
}

