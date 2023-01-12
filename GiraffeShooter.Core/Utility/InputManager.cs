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
    KeyHold,
    TouchDrag,
    TouchPress,
    TouchPinch,
}

public struct Event
{
    public EventType Type;
    public Keys Key;
    public Vector2 Position;
    public Vector2 Position2;
    public Vector2 Delta;
    public Vector2 Delta2;
    public int ScrollDelta;

    public Event(Keys EventKey, EventType type) {
        if (type == EventType.KeyPress || type == EventType.KeyRelease || type == EventType.KeyHold) {
            Key = EventKey;
            Type = type;
            Position = Vector2.Zero;
            Position2 = Vector2.Zero;
            Delta = Vector2.Zero;
            Delta2 = Vector2.Zero;
            ScrollDelta = 0;
        } else {
            throw new Exception("Invalid event type for key event");
        }
    }

    public Event(Vector2 position, EventType type) {
        // check is click or press
        if (type == EventType.MouseClick || type == EventType.TouchPress) {
            Type = type;
            Position = position;
            Position2 = Vector2.Zero;
            Delta = Vector2.Zero;
            Delta2 = Vector2.Zero;
            ScrollDelta = 0;
            Key = Keys.None;
        } else {
            throw new Exception("Invalid event type");
        }
    }

    public Event(Vector2 position, Vector2 delta, EventType type) {
        // check is mouse drag or touch drag
        if (type == EventType.MouseDrag || type == EventType.TouchDrag) {
            Type = type;
            Position = position;
            Position2 = Vector2.Zero;
            Delta = delta;
            Delta2 = Vector2.Zero;
            ScrollDelta = 0;
            Key = Keys.None;
        } else {
            throw new Exception("Invalid event type");
        }
    }

    public Event(int scrollDelta, EventType type) {
        // check is mouse scroll
        if (type == EventType.MouseScroll) {
            Type = type;
            Position = Vector2.Zero;
            Position2 = Vector2.Zero;
            Delta = Vector2.Zero;
            Delta2 = Vector2.Zero;
            ScrollDelta = scrollDelta;
            Key = Keys.None;
        } else {
            throw new Exception("Invalid event type");
        }
    }
    
    public Event(Vector2 position, Vector2 position2, Vector2 delta, Vector2 delta2, EventType type) {
        // check is touch pinch
        if (type == EventType.TouchPinch) {
            Type = type;
            Position = position;
            Position2 = position2;
            Delta = delta;
            Delta2 = delta2;
            ScrollDelta = 0;
            Key = Keys.None;
        } else {
            throw new Exception("Invalid event type");
        }
    }

}

public static class InputManager
{

    public static KeyboardState PreviousKeyboardState { get; private set; }
    public static KeyboardState CurrentKeyboardState { get; private set; }
    
    public static MouseState PreviousMouseState { get; private set; }
    public static MouseState CurrentMouseState { get; private set; }
    private static bool _mouseDragged;
    
    public static bool TouchConnected { get; private set; }
    
    public static void Initialize()
    {

        PreviousKeyboardState = new KeyboardState();
        CurrentKeyboardState = new KeyboardState();
        
        PreviousMouseState = new MouseState();
        CurrentMouseState = new MouseState();
        
        _mouseDragged = false;
        TouchConnected = false;

        TouchPanel.EnabledGestures = GestureType.Pinch | GestureType.FreeDrag;
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
                events.Add(new Event(key, EventType.KeyPress));
            }
        }
        
        // for each key add event if key is being held
        foreach (Keys key in CurrentKeyboardState.GetPressedKeys()) {
            if (PreviousKeyboardState.IsKeyDown(key)) {
                events.Add(new Event(key, EventType.KeyHold));
            }
        }

        // mouse logic
        if (PreviousMouseState.LeftButton == ButtonState.Pressed & CurrentMouseState.LeftButton == ButtonState.Pressed) {
            // only add mouse drag event if mouse was moved
            if (PreviousMouseState.Position != CurrentMouseState.Position) {
                events.Add(new Event(new Vector2(CurrentMouseState.X, CurrentMouseState.Y), new Vector2(CurrentMouseState.X, CurrentMouseState.Y) - new Vector2(PreviousMouseState.X, PreviousMouseState.Y), EventType.MouseDrag));
                _mouseDragged = true;
            }
        } 
        else if (PreviousMouseState.LeftButton == ButtonState.Released & CurrentMouseState.LeftButton == ButtonState.Released)
        {
            _mouseDragged = false;
        }

        if (PreviousMouseState.LeftButton == ButtonState.Pressed & CurrentMouseState.LeftButton == ButtonState.Released & _mouseDragged == false) {
            events.Add(new Event(new Vector2(CurrentMouseState.X, CurrentMouseState.Y), EventType.MouseClick));
        }

        if (PreviousMouseState.ScrollWheelValue != CurrentMouseState.ScrollWheelValue) {
            events.Add(new Event(CurrentMouseState.ScrollWheelValue - PreviousMouseState.ScrollWheelValue, EventType.MouseScroll));
        }
        
        // touch logic
        var touchCol = TouchPanel.GetState();
        TouchConnected = touchCol.IsConnected;

        foreach (var touch in touchCol) {
            if (touch.State == TouchLocationState.Pressed) {
                events.Add(new Event(new Vector2(touch.Position.X, touch.Position.Y), EventType.TouchPress));
            }
        }
        
        // read gestures
        while (TouchPanel.IsGestureAvailable) {
            var gesture = TouchPanel.ReadGesture();
            
            // drag
            if (gesture.GestureType == GestureType.FreeDrag) {
                events.Add(new Event(gesture.Position, gesture.Delta, EventType.TouchDrag));
            }
            
            // zoom
            if (gesture.GestureType == GestureType.Pinch) {
                events.Add(new Event(gesture.Position, gesture.Position2, gesture.Delta, gesture.Delta2, EventType.TouchPinch));
            }
        }

        return events;
    }

    public static bool IsKeyDown(Keys key) {
        return CurrentKeyboardState.IsKeyDown(key);
    }
}