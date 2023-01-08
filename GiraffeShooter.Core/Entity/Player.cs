using System;
using Microsoft.Xna.Framework;

using GiraffeShooterClient.Utility;

namespace GiraffeShooterClient.Entity
{
    class Player : Entity
    {
        
        Animation.Frame[] standFrames = new Animation.Frame[1];
        Animation.Frame[] walkingFrames = new Animation.Frame[4];
        
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

            Sprite sprite = new Sprite(AssetManager.GiraffeSpriteTexture);
            AddComponent(sprite);
            
            // stand animation frames
            standFrames[0] = new Animation.Frame(32, 0, 32, 64, 100);
            
            // walking animation frames
            walkingFrames[0] = new Animation.Frame(0, 0, 32, 64, 100);
            walkingFrames[1] = new Animation.Frame(32, 0, 32, 64, 100);
            walkingFrames[2] = new Animation.Frame(64, 0, 32, 64, 100);
            walkingFrames[3] = new Animation.Frame(32, 0, 32, 64, 100);

            Animation animation = new Animation(standFrames);
            AddComponent(animation);
        }

        public void Move(Control.Direction direction) {
            Control control = GetComponent<Control>();
            control.Move(direction);
        }

        public Vector2 GetPosition() {
            Physics physics = GetComponent<Physics>();
            return new Vector2(physics.Position.X, physics.Position.Y);
        }
        
        public override void Update(GameTime gameTime) {
            Physics physics = GetComponent<Physics>();
            Animation animation = GetComponent<Animation>();
            
            // calculate magnitude of velocity
            float velocityMagnitude = (float)Math.Sqrt(Math.Pow(physics.Velocity.X, 2) + Math.Pow(physics.Velocity.Y, 2));
            
            // if the player is moving, change the animation to walking
            if (velocityMagnitude > 0.01f) {
                animation.SetFrames(walkingFrames);
            } else {
                animation.SetFrames(standFrames);
            }
            
        }
    }
}