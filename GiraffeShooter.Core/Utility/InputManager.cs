using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;

namespace GiraffeShooterClient.Utility;

public enum EventType
{
    MouseDrag,
    MouseClick,
    MouseScroll,
    KeyPress,
    KeyRelease,
}

public struct Event
{
    public EventType Type;
    public Keys Key;
    public Vector2 MousePosition;
    public Vector2 MouseDelta;
    public int MouseScrollDelta;

    public Event(Keys EventKey) {
        Type = EventType.KeyPress;
        Key = EventKey;
        MousePosition = Vector2.Zero;
        MouseDelta = Vector2.Zero;
        MouseScrollDelta = 0;
    }

    public Event(Vector2 EventMousePosition) {
        Type = EventType.MouseClick;
        Key = Keys.None;
        MousePosition = EventMousePosition;
        MouseDelta = Vector2.Zero;
        MouseScrollDelta = 0;
    }

    public Event(Vector2 EventMousePosition, Vector2 EventMouseDelta) {
        Type = EventType.MouseDrag;
        Key = Keys.None;
        MousePosition = EventMousePosition;
        MouseDelta = EventMouseDelta;
        MouseScrollDelta = 0;
    }

    public Event(int EventMouseScrollDelta) {
        Type = EventType.MouseScroll;
        Key = Keys.None;
        MousePosition = Vector2.Zero;
        MouseDelta = Vector2.Zero;
        MouseScrollDelta = EventMouseScrollDelta;
    }

}

public static class InputManager
{

    public static KeyboardState PreviousKeyboardState { get; private set; }
    public static KeyboardState CurrentKeyboardState { get; private set; }
    public static MouseState PreviousMouseState { get; private set; }
    public static MouseState CurrentMouseState { get; private set; }
    private static bool _mouseDragged;

    public static void Initialize()
    {

        PreviousKeyboardState = new KeyboardState();
        CurrentKeyboardState = new KeyboardState();
    }

    public static void UpdateState(KeyboardState keyboardState, MouseState mouseState ) {

        PreviousKeyboardState = CurrentKeyboardState;
        CurrentKeyboardState = keyboardState;

        PreviousMouseState = CurrentMouseState;
        CurrentMouseState = mouseState;

    }

    public static List<Event> GenerateEvents()
    {
        var events = new List<Event>();

        // for each key add event if key is pressed and was not pressed before
        foreach (Keys key in CurrentKeyboardState.GetPressedKeys()) {
            if (PreviousKeyboardState.IsKeyUp(key)) {
                events.Add(new Event(key));
            }
        }

        // mouse logic
        if (PreviousMouseState.LeftButton == ButtonState.Pressed & CurrentMouseState.LeftButton == ButtonState.Pressed) {
            // only add mouse drag event if mouse was moved
            if (PreviousMouseState.Position != CurrentMouseState.Position) {
                events.Add(new Event(new Vector2(CurrentMouseState.X, CurrentMouseState.Y), new Vector2(CurrentMouseState.X, CurrentMouseState.Y) - new Vector2(PreviousMouseState.X, PreviousMouseState.Y)));
                _mouseDragged = true;
            }
        } 
        else if (PreviousMouseState.LeftButton == ButtonState.Released & CurrentMouseState.LeftButton == ButtonState.Released)
        {
            _mouseDragged = false;
        }

        if (PreviousMouseState.LeftButton == ButtonState.Pressed & CurrentMouseState.LeftButton == ButtonState.Released & _mouseDragged == false) {
            events.Add(new Event(new Vector2(CurrentMouseState.X, CurrentMouseState.Y)));
        }

        if (PreviousMouseState.ScrollWheelValue != CurrentMouseState.ScrollWheelValue) {
            events.Add(new Event(CurrentMouseState.ScrollWheelValue - PreviousMouseState.ScrollWheelValue));
        }
        
        // touch logic
        var touchCol = TouchPanel.GetState();
        
        foreach (var touch in touchCol) {
            
            if (touch.State == TouchLocationState.Pressed) {
                events.Add(new Event(new Vector2(touch.Position.X, touch.Position.Y)));
            }
        }

        return events;
    }

    public static bool IsKeyDown(Keys key) {
        return CurrentKeyboardState.IsKeyDown(key);
    }
}