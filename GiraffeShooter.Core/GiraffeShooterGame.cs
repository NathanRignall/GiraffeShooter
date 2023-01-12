﻿using System.Collections.Generic;

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
        ScreenManager.SetResolution("1920x1080");
        
#if __IOS__
        ScreenManager.Size = new Vector2(GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width, GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height);
#endif
        
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
        ContextManager.Initialize();
        VirtualManager.Initialize();

        //Microsoft.Xna.Framework.Input.KeyboardInput.Show("en-US", "Giraffe Shooter", "Enter your name", false);
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        AssetManager.LoadContent(Content);
    }

    protected override void Update(GameTime gameTime)
    {
        ContextManager.SwitchState();

// #if !__IOS__
//         if (Keyboard.GetState().IsKeyDown(Keys.Escape))
//             Exit();
// #endif

        // update the game state (for keys)
        InputManager.UpdateState(Keyboard.GetState(), Mouse.GetState());

        // create empty event list
        List<Event> events = new List<Event>();

        // generate events
        if (IsActive) {
            events = InputManager.GenerateEvents();
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

            case ContextManager.State.World:
                VirtualManager.HandleEvents(events);
                VirtualManager.Update(gameTime);
                
                Camera.HandleEvents(events);
                Camera.Update(gameTime);

                ContextManager.WorldContext.HandleEvents(events);
                ContextManager.WorldContext.Update(gameTime);
                break;

            default:
                throw new System.Exception();
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
                _spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: transformMatrix);
                ContextManager.SplashScreenContext.Draw(gameTime, _spriteBatch);
                _spriteBatch.End();
                break;
            
            case ContextManager.State.Menu:
                _spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: transformMatrix);
                ContextManager.MenuContext.Draw(gameTime, _spriteBatch);
                _spriteBatch.End();
                break;

            case ContextManager.State.World:
                _spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: transformMatrix);
                ContextManager.WorldContext.Draw(gameTime, _spriteBatch);

                if (InputManager.TouchConnected)
                    VirtualManager.Draw(gameTime, _spriteBatch);

                _spriteBatch.End();
                
                break;

            default:
                throw new System.Exception();
        }

        base.Draw(gameTime);
    }

}
