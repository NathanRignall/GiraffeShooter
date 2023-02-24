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
        public static Texture2D AmmunitionTexture { get; private set; }
        public static Texture2D AmmunitionInventoryTexture { get; private set; }
        public static Texture2D AmmunitionFireTexture { get; private set; }
        public static Texture2D PistolTexture { get; private set; }
        public static Texture2D PistolInventoryTexture { get; private set; }
        public static Texture2D MachineGunTexture { get; private set; }
        public static Texture2D MachineGunInventoryTexture { get; private set; }
        public static Texture2D ShootSpriteTexture { get; private set; }
        
        public static Texture2D PlayButtonTexture { get; private set; }
        public static Texture2D LeaderboardButtonTexture { get; private set; }
        public static Texture2D LoginButtonTexture { get; private set; }
        public static Texture2D RegisterButtonTexture { get; private set; }
        public static Texture2D SettingsButtonTexture { get; private set; }
        public static Texture2D BackButtonTexture { get; private set; }
        public static Texture2D ExitButtonTexture { get; private set; }
        public static Texture2D ResumeButtonTexture { get; private set; }
        public static Texture2D PauseButtonTexture { get; private set; }
        public static Texture2D CameraButtonTexture { get; private set; }
        public static Texture2D InventoryButtonTexture { get; private set; }
        
        public static Texture2D InputTexture { get; private set; }
        
        
        public static Texture2D PausedCoverTexture { get; private set; }
        
        public static Texture2D InventoryBarTexture { get; private set; }
        public static Texture2D InventoryItemTexture { get; private set; }
        
        public static Texture2D VirtualControlBallTexture { get; private set; }
        public static Texture2D VirtualControlStickTexture { get; private set; }
        public static Texture2D VirtualControlShootTexture { get; private set; }
        public static Texture2D VirtualControlDropTexture { get; private set; }
        
        public static SpriteFont Fontx1Normal { get; private set; }
        public static SpriteFont Fontx1Title { get; private set; }
        
        public static SpriteFont Fontx2Normal { get; private set; }
        public static SpriteFont Fontx2Title { get; private set; }
        
        public static SoundEffect Song { get; private set; }

        public static void LoadContent(ContentManager content)
        {

            MapMaster = new TiledMap("Content/Tiles/map_master.tmx");
            MapTilesets = MapMaster.GetTiledTilesets("Content/Tiles/");
            MapTilesetTextureMain = content.Load<Texture2D>("Tiles/master_tileset");
            
            PlayerTexture = content.Load<Texture2D>("player_texture");
            GiraffeTextureTest = content.Load<Texture2D>("giraffe_texture_test");
            
            GiraffeSpriteTexture = content.Load<Texture2D>("Sprites/Giraffe");
            AmmunitionTexture = content.Load<Texture2D>("Sprites/Ammunition");
            AmmunitionInventoryTexture = content.Load<Texture2D>("Sprites/AmmunitionInventory");
            AmmunitionFireTexture = content.Load<Texture2D>("Sprites/AmmunitionFire");
            PistolTexture = content.Load<Texture2D>("Sprites/Pistol");
            PistolInventoryTexture = content.Load<Texture2D>("Sprites/PistolInventory");
            MachineGunTexture = content.Load<Texture2D>("Sprites/MachineGun");
            MachineGunInventoryTexture = content.Load<Texture2D>("Sprites/MachineGunInventory");
            ShootSpriteTexture = content.Load<Texture2D>("Sprites/Shoot");
            
            PlayButtonTexture = content.Load<Texture2D>("Sprites/PlayButton");
            LeaderboardButtonTexture = content.Load<Texture2D>("Sprites/LeaderboardButton");
            LoginButtonTexture = content.Load<Texture2D>("Sprites/LoginButton");
            RegisterButtonTexture = content.Load<Texture2D>("Sprites/RegisterButton");
            SettingsButtonTexture = content.Load<Texture2D>("Sprites/SettingsButton");
            BackButtonTexture = content.Load<Texture2D>("Sprites/BackButton");
            ExitButtonTexture = content.Load<Texture2D>("Sprites/ExitButton");
            ResumeButtonTexture = content.Load<Texture2D>("Sprites/ResumeButton");
            PauseButtonTexture = content.Load<Texture2D>("Sprites/PauseButton");
            CameraButtonTexture = content.Load<Texture2D>("Sprites/CameraButton");
            InventoryButtonTexture = content.Load<Texture2D>("Sprites/InventoryButton");

            InputTexture = content.Load<Texture2D>("Sprites/Input");
            
            PausedCoverTexture = content.Load<Texture2D>("Sprites/PausedCover");
            
            InventoryBarTexture = content.Load<Texture2D>("Sprites/InventoryBar");
            InventoryItemTexture = content.Load<Texture2D>("Sprites/InventoryItem");

            VirtualControlBallTexture = content.Load<Texture2D>("Sprites/VirtualControlBall");
            VirtualControlStickTexture = content.Load<Texture2D>("Sprites/VirtualControlStick");
            VirtualControlShootTexture = content.Load<Texture2D>("Sprites/VirtualControlShoot");
            VirtualControlDropTexture = content.Load<Texture2D>("Sprites/VirtualControlDrop");
            
            Fontx1Normal = content.Load<SpriteFont>("Fonts/x1/Normal");
            Fontx1Title = content.Load<SpriteFont>("Fonts/x1/Title");

            Fontx2Normal = content.Load<SpriteFont>("Fonts/x2/Normal");
            Fontx2Title = content.Load<SpriteFont>("Fonts/x2/Title");

            Song = content.Load<SoundEffect>("Music/Song");
            Song.Play();

        }

    }
}