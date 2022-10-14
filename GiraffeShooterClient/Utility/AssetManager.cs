using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using TiledCS; 

namespace GiraffeShooterClient.Utility.Assets
{
    public static class AssetManager
    {

        public static TiledMap MapMaster { get; private set; }
        public static TiledMap MapSecondary { get; private set; }
        public static Dictionary<int, TiledTileset> MapTilesets { get; private set; }
        public static Texture2D MapTilesetMaster { get; private set; }

        public static Texture2D GiraffeTextureTest { get; private set; }

        public static void LoadContent(ContentManager content)
        {

            // MapMaster = new TiledMap(content.RootDirectory + "/masterMap.tmx");
            // MapSecondary = new TiledMap(content.RootDirectory + "/secondaryMap.tmx");
            // MapTilesets = MapMaster.GetTiledTilesets(content.RootDirectory + "/");
            // MapTilesetMaster = content.Load<Texture2D>("masterTileset");

            GiraffeTextureTest = content.Load<Texture2D>("GiraffeTextureTest");
            
        }
    }
}