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

    public Event(Keys EventKey) {
        Type = EventType.KeyPress;
        Key = EventKey;
        MousePosition = Vector2.Zero;
        MouseDelta = Vector2.Zero;
    }

    public Event(Vector2 EventMousePosition) {
        Type = EventType.MouseClick;
        Key = Keys.None;
        MousePosition = EventMousePosition;
        MouseDelta = Vector2.Zero;
    }

    public Event(Vector2 EventMousePosition, Vector2 EventMouseDelta) {
        Type = EventType.MouseDrag;
        Key = Keys.None;
        MousePosition = EventMousePosition;
        MouseDelta = EventMouseDelta;
    }

}

public class InputManager
{

    private KeyboardState _previousKeyboardState;
    private KeyboardState _currentKeyboardState;
    private MouseState _previousMouseState;
    private MouseState _currentMouseState;
    private bool _mouseDragged;

    public InputManager()
    {

        _previousKeyboardState = new KeyboardState();
        _currentKeyboardState = new KeyboardState();
    }

    public void UpdateState(KeyboardState keyboardState, MouseState mouseState ) {

        _previousKeyboardState = _currentKeyboardState;
        _currentKeyboardState = keyboardState;

        _previousMouseState = _currentMouseState;
        _currentMouseState = mouseState;

    }

    public List<Event> GenerateEvents()
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

        // mouse drag
        if (_previousMouseState.LeftButton == ButtonState.Pressed & _currentMouseState.LeftButton == ButtonState.Pressed) {
            events.Add(new Event(new Vector2(_currentMouseState.X, _currentMouseState.Y), new Vector2(_currentMouseState.X, _currentMouseState.Y) - new Vector2(_previousMouseState.X, _previousMouseState.Y)));
            _mouseDragged = true;
        }

        // mouse press on release
        if (_previousMouseState.LeftButton == ButtonState.Pressed & _currentMouseState.LeftButton == ButtonState.Released & _mouseDragged == false) {
            events.Add(new Event(new Vector2(_currentMouseState.X, _currentMouseState.Y)));
        }

        return events;
    }

    public bool IsKeyDown(Keys key) {
        return _currentKeyboardState.IsKeyDown(key);
    }
}