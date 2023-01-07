using System.Collections.Generic;
using Microsoft.Xna.Framework;

using GiraffeShooterClient.Utility;

namespace GiraffeShooterClient.Entity
{
    class Giraffe : Entity
    {
        
        Animation.Frame[] standFrames = new Animation.Frame[1];
        Animation.Frame[] walkingFrames = new Animation.Frame[3];

        public Giraffe(Vector3 position, Vector3 velocity)
        {
            id = System.Guid.NewGuid();
            name = "Giraffe";
            
            Physics physics = new Physics();
            physics.position = position;
            physics.velocity = velocity;
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

            Animation animation = new Animation(walkingFrames);
            AddComponent(animation);

        }

        public void Move(Control.Direction direction) {
            Control control = GetComponent<Control>();
            control.Move(direction);
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