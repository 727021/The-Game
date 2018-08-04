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
        public int inventorySize = 8;

        public Player(string name)
        {
            this.name = name;
            items = new Inventory(name + "'s Inventory", inventorySize);
        }

        public int ExpandInventory()
        {
            if (inventorySize != Inventory.MAX)
                inventorySize *= 2;
            items.size = inventorySize;
            return inventorySize;
        }
    }
}
