
using Microsoft.Xna.Framework;

using GiraffeShooterClient.Utility;

namespace GiraffeShooterClient.Entity
{
    class MasterMap : Entity
    {
        public MasterMap()
        {
            id = new System.Guid();
            name = "MasterMap";

            Physics physics = new Physics();
            physics.isStatic = true;
            physics.position = new Vector3(0, 0, -0.1f);
            physics.size = new Vector3 (float.PositiveInfinity, float.PositiveInfinity, 0.1f);
            AddComponent(physics);

            Collider collider = new Collider();
            AddComponent(collider);

            Tiled tiled = new Tiled();
            tiled.Map = AssetManager.MapMaster;
            tiled.Tilesets = AssetManager.MapTilesets;
            tiled.TilesetTexture = AssetManager.MapTilesetTextureMain;
            AddComponent(tiled);
        }
    }
}