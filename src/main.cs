//////////////////////////////////////////////////////////////////////////////
//  _____              _ _               ____            _                  //
// |_   _| __ __ _  __| (_)_ __   __ _  / ___| _   _ ___| |_ ___ _ __ ___   //
//   | || '__/ _` |/ _` | | '_ \ / _` | \___ \| | | / __| __/ _ \ '_ ` _ \  //
//   | || | | (_| | (_| | | | | | (_| |  ___) | |_| \__ \ ||  __/ | | | | | //
//   |_||_|  \__,_|\__,_|_|_| |_|\__, | |____/ \__, |___/\__\___|_| |_| |_| //
//                               |___/         |___/                        //
//                                                                          //
//////////////////////////////////////////////////////////////////////////////

using System;
using Threading = System.Threading;
using Convert = System.Convert;
using Collections = System.Collections;
using Generic = System.Collections.Generic;
using Diagnostics = System.Diagnostics;
using Process = System.Diagnostics.Process;
using Globalization = System.Globalization;
using TextInfo = System.Globalization.TextInfo;
using Figlet = WenceyWang.FIGlet;

namespace Game
{
    public class Item
    {
        public string name;
        public int weight;
        public int val;

        public Item(string nameInit, int valInit) 
        {
            name = nameInit;
            val = valInit;
        }

        public static Item[] makeItems()
        {
            var grain = new Item("Grain", 20); 
            var fish = new Item("Fish", 2);
            var meat = new Item("Meat", 5);
            var timbers = new Item("Timbers", 8);
            Item[] items = new Item[4] {grain, fish, meat, timbers};
            return items;
        }

        public static void listItems(Generic.IEnumerable<Item> list) 
        {
            int itemVar = 1;
            foreach (var i in list)
            {
                Console.WriteLine(itemVar + ": " + i.name);
                itemVar++;
            }
        }

        public static Item getItem(Item[] list)
        {
            Console.WriteLine("Select item number: ");
            string itemStr = Console.ReadLine();
            int item = Convert.ToInt32(itemStr);
            item = (item - 1);
            Item selectedItem = list[item]; 
            return selectedItem;
        }
    }

    class Program
    {
        public static void delayPrint(string text)
        {
            foreach (var c in text)
            {
                Console.Write(c);
                Threading.Thread.Sleep(30);
            }
            Console.Write("\n");
        }

        public static void showHelp()
        {
            delayPrint("List of Commands:\nInventory - see how much of every item you have\nbuy item - purchase an item\ncheck item - check the info of an item\nquit - close the game\nPress the return key to wait without doing anything");
            delayPrint("To show this message again, type 'help'");
        }

        public static void showInventory(Generic.Dictionary<Item, int> inv, int gold)
        {
            Console.WriteLine("\nGold: " + gold);
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine("|     Item Name     |      Value     |     Amount held     |");
            Console.WriteLine("------------------------------------------------------------");
            foreach (var i in inv)
            {
                Console.WriteLine("{0,0} {1,11} {0,7} {2,8} {0,7} {3,10} {0,10}",
                        "|",
                        i.Key.name,
                        i.Key.val,
                        i.Value.ToString());
            }
            Console.WriteLine("------------------------------------------------------------\n");
        }

        static void Main(string[] args)
        {
            TextInfo tI = new Globalization.CultureInfo("en-gb",false).TextInfo;
            Item[] items = Item.makeItems(); 
            var inventory = new Generic.Dictionary<Item, int>(); 
            foreach (var i in items)
            {
                inventory.Add(i, 0);
            }
            int gold = 100;
            // delayPrint("It's a tough world out there, and we're all just trying to make a living.\nFollowing his passing, you've inherited your father's general goods shop.\nAccordingly, you repaint the sign with your name.\n");

            delayPrint("What is your name? "); 
            string name = Console.ReadLine(); 
            if (name == "") {name = "Username";}
            else {name = tI.ToTitleCase(name);}

            delayPrint("Excellent, and what did you call the shop?");
            string shop = Console.ReadLine();
            if (shop == ""){shop = (name + "'s General Goods");}
            else {shop = tI.ToTitleCase(shop);}
            delayPrint("Welcome to... ");
            var figletText = new Figlet.AsciiArt(shop); 
            Console.WriteLine(figletText.ToString() + "\n"); 

            // delayPrint("Your father's words ring in your ears.\n'It's an important business you know, lots of wandering adventurer types come through here.'");
            // showHelp();

            while (true)
            {
                Console.WriteLine("What would you like to do? ");
                string command = Console.ReadLine();
                command = command.ToLower();
                switch(command)
                {
                    case "list items":
                    case "items":
                        Item.listItems(items);
                        break;

                    case "check item":
                        Item selectedCheck = Item.getItem(items); 
                            Console.WriteLine("Name: " + selectedCheck.name);
                        Console.WriteLine("Value: " + selectedCheck.val + " gold \n");
                        break;

                    case "buy item":
                    case "buy":
                        Item.listItems(items);
                        Item selectedItem = Item.getItem(items);
                        delayPrint("How many would you like to buy: ");
                        string amountStr = Console.ReadLine();
                        int amount = Convert.ToInt32(amountStr);
                        gold = (gold - (selectedItem.val*amount));
                        Console.WriteLine("Gold: " + gold); 
                        inventory[selectedItem] = (inventory[selectedItem]+amount);
                        break;

                    case "sell item":
                    case "sell":
                        showInventory(inventory, gold);
                        Item.listItems(items);
                        // something about an if (soldItem.Value - amount >= 0 {fucking don't lol})
                        break;

                    case "inventory":
                    case "inv":
                        showInventory(inventory, gold);
                        break;

                    case "help":
                        showHelp();
                        break;

                    case "":
                        delayPrint("You wait for 5 minutes");
                        break;

                    case "quit":
                        Threading.Thread.Sleep(10); 
                        Environment.Exit(0);
                        break;
                            
                    default:
                        Console.WriteLine("Unrecognised option");
                        break;
                }
            }
        }
    }
}
