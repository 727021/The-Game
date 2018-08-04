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

        public static Inventory All = new Inventory("All Items", Inventory.MAX);

        public string name { get; private set; }
        public string description;
        public string interact;

        public bool canTake = true;
        public bool canGive = true;
        public bool breakOnDrop = false;
        public bool oneUse = false;
        public int uses = -1; // -1 means unlimited uses

        public Item(string name)
        {
            this.name = name;
            if (!All.Contains(this))
                All.Add(this);
        }

        public override string ToString() => name;
    }

    class Inventory
    {
        public const int MAX = 256;

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

        public void Add(Item item, int count)
        {
            for (int i = 0; i < count; i++)
                items.Add(item);
        }

        public void AddRange(Item[] iitems) => items.AddRange(items);

        public bool Remove(Item item) => items.Remove(item);

        public bool Remove(string item)
        {
            item = item.Trim().ToLower();
            foreach (Item i in items)
                if (i.name.ToLower() == item)
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
            item = item.Trim().ToLower();
            foreach (Item i in items)
                if (i.name.ToLower() == item)
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
            item = item.Trim().ToLower();
            foreach (Item i in items)
                if (i.name.ToLower() == item)
                    return true;
            return false;
        }

        public void Clear() => items.Clear();

        public List<Item> All()
        {
            return new List<Item>(items);
        }

        public int Count() => items.Count;

        public int Count(Item item)
        {
            int count = 0;
            foreach (Item i in items)
                if (i == item)
                    count++;
            return count;
        }

        public int Count(string item)
        {
            item = item.Trim().ToLower();
            int count = 0;
            foreach (Item i in items)
                if (i.name.ToLower() == item)
                    count++;
            return count;
        }

    }
}
