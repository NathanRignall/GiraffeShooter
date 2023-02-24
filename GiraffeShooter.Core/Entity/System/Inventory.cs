using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;

using GiraffeShooterClient.Utility;

namespace GiraffeShooterClient.Entity
{
    public class Inventory : Component
    {
        private InventoryBar _inventoryBar;
        
        public List<Meta> Items = new List<Meta>();
        public Meta selectedItem;
        public int MaxItems = 5;

        public Inventory(InventoryBar inventoryBar = null)
        {
            _inventoryBar = inventoryBar;
            
            InventorySystem.Register(this);
        }
        
        public void SelectItem(Meta item)
        {
            selectedItem = item;
        }
        
        public bool AddItem(Meta item)
        {

            // before adding item check has quantity
            if (item.Quantity != 0)
            {
                // find item of same type
                foreach (var i in Items)
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
            if (Items.Count >= MaxItems)
                return false;
            
            // then add item
            Items.Add(item);
            
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
            // get the player physics component
            var physics = entity.GetComponent<Physics>();
            
            // place the item at a random position around the player (outside of size)
            var position = new Vector3(physics.Position.X + (float) (new Random().NextDouble() * 2 - 1) * physics.Size.X, physics.Position.Y + (float) (new Random().NextDouble() * 2 - 1) * physics.Size.Y, 0);
            
            // give a velocity along the vector from the player to the item
            var velocity = (position - physics.Position) * 10;
            
            // create the item entity and add it to the world
            item.Create(position, velocity);
            
            // remove item from inventory
            Items.Remove(item);
        }
        
        public bool HasItem(Type type)
        {
            foreach (var item in Items)
            {
                if (item.GetType() == type)
                    return true;
            }
            
            return false;
        }
        
        public bool ReduceItem(Type type, int quantity)
        {
            foreach (var item in Items)
            {
                if (item.GetType() == type)
                {
                    if (item.Quantity >= quantity)
                    {
                        item.Quantity -= quantity;

                        if (item.Quantity == 0)
                        {
                            if (_inventoryBar != null)
                                _inventoryBar.RemoveItem(item);
                        
                            Items.Remove(item);
                        }
                        
                        return true;
                    }
                }
            }
            
            return false;
        }
        
        public bool Action(TimeSpan time)
        {
            if (selectedItem != null)
            {
                return selectedItem.Action(time, entity);
            }
            
            return false;
        }
        
        public override void Update(GameTime gameTime)
        {
            if (ContextManager.Paused)
                return;
            
            // clear the inventory of items with 0 quantity
            for (int i = 0; i < Items.Count; i++)
            {
                if (Items[i].Quantity == 0 && Items[i].MaxQuantity != 0)
                {
                    Items.RemoveAt(i);
                    i--;
                }
            }

            if (_inventoryBar != null)
                _inventoryBar.Update(gameTime);
        }

        public override void Deregister()
        {
            InventorySystem.Deregister(this);
        }
    }
}

