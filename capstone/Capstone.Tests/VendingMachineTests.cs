using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace Capstone.Tests
{
    [TestClass]
    public class VendingMachineTests
    {
        [TestMethod]
        public void AddItemsToMachineTests()
        {
            VendingMachine vendingMachine = new VendingMachine();
            List<FoodItem> expectedFoodList = new List<FoodItem>();
            expectedFoodList.Add(new Chip { ItemNo = "A1", Name = "Potato Crisps", Price = 3.05M, Type = "Chip" });
            expectedFoodList.Add(new Candy { ItemNo = "B1", Name = "Moonpie", Price = 1.80M, Type = "Candy" });
            expectedFoodList.Add(new Drink { ItemNo = "C1", Name = "Cola", Price = 1.25M, Type = "Drink" });
            expectedFoodList.Add(new Gum { ItemNo = "D1", Name = "U-Chews", Price = 0.85M, Type = "Gum" });
            vendingMachine.AddItemsToMachine("vendingmachinetest.csv");

            int i = 0;
            foreach (FoodItem item in vendingMachine.FoodItemList)
            {
                Assert.AreEqual(vendingMachine.FoodItemList[i].ItemNo, expectedFoodList[i].ItemNo);
                Assert.AreEqual(vendingMachine.FoodItemList[i].Name, expectedFoodList[i].Name);
                Assert.AreEqual(vendingMachine.FoodItemList[i].Price, expectedFoodList[i].Price);
                Assert.AreEqual(vendingMachine.FoodItemList[i].Type, expectedFoodList[i].Type);
                i++;
            }
        }

        [TestMethod]
        public void FillStockTest()
        {
            VendingMachine vendingMachine = new VendingMachine();
            vendingMachine.FoodItemList.Add(new Chip { ItemNo = "A1", Name = "a", Price = 3.05M, Type = "Chip" });
            vendingMachine.FoodItemList.Add(new Candy { ItemNo = "B1", Name = "b", Price = 1.80M, Type = "Candy" });
            vendingMachine.FoodItemList.Add(new Drink { ItemNo = "C1", Name = "c", Price = 1.25M, Type = "Drink" });
            vendingMachine.StockValues["a"] = "1";
            vendingMachine.StockValues["b"] = "190";
            vendingMachine.StockValues["c"] = "avass";


            vendingMachine.FillStock();

            string expectedResult = "5";

            foreach (KeyValuePair<string, string> item in vendingMachine.StockValues)
            {
                Assert.AreEqual(expectedResult, item.Value);
            }
        }

        [TestMethod]
        public void MinusStockTest()
        {
            VendingMachine vendingMachine = new VendingMachine();
            vendingMachine.StockValues["a"] = "2";
            vendingMachine.StockValues["b"] = "190";
            vendingMachine.StockValues["c"] = "avass";

            vendingMachine.MinusStock("a");
            string expectedValue1 = "1";
            Assert.AreEqual(expectedValue1, vendingMachine.StockValues["a"]);

            vendingMachine.MinusStock("a");
            string expectedValue2 = "SOLD OUT";
            Assert.AreEqual(expectedValue2, vendingMachine.StockValues["a"]);

            vendingMachine.MinusStock("b");
            string expectedValue3 = "189";
            Assert.AreEqual(expectedValue3, vendingMachine.StockValues["b"]);

            vendingMachine.MinusStock("b");
            string expectedValue4 = "188";
            Assert.AreEqual(expectedValue4, vendingMachine.StockValues["b"]);
        }

        [TestMethod]
        public void ItemNo2NameTests()
        {
            VendingMachine vendingMachine = new VendingMachine();
            vendingMachine.FoodItemList.Add(new Chip { ItemNo = "A1", Name = "a", Price = 3.05M, Type = "Chip" });
            vendingMachine.FoodItemList.Add(new Candy { ItemNo = "B1", Name = "b", Price = 1.80M, Type = "Candy" });
            vendingMachine.FoodItemList.Add(new Drink { ItemNo = "C1", Name = "c", Price = 1.25M, Type = "Drink" });

            string param1ItemNo = "a1";
            string param2ItemNo = "A1";
            string param3ItemNo = "a";

            string expectedResult1 = "a";
            string expectedResult2 = "a";
            string expectedResult3 = null;

            string actualResult1 = vendingMachine.ItemNo2Name(param1ItemNo);
            string actualResult2 = vendingMachine.ItemNo2Name(param2ItemNo);
            string actualResult3 = vendingMachine.ItemNo2Name(param3ItemNo);

            Assert.AreEqual(expectedResult1, actualResult1);
            Assert.AreEqual(expectedResult2, actualResult2);
            Assert.AreEqual(expectedResult3, actualResult3);
        }

        [TestMethod]
        public void Name2PriceTests()
        {
            VendingMachine vendingMachine = new VendingMachine();
            vendingMachine.FoodItemList.Add(new Chip { ItemNo = "A1", Name = "a", Price = 3.05M, Type = "Chip" });
            vendingMachine.FoodItemList.Add(new Candy { ItemNo = "B1", Name = "b", Price = 1.80M, Type = "Candy" });
            vendingMachine.FoodItemList.Add(new Drink { ItemNo = "C1", Name = "c", Price = 1.25M, Type = "Drink" });

            string param1ItemNo = "a";
            string param2ItemNo = "c";
            string param3ItemNo = "safasfsaf";

            decimal expectedResult1 = 3.05M;
            decimal expectedResult2 = 1.25M;
            decimal expectedResult3 = 0;

            decimal actualResult1 = vendingMachine.Name2Price(param1ItemNo);
            decimal actualResult2 = vendingMachine.Name2Price(param2ItemNo);
            decimal actualResult3 = vendingMachine.Name2Price(param3ItemNo);

            Assert.AreEqual(expectedResult1, actualResult1);
            Assert.AreEqual(expectedResult2, actualResult2);
            Assert.AreEqual(expectedResult3, actualResult3);
        }

        [TestMethod]
        public void WriteLogsTest()
        {
            VendingMachine vendingMachine = new VendingMachine();
            string directory = Environment.CurrentDirectory;
            string relativeFileName = $@"..\..\..\..\log.txt";
            string fullPath = Path.Combine(directory, relativeFileName);

            List<string> expectedList = new List<string>();

            expectedList.Add("02/10/2022 22:25:06 PM FEED MONEY: $1.00 $1.00");
            expectedList.Add("02/10/2022 22:25:08 PM Chiclets D3 $1.00 $0.25");
            expectedList.Add("02/10/2022 22:25:15 PM FEED MONEY: $5.00 $5.25");
            expectedList.Add("02/10/2022 22:25:25 PM Chiclets D3 $5.25 $4.50");

            vendingMachine.Log.Add("02/10/2022 22:25:06 PM FEED MONEY: $1.00 $1.00");
            vendingMachine.Log.Add("02/10/2022 22:25:08 PM Chiclets D3 $1.00 $0.25");
            vendingMachine.Log.Add("02/10/2022 22:25:15 PM FEED MONEY: $5.00 $5.25");
            vendingMachine.Log.Add("02/10/2022 22:25:25 PM Chiclets D3 $5.25 $4.50");
            vendingMachine.WriteLog();

            using(StreamReader sr = new StreamReader(fullPath))
            {
                int i = 0;
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    Assert.AreEqual(expectedList[i], line);
                    i++;
                }
            }
        }
    }
}
