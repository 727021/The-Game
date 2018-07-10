using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Game
{
    class Room
    {
        public static Room Home;
        public static Room Trail;

        public string name { get; private set; }
        public Room parent { get; private set; }
        public RoomList children = new RoomList();
        public Inventory items;
        public PersonList people = new PersonList();
        public string description = "";
        public string lookDescription = "";
        public bool canEnter { get; private set; } // Maybe add a command to PUT items in a room without entering it?
                                                   // There could be some kind of small storage attached to Home

        public Room(string name, Room parent = null, bool canEnter = true)
        {
            this.name = name;
            this.parent = parent;
            this.items = new Inventory(name + " Items", Inventory.max);
            this.canEnter = canEnter;
        }

        public void LookAround()
        {
            Console.WriteLine(lookDescription);
            if (children.Count() == 0 && items.Count() == 0 && people.Count() == 0)
                Console.WriteLine("There isn't much to see here...");
            else
            {
                if (children.Count() > 0)
                {
                    Console.WriteLine("Rooms:");
                    foreach (Room r in children.All())
                        Console.WriteLine("\t" + r.name);
                }
                if (people.Count() > 0)
                {
                    Console.WriteLine("People:");
                    foreach (Person p in people.All())
                        Console.WriteLine("\t" + p.name + " (" + p.description + ")");
                }
                if (items.Count() > 0)
                {
                    Console.WriteLine("Items:");
                    foreach (Item i in items.All())
                        Console.WriteLine("\t" + i.name);
                }
            }
            if (parent != null)
                Console.WriteLine("\r\nType 'back' to go back to " + parent.name);
        }
    }

    class RoomList
    {
        private List<Room> rooms;

        public RoomList() => rooms = new List<Room>();

        public void Add(Room room) => rooms.Add(room);

        public bool Remove(Room room) => rooms.Remove(room);

        public bool Remove(string room)
        {
            foreach (Room r in rooms)
                if (r.name == room)
                    return rooms.Remove(r);
            return false;
        }

        public Room Find(Room room)
        {
            foreach (Room r in rooms)
                if (r == room)
                    return r;
            return null;
        }

        public Room Find(string room)
        {
            foreach (Room r in rooms)
                if (r.name == room)
                    return r;
            return null;
        }

        public bool Contains(Room room) => rooms.Contains(room);

        public bool Contains(string room)
        {
            foreach (Room r in rooms)
                if (r.name == room)
                    return true;
            return false;
        }

        public List<Room> All() => new List<Room>(rooms);

        public int Count() => rooms.Count;
    }
}
