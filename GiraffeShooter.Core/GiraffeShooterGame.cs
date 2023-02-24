using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using GiraffeShooterClient.Utility;

namespace GiraffeShooterClient;

public class GiraffeShooter : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    public GiraffeShooter()
    {
        
        _graphics = new GraphicsDeviceManager(this);

#if !__ANDROID__ && !__IOS__
        ScreenManager.SetResolution("1280x720");
        _graphics.PreferredBackBufferWidth = (int)ScreenManager.Size.X;
        _graphics.PreferredBackBufferHeight = (int)ScreenManager.Size.Y;
#endif

        Content.RootDirectory = "Content";
        IsMouseVisible = false;
    }

    protected override void Initialize()
    {
        base.Initialize();

        Camera.Initialize();
        InputManager.Initialize(); 
        SupabaseManager.Initialize();
        ContextManager.Initialize();
        VirtualManager.Initialize();
    }

    protected override void LoadContent()
    {
        
#if __IOS__ || __ANDROID__
        _graphics.IsFullScreen = true;
        _graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
        _graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
        _graphics.ApplyChanges();
        ScreenManager.Size = new Vector2(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height);
#endif
        
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        AssetManager.LoadContent(Content);
    }

    protected override void Update(GameTime gameTime)
    {
        // switch the game state at the start of the update
        ContextManager.SwitchState();

        // update the game state (for keys)
        InputManager.UpdateState(Keyboard.GetState(), Mouse.GetState());

        // create empty event list
        List<Event> events = new List<Event>();

        // generate events
        if (IsActive) {
            events = InputManager.GenerateEvents(gameTime);
        }

        // update the contexts (this is for animations etc)
        switch (ContextManager.CurrentState)
        {
            case ContextManager.State.SplashScreen:
                ContextManager.SplashScreenContext.Update(gameTime);
                break;
            
            case ContextManager.State.Menu:
                ContextManager.MenuContext.HandleEvents(events);
                ContextManager.MenuContext.Update(gameTime);
                
                Camera.Update(gameTime);
                break;
            
            case ContextManager.State.Leaderboard:
                ContextManager.LeaderboardContext.HandleEvents(events);
                ContextManager.LeaderboardContext.Update(gameTime);
                
                Camera.Update(gameTime);
                break;

            case ContextManager.State.World:

                if (InputManager.TouchConnected)
                {
                    VirtualManager.HandleEvents(events, gameTime);
                    VirtualManager.Update(gameTime);
                }

                Camera.HandleEvents(events);
                Camera.Update(gameTime);

                ContextManager.WorldContext.HandleEvents(events);
                ContextManager.WorldContext.Update(gameTime);
                break;
            
            case ContextManager.State.Win:
                ContextManager.WinContext.HandleEvents(events);
                ContextManager.WinContext.Update(gameTime);
                break;
            
            case ContextManager.State.Lose:
                ContextManager.LoseContext.HandleEvents(events);
                ContextManager.LoseContext.Update(gameTime);
                break;
        }

        // update monogame
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

        var transformMatrix = Matrix.CreateScale(1f, 1f, 1f);
        
        switch (ContextManager.CurrentState)
        {
            case ContextManager.State.SplashScreen:
                IsMouseVisible = false;
                _spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: transformMatrix);
                ContextManager.SplashScreenContext.Draw(gameTime, _spriteBatch);
                _spriteBatch.End();
                break;
            
            case ContextManager.State.Menu:
                IsMouseVisible = true;
                _spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: transformMatrix);
                ContextManager.MenuContext.Draw(gameTime, _spriteBatch);
                _spriteBatch.End();
                break;
            
            case ContextManager.State.Leaderboard:
                IsMouseVisible = true;
                _spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: transformMatrix);
                ContextManager.LeaderboardContext.Draw(gameTime, _spriteBatch);
                _spriteBatch.End();
                break;

            case ContextManager.State.World:
                IsMouseVisible = ContextManager.Paused;
                _spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: transformMatrix);
                ContextManager.WorldContext.Draw(gameTime, _spriteBatch);

                if (InputManager.TouchConnected && !ContextManager.Paused)
                    VirtualManager.Draw(gameTime, _spriteBatch);

                _spriteBatch.End();
                
                break;
            
            case ContextManager.State.Win:
                IsMouseVisible = false;
                _spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: transformMatrix);
                ContextManager.WinContext.Draw(gameTime, _spriteBatch);
                _spriteBatch.End();
                break;
            
            case ContextManager.State.Lose:
                IsMouseVisible = false;
                _spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: transformMatrix);
                ContextManager.LoseContext.Draw(gameTime, _spriteBatch);
                _spriteBatch.End();
                break;
            
            case ContextManager.State.Exit:
                Exit();
                break;
        }

        base.Draw(gameTime);
    }

}
