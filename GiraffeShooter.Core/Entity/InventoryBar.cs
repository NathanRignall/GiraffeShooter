using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;

using GiraffeShooterClient.Utility;
using Microsoft.Xna.Framework.Input;

namespace GiraffeShooterClient.Entity
{
    class InventoryBar : Entity
    {
        // TEMP RECORD TO CALL REMOVE ITEM
        public Inventory Inventory;
        
        private InventoryItem[] _items = new InventoryItem[5];
        private InventoryItem _selectedItem;

        public InventoryBar()
        {
            Id = Guid.NewGuid();
            Name = "InventoryBar";

            Screen screen = new Screen(new Vector2(0f, 3.0f), ScreenManager.CenterType.BottomCenter);
            AddComponent(screen);

            Sprite sprite = new Sprite(AssetManager.InventoryBarTexture, new Rectangle(338 * 5, 0, 338, 70));
            sprite.zOrder = 7;
            AddComponent(sprite);
            
            // add inventory items
            for (int i = 0; i < _items.Length; i++)
            {
                _items[i] = new InventoryItem(new Vector2(4.20f - ( 2.09f ) * i, 3.0f), this);
            }
        }
        
        public void SetSelected(InventoryItem item)
        {
            // check if bar contains item
            if (item.IsEmpty)
                return;
            
            // check if item is already selected
            if (_selectedItem == item)
                return;
            
            _selectedItem = item;
        }
        
        public void FillSlot(Meta meta)
        {
            // find empty slot
            for (int i = 0; i < _items.Length; i++)
            {
                if (_items[i].IsEmpty)
                {
                    // fill slot
                    _items[i].FillSlot(meta);
                    
                    // check if item is already selected
                    if (_selectedItem == null)
                        _selectedItem = _items[i];
                    return;
                }
            }

            // no empty slots
            throw new Exception("No empty slots in inventory bar");
        }

        public void EmptySlot()
        {
            // find selected slot
            for (int i = 0; i < _items.Length; i++)
            {
                if (_items[i] == _selectedItem)
                {
                    // remove item from players inventory (BAD CODE ATM)
                    Inventory.RemoveItem(_items[i].Meta);
                    
                    // empty slot
                    _items[i].EmptySlot();
                    
                    // select next slot if not empty
                    if (i < _items.Length - 1)
                    {
                        if (!_items[i + 1].IsEmpty)
                        {
                            _selectedItem = _items[i + 1];
                            return;
                        }
                    }
                    
                    // select previous slot if not empty
                    if (i > 0)
                    {
                        if (!_items[i - 1].IsEmpty)
                        {
                            _selectedItem = _items[i - 1];
                            return;
                        }
                    }
                    
                    // select any slot if not empty
                    for (int j = 0; j < _items.Length; j++)
                    {
                        if (!_items[j].IsEmpty)
                        {
                            _selectedItem = _items[j];
                            return;
                        }
                    }
                    
                    _selectedItem = null;
                }
            }
        }
        
        public bool IsFull()
        {
            foreach (InventoryItem item in _items)
            {
                if (item.IsEmpty)
                    return false;
            }
            
            return true;
        }

        public override void Update(GameTime gameTime)
        {
            // update inventory items
            foreach (InventoryItem item in _items)
            {
                if (item != null)
                    item.Update(gameTime);
            }
            
            if (_selectedItem != null)
                Inventory.SelectItem(_selectedItem.Meta);
        }

        public override void HandleEvents(List<Event> events)
        {
            if (ContextManager.Paused)
                return;
            
            // pass all events to inventory items
            foreach (InventoryItem item in _items)
            {
                item.HandleEvents(events);
            }
            
            // get the index of the selected item
            int index = Array.IndexOf(_items, _selectedItem);

            // use the index to set the texture offset of the inventory bar
            Sprite sprite = GetComponent<Sprite>();
            sprite.SourceRectangle = new Rectangle((index != -1 ? 338 * index : 338 * 5), 0, 338, 70);
            
            // handle events for self
            foreach (Event e in events)
            {
                switch (e.Type)
                {
                    case EventType.KeyPress:

                        switch (e.Key)
                        {
                            case Keys.Q:
                                EmptySlot();
                                break;
                        }

                        break;

                }
            }

        }
    }
}
