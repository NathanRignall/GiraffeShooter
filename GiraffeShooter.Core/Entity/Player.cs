using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

using GiraffeShooterClient.Utility;
using Microsoft.Xna.Framework.Input;

namespace GiraffeShooterClient.Entity
{
    class Player : Entity
    {
        
        enum State
        {
            Idle,
            Walking,
            Shooting,
            WalkingShooting
        }

        private State _state;

        Animation.Frame[] standLeftFrames = new Animation.Frame[1];
        Animation.Frame[] standRightFrames = new Animation.Frame[1];
        Animation.Frame[] walkingLeftFrames = new Animation.Frame[4];
        Animation.Frame[] walkingRightFrames = new Animation.Frame[4];
        Animation.Frame[] shootLeftFrames = new Animation.Frame[4];
        Animation.Frame[] shootRightFrames = new Animation.Frame[4];
        Animation.Frame[] walkShootLeftFrames = new Animation.Frame[4];
        Animation.Frame[] walkShootRightFrames = new Animation.Frame[4];
        
        private InventoryBar _inventoryBar;
        
        public Player()
        {
            Id = Guid.NewGuid();
            Name = "Player";

            // state
            _state = State.Idle;

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
            
            Aim aim = new Aim();
            AddComponent(aim);
            
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
            shootLeftFrames[0] = new Animation.Frame(192, 0, 32, 64, 100);
            shootLeftFrames[1] = new Animation.Frame(224, 0, 32, 64, 100);
            shootLeftFrames[2] = new Animation.Frame(192, 0, 32, 64, 100);
            shootLeftFrames[3] = new Animation.Frame(0, 0, 32, 64, 100, true);
            shootRightFrames[0] = new Animation.Frame(320, 0, 32, 64, 100);
            shootRightFrames[1] = new Animation.Frame(352, 0, 32, 64, 100);
            shootRightFrames[2] = new Animation.Frame(320, 0, 32, 64, 100);
            shootRightFrames[3] = new Animation.Frame(96, 0, 32, 64, 100, true);
            
            // walk shoot animation frames
            walkShootLeftFrames[0] = new Animation.Frame(224, 0, 32, 64, 100);
            walkShootLeftFrames[1] = new Animation.Frame(256, 0, 32, 64, 100);
            walkShootLeftFrames[2] = new Animation.Frame(224, 0, 32, 64, 100);
            walkShootLeftFrames[3] = new Animation.Frame(288, 0, 32, 64, 100, true);
            walkShootRightFrames[0] = new Animation.Frame(352, 0, 32, 64, 100);
            walkShootRightFrames[1] = new Animation.Frame(384, 0, 32, 64, 100);
            walkShootRightFrames[2] = new Animation.Frame(352, 0, 32, 64, 100);
            walkShootRightFrames[3] = new Animation.Frame(416, 0, 32, 64, 100, true);

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
            var rotation = GetComponent<Aim>().Rotation;
            
            // set the animation to the correct direction if left or right based on rotation
            Animation animation = GetComponent<Animation>();
            if ((rotation > 0 && rotation < Math.PI * 0.5) || (rotation < 0 && rotation > -Math.PI * 0.5)) {
                animation.SetFrames(shootRightFrames, false);
            } else {
                animation.SetFrames(shootLeftFrames, false);
            }
            _state = State.Shooting;
            
            // action the inventory item
            Inventory inventory = GetComponent<Inventory>();
            inventory.Action();
        }

        public Vector2 GetPosition() {
            Physics physics = GetComponent<Physics>();
            return new Vector2(physics.Position.X, physics.Position.Y);
        }

        public override void Update(GameTime gameTime)
        {
            Physics physics = GetComponent<Physics>();
            Animation animation = GetComponent<Animation>();

            // calculate magnitude of velocity
            float velocityMagnitude =
                (float)Math.Sqrt(Math.Pow(physics.Velocity.X, 2) + Math.Pow(physics.Velocity.Y, 2));

            // calculate angle of velocity in pi radians
            double velocityAngle = Math.Atan2(physics.Velocity.Y, physics.Velocity.X);

            // switch case for state
            switch (_state)
            {
                case State.Idle:
                    // if velocity is greater than 0.1, set animation to walking animation
                    if (velocityMagnitude > 0.1)
                    {
                        if ((velocityAngle > 0 && velocityAngle < Math.PI * 0.5) ||
                            (velocityAngle < 0 && velocityAngle > -Math.PI * 0.5))
                        {
                            animation.SetFrames(walkingRightFrames);
                        }
                        else
                        {
                            animation.SetFrames(walkingLeftFrames);
                        }

                        _state = State.Walking;
                    }
                    break;
                case State.Walking:
                    // if velocity is greater than 0.1, set animation to walking animation
                    if (velocityMagnitude > 0.1)
                    {
                        if ((velocityAngle > 0 && velocityAngle < Math.PI * 0.5) ||
                            (velocityAngle < 0 && velocityAngle > -Math.PI * 0.5))
                        {
                            animation.SetFrames(walkingRightFrames);
                        }
                        else
                        {
                            animation.SetFrames(walkingLeftFrames);
                        }

                        _state = State.Walking;
                    }
                    else
                    {
                        if ((velocityAngle > 0 && velocityAngle < Math.PI * 0.5) ||
                            (velocityAngle < 0 && velocityAngle > -Math.PI * 0.5))
                        {
                            animation.SetFrames(standRightFrames);
                        }
                        else
                        {
                            animation.SetFrames(standLeftFrames);
                        }

                        _state = State.Idle;
                    }
                    break;
                case State.Shooting:
                    // if animation is finished, set animation to idle animation
                    if (animation.Finished)
                    {
                        if ((velocityAngle > 0 && velocityAngle < Math.PI * 0.5) ||
                            (velocityAngle < 0 && velocityAngle > -Math.PI * 0.5))
                        {
                            animation.SetFrames(standRightFrames);
                        }
                        else
                        {
                            animation.SetFrames(standLeftFrames);
                        }

                        _state = State.Idle;
                    }
                    break;
            }

            // // set shoot position to player position depending if shooting or not
            // if (_state == State.Shooting)
            //     _shoot.SetPosition(physics.Position + new Vector3(0, -0.7f, 0));
            // else
            //     _shoot.SetPosition(physics.Position + new Vector3(0, -0.35f, 0));
        }

        public override void HandleEvents(List<Event> events)
        {
            if (ContextManager.Paused)
                return;
            
            GetComponent<Aim>().HandleEvents(events);
            
            foreach (Event e in events)
            {
                switch (e.Type)
                {
                    case EventType.KeyHold:

                        switch (e.Key)
                        {
                            case Keys.W:
                                Move(Control.Direction.up);
                                break;
                            case Keys.S:
                                Move(Control.Direction.down);
                                break;
                            case Keys.A:
                                Move(Control.Direction.left);
                                break;
                            case Keys.D:
                                Move(Control.Direction.right);
                                break;
                        }
                        
                        break;
                    
                    case EventType.KeyPress:
                        
                        switch (e.Key)
                        {
                            case Keys.Space:
                                Shoot();
                                break;
                        }
                        break;

                    case EventType.MousePress:
                    case EventType.MouseDrag:
                    case EventType.MouseHold:
                        Shoot();
                        break;
                    
                    case EventType.StickLeftMove:

                        // use delta to calculate rotation
                        Vector2 delta = e.Delta;
                        
                        // set rotation
                        var direction = (float)Math.Atan2(delta.Y, delta.X);
                        
                        Move(direction, delta.Length());

                        break;
                }

            }
            
            _inventoryBar.HandleEvents(events);
        }
    }
}