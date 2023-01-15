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
        public static Dictionary<int, TiledTileset> MapTilesets { get; private set; }
        public static Texture2D MapTilesetTextureMain { get; private set; }

        public static Texture2D PlayerTexture { get; private set; }
        public static Texture2D GiraffeTextureTest { get; private set; }
        
        public static Texture2D GiraffeSpriteTexture { get; private set; }
        public static Texture2D AmmunitionSpriteTexture { get; private set; }
        public static Texture2D ShootSpriteTexture { get; private set; }
        public static Texture2D PlayButtonTexture { get; private set; }
        public static Texture2D LoginButtonTexture { get; private set; }
        public static Texture2D SettingsButtonTexture { get; private set; }
        public static Texture2D BackButtonTexture { get; private set; }
        
        public static Texture2D VirtualControlBallTexture { get; private set; }
        public static Texture2D VirtualControlStickTexture { get; private set; }
        public static Texture2D VirtualControlShootTexture { get; private set; }
        
        public static SpriteFont Fontx1Normal { get; private set; }
        public static SpriteFont Fontx1Title { get; private set; }
        
        public static SpriteFont Fontx2Normal { get; private set; }
        public static SpriteFont Fontx2Title { get; private set; }
        
        public static SoundEffect Song { get; private set; }

        public static void LoadContent(ContentManager content)
        {

            MapMaster = new TiledMap("Content/map_master.tmx");
            MapTilesets = MapMaster.GetTiledTilesets("Content/");
            MapTilesetTextureMain = content.Load<Texture2D>("master_tileset");
            
            PlayerTexture = content.Load<Texture2D>("player_texture");
            GiraffeTextureTest = content.Load<Texture2D>("giraffe_texture_test");
            
            GiraffeSpriteTexture = content.Load<Texture2D>("Sprites/Giraffe");
            AmmunitionSpriteTexture = content.Load<Texture2D>("Sprites/DemoAmmunition");
            ShootSpriteTexture = content.Load<Texture2D>("Sprites/Shoot");
            PlayButtonTexture = content.Load<Texture2D>("Sprites/PlayButton");
            LoginButtonTexture = content.Load<Texture2D>("Sprites/LoginButton");
            SettingsButtonTexture = content.Load<Texture2D>("Sprites/SettingsButton");
            BackButtonTexture = content.Load<Texture2D>("Sprites/BackButton");
            
            VirtualControlBallTexture = content.Load<Texture2D>("Sprites/VirtualControlBall");
            VirtualControlStickTexture = content.Load<Texture2D>("Sprites/VirtualControlStick");
            VirtualControlShootTexture = content.Load<Texture2D>("Sprites/VirtualControlShoot");
            
            Fontx1Normal = content.Load<SpriteFont>("Fonts/x1/Normal");
            Fontx1Title = content.Load<SpriteFont>("Fonts/x1/Title");

            Fontx2Normal = content.Load<SpriteFont>("Fonts/x2/Normal");
            Fontx2Title = content.Load<SpriteFont>("Fonts/x2/Title");

            Song = content.Load<SoundEffect>("Music/Song");
            Song.Play();

        }

    }
}