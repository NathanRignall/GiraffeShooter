using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using GiraffeShooterClient.Container.Game;
using GiraffeShooterClient.Entity.System;
using GiraffeShooterClient.Utility.Assets;

namespace GiraffeShooterClient;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private Giraffe _giraffe1;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        base.Initialize();

        _giraffe1 = new Giraffe();

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

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        // switch (GameContext.CurrentState)
        // {
        //     case GameContext.State.SplashScreen:
        //         _spriteBatch.Begin();
        //         GameContext.SplashScreenContext.SplashScreenRender.Draw(gameTime, _spriteBatch);
        //         _spriteBatch.End();
        //         break;

        //     default:
        //         throw new System.Exception();
        // }

        // test code
        _spriteBatch.Begin();
        SpriteSystem.Draw(gameTime, _spriteBatch);
        _spriteBatch.End();

        base.Draw(gameTime);
    }

}
