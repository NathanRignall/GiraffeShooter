using System.Collections.Generic;
using GiraffeShooterClient.Utility.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace GiraffeShooterClient.Container.Camera
{
    public class FollowContext : Camera
    {
        public override void HandleEvents(List<Event> events)
        {
            // used to make the camera lag behind the player
            foreach (Event e in events)
            {
                switch (e.Type)
                {
                    case EventType.KeyPress:
                        switch (e.Key)
                        {
                            case Keys.Up:
                                System.Console.WriteLine("Up");
                                break;
                            case Keys.Down:
                                System.Console.WriteLine("Down");
                                break;
                            case Keys.Left:
                                System.Console.WriteLine("Left");
                                break;
                            case Keys.Right:
                                System.Console.WriteLine("Right");
                                break;
                        }
                        break;
                    case EventType.MouseDrag:
                        System.Console.WriteLine("Mouse Drag");

                        _velocity += e.MouseDelta * 10;
                        break;
                }
            }
        }
    }
}
