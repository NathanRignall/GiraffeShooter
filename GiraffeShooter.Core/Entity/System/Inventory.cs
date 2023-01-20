using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace GiraffeShooterClient.Entity
{
    class Inventory : Component
    {
        private InventoryBar _inventoryBar;
        
        public List<Meta> items = new List<Meta>();
        public List<Meta> itemsInUse = new List<Meta>();
        public Meta selectedItem;
        public int MaxItems = 3;
        
        public Inventory(InventoryBar inventoryBar = null)
        {
            _inventoryBar = inventoryBar;
            
            InventorySystem.Register(this);
        }
        
        public bool AddItem(Meta item)
        {

            // before adding item check has quantity
            if (item.Quantity != 0)
            {
                // find item of same type
                foreach (var i in items)
                {
                    if (i.GetType() == item.GetType())
                    {
                        // check if item max quantity is reached
                        if ((i.Quantity + item.Quantity) < item.MaxQuantity)
                        {
                            // add item to inventory
                            i.Quantity += item.Quantity;
                            return true;
                        } 
                        
                        // if max quantity is reached, add the difference to inventory
                        if (item.Quantity < i.MaxQuantity)
                        {
                            var quantity = i.MaxQuantity - i.Quantity;
                            i.Quantity += quantity;
                            
                            item.Quantity -= quantity;
                        }
                    }
                }
            }
            
            // check if full
            if (items.Count >= MaxItems)
                return false;
            
            // then add item
            items.Add(item);
            
            // check if the inventory bar is set
            if (_inventoryBar != null)
            {
                // check if the inventory bar is full
                if (!_inventoryBar.IsFull())
                {
                    // fill slot with item type and id
                    _inventoryBar.FillSlot(item);
                }
            }
            
            return true;
            
        }
        
        public void RemoveItem(Meta item)
        {
            // write meta item data to console
            Console.WriteLine("Item: " + item.GetType() + " " + item.Id + " " + item.Quantity);
            items.Remove(item);
        }

        public void SelectItem(Meta item)
        {
            selectedItem = item;
        }

        public override void Update(GameTime gameTime)
        {
            _inventoryBar.Update(gameTime);
        }

        public override void Deregister()
        {
            InventorySystem.Deregister(this);
        }
    }
}
