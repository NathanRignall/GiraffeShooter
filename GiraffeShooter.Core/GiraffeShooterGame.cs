using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using GiraffeShooterClient.Container.Game;
using GiraffeShooterClient.Utility;

namespace GiraffeShooterClient;

public class GiraffeShooter : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private float _scaleFactor = 1f;

    public GiraffeShooter()
    {
        ScreenManager.SetResolution("1280x720");

        _graphics = new GraphicsDeviceManager(this);
        _graphics.PreferredBackBufferWidth = (int)ScreenManager.Size.X;
        _graphics.PreferredBackBufferHeight = (int)ScreenManager.Size.Y;

        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        base.Initialize();

        Camera.Initialize();
        InputManager.Initialize(); 
        SupabaseManager.Initialize();

        GameContext.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        AssetManager.LoadContent(Content);
    }

    protected override void Update(GameTime gameTime)
    {

// #if !__IOS__
//         if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
//             Exit();
// #endif

        // change game state
        GameContext.CurrentState = GameContext.NextState;

        // get network state

        // update the game state (for keys)
        InputManager.UpdateState(Keyboard.GetState(), Mouse.GetState());

        // create empty event list
        List<Event> events = new List<Event>();

        // generate events
        if (IsActive) {
            events = InputManager.GenerateEvents();
        }

        // send the events to camera
        Camera.HandleEvents(events);

        // reset scale factor
        _scaleFactor = 1f;

        // update the contexts (this is for animations etc)
        switch (GameContext.CurrentState)
        {
            case GameContext.State.SplashScreen:
                GameContext.SplashScreenContext.Update(gameTime);
                break;
            
            case GameContext.State.Menu:
                GameContext.MenuContext.HandleEvents(events);
                GameContext.MenuContext.Update(gameTime);
                break;

            case GameContext.State.World:
                _scaleFactor = Camera.Zoom;
                GameContext.WorldContext.HandleEvents(events);
                GameContext.WorldContext.Update(gameTime);
                break;

            default:
                throw new System.Exception();
        }

        // update the camera context
        Camera.Update(gameTime);
        
        // update monogame
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        var transformMatrix = Matrix.CreateScale(1f, 1f, 1f);
        
        switch (GameContext.CurrentState)
        {
            case GameContext.State.SplashScreen:
                _spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: transformMatrix);
                GameContext.SplashScreenContext.Draw(gameTime, _spriteBatch);
                _spriteBatch.End();
                break;
            
            case GameContext.State.Menu:
                _spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: transformMatrix);
                GameContext.MenuContext.Draw(gameTime, _spriteBatch);
                _spriteBatch.End();
                break;

            case GameContext.State.World:
                transformMatrix = Matrix.CreateScale(_scaleFactor, _scaleFactor, 1f);
                _spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: transformMatrix);
                GameContext.WorldContext.Draw(gameTime, _spriteBatch);
                _spriteBatch.End();
                break;

            default:
                throw new System.Exception();
        }

        base.Draw(gameTime);
    }

}
