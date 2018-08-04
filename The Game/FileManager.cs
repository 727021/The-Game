using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using IWshRuntimeLibrary;

namespace The_Game
{
    class FileManager
    {
        private const int fileFormat = 1;

        public static void SaveGame(Game game)
        {
            string filename = game.player.name + ".tgs";
            if (!Directory.Exists("saves"))
                Directory.CreateDirectory("saves");
            if (!Directory.Exists("saves\\data"))
                Directory.CreateDirectory("saves\\data");
            System.IO.File.Create("saves/data/" + filename).Close();
            StreamWriter sw = new StreamWriter("saves/data/" + filename);

            sw.WriteLine(fileFormat);
            sw.WriteLine(game.player.name);
            sw.WriteLine(game.player.inventorySize);
            sw.WriteLine(game.currentRoom.name);
            sw.WriteLine(game.player.items.Count());
            foreach (Item i in game.player.items.All())
                sw.WriteLine(i.name + ":" + i.uses);
            sw.WriteLine(Room.All.Count());
            foreach (Room r in Room.All.All())
            {
                sw.WriteLine(r.name);
                sw.WriteLine(r.items.Count());
                foreach (Item i in r.items.All())
                    sw.WriteLine(i.name + ":" + i.uses);
                sw.WriteLine(r.people.Count());
                foreach (Person p in r.people.All())
                {
                    sw.WriteLine(p.name);
                    sw.WriteLine(p.items.Count());
                    foreach (Item i in p.items.All())
                        sw.WriteLine(i.name + ":" + i.uses);
                }
            }

            sw.Close();
            sw.Dispose();
            CreateShortcut(filename, "saves", filename);
        }

        public static int LoadGame(Game game, string player)
        {
            if (!System.IO.File.Exists("saves/data/" + player + ".tgs"))
                return 0;
            StreamReader sr = new StreamReader("saves/data/" + player + ".tgs");
            if (sr.ReadLine() != fileFormat.ToString())
            {
                sr.Close();
                sr.Dispose();
                return -1;
            }
            game.player = new Player(sr.ReadLine());                    // Player name
            game.player.inventorySize = Convert.ToInt32(sr.ReadLine()); // Player inventory size
            game.currentRoom = Room.All.Find(sr.ReadLine());            // Current Room
            int playerItems = Convert.ToInt32(sr.ReadLine());           // Player item count
            for (; playerItems > 0; playerItems--)
            {
                string[] line = sr.ReadLine().Split(':');
                Item item = Item.All.Find(line[0]);                     // Player item name
                item.uses = Convert.ToInt32(line[1]);                   // Player item uses
                game.player.items.Add(item);
            }

            int rooms = Convert.ToInt32(sr.ReadLine());                 // Room count
            for (; rooms > 0; rooms--)
            {
                Room room = Room.All.Find(sr.ReadLine());               // Room name
                int roomItems = Convert.ToInt32(sr.ReadLine());         // Room item count
                room.items.Clear();
                for (; roomItems > 0; roomItems--)
                {
                    string[] line = sr.ReadLine().Split(':');
                    Item item = Item.All.Find(line[0]);                 // Room item name
                    item.uses = Convert.ToInt32(line[1]);               // Room item uses
                    room.items.Add(item);
                }

                int roomPeople = Convert.ToInt32(sr.ReadLine());        // Room person count
                for (; roomPeople > 0; roomPeople--)
                {
                    Person person = Person.All.Find(sr.ReadLine());     // Person name
                    int personItems = Convert.ToInt32(sr.ReadLine());   // Person item count
                    for (; personItems > 0; personItems--)
                    {
                        string[] line = sr.ReadLine().Split(':');
                        Item item = Item.All.Find(line[0]);             // Person item name
                        item.uses = Convert.ToInt32(line[1]);           // Person item uses
                        person.items.Add(item);
                    }
                }
            }

            sr.Close();
            sr.Dispose();
            return 1;
        }

        public static void CreateShortcut(string shortcutName, string shortcutPath, string saveFile)
        {
            string shortcutLocation = Path.Combine(shortcutPath, shortcutName + ".lnk");
            if (System.IO.File.Exists(shortcutLocation))
                System.IO.File.Delete(shortcutLocation);

            WshShell shell = new WshShell();
            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutLocation);

            shortcut.Description = "Quickstart shortcut for a \"The Game\" save file";
            //shortcut.IconLocation = "";
            shortcut.TargetPath = Directory.GetCurrentDirectory() + "\\The Game.exe";
            shortcut.Arguments = saveFile;
            shortcut.WorkingDirectory = Directory.GetCurrentDirectory();
            shortcut.Save();
        }
    }
}
/*
    FILE FORMAT

    FileFormat
    PlayerName
    InventorySize
    CurrentRoom
    ItemCount
    Item:Uses
    Item:Uses
    Item:Uses
    RoomCount
    Room
    ItemCount
    Item:Uses
    Item:Uses
    Item:Uses
    PersonCount
    Person
    ItemCount
    Item:Uses
    Item:Uses
    Item:Uses
    ...

 */
