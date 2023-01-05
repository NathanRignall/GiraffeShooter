using System.Collections.Generic;

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using TiledCS; 

namespace GiraffeShooterClient.Utility
{
    public static class AssetManager
    {

        public static TiledMap MapMaster { get; private set; }
        // public static TiledLayer MapMasterCollisionLayer { get; private set; }

        public static Dictionary<int, TiledTileset> MapTilesets { get; private set; }
        
        public static Texture2D MapTilesetTextureMain { get; private set; }

        public static Texture2D PlayerTexture { get; private set; }
        public static Texture2D GiraffeTextureTest { get; private set; }

        public static void LoadContent(ContentManager content)
        {

            MapMaster = new TiledMap("Content/map_master.tmx");
            // MapMasterCollisionLayer = MapMaster.Layers.First(l => l.name == "Ground");

            MapTilesets = MapMaster.GetTiledTilesets("Content/");

            MapTilesetTextureMain = content.Load<Texture2D>("master_tileset");
            
            PlayerTexture = content.Load<Texture2D>("player_texture");
            GiraffeTextureTest = content.Load<Texture2D>("giraffe_texture_test");
            
        }
    }
}