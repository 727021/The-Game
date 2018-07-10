using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Game
{
    class Player
    {
        public string name { get; private set; }
        public Inventory items;
        public bool inventoryOpen = false;

        public Player(string name)
        {
            this.name = name;
            items = new Inventory(name + "'s Inventory", 8);
        }

        public int ExpandInventory()
        {
            if (items.size != Inventory.max)
                items.size *= 2;
            return items.size;
        }
    }
}
