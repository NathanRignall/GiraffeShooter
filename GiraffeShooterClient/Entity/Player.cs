using Microsoft.Xna.Framework;

using GiraffeShooterClient.Utility;

namespace GiraffeShooterClient.Entity
{
    class Player : Entity
    {
        public enum direction { up, down, left, right };
        private int speed = 200;

        public Player()
        {
            Physics physics = new Physics();
            physics.deceleration = 0.1f;
            AddComponent(physics);

            Sprite sprite = new Sprite();
            sprite.texture = AssetManager.PlayerTexture;
            AddComponent(sprite);
        }

        public void Move(direction direction) {
            Physics physics = GetComponent<Physics>();

            switch (direction) {
                case direction.up:
                    physics.velocity.Y = -speed;
                    break;
                case direction.down:
                    physics.velocity.Y = speed;
                    break;
                case direction.left:
                    physics.velocity.X = -speed;
                    break;
                case direction.right:
                    physics.velocity.X = speed;
                    break;
            }
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