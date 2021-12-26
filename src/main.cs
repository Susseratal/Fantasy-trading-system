using System;
using Threading = System.Threading;
using Convert = System.Convert;
using Collections = System.Collections;
using Generic = System.Collections.Generic;

// zf and zo to fold and open chunks of code respectively

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
    }

    class Program
    {
        public static void listItems(Generic.IEnumerable<Item> list)
        {
            int itemVar = 1;
            foreach (var i in list)
            {
                Console.WriteLine(itemVar + ": " + i.name);
                itemVar++;
            }
        }

        public static Item getItem(Generic.IEnumerable<Item> list)
        {
            Console.WriteLine("Select item number: ");
            string itemStr = Console.ReadLine();
            int item = Convert.ToInt32(itemStr);
            item = (item - 1);
            Item selectedItem = list[item];
            return selectedItem;
        }

        static void Main(string[] args)
        {
            var grain = new Item("Grain", 10, 20); 
            var fish = new Item("Fish", 1, 2);
            Item[] items = new Item[2] {grain, fish};

            var inventory = new Generic.List<Item>(); // initialise an empty list than can have things added to it

            // Initialise some items and add them to an array
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
                        listItems(items);
                        break;

                    case "check item":
                        Console.WriteLine("Select item number: ");
                        Item selectedItem = new getItem(items); // Figure out this function call come morning
                        Console.WriteLine("Name: " + selectedItem.name);
                        Console.WriteLine(selectedItem.weight + " grams");
                        Console.WriteLine(selectedItem.val + " gold \n");
                        break;

                    case "add to inventory":
                        listItems(items);
                        Item selectedItem = new getItem(items);
                        inventory.Add(selectedItem);
                        break;

                    case "check inventory":
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
