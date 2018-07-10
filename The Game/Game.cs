using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Game
{
    class Game
    {
        public const string version = "1.0.0.0";
        public bool running = true;
        public Room currentRoom;
        public Player player;

        public Game()
        {
            // Add option to load from a file


            DisplayTitle();

            // Get the player's name
            Console.WriteLine("Welcome to The Game! Before we start, what's your name?");
            Console.Write(" > ");
            string playerName = Console.ReadLine();
            Console.WriteLine();
            Console.WriteLine("Okay, " + playerName + "! Let's get started.");
            Console.Clear();

            player = new Player(playerName);

            InitializeItems();
            InitializePeople();
            InitializeRooms();

            currentRoom = Room.Home;

            Console.Title = "The Game - " + currentRoom.name;
            Console.WriteLine(currentRoom.description);
            Console.WriteLine("(Welcome to " + currentRoom.name + ")");
            Console.WriteLine();
            Console.WriteLine("Type 'look' to look around.");
        }

        public static void DisplayTitle()
        {
            string[] title = {
                "_________          _______    _______  _______  _______  _______ ",
                "\\__   __/|\\     /|(  ____ \\  (  ____ \\(  ___  )(       )(  ____ \\",
                "   ) (   | )   ( || (    \\/  | (    \\/| (   ) || () () || (    \\/",
                "   | |   | (___) || (__      | |      | (___) || || || || (__    ",
                "   | |   |  ___  ||  __)     | | ____ |  ___  || |(_)| ||  __)   ",
                "   | |   | (   ) || (        | | \\_  )| (   ) || |   | || (      ",
                "   | |   | )   ( || (____/\\  | (___) || )   ( || )   ( || (____/\\",
                "   )_(   |/     \\|(_______/  (_______)|/     \\||/     \\|(_______/",
                "",
                "v" + version,
                "",
                "",
                "",
                "",
                "Press any key to play...",
                "",
                "",
                "",
                "",
                "Developed by:",
                "Andrew \"727021\" Schimelpfening",
                "(Copyright © 2018)"
            };

            TextHelper.BorderedText(title);
            Console.ReadKey();
        }

        public static bool debug = true;
        public static void Debug(string text)
        {
            string filename = DateTime.Today.ToShortDateString().Replace('/', '_') + ".txt";
            StreamWriter fdebug = File.AppendText(filename);
            fdebug.WriteLine("[" + DateTime.Now.ToLongTimeString() + "]: " + text);
            fdebug.Close();
            fdebug.Dispose();
        }

        #region = Initializers = 

        private void InitializeItems()
        {
            Debug("Initializing Items");
            // WELCOME LETTER
            Item.WelcomeLetter = new Item("WelcomeLetter");
            Item.WelcomeLetter.description = "A helpful letter. Type 'use WelcomeLetter' to read it.";
            Item.WelcomeLetter.interact = player.name + ",\r\n" +
                "Welcome to The Game! If you're new here, you might need some help.\r\n" +
                "If you want to see what's around you, type 'look' to look around a room.\r\n" +
                "Type 'go [ROOM]' to go to another room or type 'back' to go back where you came from.\r\n" +
                "Type 'check [ITEM]' to look at an item, 'use [ITEM]' to use it, or 'take [ITEM]' to put it in your inventory.\r\n" +
                "Speaking of your inventory, type 'inv' to view it, and 'drop [ITEM]' to drop and item out of it.\r\n" +
                "You can also give items to people by typing 'give [ITEM] [PERSON]'.\r\n" +
                "You can talk to people too! Just type 'talk [PERSON]' and see what they have to say.\r\n\r\n" +
                "To quit the game, just type 'quit'. If you ever get lost, type 'help' for some ideas!";
            Item.WelcomeLetter.canTake = false;

            Item.Egg = new Item("Egg");
            Item.Egg.description = "It's just a little brown egg. It smells funny...";
            Item.Egg.interact = "Smells rotten... I'd better not eat it.";
            Item.Egg.breakOnDrop = true;

            Item.Bread = new Item("Bread");
            Item.Bread.description = "Look! A loaf of bread! You know, I am kind of hungry...";
            Item.Bread.interact = "Mmmmm, that bread was delicious! I wish I had more...";
            Item.Bread.oneUse = true;
        }

        private void InitializeRooms()
        {
            Debug("Initializing Rooms");
            // HOME
            Room.Home = new Room("Home");
            Room.Home.description = player.name + "'s house";
            Room.Home.lookDescription = "Ahh, home sweet home. It's not much, but hey, it's yours.\r\n" +
                "It looks like someone left you a letter... Type 'check WelcomeLetter' to look at it!";
            Room.Home.items.Add(Item.WelcomeLetter);
            Room.Home.items.Add(Item.Egg);
            Room.Home.items.Add(Item.Bread);
            Room.Home.items.Add(Item.Bread);
            Room.Home.items.Add(Item.Bread);
            Room.Home.items.Add(Item.Bread);

            // TRAIL
            Room.Trail = new Room("Trail", Room.Home);
            Room.Trail.description = "What a dusty old road...";
            Room.Trail.lookDescription = "It's just a long, dusty trail. Some small shrubs line one side of the road.\r\n" +
                "What could be at the end?";
            Room.Trail.items.Add(Item.Egg);
            Room.Home.items.Add(Item.Bread);
            Room.Home.items.Add(Item.Bread);
            Room.Trail.people.Add(Person.Paul);

            Debug("Adding children to rooms");
            // Children
            Room.Home.children.Add(Room.Trail);
        }

        private void InitializePeople()
        {
            Person.Paul = new Person("Paul");
            Person.Paul.description = "Just a guy";
            Person.Paul.Talk = delegate (Game game)
            {
                Console.WriteLine("Hi, I'm paul.");
                Console.Write("What's your name? ");
                string name = Console.ReadLine();
                Console.WriteLine("Nice to meet you, " + name + "!");
            };
        }

        #endregion
    }
}
