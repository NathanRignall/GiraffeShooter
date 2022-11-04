using System.Collections.Generic;

using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace GiraffeShooterClient.Utility.Input;

public struct Event
{
    public Keys Key;

    public Event(Keys EventKey) {
        Key = EventKey;
    }
}

public class InputManager
{

    private KeyboardState previous_state;
    private KeyboardState current_state;

    public InputManager()
    {

        previous_state = new KeyboardState();
        current_state = new KeyboardState();
    }

    public void UpdateState(KeyboardState state) {

        previous_state = current_state;
        current_state = state;
    }

    public List<Event> GenerateEvents()
    {
        var events = new List<Event>();

        if (previous_state.IsKeyDown(Keys.Up) & current_state.IsKeyUp(Keys.Up)) {
            events.Add(new Event(Keys.Up));
        }

        if (previous_state.IsKeyUp(Keys.Down) & current_state.IsKeyDown(Keys.Down)) {
            events.Add(new Event(Keys.Down));
        }

        if (previous_state.IsKeyUp(Keys.Left) & current_state.IsKeyDown(Keys.Left)) {
            events.Add(new Event(Keys.Left));
        }

        if (previous_state.IsKeyUp(Keys.Right) & current_state.IsKeyDown(Keys.Right)) {
            events.Add(new Event(Keys.Right));
        }

        return events;
    }

}