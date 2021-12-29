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

        public Item(string nameInit, int weightInit, int valInit) // This is a constructor
        {
            name = nameInit;
            weight = weightInit;
            val = valInit;
        }

        public static Item[] makeItems()
        {
            var grain = new Item("Grain", 10, 20); 
            var fish = new Item("Fish", 1, 2);
            Item[] items = new Item[2] {grain, fish};
            return items;
        }

        public static void listItems(Generic.IEnumerable<Item> list) // this could be better, but it works fine for now
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
            Item selectedItem = list[item]; // some type issue here
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

        static void Main(string[] args)
        {
            // This is all very messy 
            TextInfo tI = new Globalization.CultureInfo("en-gb",false).TextInfo;

            // construct some basic stuff
            Item[] items = Item.makeItems(); 
            var inventory = new Generic.Dictionary<Item, int>(); 

            // add all of the items to the players inventory with a quantity of 0
            foreach (var i in items)
            {
                inventory.Add(i, 0);
            }

            // Create a default username so if for some reason the opening doesn't run, there's still a username
            string name = "username";
            int gold = 100;

            // exposit some stuff
            delayPrint("It's a tough world out there, and we're all just trying to make a living.\nFollowing his passing, you've inherited your father's general goods shop.\nAccordingly, you repaint the sign with your name.\n");

            // get the player's name
            delayPrint("What is your name? "); 
            name = Console.ReadLine(); 
            name = tI.ToTitleCase(name); 
            delayPrint("Excellent, and what did you call the shop? (This defaults to 'username's general goods' if left blank)");
            string shop = Console.ReadLine();
            if (shop == ""){shop = (name + "'s General Goods");}
            else {shop = tI.ToTitleCase(shop);}

            delayPrint("Welcome to... ");

            // Figlet some stuff
            var figletText = new Figlet.AsciiArt(shop); 
            Console.WriteLine(figletText.ToString() + "\n"); 

            // Time for some more exposition
            delayPrint("Your father's words ring in your ears.\n'It's an important business you know, lots of wandering adventurer types come through here.'");
            showHelp();

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
                        Console.WriteLine("Weight: " + selectedCheck.weight + " gram(s)");
                        Console.WriteLine("Value: " + selectedCheck.val + " gold \n");
                        break;

                    case "buy item":
                        Item.listItems(items);
                        Item selectedItem = Item.getItem(items);
                        gold = gold - selectedItem.val;
                        Console.WriteLine("Gold: " + gold); 
                        inventory[selectedItem]++;
                        break;

                    case "inventory":
                        Console.WriteLine("Item Name | Value | Weight | Amount held");
                        Console.WriteLine("-----------------------------------------");
                        foreach (var i in inventory)
                        {
                            Console.WriteLine(i.Key.name + "   |   " + i.Key.val + "   |   " + i.Key.weight + "   |   " + i.Value.ToString()); 
                        }
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
                        Console.WriteLine("Your command was: " + "\n" + command + "\n");
                        break;
                }
            }
        }
    }
}
