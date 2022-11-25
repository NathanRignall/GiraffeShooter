using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using TiledCS; 

namespace GiraffeShooterClient.Utility.Assets
{
    public static class AssetManager
    {

        public static TiledMap MapMaster { get; private set; }
        // public static TiledLayer MapMasterCollisionLayer { get; private set; }

        public static Dictionary<int, TiledTileset> MapTilesets { get; private set; }
        
        public static Texture2D MapTilesetTextureMain { get; private set; }

        public static Texture2D GiraffeTextureTest { get; private set; }

        public static void LoadContent(ContentManager content)
        {

            MapMaster = new TiledMap(content.RootDirectory + "/map_master.tmx");
            // MapMasterCollisionLayer = MapMaster.Layers.First(l => l.name == "Ground");

            MapTilesets = MapMaster.GetTiledTilesets(content.RootDirectory + "/");

            MapTilesetTextureMain = content.Load<Texture2D>("master_tileset");

            GiraffeTextureTest = content.Load<Texture2D>("giraffe_texture_test");
            
        }
    }
}