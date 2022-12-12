using Microsoft.Xna.Framework;

using GiraffeShooterClient.Utility;

namespace GiraffeShooterClient.Entity
{
    class Giraffe : Entity
    {
        public Giraffe(Vector3 position, Vector3 velocity)
        {
            id = System.Guid.NewGuid();
            
            Physics physics = new Physics();
            physics.position = position;
            physics.velocity = velocity;
            AddComponent(physics);

            Collider collider = new Collider();
            AddComponent(collider);

            Control control = new Control();
            AddComponent(control);

            Sprite sprite = new Sprite();
            sprite.texture = AssetManager.PlayerTexture;
            AddComponent(sprite);
        }

        public void Move(Control.Direction direction) {
            Control control = GetComponent<Control>();
            control.Move(direction);
        }

        public void Delete()
        {
            RemoveComponent<Physics>();
            RemoveComponent<Sprite>();
        }
    }

}