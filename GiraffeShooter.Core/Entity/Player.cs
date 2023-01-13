using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

using GiraffeShooterClient.Utility;

namespace GiraffeShooterClient.Entity
{
    class Player : Entity
    {
        
        Animation.Frame[] standFrames = new Animation.Frame[1];
        Animation.Frame[] walkingFrames = new Animation.Frame[4];

        private Shoot _shoot;
        
        public Player()
        {
            Id = Guid.NewGuid();
            Name = "Player";
            
            Physics physics = new Physics();
            physics.Position = new Vector3(0.5f, 0.5f, 0);
            physics.Size = new Vector3(1, 1, 2);
            physics.deceleration = 0.1f;
            AddComponent(physics);

            Collider collider = new Collider();
            AddComponent(collider);

            Control control = new Control();
            control.Speed = 10;
            AddComponent(control);

            Sprite sprite = new Sprite(AssetManager.GiraffeSpriteTexture, new Vector2(0,-16));
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
            
            // shoot component
            _shoot = new Shoot();
        }

        public void Move(Control.Direction direction) 
        {
            Control control = GetComponent<Control>();
            control.Move(direction);
        }

        public void Move(float angle, float speedFactor)
        {
            Control control = GetComponent<Control>();
            control.Move(angle, speedFactor);
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
            
            // set shoot position to player position
            _shoot.SetPosition(physics.Position);
            
        }

        public override void HandleEvents(List<Event> events)
        {
            _shoot.HandleEvents(events);
        }
    }
}