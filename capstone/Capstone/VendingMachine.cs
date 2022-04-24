using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Capstone
{
    public class VendingMachine
    {
        public List<FoodItem> FoodItemList { get; set; } = new List<FoodItem>();
        public List<string> ItemSlots { get; set; } = new List<string>();
        public Dictionary<string, string> StockValues { get; set; } = new Dictionary<string, string>();
        public List<string> Log { get; set; } = new List<string>();
        public Dictionary<string, decimal> SalesReport { get; set; } = new Dictionary<string, decimal>();
        private string Directory { get; set; } = Environment.CurrentDirectory;

        public void AddItemsToMachine(string fileInput)
        {
            string relativeFileName = $@"..\..\..\..\{fileInput}";
            string fullPath = Path.Combine(Directory, relativeFileName);
            int count = 0;
            try
            {
                using (StreamReader sr = new StreamReader(fullPath))
                {
                    while (!sr.EndOfStream)
                    {
                        count++;
                        string line = sr.ReadLine();
                        string[] item = line.Split("|");
                        string type = item[3];

                        if (item[3] == "Chip")
                        {
                            FoodItemList.Add(new Chip { ItemNo = item[0], Name = item[1], Price = decimal.Parse(item[2]), Type = item[3] });
                            ItemSlots.Add(item[0]);
                        }
                        else if (item[3] == "Candy")
                        {
                            FoodItemList.Add(new Candy { ItemNo = item[0], Name = item[1], Price = decimal.Parse(item[2]), Type = item[3] });
                            ItemSlots.Add(item[0]);
                        }
                        else if (item[3] == "Drink")
                        {
                            FoodItemList.Add(new Drink { ItemNo = item[0], Name = item[1], Price = decimal.Parse(item[2]), Type = item[3] });
                            ItemSlots.Add(item[0]);
                        }
                        else if (item[3] == "Gum")
                        {
                            FoodItemList.Add(new Gum { ItemNo = item[0], Name = item[1], Price = decimal.Parse(item[2]), Type = item[3] });
                            ItemSlots.Add(item[0]);
                        }

                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine("Error Reading the File: ");
                Console.WriteLine(ex.Message);
            }
        }

        public void DisplayItems()
        {
            string name = "";
            string type = "";
            Console.WriteLine("");
            Console.WriteLine("============================================================");
            Console.WriteLine("(Item No)       (Name)         (Price)   (Type)    (Stock)");
            Console.WriteLine("============================================================");
            foreach (FoodItem item in FoodItemList)
            {
                if (item.Name.Length < 18)
                {
                    for (int i = 0; i < 18 - item.Name.Length; i++)
                    {
                        name += " ";
                    }

                }
                if (item.Type.Length < 6)
                {
                    for (int i = 0; i < 6 - item.Type.Length; i++)
                    {
                        type += " ";
                    }

                }
                Console.WriteLine($"    {item.ItemNo}      {item.Name}{name}  {item.Price}      {item.Type}{type}    {StockValues[item.Name]}");
                name = "";
                type = "";
            }
        }

        public void FillStock()
        {
            foreach (FoodItem item in FoodItemList)
            {
                StockValues[item.Name] = "5";
            }
        }

        public void MinusStock(string name)
        {
            int newStock = int.Parse(StockValues[name]) - 1;
            if (newStock == 0)
            {
                StockValues[name] = "SOLD OUT";
            }
            else
            {
                StockValues[name] = newStock.ToString();
            }
        }

        public string ItemNo2Name(string itemNo)
        {
            foreach (FoodItem item in FoodItemList)
            {
                if (item.ItemNo == itemNo.ToUpper())
                {
                    return item.Name;
                }
            }
            return null;
        }

        public decimal Name2Price(string name)
        {
            foreach (FoodItem item in FoodItemList)
            {
                if (item.Name == name)
                {
                    return item.Price;
                }
            }
            return 0;
        }

        public void WriteLog()
        {
            string relativeFileName = $@"..\..\..\..\log.txt";
            string fullPath = Path.Combine(Directory, relativeFileName);
            try
            {
                using StreamWriter sw = new StreamWriter(fullPath, false);
                foreach (string line in Log)
                {
                    sw.WriteLine(line);
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine("Error writing file: ");
                Console.WriteLine(ex.Message);
            }
        }

        public void Add2SalesReport(string name, decimal price)
        {
            if (SalesReport.ContainsKey(name))
            {
                SalesReport[name] += price;
            }
            else
            {
                SalesReport[name] = price;
            }
        }

        public void MakeSalesReport()
        {
            string dateTime = DateTime.Now.ToString("-MM-dd-yyyy-HH.mm.ss");
            string relativeFileName = $@"..\..\..\Files\salesreport{dateTime}.csv";
            string fullPath = Path.Combine(Directory, relativeFileName);
            try
            {
                using StreamWriter sw = new StreamWriter(fullPath);
                foreach (KeyValuePair<string, decimal> line in SalesReport)
                {
                    int intPrice = (int)line.Value;
                    sw.WriteLine($"{line.Key}|{intPrice}");
                }
                Console.WriteLine("");
                Console.WriteLine("Sales Report Made!");
                Console.WriteLine("");
            }
            catch (IOException ex)
            {
                Console.WriteLine("Error writing file: ");
                Console.WriteLine(ex.Message);
            }
        }
    }
}
