using System.Collections.Generic;

using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

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
        
        
        public static Texture2D GiraffeSpriteTexture { get; private set; }
        public static Texture2D AmmunitionSpriteTexture { get; private set; }
        public static Texture2D PlayButtonTexture { get; private set; }
        public static Texture2D LoginButtonTexture { get; private set; }
        public static Texture2D SettingsButtonTexture { get; private set; }
        public static Texture2D BackButtonTexture { get; private set; }
        
        public static Texture2D VirtualControlArrowTexture { get; private set; }
        public static Texture2D VirtualControlBallTexture { get; private set; }
        public static Texture2D VirtualControlStickTexture { get; private set; }
        
        public static SpriteFont Font { get; private set; }
        public static SpriteFont FontTitle { get; private set; }

        public static SoundEffect Song { get; private set; }

        public static void LoadContent(ContentManager content)
        {

            MapMaster = new TiledMap("Content/map_master.tmx");
            // MapMasterCollisionLayer = MapMaster.Layers.First(l => l.name == "Ground");

            MapTilesets = MapMaster.GetTiledTilesets("Content/");

            MapTilesetTextureMain = content.Load<Texture2D>("master_tileset");
            
            PlayerTexture = content.Load<Texture2D>("player_texture");
            GiraffeTextureTest = content.Load<Texture2D>("giraffe_texture_test");
            
            GiraffeSpriteTexture = content.Load<Texture2D>("Sprites/Giraffe");
            AmmunitionSpriteTexture = content.Load<Texture2D>("Sprites/DemoAmmunition");
            PlayButtonTexture = content.Load<Texture2D>("Sprites/PlayButton");
            LoginButtonTexture = content.Load<Texture2D>("Sprites/LoginButton");
            SettingsButtonTexture = content.Load<Texture2D>("Sprites/SettingsButton");
            BackButtonTexture = content.Load<Texture2D>("Sprites/BackButton");
            
            VirtualControlArrowTexture = content.Load<Texture2D>("Sprites/VirtualControlArrow");
            VirtualControlBallTexture = content.Load<Texture2D>("Sprites/VirtualControlBall");
            VirtualControlStickTexture = content.Load<Texture2D>("Sprites/VirtualControlStick");
            
            Font = content.Load<SpriteFont>("Fonts/Normal");
            FontTitle = content.Load<SpriteFont>("Fonts/Title");

            Song = content.Load<SoundEffect>("Music/Song");
            Song.Play();

        }
    }
}