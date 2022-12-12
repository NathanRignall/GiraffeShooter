using Microsoft.Xna.Framework;

using GiraffeShooterClient.Utility;

namespace GiraffeShooterClient.Entity
{
    class Player : Entity
    {
        public Player()
        {
            id = new System.Guid();
            
            Physics physics = new Physics();
            physics.deceleration = 0.1f;
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

        public Vector2 SetPosition(Vector2 position) {
            Physics physics = GetComponent<Physics>();
            physics.position = new Vector3(position, 0);
            return new Vector2(physics.position.X, physics.position.Y);
        }

        public Vector2 GetPosition() {
            Physics physics = GetComponent<Physics>();
            return new Vector2(physics.position.X, physics.position.Y);
        }

        public void Delete()
        {
            RemoveComponent<Physics>();
            RemoveComponent<Sprite>();
        }
    }

}