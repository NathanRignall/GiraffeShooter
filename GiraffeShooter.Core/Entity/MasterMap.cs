using System;
using Microsoft.Xna.Framework;

using GiraffeShooterClient.Utility;

namespace GiraffeShooterClient.Entity
{
    class MasterMap : Entity
    {
        public MasterMap()
        {
            Id = Guid.NewGuid();
            Name = "MasterMap";

            Physics physics = new Physics();
            physics.IsStatic = true;
            physics.Position = new Vector3(0, 0, -0.1f);
            physics.Size = new Vector3 (float.PositiveInfinity, float.PositiveInfinity, 0.1f);
            AddComponent(physics);

            Tiled tiled = new Tiled(AssetManager.MapMaster, AssetManager.MapTilesets, AssetManager.MapTilesetTextureMain, new Vector2(-50,-50));
            AddComponent(tiled);
        }
    }
}