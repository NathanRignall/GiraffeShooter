using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;

using GiraffeShooterClient.Utility;
using Microsoft.Xna.Framework.Graphics;

namespace GiraffeShooterClient.Entity
{
    
    class InventoryItem : Entity
    {
        private static Dictionary<Type, Texture2D> _textures = new Dictionary<Type, Texture2D>
        {
            { typeof(MetaAmmunition), AssetManager.AmmunitionInventoryTexture },
            { typeof(MetaPistol), AssetManager.PistolInventoryTexture}
        };

        public bool IsEmpty { get; set; } = true;
        public Meta Meta { get; set; } = new Meta();
        
        public InventoryBar ParentBar { get; set; }
        
        public InventoryItem(Vector2 offset, InventoryBar _parentBar)
        {
            Id = Guid.NewGuid();
            Name = "InventoryItem";
            
            ParentBar = _parentBar;

            Screen screen = new Screen(offset, ScreenManager.CenterType.BottomCenter);
            AddComponent(screen);

            Sprite sprite = new Sprite(AssetManager.InventoryItemTexture);
            sprite.zOrder = 9;
            AddComponent(sprite);

            Text text = new Text();
            text.String = "";
            text.Offset = new Vector2(0, 64);
            AddComponent(text);
        }
        
        public void FillSlot(Meta meta)
        {
            Sprite sprite = GetComponent<Sprite>();
            sprite.Texture = _textures[meta.GetType()];
            IsEmpty = false;
            Meta = meta;
        }
        
        public void EmptySlot()
        {
            Sprite sprite = GetComponent<Sprite>();
            sprite.Texture = AssetManager.InventoryItemTexture;
            IsEmpty = true;
        }
        
        public override void Update(GameTime gameTime)
        {
            if (!IsEmpty && Meta.Quantity > 0)
            {
                Text text = GetComponent<Text>();
                text.String = Meta.Quantity.ToString();
            }
            else
            {
                Text text = GetComponent<Text>();
                text.String = "";
            }
        }
        
        public override void HandleEvents(List<Event> events)
        {
            foreach (Event e in events)
            {
                switch (e.Type)
                {
                    case EventType.MouseRelease:
                    case EventType.TouchPress:

                        if (GetComponent<Sprite>().Bounds.Contains(e.Position / ScreenManager.GetScaleFactor()))
                        {
                            ParentBar.SetSelected(this);
                        }
                        break;
                }
            }
        }
    }
}
