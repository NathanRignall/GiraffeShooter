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
    TouchHold,
    TouchPinch,
    StickLeftMove,
    StickRightMove,
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
    public TimeSpan Time;

    public Event(Keys EventKey, EventType type, TimeSpan time) {
        if (type == EventType.KeyPress | type == EventType.KeyRelease | type == EventType.KeyHold) {
            Key = EventKey;
            Type = type;
            Position = Vector2.Zero;
            Position2 = Vector2.Zero;
            Delta = Vector2.Zero;
            Delta2 = Vector2.Zero;
            ScrollDelta = 0;
            Time = time;
        } else {
            throw new Exception("Invalid event type for key event");
        }
    }

    public Event(Vector2 position, EventType type, TimeSpan time) {
        // check is click or press
        if (type == EventType.MouseClick | type == EventType.TouchPress | type == EventType.TouchHold)
        {
            Type = type;
            Position = position;
            Position2 = Vector2.Zero;
            Delta = Vector2.Zero;
            Delta2 = Vector2.Zero;
            ScrollDelta = 0;
            Key = Keys.None;
            Time = time;
        }
        // stick move
        else if (type == EventType.StickLeftMove | type == EventType.StickRightMove)
        {
            Type = type;
            Position = Vector2.Zero;
            Position2 = Vector2.Zero;
            Delta = position;
            Delta2 = Vector2.Zero;
            ScrollDelta = 0;
            Key = Keys.None;
            Time = time;
        } else {
            throw new Exception("Invalid event type");
        }
    }

    public Event(Vector2 position, Vector2 delta, EventType type, TimeSpan time) {
        // check is mouse drag or touch drag
        if (type == EventType.MouseDrag | type == EventType.TouchDrag) {
            Type = type;
            Position = position;
            Position2 = Vector2.Zero;
            Delta = delta;
            Delta2 = Vector2.Zero;
            ScrollDelta = 0;
            Key = Keys.None;
            Time = time;
        } else {
            throw new Exception("Invalid event type");
        }
    }

    public Event(int scrollDelta, EventType type, TimeSpan time) {
        // check is mouse scroll
        if (type == EventType.MouseScroll) {
            Type = type;
            Position = Vector2.Zero;
            Position2 = Vector2.Zero;
            Delta = Vector2.Zero;
            Delta2 = Vector2.Zero;
            ScrollDelta = scrollDelta;
            Key = Keys.None;
            Time = time;
        } else {
            throw new Exception("Invalid event type");
        }
    }
    
    public Event(Vector2 position, Vector2 position2, Vector2 delta, Vector2 delta2, EventType type, TimeSpan time) {
        // check is touch pinch
        if (type == EventType.TouchPinch) {
            Type = type;
            Position = position;
            Position2 = position2;
            Delta = delta;
            Delta2 = delta2;
            ScrollDelta = 0;
            Key = Keys.None;
            Time = time;
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

    public struct Touch
    {
        public int Count;
        public Vector2 Position;
    }
    
    public static bool TouchConnected { get; private set; }
    public static Touch[] TouchState;
    private static bool _touchGesture;
    
    public static void Initialize()
    {

        PreviousKeyboardState = new KeyboardState();
        CurrentKeyboardState = new KeyboardState();
        
        PreviousMouseState = new MouseState();
        CurrentMouseState = new MouseState();
        _mouseDragged = false;
        
        TouchConnected = false;
        TouchState = new Touch[10];
        _touchGesture = false;

        TouchPanel.EnabledGestures = GestureType.Pinch | GestureType.FreeDrag;
    }

    public static void UpdateState(KeyboardState keyboardState, MouseState mouseState ) {

        PreviousKeyboardState = CurrentKeyboardState;
        CurrentKeyboardState = keyboardState;

        PreviousMouseState = CurrentMouseState;
        CurrentMouseState = mouseState;

    }

    public static List<Event> GenerateEvents(GameTime gameTime)
    {
        var events = new List<Event>();

        // for each key add event if key is pressed and was not pressed before
        foreach (Keys key in CurrentKeyboardState.GetPressedKeys()) {
            if (PreviousKeyboardState.IsKeyUp(key)) {
                events.Add(new Event(key, EventType.KeyPress, gameTime.TotalGameTime));
            }
        }
        
        // for each key add event if key is being held
        foreach (Keys key in CurrentKeyboardState.GetPressedKeys()) {
            if (PreviousKeyboardState.IsKeyDown(key)) {
                events.Add(new Event(key, EventType.KeyHold, gameTime.TotalGameTime));
            }
        }

        // mouse logic
        if (PreviousMouseState.LeftButton == ButtonState.Pressed & CurrentMouseState.LeftButton == ButtonState.Pressed) {
            // only add mouse drag event if mouse was moved
            if (PreviousMouseState.Position != CurrentMouseState.Position) {
                events.Add(new Event(new Vector2(CurrentMouseState.X, CurrentMouseState.Y), new Vector2(CurrentMouseState.X, CurrentMouseState.Y) - new Vector2(PreviousMouseState.X, PreviousMouseState.Y), EventType.MouseDrag, gameTime.TotalGameTime));
                _mouseDragged = true;
            }
        } 
        else if (PreviousMouseState.LeftButton == ButtonState.Released & CurrentMouseState.LeftButton == ButtonState.Released)
        {
            _mouseDragged = false;
        }

        if (PreviousMouseState.LeftButton == ButtonState.Pressed & CurrentMouseState.LeftButton == ButtonState.Released & _mouseDragged == false) {
            events.Add(new Event(new Vector2(CurrentMouseState.X, CurrentMouseState.Y), EventType.MouseClick, gameTime.TotalGameTime));
        }

        if (PreviousMouseState.ScrollWheelValue != CurrentMouseState.ScrollWheelValue) {
            events.Add(new Event(CurrentMouseState.ScrollWheelValue - PreviousMouseState.ScrollWheelValue, EventType.MouseScroll, gameTime.TotalGameTime));
        }
        
        // touch logic
        var touchCol = TouchPanel.GetState();
        TouchConnected = touchCol.IsConnected;

        // for loop over touch collection
        for (int i = 0; i < touchCol.Count; i++) {
            // if touch is pressed
            if (touchCol[i].State == TouchLocationState.Pressed) {
                // set touch state to pressed
                TouchState[i].Count = 1;
                TouchState[i].Position = new Vector2(touchCol[i].Position.X, touchCol[i].Position.Y);
            }
            
            // if touch move update touch state
            if (touchCol[i].State == TouchLocationState.Moved) {
                // update position
                if (TouchState[i].Count > 0) {
                    TouchState[i].Position = new Vector2(touchCol[i].Position.X, touchCol[i].Position.Y);
                }
            }
            
            // if touch is released
             if (touchCol[i].State == TouchLocationState.Released) {
                 // check touch state
                 if (TouchState[i].Count <= 15)
                     events.Add(new Event(TouchState[i].Position, EventType.TouchPress, gameTime.TotalGameTime));
                 
                 // reset touch state
                 TouchState[i].Count = 0;
                 TouchState[i].Position = Vector2.Zero;
             }
        }
        
        // loop over touch state and increment value if not 0
        for (int i = 0; i < TouchState.Length; i++) {
            if (TouchState[i].Count != 0) {
                TouchState[i].Count++;
            }
        }

        // reset gesture state
        _touchGesture = false;
        
        // read gestures
        while (TouchPanel.IsGestureAvailable) {
            var gesture = TouchPanel.ReadGesture();
            
            // drag
            if (gesture.GestureType == GestureType.FreeDrag) {
                events.Add(new Event(gesture.Position, gesture.Delta, EventType.TouchDrag, gameTime.TotalGameTime));
                _touchGesture = true;
            }
            
            // zoom
            if (gesture.GestureType == GestureType.Pinch) {
                events.Add(new Event(gesture.Position, gesture.Position2, gesture.Delta, gesture.Delta2, EventType.TouchPinch, gameTime.TotalGameTime));
                _touchGesture = true;
            }
        }
        
        // if no gesture send hold events loop over touch state and add hold events
        if (_touchGesture == false) {
            for (int i = 0; i < TouchState.Length; i++) {
                if (TouchState[i].Count > 15) {
                    // send hold event
                    events.Add(new Event(TouchState[i].Position, EventType.TouchHold, gameTime.TotalGameTime));
                }
            }
        }

        return events;
    }

    public static bool IsKeyDown(Keys key) {
        return CurrentKeyboardState.IsKeyDown(key);
    }
}