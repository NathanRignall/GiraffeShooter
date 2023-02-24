using System;
using Microsoft.Xna.Framework;
using GiraffeShooterClient.Utility;

namespace GiraffeShooterClient.Entity
{
    class MetaMachineGun : MetaGun
    {
        public MetaMachineGun()
        {
            TimeDelay = TimeSpan.FromSeconds(0.1);
            RequiredQuanity = 1;
            
            Damage = 1;
        }
        
        public override void Create(Vector3 position, Vector3 velocity)
        {
            new MachineGun(position, velocity, this);
        }
    }
    
    class MachineGun : Gun
    {
        public MachineGun(Vector3 position, Vector3 velocity, Meta meta = null) : base(position, velocity, meta)
        {
            if (meta == null)
                _meta = new MetaMachineGun();
            else
                _meta = (MetaGun)meta;
            
            // get the sprite component
            var sprite = GetComponent<Sprite>();
            sprite.UpdateSprite(AssetManager.MachineGunTexture);
        }
    }
}

