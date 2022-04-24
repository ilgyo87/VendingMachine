using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone
{
    public static class StringMenu
    {
        public static void FirstMenu()
        {
            Console.WriteLine("");
            Console.WriteLine("(1) Display Vending Machine Items");
            Console.WriteLine("(2) Purchase");
            Console.WriteLine("(3) Exit");
            Console.WriteLine("");
            Console.Write("Please select an option (1, 2, or 3): ");
        }

        public static void SecondMenu(decimal money)
        {
            Console.WriteLine("");
            Console.WriteLine("(1) Feed Money");
            Console.WriteLine("(2) Select Product");
            Console.WriteLine("(3) Finish Transaction");
            Console.WriteLine("");
            Console.WriteLine($"Current Money Provided: ${money}");
            Console.WriteLine("");
            Console.Write("Please select an option (1, 2, or 3): ");
        }

        public static void FeedString()
        {
            Console.WriteLine("");
            Console.Write("Please enter amount you want to put in (Only in whole dollars): $");
        }

        public static void ItemString()
        {
            Console.WriteLine("");
            Console.Write("Please enter the Item No of the product you want to purchase or press '1' to go back: ");
        }

        public static void ExitString()
        {
            Console.WriteLine("");
            Console.WriteLine("VENDING MACHINE CLOSED.");
        }
    }
}
