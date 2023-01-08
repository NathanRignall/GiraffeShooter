using Microsoft.Xna.Framework;

using GiraffeShooterClient.Utility;

namespace GiraffeShooterClient.Entity
{
    class Player : Entity
    {
        public Player()
        {
            id = new System.Guid();
            name = "Player";
            
            Physics physics = new Physics();
            physics.Position = new Vector3(0, 0, 0);
            physics.Size = new Vector3(1, 1, 2);
            physics.deceleration = 0.1f;
            AddComponent(physics);

            Collider collider = new Collider();
            AddComponent(collider);

            Control control = new Control();
            AddComponent(control);

            Sprite sprite = new Sprite(AssetManager.GiraffeTextureTest, new Rectangle(300,300,32,32));
            AddComponent(sprite);
        }

        public void Move(Control.Direction direction) {
            Control control = GetComponent<Control>();
            control.Move(direction);
        }

        public Vector2 GetPosition() {
            Physics physics = GetComponent<Physics>();
            return new Vector2(physics.Position.X, physics.Position.Y);
        }
    }
}