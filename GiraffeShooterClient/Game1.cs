using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using TiledCS; 

namespace GiraffeShooterClient;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    private TiledMap _map;
    private Dictionary<int, TiledTileset> _tilesets;
    private Texture2D _tilesetTexture;
    private TiledLayer _collisionLayer;

    const int scaleFactor = 1;
    private Matrix _transformMatrix;

    private int _camera_x = 0;
    private int _camera_y = 0;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        
        _graphics.PreferredBackBufferWidth = 1500;  // set this value to the desired width of your window
        _graphics.PreferredBackBufferHeight = 1000;
    }

    protected override void Initialize()
    {
        _transformMatrix = Matrix.CreateScale(scaleFactor, scaleFactor, 1f);

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        _map = new TiledMap(Content.RootDirectory + "/base.tmx");
        _tilesets = _map.GetTiledTilesets(Content.RootDirectory + "/");

        _tilesetTexture = Content.Load<Texture2D>("spr_road_2_strip29");

        _collisionLayer = _map.Layers.First(l => l.name == "Solid");
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        if (Keyboard.GetState().IsKeyDown(Keys.Down))
            _camera_y = _camera_y - 1;

        if (Keyboard.GetState().IsKeyDown(Keys.Up))
            _camera_y = _camera_y + 1;

        if (Keyboard.GetState().IsKeyDown(Keys.Right))
            _camera_x = _camera_x - 1;

        if (Keyboard.GetState().IsKeyDown(Keys.Left))
            _camera_x = _camera_x + 1;
        

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);

       _spriteBatch.Begin(samplerState: SamplerState.PointClamp, transformMatrix: _transformMatrix);

            var tileLayers = _map.Layers.Where(x => x.type == TiledLayerType.TileLayer);

            foreach (var layer in tileLayers)
            {
                for (var y = 0; y < layer.height; y++)
                {
                    for (var x = 0; x < layer.width; x++)
                    {
                        var index = (y * layer.width) + x;
                        var gid = layer.data[index];
                        var tileX = (( _graphics.PreferredBackBufferWidth -  _map.TileWidth ) / 2 ) - ( (y + _camera_y) * _map.TileWidth / 2) + ( (x + _camera_x)  * _map.TileWidth / 2);
                        var tileY = ( (x + _camera_x) * _map.TileHeight / 2) + ( (y + _camera_y) * _map.TileHeight / 2);

                        if (gid == 0)
                        {
                            continue;
                        }

                        var mapTileset = _map.GetTiledMapTileset(gid);

                        var tileset = _tilesets[mapTileset.firstgid];

                        var rect = _map.GetSourceRect(mapTileset, tileset, gid);

                        var source = new Rectangle(rect.x, rect.y, rect.width, rect.height);
                        var destination = new Rectangle(tileX, tileY, _map.TileWidth, _map.TileHeight);

                        SpriteEffects effects = SpriteEffects.None;
                        if (_map.IsTileFlippedHorizontal(layer, x, y))
                        {
                            effects |= SpriteEffects.FlipHorizontally;
                        }
                        if (_map.IsTileFlippedVertical(layer, x, y))
                        {
                            effects |= SpriteEffects.FlipVertically;
                        }

                        _spriteBatch.Draw(_tilesetTexture, destination, source, Color.White, 0f, Vector2.Zero, effects, 0);
                    }
                }
            }

            foreach (var obj in _collisionLayer.objects)
            {
                Texture2D _texture = new Texture2D(GraphicsDevice, 1, 1);
                _texture.SetData(new Color[] { Color.Green });

                var objRect = new Rectangle((int)obj.x, (int)obj.y, (int)obj.width, (int)obj.height);

                _spriteBatch.Draw(_texture, (Rectangle)objRect, Color.White);
            }

            Texture2D _sprite_texture = new Texture2D(GraphicsDevice, 1, 1);
            _sprite_texture.SetData(new Color[] { Color.Red });

            Vector2 location = new Vector2(0,0);
            SpriteEffects effects2 = SpriteEffects.None;

            var sprite = new Rectangle((int)0, (int)0, (int)64, (int)64);

            _spriteBatch.Draw(_sprite_texture, location, (Rectangle)sprite, Color.White, 10f, Vector2.Zero, 1f, effects2, 0);

            _spriteBatch.End();

        base.Draw(gameTime);
    }
}
