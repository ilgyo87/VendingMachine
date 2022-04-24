using System;

namespace Capstone
{
    class Program
    {
        static void Main(string[] args)
        {
            VendingMachine vendingMachine = new VendingMachine();
            Balance moneyPut = new Balance();
            vendingMachine.AddItemsToMachine("vendingmachine.csv");
            vendingMachine.FillStock();
            bool isVendingMachineRunning = true;



            while (isVendingMachineRunning)
            {
                try
                {
                    StringMenu.FirstMenu();
                    string menuOption = Console.ReadLine();
                    if (menuOption == "1")
                    {
                        vendingMachine.DisplayItems();
                    }
                    else if (menuOption == "2")
                    {
                        while (isVendingMachineRunning)
                        {
                            try
                            {
                                StringMenu.SecondMenu(moneyPut.Money);
                                string secondMenuOption = Console.ReadLine();
                                if (secondMenuOption == "1")
                                {
                                    while (isVendingMachineRunning)
                                    {
                                        StringMenu.FeedString();
                                        string amountIn = Console.ReadLine();
                                        vendingMachine.Log.Add(moneyPut.AddMoney(amountIn));
                                        break;
                                    }
                                }
                                else if (secondMenuOption == "2")
                                {
                                    while (isVendingMachineRunning)
                                    {
                                        try
                                        {
                                            vendingMachine.DisplayItems();
                                            StringMenu.ItemString();
                                            string itemNo = Console.ReadLine();
                                            if (itemNo == "1")
                                            {
                                                break;
                                            }
                                            else if (vendingMachine.ItemSlots.Contains(itemNo.ToUpper()))
                                            {
                                                string value = moneyPut.SubtractMoney(itemNo, vendingMachine.FoodItemList, vendingMachine.StockValues);
                                                if (value == "THIS ITEM IS SOLD OUT" || value == "YOU DON'T HAVE ENOUGH MONEY")
                                                {
                                                    break;
                                                }
                                                else
                                                {
                                                    vendingMachine.Log.Add(value);
                                                    string name = vendingMachine.ItemNo2Name(itemNo);
                                                    decimal price = vendingMachine.Name2Price(name);
                                                    vendingMachine.MinusStock(name);
                                                    vendingMachine.Add2SalesReport(name, price);
                                                    break;
                                                }

                                            }
                                            else
                                            {
                                                throw new Exception("THIS IS NOT A VALID INPUT, PLEASE ENTER AGAIN.");
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            Console.WriteLine("");
                                            Console.WriteLine(ex.Message);
                                        }
                                    }
                                }
                                else if (secondMenuOption == "3")
                                {
                                    vendingMachine.Log.Add(moneyPut.DispenseMoney());
                                    vendingMachine.WriteLog();
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("");
                                    throw new Exception("THIS IS NOT A VALID INPUT, PLEASE ENTER AGAIN.");
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                        }
                    }
                    else if (menuOption == "3")
                    {
                        StringMenu.ExitString();
                        break;
                    }
                    else if (menuOption == "4")
                    {
                        vendingMachine.MakeSalesReport();
                    }
                    else
                    {
                        Console.WriteLine("");
                        throw new Exception("THIS IS NOT A VALID INPUT, PLEASE ENTER AGAIN.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
