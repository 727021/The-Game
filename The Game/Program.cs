using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Game
{
    class Program
    {
        static void Main(string[] args)
        {
            Game.Debug("PROGRAM START");
            Console.Title = "The Game";

            Game g = new Game(); // Game's initializer should check whether we load a game or
                                 // start a new one

            while (g.running)
            {
                // Command handling
                Console.WriteLine();
                Console.Write(" > ");
                string[] input = Console.ReadLine().Split(' ');
                Console.WriteLine();
                string command = input[0];
                string[] cargs = new string[8];
                if (input.Length > 1)
                    for (int i = 1; i < input.Length; i++)
                        cargs[i - 1] = input[i];
                switch (command.ToLower())
                {
                    case "look":
                        Command.Look(g);
                        break;
                    case "go":
                        if (!String.IsNullOrEmpty(cargs[0]))
                            Command.Go(g, cargs[0]);
                        else
                            Console.WriteLine("Where do you want to go?");
                        break;
                    case "back":
                        Command.Back(g);
                        break;
                    case "check":
                        if (!String.IsNullOrEmpty(cargs[0]))
                            Command.Check(g, cargs[0]);
                        else
                            Console.WriteLine("What do you want to check?");
                        break;
                    case "use":
                        if (!String.IsNullOrEmpty(cargs[0]))
                            Command.Use(g, cargs[0]);
                        else
                            Console.WriteLine("What do you want to use?");
                        break;
                    case "take":
                        if (!String.IsNullOrEmpty(cargs[0]))
                            Command.Take(g, cargs[0]);
                        else
                            Console.WriteLine("What do you want to take?");
                        break;
                    case "inv":
                        Command.Inv(g);
                        break;
                    case "drop":
                        if (!String.IsNullOrEmpty(cargs[0]))
                            Command.Drop(g, cargs[0]);
                        else
                            Console.WriteLine("What do you want to drop?");
                        break;
                    case "give":
                        break;
                    case "talk":
                        if (!String.IsNullOrEmpty(cargs[0]))
                            Command.Talk(g, cargs[0]);
                        else
                            Console.WriteLine("Who do you want to talk to?");
                        break;
                    case "quit":
                        Command.Quit(g);
                        break;
                    case "help":
                        if (!String.IsNullOrEmpty(cargs[0]))
                            Command.Help(cargs[0]);
                        else
                            Command.Help();
                            break;
                    case "clear":
                        Console.Clear();
                        break;
                    default:
                        break;
                }
            }
            Game.Debug("PROGRAM EXIT");
        }
    }

    class Command
    {
        public static void Look(Game game)
        {
            if (!game.player.inventoryOpen)
            {
                game.currentRoom.LookAround();
                return;
            }

            Console.WriteLine("You look in your inventory:");
            if (game.player.items.Count() == 0)
                Console.WriteLine("Hey, there's nothing in here!");
            int c = 0;
            foreach (Item i in game.player.items.All())
            {
                c++;
                Console.Write("\t" + i.name);
                if (c % 3 == 0)
                    Console.Write("\r\n");
            }
            Console.WriteLine();
        }

        public static void Go(Game game, string room)
        {
            if (game.currentRoom.name == room)
            {
                Console.WriteLine("You're already in " + room + "!");
                return;
            }
            if (game.currentRoom.parent != null && game.currentRoom.parent.name == room)
            {
                Back(game);
                return;
            }
            if (!game.currentRoom.children.Contains(room))
            {
                Console.WriteLine(room + "? Where is that?");
                return;
            }
            game.currentRoom = game.currentRoom.children.Find(room);
            Console.Clear();
            Console.Title = "The Game - " + game.currentRoom.name;
            Console.WriteLine(game.currentRoom.description);
            Console.WriteLine("(Welcome to " + game.currentRoom.name + ")");
            Console.WriteLine();
            Console.WriteLine("Type 'look' to look around.");
        }

        public static void Back(Game game)
        {
            if (game.currentRoom.parent == null)
            {
                Console.WriteLine("You're already in the first room!");
                return;
            }
            game.currentRoom = game.currentRoom.parent;
            Console.Clear();
            Console.Title = "The Game - " + game.currentRoom.name;
            Console.WriteLine(game.currentRoom.description);
            Console.WriteLine("(Welcome back to " + game.currentRoom.name + ")");
            Console.WriteLine();
            Console.WriteLine("Type 'look' to look around.");
        }

        public static void Check(Game game, string item)
        {
            Inventory inventory;
            if (game.player.inventoryOpen)
                inventory = game.player.items;
            else
                inventory = game.currentRoom.items;

            if (!inventory.Contains(item))
            {
                Console.WriteLine("What? There's no " + item + " in here...");
                return;
            }
            Console.WriteLine("You check the " + item + "...");
            Console.WriteLine();
            Console.WriteLine(inventory.Find(item).description);
        }

        public static void Use(Game game, string item)
        {
            Inventory inventory;
            if (game.player.inventoryOpen)
                inventory = game.player.items;
            else
                inventory = game.currentRoom.items;

            if (!inventory.Contains(item))
            {
                Console.WriteLine("What? There's no " + item + " in here...");
                return;
            }
            Console.WriteLine("You use the " + item + "...");
            Console.WriteLine();
            Console.WriteLine(inventory.Find(item).interact);
            if (inventory.Find(item).oneUse)
                inventory.Remove(item);
        }

        public static void Take(Game game, string item)
        {
            if (!game.currentRoom.items.Contains(item))
            {
                Console.WriteLine("What? There's no " + item + " in here...");
                return;
            }
            if (game.player.items.Full())
            {
                Console.WriteLine("Your inventory is full! Drop something to make some space.");
                return;
            }
            if (!game.currentRoom.items.Find(item).canTake)
            {
                Console.WriteLine("Sorry " + game.player.name + ", that " + item + " has to stay here.");
                return;
            }
            game.player.items.Add(game.currentRoom.items.Find(item));
            game.currentRoom.items.Remove(game.currentRoom.items.Find(item));
            Console.WriteLine("You picked up the " + item + ".");
        }

        public static void Inv(Game game)
        {
            if (game.player.inventoryOpen)
            {
                game.player.inventoryOpen = false;
                Console.WriteLine("You closed your inventory.");
                Console.WriteLine("You have " + game.player.items.Space() + " space left.");
                return;
            }
            game.player.inventoryOpen = true;
            Console.WriteLine("You open your inventory:");
            if (game.player.items.Count() == 0)
                Console.WriteLine("Hey, there's nothing in here!");
            int c = 0;
            foreach (Item i in game.player.items.All())
            {
                c++;
                Console.Write("\t" + i.name);
                if (c % 3 == 0)
                    Console.Write("\r\n");
            }
            Console.WriteLine();
        }

        public static void Drop(Game game, string item)
        {
            if (!game.player.inventoryOpen)
            {
                Console.WriteLine("Do you even have any " + item + "? Open your inventory.");
                return;
            }
            if (!game.player.items.Contains(item))
            {
                Console.WriteLine("You don't have any " + item + "...");
                return;
            }
            bool breakItem = game.player.items.Find(item).breakOnDrop;
            if (!breakItem)
                game.currentRoom.items.Add(game.player.items.Find(item));
            game.player.items.Remove(item);
            Console.WriteLine("Yeah, just drop that " + item + " anywhere.");
            if (breakItem)
                Console.WriteLine("Oh look, you broke it. Nice one, " + game.player.name + ".");
            else
                Console.WriteLine("Don't worry. That " + item + " will be waiting for you right where you left it.");

        }

        public static void Talk(Game game, string person)
        {
            if (!game.currentRoom.people.Contains(person))
            {
                Console.WriteLine("Nope. No " + person + " here.");
                return;
            }
            game.currentRoom.people.Find(person).Talk(game);
        }

        public static void Help(string command = null)
        {
            // List all commands
            if (command == null)
            {
                Console.WriteLine("This is a list of all the actions you can take.");
                Console.WriteLine("Type 'help [ACTION]' for help with something specific.");
                Console.WriteLine();
                Console.WriteLine("Navigation: look, go, back");
                Console.WriteLine("Items: check, use, take, inv, drop");
                Console.WriteLine("People: give, talk");
                Console.WriteLine("Game: quit, help, clear");
                return;
            }

            // Help for specific commands
            switch (command.ToLower())
            {
                case "look":
                    Console.WriteLine("Type 'look' to look around the room you're in.");
                    break;
                case "go":
                    Console.WriteLine("Type 'go [ROOM]' to go to another room.");
                    break;
                case "back":
                    Console.WriteLine("Type 'back' to go back to a previous room.");
                    break;
                case "check":
                    Console.WriteLine("Type 'check [ITEM]' to check on an item.");
                    break;
                case "use":
                    Console.WriteLine("Type 'use [ITEM]' to use an item.");
                    Console.WriteLine("Remember, some items can only be used once!");
                    break;
                case "take":
                    Console.WriteLine("Type 'take [ITEM]' to put an item in your inventory.");
                    Console.WriteLine("Remember, space is limited! Pack light!");
                    break;
                case "inv":
                    Console.WriteLine("Type 'inv' to open or close your inventory.");
                    Console.WriteLine("This also shows you how much space you have left.");
                    break;
                case "drop":
                    Console.WriteLine("Type 'drop [ITEM]' to remove an item from your inventory.");
                    Console.WriteLine("Remember, some items will disappear if you drop them!");
                    break;
                case "give":
                    Console.WriteLine("Type 'give [ITEM] [PERSON]' to give someone one of your items.");
                    Console.WriteLine("Once you give something away, you can't get it back. Be careful!");
                    break;
                case "talk":
                    Console.WriteLine("Type 'talk [PERSON]' to see what someone has to say.");
                    Console.WriteLine("Talk to everyone! They can help you figure out where to go.");
                    break;
                case "quit":
                    Console.WriteLine("Type 'quit' to quit the game.");
                    Console.WriteLine("You'll also be asked if you want to save the game.");
                    break;
                case "help":
                    Console.WriteLine("...Really?");
                    break;
                case "clear":
                    Console.WriteLine("Type 'clear' to clear the screen.");
                    break;
                default:
                    Console.WriteLine("Sorry, I don't know anything about " + command + ".");
                    break;
            }
        }

        public static void Quit(Game game)
        {
            Console.Write("Are you sure you want to stop playing? Type 'yes' to confirm: ");
            if (Console.ReadLine().ToLower() != "yes")
                return;

            // Add option to save to a file

            Console.WriteLine();
            Console.WriteLine("Thanks for playing!");
            Console.WriteLine("Press any key to exit...");
            game.running = false;
            Console.ReadKey();
        }
    }
}
