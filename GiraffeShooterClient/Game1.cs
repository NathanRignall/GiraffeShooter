using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using GiraffeShooterClient.Container.Game;
using GiraffeShooterClient.Entity.System;
using GiraffeShooterClient.Utility.Assets;
using GiraffeShooterClient.Utility.Input;

namespace GiraffeShooterClient;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private InputManager _inputManager;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;

        _inputManager = new InputManager();
    }

    protected override void Initialize()
    {
        base.Initialize();

        GameContext.Initialize();
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
        _inputManager.UpdateState(Keyboard.GetState());

        // generate events from the key presses
        List<Event> events = _inputManager.GenerateEvents();

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

        // update the camera

        // set network state

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // draw the correct context
        
        switch (GameContext.CurrentState)
        {
            case GameContext.State.SplashScreen:
                _spriteBatch.Begin();
                GameContext.SplashScreenContext.SplashScreenRender.Draw(gameTime, _spriteBatch);
                _spriteBatch.End();
                break;

            case GameContext.State.World:
                _spriteBatch.Begin();
                GameContext.WorldContext.WorldRender.Draw(gameTime, _spriteBatch);
                _spriteBatch.End();
                break;

            default:
                throw new System.Exception();
        }

        base.Draw(gameTime);
    }

}
