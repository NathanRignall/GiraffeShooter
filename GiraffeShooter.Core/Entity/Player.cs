using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

using GiraffeShooterClient.Utility;

namespace GiraffeShooterClient.Entity
{
    class Player : Entity
    {

        Animation.Frame[] standLeftFrames = new Animation.Frame[1];
        Animation.Frame[] standRightFrames = new Animation.Frame[1];
        Animation.Frame[] walkingLeftFrames = new Animation.Frame[4];
        Animation.Frame[] walkingRightFrames = new Animation.Frame[4];
        Animation.Frame[] shootLeftFrames = new Animation.Frame[4];
        Animation.Frame[] shootRightFrames = new Animation.Frame[4];

        private Shoot _shoot;
        private InventoryBar _inventoryBar;
        
        public Player()
        {
            Id = Guid.NewGuid();
            Name = "Player";

            // shoot component
            _shoot = new Shoot();
            
            // inventory bar component
            _inventoryBar = new InventoryBar();
            
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

            Sprite sprite = new Sprite(AssetManager.GiraffeSpriteTexture, new Vector2(0,-24));
            sprite.zOrder = 7;
            AddComponent(sprite);
            
            // stand animation frames
            standLeftFrames[0] = new Animation.Frame(0, 0, 32, 64, 100, true);
            standRightFrames[0] = new Animation.Frame(96, 0, 32, 64, 100, true);
            
            // walking animation frames
            walkingLeftFrames[0] = new Animation.Frame(0, 0, 32, 64, 100);
            walkingLeftFrames[1] = new Animation.Frame(32, 0, 32, 64, 100);
            walkingLeftFrames[2] = new Animation.Frame(0, 0, 32, 64, 100);
            walkingLeftFrames[3] = new Animation.Frame(64, 0, 32, 64, 100);
            walkingRightFrames[0] = new Animation.Frame(96, 0, 32, 64, 100);
            walkingRightFrames[1] = new Animation.Frame(128, 0, 32, 64, 100);
            walkingRightFrames[2] = new Animation.Frame(96, 0, 32, 64, 100);
            walkingRightFrames[3] = new Animation.Frame(160, 0, 32, 64, 100);

            // shoot animation frames
            shootLeftFrames[0] = new Animation.Frame(192, 0, 32, 64, 200);
            shootLeftFrames[1] = new Animation.Frame(224, 0, 32, 64, 1000);
            shootLeftFrames[2] = new Animation.Frame(192, 0, 32, 64, 200);
            shootLeftFrames[3] = new Animation.Frame(0, 0, 32, 64, 100, true);
            shootRightFrames[0] = new Animation.Frame(256, 0, 32, 64, 200);
            shootRightFrames[1] = new Animation.Frame(288, 0, 32, 64, 300);
            shootRightFrames[2] = new Animation.Frame(256, 0, 32, 64, 200);
            shootRightFrames[3] = new Animation.Frame(96, 0, 32, 64, 100, true);

            Animation animation = new Animation(standLeftFrames);
            AddComponent(animation);

            Inventory inventory = new Inventory(_inventoryBar);
            AddComponent(inventory);
            
            _inventoryBar.Inventory = inventory;
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
        
        public void Shoot()
        {
            // get the current shoot direction
            var rotation = _shoot.GetRotation();
            
            // set the animation to the correct direction if left or right based on rotation
            Animation animation = GetComponent<Animation>();
            if ((rotation > 0 && rotation < Math.PI * 0.5) || (rotation < 0 && rotation > -Math.PI * 0.5)) {
                animation.SetFrames(shootRightFrames);
            } else {
                animation.SetFrames(shootLeftFrames);
            }
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
            
            // calculate angle of velocity in pi radians
            double velocityAngle = Math.Atan2(physics.Velocity.Y, physics.Velocity.X);
            
            // if velocity is greater than 0.1, set animation to walking animation
            if (velocityMagnitude > 0.1 && animation.Frames != shootLeftFrames && animation.Frames != shootRightFrames) {
                if ((velocityAngle > 0 && velocityAngle < Math.PI * 0.5) || (velocityAngle < 0 && velocityAngle > -Math.PI * 0.5)) {
                    animation.SetFrames(walkingRightFrames);
                } else {
                    animation.SetFrames(walkingLeftFrames);
                }
            } else if (velocityMagnitude <= 0.1){
                // get the current shoot direction
                var rotation = _shoot.GetRotation();
            
                // set the animation to the correct direction if left or right based on rotation
                if ((rotation > 0 && rotation < Math.PI * 0.5) || (rotation < 0 && rotation > -Math.PI * 0.5)) {
                    animation.SetFrames(standRightFrames);
                } else {
                    animation.SetFrames(standLeftFrames);
                }
            }

            // once shoot animation is done, set animation to stand animation
            if (animation.Finished && animation.Frames == shootLeftFrames) {
                animation.SetFrames(standLeftFrames);
            } else if (animation.Finished && animation.Frames == shootRightFrames) {
                animation.SetFrames(standRightFrames);
            }

            // set shoot position to player position
            _shoot.SetPosition(physics.Position + new Vector3(0,-0.35f,0));
            
            // update shoot
            _shoot.Update(gameTime);
            
        }

        public override void HandleEvents(List<Event> events)
        {
            _shoot.HandleEvents(events);
            _inventoryBar.HandleEvents(events);
        }
    }
}