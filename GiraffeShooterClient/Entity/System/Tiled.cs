using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using GiraffeShooterClient.Utility;

using TiledCS;

namespace GiraffeShooterClient.Entity
{
    class Tiled : Component
    {

        public TiledMap Map;
        public Dictionary<int, TiledTileset> Tilesets;
        public Texture2D TilesetTexture;

        enum Trans
        {
            None = 0,
            Flip_H = 1 << 0,
            Flip_V = 1 << 1,
            Flip_D = 1 << 2,

            Rotate_90 = Flip_D | Flip_H,
            Rotate_180 = Flip_H | Flip_V,
            Rotate_270 = Flip_V | Flip_D,

            Rotate_90AndFlip_H = Flip_H | Flip_V | Flip_D,
        }

        public Tiled()
        {
            TiledSystem.Register(this);
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            // get layers
            var tileLayers = Map.Layers.Where(x => x.type == TiledLayerType.TileLayer);

            // get the current camera position
            var cameraOffset = Camera.Offset;

            foreach (var layer in tileLayers)
            {
                for (var y = 0; y < layer.height; y++)
                {
                    for (var x = 0; x < layer.width; x++)
                    {
                        var index = (y * layer.width) + x; // Assuming the default render order is used which is from right to bottom
                        var gid = layer.data[index]; // The tileset tile index
                        var tileX = x * Map.TileWidth + (int)cameraOffset.X;
                        var tileY = y * Map.TileHeight + (int)cameraOffset.Y;

                        // Gid 0 is used to tell there is no tile set
                        if (gid == 0)
                        {
                            continue;
                        }

                        // Helper method to fetch the right TieldMapTileset instance
                        // This is a connection object Tiled uses for linking the correct tileset to the gid value using the firstgid property
                        var mapTileset = Map.GetTiledMapTileset(gid);

                        // Retrieve the actual tileset based on the firstgid property of the connection object we retrieved just now
                        var tileset = Tilesets[mapTileset.firstgid];

                        // Use the connection object as well as the tileset to figure out the source rectangle
                        var rect = Map.GetSourceRect(mapTileset, tileset, gid);

                        // Create destination and source rectangles
                        var source = new Rectangle(rect.x, rect.y, rect.width, rect.height);
                        var destination = new Rectangle(tileX, tileY, Map.TileWidth, Map.TileHeight);


                        // You can use the helper methods to get information to handle flips and rotations
                        Trans tileTrans = Trans.None;
                        if (Map.IsTileFlippedHorizontal(layer, x, y)) tileTrans |= Trans.Flip_H;
                        if (Map.IsTileFlippedVertical(layer, x, y)) tileTrans |= Trans.Flip_V;
                        if (Map.IsTileFlippedDiagonal(layer, x, y)) tileTrans |= Trans.Flip_D;

                        SpriteEffects effects = SpriteEffects.None;
                        double rotation = 0f;
                        switch (tileTrans)
                        {
                            case Trans.Flip_H: effects = SpriteEffects.FlipHorizontally; break;
                            case Trans.Flip_V: effects = SpriteEffects.FlipVertically; break;

                            case Trans.Rotate_90:
                                rotation = Math.PI * .5f;
                                destination.X += Map.TileWidth;
                                break;

                            case Trans.Rotate_180:
                                rotation = Math.PI;
                                destination.X += Map.TileWidth;
                                destination.Y += Map.TileHeight;
                                break;

                            case Trans.Rotate_270:
                                rotation = Math.PI * 3 / 2;
                                destination.Y += Map.TileHeight;
                                break;

                            case Trans.Rotate_90AndFlip_H:
                                effects = SpriteEffects.FlipHorizontally;
                                rotation = Math.PI * .5f;
                                destination.X += Map.TileWidth;
                                break;

                            default:
                                break;
                        }


                        // Render sprite at position tileX, tileY using the rect
                        spriteBatch.Draw(TilesetTexture, destination, source, Color.White, (float)rotation, Vector2.Zero, effects, 0);
                    }
                }
            }

        }

        public override void Deregister()
        {
            TiledSystem.Deregister(this);
        }
    }
}