namespace SwinAdven
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter you name: ");
            string playerName = Console.ReadLine();
            Console.WriteLine("Enter your description: ");
            string playerDesc = Console.ReadLine();

            Player player = new Player(playerName, playerDesc);

            Item dagger = new Item(new string[] { "weapon", "dagger" }, "stone dagger", "a sharp looking dagger");
            Item food = new Item(new string[] { "food", "meat" }, "raw meat", "flesh meat obtain from wild animal");
            //put 2 items inside inventory
            player.Inventory.Put(dagger);
            player.Inventory.Put(food);
            
            Bag bag = new Bag(new string[] { "bag" }, "leather bag", "A small leather bag for beginner");
            //put bag in player's inventory
            player.Inventory.Put(bag);

            Item potion = new Item(new string[] { "potion" , "magic potion"}, "health potion", "recover player's health by 15%");
            //Player location
            Location spawnpoint = new Location(new string[] { "start" }, "Starting Point", "Welcome to the Sky Realmm, this is your starting point");
            Item tree = new Item(new string[] { "tree", "oak tree" }, "oak tree", "just a tree");
            spawnpoint.Inventory.Put(tree);

            player.Location = spawnpoint;
            LookCommand lookCommand = new LookCommand();
            MoveCommand moveCommand = new MoveCommand();
            //add another item in player's bag
            bag.Inventory.Put(potion);
           
            //add paths and new location
            Location forest = new Location(new string[] { "forest" }, "Forest", "A dark forest");
            Path pathToForest = new Path(new string[] { "north" }, forest);
            spawnpoint.AddPath(Direction.North, pathToForest);

            Location village = new Location(new string[] { "structure" }, "Small Village", "A small village.");
            Path pathToVillage = new Path(new string[] { "south" }, village);
            spawnpoint.AddPath(Direction.South, pathToVillage);
            //Loop
            while (true)
            {
                Console.WriteLine("Enter your command:");
                string command = Console.ReadLine();
                //exit game condition
                if (command.ToLower() == "quit")
                    break;
                string[] cmdWords = command.Split(' ');
                //execute command
                string output;
                if (cmdWords[0].ToLower() == "look")
                {
                    output = lookCommand.Execute(player, cmdWords);
                }
                else
                {
                    output = moveCommand.Execute(player, cmdWords);
                }
                Console.WriteLine(output);
            }
        }
    }
}