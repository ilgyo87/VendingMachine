using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone
{
    public class Balance
    {
        public decimal Money { get; private set; }
        public Balance()
        {
            Money = 0.00M;
        }

        public string AddMoney(string amount)
        {
            string line = "";
            try
            {
                int convertToInt = int.Parse(amount);
                Money += (decimal)convertToInt;
                line = $"{DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss tt")} FEED MONEY: ${amount}.00 ${Money}";
            }
            catch (Exception)
            {
                Console.WriteLine("");
                Console.WriteLine("THIS IS NOT A VALID INPUT");
            }
            return line;
        }

        public string SubtractMoney(string itemNo, List<FoodItem> foodItems, Dictionary<string, string> stockValues)
        {
            decimal moneyBefore = Money;
            string line = "";
            foreach (FoodItem item in foodItems)
            {
                if (itemNo.ToUpper() == item.ItemNo.ToUpper() && stockValues[item.Name] == "SOLD OUT")
                {
                    line = "THIS ITEM IS SOLD OUT";
                    Console.WriteLine("");
                    Console.WriteLine(line);
                }
                else if (itemNo.ToUpper() == item.ItemNo.ToUpper() && item.Price < Money)
                {
                    Money -= item.Price;
                    Console.WriteLine("");
                    Console.WriteLine($"You have selected {item.Name} for ${item.Price}. You now have ${Money}");
                    Console.WriteLine(item.Message);
                    line = $"{DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss tt")} {item.Name} {item.ItemNo} ${moneyBefore} ${Money} ";
                }
                else if (itemNo.ToUpper() == item.ItemNo.ToUpper() && item.Price > Money)
                {
                    line = "YOU DON'T HAVE ENOUGH MONEY";
                    Console.WriteLine("");
                    Console.WriteLine(line);
                }
            }
            return line;
        }

        public string DispenseMoney()
        {
            string line = $"{DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss tt")} GIVE CHANGE: ${Money} $0.00";
            int q = 0;
            int d = 0;
            int n = 0;
            decimal moneyBack = Money;
            if (Money >= 0.25M)
            {
                decimal quarters = Money / .25M;
                q = (int)quarters;
                Money -= .25M * (decimal)q;
            }
            if (Money >= 0.10M)
            {
                decimal dimes = Money / .10M;
                d = (int)dimes;
                Money -= .10M * (decimal)d;
            }
            if (Money >= 0.05M)
            {
                decimal nickels = Money / .05M;
                n = (int)nickels;
                Money -= .05M * (decimal)n;
            }
            Console.WriteLine("");
            Console.WriteLine($"Your change is: ${moneyBack}, {q} quarter(s), {d} dime(s), and {n} nickel(s)");
            Console.WriteLine("");
            return line;
        }
    }
}
