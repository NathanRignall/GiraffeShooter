using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;

using GiraffeShooterClient.Utility;

namespace GiraffeShooterClient.Entity
{
    public class MetaGiraffe : Meta
    {
        public MetaGiraffe()
        {
            MetaType = MetaType.Player;
        }
        
        public override void Create(Vector3 position, Vector3 velocity)
        {
            new Giraffe(position, velocity, Id,this);
        }
    }
    
    public class Giraffe : Entity
    {
        enum State
        {
            Idle,
            Walking,
            Shooting
        }

        private State _state;

        Animation.Frame[] standLeftFrames = new Animation.Frame[1];
        Animation.Frame[] standRightFrames = new Animation.Frame[1];
        Animation.Frame[] walkingLeftFrames = new Animation.Frame[4];
        Animation.Frame[] walkingRightFrames = new Animation.Frame[4];
        Animation.Frame[] shootLeftFrames = new Animation.Frame[4];
        Animation.Frame[] shootRightFrames = new Animation.Frame[4];

        public Giraffe(Vector3 position = default, Vector3 velocity = default, Guid id = default, Meta meta = null)
        {
            Id = (id == default) ? Guid.NewGuid() : id;
            Name = "Giraffe";
            
            if (meta == null)
                Meta = new MetaGiraffe();
            else
                Meta = (MetaGiraffe)meta;

            Physics physics = new Physics();
            physics.Position = position;
            physics.Size = new Vector3(1, 1, 2);
            physics.Velocity = velocity;
            AddComponent(physics);

            Collider collider = new Collider();
            AddComponent(collider);

            Control control = new Control();
            AddComponent(control);
            
            Sprite sprite = new Sprite(AssetManager.GiraffeSpriteTexture, new Vector2(0,-24));
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
            shootLeftFrames[0] = new Animation.Frame(192, 0, 32, 64, 100);
            shootLeftFrames[1] = new Animation.Frame(224, 0, 32, 64, 100);
            shootLeftFrames[2] = new Animation.Frame(192, 0, 32, 64, 100);
            shootLeftFrames[3] = new Animation.Frame(0, 0, 32, 64, 100, true);
            shootRightFrames[0] = new Animation.Frame(256, 0, 32, 64, 100);
            shootRightFrames[1] = new Animation.Frame(288, 0, 32, 64, 100);
            shootRightFrames[2] = new Animation.Frame(256, 0, 32, 64, 100);
            shootRightFrames[3] = new Animation.Frame(96, 0, 32, 64, 100, true);

            Animation animation = new Animation(standLeftFrames);
            AddComponent(animation);
            
            Aim aim = new Aim();
            AddComponent(aim);
            
            Inventory inventory = new Inventory();
            AddComponent(inventory);
            
            Health health = new Health();
            AddComponent(health);
            
            Bot bot = new Bot();
            AddComponent(bot);
        }
        
        public void Shoot(TimeSpan time)
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
            inventory.Action(time);
        }
        
        public void SetPosition(Vector3 position)
        {
            Physics physics = GetComponent<Physics>();
            physics.Position = new Vector3(position.X, position.Y, physics.Position.Z);
        }
        
        public void SetVelocity(Vector3 velocity)
        {
            Physics physics = GetComponent<Physics>();
            physics.Velocity = velocity;
        }
        
        public Vector3 GetPosition()
        {
            Physics physics = GetComponent<Physics>();
            return physics.Position;
        }

        public Vector3 GetVelocity()
        {
            Physics physics = GetComponent<Physics>();
            return physics.Velocity;
        }

        public override void Update(GameTime gameTime) {
            Physics physics = GetComponent<Physics>();
            Animation animation = GetComponent<Animation>();
            
            // calculate magnitude of velocity
            float velocityMagnitude = (float)Math.Sqrt(Math.Pow(physics.Velocity.X, 2) + Math.Pow(physics.Velocity.Y, 2));
            
            // calculate angle of velocity in pi radians
            double velocityAngle = Math.Atan2(physics.Velocity.Y, physics.Velocity.X);
            
            // switch case for state
            switch (_state) {
                case State.Idle:
                    // if velocity is greater than 0.1, set animation to walking animation
                    if (velocityMagnitude > 0.1) {
                        if ((velocityAngle > 0 && velocityAngle < Math.PI * 0.5) || (velocityAngle < 0 && velocityAngle > -Math.PI * 0.5)) {
                            animation.SetFrames(walkingRightFrames);
                        } else {
                            animation.SetFrames(walkingLeftFrames);
                        }
                        _state = State.Walking;
                    }
                    break;
                case State.Walking:
                    // if velocity is greater than 0.1, set animation to walking animation
                    if (velocityMagnitude > 0.1) {
                        if ((velocityAngle > 0 && velocityAngle < Math.PI * 0.5) || (velocityAngle < 0 && velocityAngle > -Math.PI * 0.5)) {
                            animation.SetFrames(walkingRightFrames);
                        } else {
                            animation.SetFrames(walkingLeftFrames);
                        }
                        _state = State.Walking;
                    }
                    else
                    {
                        if ((velocityAngle > 0 && velocityAngle < Math.PI * 0.5) || (velocityAngle < 0 && velocityAngle > -Math.PI * 0.5)) {
                            animation.SetFrames(standRightFrames);
                        } else {
                            animation.SetFrames(standLeftFrames);
                        }
                        _state = State.Idle;
                    }
                    break;
                case State.Shooting:
                    // if animation is finished, set animation to idle animation
                    if (animation.Finished) {
                        if ((velocityAngle > 0 && velocityAngle < Math.PI * 0.5) || (velocityAngle < 0 && velocityAngle > -Math.PI * 0.5)) {
                            animation.SetFrames(standRightFrames);
                        } else {
                            animation.SetFrames(standLeftFrames);
                        }
                        _state = State.Idle;
                    }
                    break;
            }

        }

        public override void HandleEvents(List<Event> events)
        {
            foreach (Event e in events)
            {
                switch (e.Type)
                {
                    case EventType.KeyPress:
                        if (e.Key == Microsoft.Xna.Framework.Input.Keys.G)
                        {
                            // pick a number between 1 and 2 and delete if 1
                            var random = new System.Random();
                            if (random.Next(1, 3) == 1)
                            {
                                Delete();
                            }
                        }
                        break;
                }
            }
        }
    }
}