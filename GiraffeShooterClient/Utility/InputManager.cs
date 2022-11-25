using System.Collections.Generic;

using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace GiraffeShooterClient.Utility.Input;

public enum EventType
{
    MouseDrag,
    MouseClick,
    MouseScroll,
    KeyPress,
    KeyRelease
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

    private static KeyboardState _previousKeyboardState;
    private static KeyboardState _currentKeyboardState;
    private static MouseState _previousMouseState;
    private static MouseState _currentMouseState;
    private static bool _mouseDragged;

    public static void Initialize()
    {

        _previousKeyboardState = new KeyboardState();
        _currentKeyboardState = new KeyboardState();
    }

    public static void UpdateState(KeyboardState keyboardState, MouseState mouseState ) {

        _previousKeyboardState = _currentKeyboardState;
        _currentKeyboardState = keyboardState;

        _previousMouseState = _currentMouseState;
        _currentMouseState = mouseState;

    }

    public static List<Event> GenerateEvents()
    {
        var events = new List<Event>();

        if (_previousKeyboardState.IsKeyDown(Keys.Up) & _currentKeyboardState.IsKeyUp(Keys.Up)) {
            events.Add(new Event(Keys.Up));
        }

        if (_previousKeyboardState.IsKeyUp(Keys.Down) & _currentKeyboardState.IsKeyDown(Keys.Down)) {
            events.Add(new Event(Keys.Down));
        }

        if (_previousKeyboardState.IsKeyUp(Keys.Left) & _currentKeyboardState.IsKeyDown(Keys.Left)) {
            events.Add(new Event(Keys.Left));
        }

        if (_previousKeyboardState.IsKeyUp(Keys.Right) & _currentKeyboardState.IsKeyDown(Keys.Right)) {
            events.Add(new Event(Keys.Right));
        }

        if (_previousMouseState.LeftButton == ButtonState.Pressed & _currentMouseState.LeftButton == ButtonState.Pressed) {
            events.Add(new Event(new Vector2(_currentMouseState.X, _currentMouseState.Y), new Vector2(_currentMouseState.X, _currentMouseState.Y) - new Vector2(_previousMouseState.X, _previousMouseState.Y)));
            _mouseDragged = true;
        }

        if (_previousMouseState.LeftButton == ButtonState.Pressed & _currentMouseState.LeftButton == ButtonState.Released & _mouseDragged == false) {
            events.Add(new Event(new Vector2(_currentMouseState.X, _currentMouseState.Y)));
        }

        if (_previousMouseState.ScrollWheelValue != _currentMouseState.ScrollWheelValue) {
            events.Add(new Event(_currentMouseState.ScrollWheelValue - _previousMouseState.ScrollWheelValue));
        }

        return events;
    }

    public static bool IsKeyDown(Keys key) {
        return _currentKeyboardState.IsKeyDown(key);
    }
}