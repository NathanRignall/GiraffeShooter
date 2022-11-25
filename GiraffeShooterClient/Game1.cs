using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using GiraffeShooterClient.Container.Game;
using GiraffeShooterClient.Container.Camera;

using GiraffeShooterClient.Entity.System;
using GiraffeShooterClient.Utility.Assets;
using GiraffeShooterClient.Utility.Input;

namespace GiraffeShooterClient;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private InputManager _inputManager;

    private const int _scaleFactor = 1;
    private Matrix _transformMatrix;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        _graphics.PreferredBackBufferWidth = 1500;
        _graphics.PreferredBackBufferHeight = 1000;

        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        
        _inputManager = new InputManager();
    }

    protected override void Initialize()
    {
        base.Initialize();

        _transformMatrix = Matrix.CreateScale(_scaleFactor, _scaleFactor, 1f);

        GameContext.Initialize();
        CameraContext.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        AssetManager.LoadContent(Content);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // get network state

        // update the game state (for keys)
        _inputManager.UpdateState(Keyboard.GetState(), Mouse.GetState());

        // create empty event list
        List<Event> events = new List<Event>();

        // generate events
        if (IsActive) {
            events = _inputManager.GenerateEvents();
        }

        // send the events to camera
        CameraContext.HandleEvents(events);

        // update the contexts (this is for animations etc)
        switch (GameContext.CurrentState)
        {
            case GameContext.State.SplashScreen:
                GameContext.SplashScreenContext.Update(gameTime);
                break;

            case GameContext.State.World:
                GameContext.WorldContext.HandleEvents(events);
                GameContext.WorldContext.Update(gameTime);
                break;

            default:
                throw new System.Exception();
        }

        // update the camera context
        CameraContext.Update(gameTime);
        
        // set network state

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);
        
        switch (GameContext.CurrentState)
        {
            case GameContext.State.SplashScreen:
                _spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: _transformMatrix);
                GameContext.SplashScreenContext.Draw(gameTime, _spriteBatch);
                _spriteBatch.End();
                break;

            case GameContext.State.World:
                _spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: _transformMatrix);
                GameContext.WorldContext.Draw(gameTime, _spriteBatch);
                _spriteBatch.End();
                break;

            default:
                throw new System.Exception();
        }

        base.Draw(gameTime);
    }

}
