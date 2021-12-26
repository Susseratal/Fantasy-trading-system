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
        static void Main(string[] args)
        {
            Item[] items = Item.makeItems(); // make the array of items
            var inventory = new Generic.List<Item>(); // initialise an empty list than can have things added to it

            while (true)
            {
                Console.WriteLine("Enter command");
                string command = Console.ReadLine();
                command = command.ToLower();
                switch(command)
                {
                    case "list items":
                    case "check items":
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
                        inventory.Add(selectedItem);
                        break;

                    case "inventory":
                        foreach (var i in inventory)
                        {
                            Console.WriteLine(i.name);
                        }
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
