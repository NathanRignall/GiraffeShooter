using System;
using Microsoft.Xna.Framework;

using GiraffeShooterClient.Utility;


namespace GiraffeShooterClient.Entity
{
    
    class MetaBullet : Meta
    {
        public override void Create(Vector3 position, Vector3 velocity)
        {
            new Bullet(position, velocity, Id,this);
        }
    }
    
    class Bullet : Entity
    {
        public Bullet(Vector3 position = default, Vector3 velocity = default, Guid id = default, Meta meta = null)
        {
            Id = (id == default) ? Guid.NewGuid() : id;
            Name = "Giraffe";
            Meta = (meta == null) ? new MetaBullet() : (MetaGiraffe)meta;
            
            Physics physics = new Physics();
            physics.Position = position;
            physics.Velocity = velocity;
            physics.deceleration = 1f;
            AddComponent(physics);
            
            Collider collider = new Collider();
            Action<Entity> hitAction = (Entity subject) =>
            {
                subject.Delete();
                Delete();
            };
            collider.AddResponse<Giraffe>(hitAction);
            AddComponent(collider);
            
            Sprite sprite = new Sprite(AssetManager.AmmunitionFireTexture);
            AddComponent(sprite);
            
            Cleaner cleaner = new Cleaner(TimeSpan.FromSeconds(2));
            AddComponent(cleaner);
        }
    }
}

