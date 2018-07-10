using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Game
{
    class Item
    {
        public static Item WelcomeLetter;
        public static Item Egg;
        public static Item Bread;

        public string name { get; private set; }
        public string description;
        public string interact;

        public bool canTake = true;
        public bool canGive = true;
        public bool breakOnDrop = false;
        public bool oneUse = false;

        public Item(string name) => this.name = name;
    }

    class Inventory
    {
        public const int max = 256;

        private List<Item> items = new List<Item>();
        public string name { get; private set; }
        public int size = 1;

        public Inventory(string name, int size)
        {
            this.name = name;
            this.size = size;
        }

        public bool Full() => items.Count >= size;

        public int Space() => size - items.Count;

        public void Add(Item item) => items.Add(item);

        public bool Remove(Item item) => items.Remove(item);

        public bool Remove(string item)
        {
            foreach (Item i in items)
                if (i.name == item)
                    return items.Remove(i);
            return false;
        }

        public Item Find(Item item)
        {
            foreach (Item i in items)
                if (i == item)
                    return i;
            return null;
        }

        public Item Find(string item)
        {
            foreach (Item i in items)
                if (i.name == item)
                    return i;
            return null;
        }

        public bool Contains(Item item)
        {
            foreach (Item i in items)
                if (i == item)
                    return true;
            return false;
        }

        public bool Contains(string item)
        {
            foreach (Item i in items)
                if (i.name == item)
                    return true;
            return false;
        }

        public void Clear() => items.Clear();

        public List<Item> All()
        {
            return new List<Item>(items);
        }

        public int Count() => items.Count;
    }
}
