using System;
using System.Collections.Generic;
using System.IO;
using NUnit.Framework; //Don't forget this.
using SwinAdven;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TestProject
{
    [TestFixture]
    public class TestIdentifiableObj
    {
        private IdentifiableObj _obj;
        private IdentifiableObj _empty_obj;

        [SetUp]
        public void SetUp()
        {
            _obj = new IdentifiableObj(new string[] { "fred", "bob" });
            _empty_obj = new IdentifiableObj(new string[] { "" });
        }

        [Test]
        public void TestAreYou()
        {
            Assert.IsTrue(_obj.AreYou("fred"));
            Assert.IsTrue(_obj.AreYou("bob"));
        }

        [Test]
        public void TestNotAreYou()
        {
            Assert.IsFalse(_obj.AreYou("wilma"));
            Assert.IsFalse(_obj.AreYou("boby"));
        }

        [Test]
        public void TestCaseSensitive()
        {
            Assert.IsTrue(_obj.AreYou("FRED"));
            Assert.IsTrue(_obj.AreYou("bOB"));
        }

        [Test]
        public void TestFirstId()
        {
            Assert.AreEqual("fred", _obj.FirstID);
        }

        [Test]
        public void TestFirstIdEmpty()
        {
            Assert.AreEqual("", _empty_obj.FirstID);
        }

        [Test]
        public void TestAddId()
        {
            _obj.AddIdentifier("Wilma");
            Assert.IsTrue(_obj.AreYou("wilma"));
        }


    }

    [TestFixture]
    //Iteration2
    public class Iteration2Test
    {
        private Player player;
        private Item item;
        private Inventory inventory;
        private Item bow;
        private Item arrow;

        [SetUp]
        public void Setup()
        {
            player = new Player("Gran", "a brave Skyfarer");
            string[] ids = { "token", "currency" };
            item = new Item(ids, "Swin Gold", "Swinburne trading currency");

            inventory = new Inventory();
            string[] weapID = { "weapon", "rare" };
            bow = new Item(weapID, "Silver bow", "shiny weapon");
            arrow = new Item(weapID, "rusty arrow", "looks old but still usable");

            //Add item into inventory
            inventory.Put(item);
            inventory.Put(bow);
            inventory.Put(arrow);

        }
        //Item Unit Tests
        [Test]
        public void IdentifyItem()
        {
            Assert.True(item.AreYou("token"));
            Assert.True(item.AreYou("currency"));
            Assert.False(item.AreYou("weapon"));
        }
        [Test]
        public void ShortDescTest()
        {
            //return the Name and the First ID of game obj
            //id: token
            //name: Swin Gold
            Assert.AreEqual("Swin Gold - token", item.ShortDescription);
        }
        [Test]
        public void FullDescTest()
        {
            //long desc
            Assert.AreEqual("Swinburne trading currency", item.FullDescription);
        }

        //Inventory Unit Tests
        [Test]
        public void FindItem()
        {
            Assert.True(inventory.HasItem("weapon"));
            Assert.True(inventory.HasItem("rare"));
        }
        [Test]
        public void NoItemFound()
        {
            //return False if couldn't found the item
            Assert.False(inventory.HasItem("potion"));
        }
        [Test]
        public void FetchItem()
        {
            //test if Silver bow is a weapon by fetching its ID
            Assert.AreEqual(bow, inventory.Fetch("weapon"));
        }
        [Test]
        public void TakeItem()
        {
            //remove item with "token" ID from the inventory
            Item takenItem = inventory.Take("token");

            //it should return False (notice that in previous test, it returned true)
            Assert.False(inventory.HasItem("token"));
        }
        [Test]
        public void TestItemList()
        {
            string itemList = inventory.ItemList;

            Assert.AreEqual("    Swin Gold - token\n    Silver bow - weapon\n    rusty arrow - weapon\n", itemList);
        }

        //Player Unit Tests
        [Test]
        public void IndentifyPlayer()
        {
            Assert.True(player.AreYou("me"));
            Assert.True(player.AreYou("inventory"));
        }
        [Test]
        public void LocateItem()
        {
            //player add item into Inventory
            player.Inventory.Put(bow);
            //Locate item and define bow as weapon
            Assert.NotNull(player.Locate("weapon"));
            Assert.AreEqual(bow, player.Locate("weapon"));
            Assert.True(player.Inventory.HasItem("weapon"));
        }
        [Test]
        public void LocatePlayer()
        {
            //returns itself if asked to locate "me" or "inventory"
            Assert.AreEqual(player, player.Locate("me"));
            Assert.AreEqual(player, player.Locate("inventory"));
        }
        [Test]
        public void LocateNothing()
        {
            //player doesn't has potion
            Assert.Null(player.Locate("potion"));
        }
        [Test]
        public void PlayerFullDesc()
        {
            player.Inventory.Put(bow);
            player.Inventory.Put(arrow);
            Assert.That(player.FullDescription, Is.EqualTo("Gran, a brave Skyfarer is carrying:\n    Silver bow - weapon\n    rusty arrow - weapon\n"));
        }
    }


    [TestFixture]
    //Iteration3
    public class Iteration3Test
    {
        private Bag _bag;
        private Item _helmet;
        private Item _chestplate;

        [SetUp]
        public void SetUp()
        {   //                                    ids           / name          / description
            _bag = new Bag(new string[] { "bag", "leather bag" }, "Leather Bag", "A small leather bag");
            _helmet = new Item(new string[] { "head-equipment", "iron helmet" }, "Iron Helmet", "tough armor");
            _chestplate = new Item(new string[] { "body-equipment", "iron chestplate" }, "Iron Chestplate", "tough armor");

            _bag.Inventory.Put(_helmet);
            _bag.Inventory.Put(_chestplate);
        }
        [Test]
        public void BagLocateItem()
        {
            Assert.AreEqual(_chestplate, _bag.Locate("iron chestplate") as Item);
            Assert.True(_bag.Inventory.HasItem("head-equipment"));
        }
        [Test]
        public void BagLocateItSelf()
        {
            Assert.AreEqual(_bag, _bag.Locate("bag"));
        }
        [Test]
        public void BagLocatesNothing()
        {
            //bag has not token or potion inside -> return as false
            Assert.False(_bag.Inventory.HasItem("token"));
            Assert.Null(_bag.Locate("potion"));
        }
        [Test]
        public void BagFullDescription()
        {
            string expectedDesc = "Leather Bag consist of:\n    Iron Helmet - head-equipment\n    Iron Chestplate - body-equipment\n";
            Assert.AreEqual(expectedDesc, _bag.FullDescription);
        }
        [Test]
        public void BagInBag()
        {
            Bag b1 = new Bag(new string[] { "b1", "first bag" }, "First Bag", "The new bag");
            Bag b2 = new Bag(new string[] { "b2", "second bag" }, "Second Bag", "The old bag");
            Item potion = new Item(new string[] { "potion", "mystery potion" }, "Mystery Potion", "A weird-looking potion\n what could happen if i drink it?");
            Item ring = new Item(new string[] { "ring", "gold ring" }, "Gold Ring", "mystery drop from the goblin");
            //put b2 inside b1
            b1.Inventory.Put(b2);
            //put item inside b1
            b1.Inventory.Put(potion);
            //put item inside b2
            b2.Inventory.Put(ring);

            // b1 locate b2
            Assert.AreEqual(b2, b1.Locate("b2"));
            Assert.AreEqual(b2, b1.Locate("second bag"));

            // b1 locate other items in b1
            Assert.AreEqual(potion, b1.Locate("potion"));

            // b1 cannot locate items in b2
            Assert.Null(b1.Locate("gold ring"));
        }
    }
    
    [TestFixture]
    //Intergration 4
    public class Iteration4Test
    {
        private Player player;
        private Bag bag;
        private Item gem;
        [SetUp]
        public void SetUp()
        { // ids / name / description
            bag = new Bag(new string[] { "bag", "leather bag" }, "Leather Bag",
            "A small leather bag");
            gem = new Item(new string[] { "gem", "mystery gem" }, "mystery gem",
            "A shiny gem");
            player = new Player("Gran", "a brave Skyfarer");
        }
        [Test]
        public void TestLookAtMe()
        {
            LookCommand command = new LookCommand();
            string result = command.Execute(player, new string[] { "look", "at", "inventory" });
            Assert.AreEqual(player.FullDescription, result);
        }
        [Test]
        public void TestLookAtGem()
        {
            LookCommand command = new LookCommand();
            //put gem in player's inventory
            player.Inventory.Put(gem);
            player.Inventory.Put(bag);
            //bag.Inventory.Put(gem);
            string result = command.Execute(player, new string[] { "look", "at", "gem" });
            Assert.AreEqual(gem.FullDescription, result);
        }
        [Test]
        public void TestLookAtUnk()
        {
            LookCommand command = new LookCommand();
            player.Inventory.Put(bag);
            bag.Inventory.Put(gem);
            string result = command.Execute(player, new string[] { "look", "at", "gem" });
            Assert.AreEqual("I can't find the gem", result);
        }
        [Test]
        public void TestLookAtGemInMe()
        {
            LookCommand command = new LookCommand();
            player.Inventory.Put(bag);
            //put gem in player's inventory
            player.Inventory.Put(gem);
            //bag.Inventory.Put(gem);
            string result = command.Execute(player, new string[] { "look", "at", "gem", "in", "inventory" });
            Assert.AreEqual("A shiny gem", result);
        }
        [Test]
        public void TestLookAtGemInBag()
        {
            LookCommand command = new LookCommand();
            player.Inventory.Put(bag);
            //put gem in player's bag
            bag.Inventory.Put(gem);
            string result = command.Execute(player, new string[] { "look", "at", "gem", "in", "bag" });
            Assert.AreEqual("A shiny gem", result);
        }
        [Test]
        public void TestLookAtGemInNoBag()
        {
            LookCommand command = new LookCommand();
            //doesn't put bag in player's inventory
            //player.Inventory.Put(bag);
            bag.Inventory.Put(gem);
            string result = command.Execute(player, new string[] { "look", "at", "gem", "in", "bag" });
            Assert.AreEqual("I can't find the bag", result);
        }
        [Test]
        public void TestLookAtNoGemInBag()
        {
            LookCommand command = new LookCommand();
            player.Inventory.Put(bag);
            //doesn't put gem in bag
            //bag.Inventory.Put(gem);
            string result = command.Execute(player, new string[] { "look", "at", "gem", "in", "bag" });
            Assert.AreEqual("I can't find the gem in the Leather Bag", result);
        }
        [Test]
        public void TestInvalidLook()
        {
            LookCommand command = new LookCommand();
            player.Inventory.Put(bag);
            //doesn't put gem in bag
            //bag.Inventory.Put(gem);
            string result1 = command.Execute(player, new string[] { "hi", "at", "gem" });
            string result2 = command.Execute(player, new string[] { "look", "around" });
            string result3 = command.Execute(player, new string[] { "look", "at", "a", "at", "b" });
            string result4 = command.Execute(player, new string[] { "look", "at", "a", "in", "b", "c" });
            string result5 = command.Execute(player, new string[] { "look", "around", "gem" });
            //1st word must be "look"
            Assert.AreEqual("Error in look input", result1);
            //2nd word must be "at"
            Assert.AreEqual("I don't know how to look like that", result2);
            //4th word must be "in"
            Assert.AreEqual("What do you want to look in?", result3);
            //2nd word is "at" and item must be exist
            Assert.AreEqual("What do you want to look at?", result5);
        }
    }
    [TestFixture]
    //Interation 6 
    public class Iteration6Test
    {
        private Player player;
        private LookCommand lookCommand;
        private Location location;
        private Inventory inventory;

        [SetUp]
        public void SetUp()
        {
            player = new Player("Gran", "The brave skyfarer");
            Inventory inventory = new Inventory();
        }

        [Test]
        public void IdentifiableLocation()
        {
            Location location1 = new Location(new string[] { "location", "room" }, "Room", "A room for testing");
            Assert.IsTrue(location1.AreYou("room"));
            Assert.AreEqual("Room", location1.Name);
            Assert.AreEqual("A room for testing", location1.FullDescription);
        }

        [Test]
        public void LocateItems()
        {
            Location location1 = new Location(new string[] { "location", "dungeon" }, "Small dungeon", "A small dungeon for testing");

            Item lootChest = new Item(new string[] { "lootChest", "iron chest" }, "iron chest", "A shiny chest found in dungeon");
            location1.Inventory.Put(lootChest);

            Assert.AreEqual(lootChest, location1.Locate("lootChest"));
        }

        [Test]
        public void PlayerLocateItem()
        {
            Location location1 = new Location(new string[] { "location", "dungeon" }, "Small dungeon", "A small dungeon for testing");
            //put player in location
            player.Location = location1;

            Item gem = new Item(new string[] { "gem", "ruby" }, "gem", "a shiny gem found in dungeon chest");
            location1.Inventory.Put(gem);

            Assert.AreEqual(gem, player.Locate("ruby"));
        }
    }
    [TestFixture]
    //Interation 7
    public class Iteration7Test
    {
        private Player player;
        private Location location0;
        private Location location1;
        private Location location2;
        private SwinAdven.Path path1;
        private SwinAdven.Path path2;

        [SetUp]
        public void SetUp()
        {
            player = new Player("Gran", "The brave skyfarer");
            location0 = new Location(new string[] { "location", "spawnpoint" }, "Starting point", "Your journey begin here");
            location1 = new Location(new string[] { "location", "dungeon" }, "Small dungeon", "A deep dark fantasy dungeon");
            location2 = new Location(new string[] { "location", "forest" }, "Dark Forest", "A deep dark forest");
            path1 = new SwinAdven.Path(new string[] { "north" }, location1);
            path2 = new SwinAdven.Path(new string[] { "south" }, location2);
            player.Location = location0;
            location0.AddPath(Direction.North, path1);
            location0.AddPath(Direction.South, path2);
            MoveCommand moveCommand = new MoveCommand();
        }
        [Test]
        public void GetPathFromLocation()
        {
            SwinAdven.Path retrievedPath1 = location0.GetPath(Direction.North);
            SwinAdven.Path retrievedPath2 = location0.GetPath(Direction.South);

            Assert.AreEqual(path1, retrievedPath1);
            Assert.AreEqual(path2, retrievedPath2);
        }
        [Test]
        public void MoveToLocation()
        {
            MoveCommand moveCommand = new MoveCommand();
            string result = moveCommand.Execute(player, new string[] { "move", "north" });
            Assert.AreEqual("You head north.\nYou have arrived in a Small dungeon.", result);
            Assert.AreEqual(location1, player.Location);
        }
        [Test]
        public void TestLeaveToPreviousLocation()
        {
            MoveCommand moveCommand = new MoveCommand();
            moveCommand.Execute(player, new string[] { "move", "north" });
            string result = moveCommand.Execute(player, new string[] { "leave" });
            Assert.AreEqual("You have returned to Starting point.", result);
            Assert.AreEqual(location0, player.Location);
        }
        [Test]
        public void MoveInvalidDirection()
        {
            MoveCommand moveCommand = new MoveCommand();
            string result = moveCommand.Execute(player, new string[] { "move", "t" });
            Assert.AreEqual("Invalid path identifier", result);
            Assert.AreEqual(location0, player.Location);
        }
    }
    [TestFixture]
    //Interation 8
    public class Iteration8Test
    {
        private Player player;
        private Location location0;
        private Location location1;
        private SwinAdven.Path path1;
        private CommandProcessor commandProcessor;

        [SetUp]
        public void SetUp()
        {
            player = new Player("Gran", "The brave skyfarer");
            Item potion = new Item(new string[] { "potion", "magic potion" }, "health potion", "recover player's health by 15%");
            player.Inventory.Put(potion);
            //
            location0 = new Location(new string[] { "location", "spawnpoint" }, "Starting point", "Your journey begins here");
            location1 = new Location(new string[] { "location", "dungeon" }, "Small dungeon", "A deep dark dungeon");
            path1 = new SwinAdven.Path(new string[] { "north" }, location1);
            player.Location = location0;
            location0.AddPath(Direction.North, path1);
            //
            commandProcessor = new CommandProcessor();
            commandProcessor.AddCommand(new MoveCommand());
            commandProcessor.AddCommand(new LookCommand());
            commandProcessor.AddCommand(new QuitCommand());
        }
        [Test]
        public void TestQuitCommand()
        {
            var text = new string[] { "quit" };
            var result = commandProcessor.Process(player, text);
            Assert.AreEqual("Bye.", result);
        }
        [Test]
        public void TestMoveCommand()
        {
            var text1 = new string[] { "move", "north" };
            var text2 = new string[] { "leave" };
            string result1 = commandProcessor.Process(player, text1);
            string result2 = commandProcessor.Process(player, text2);

            Assert.AreEqual("You head north.\nYou have arrived in a Small dungeon.", result1);
            Assert.AreEqual("You have returned to Starting point.", result2);
            //player must return to the starting point
            Assert.AreEqual(location0, player.Location);
        }
        [Test]
        public void TestLookCommand()
        {
            var text1 = new string[] { "look" };
            var text2 = new string[] { "look", "at", "inventory" };
            string result1 = commandProcessor.Process(player, text1);
            string result2 = commandProcessor.Process(player, text2);
            Assert.AreEqual("Your journey begins here", result1);
            Assert.AreEqual("Gran, The brave skyfarer is carrying:\n    health potion - potion\n", result2);
        }
    }
}