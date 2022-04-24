using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace Capstone.Tests
{
    [TestClass]
    public class BalanceTests
    {
        [TestMethod]
        public void AddMoneyTests()
        {
            Balance balance = new Balance();

            string lowParam = "1";
            decimal expectedLowParamValue = 1.00M;
            balance.AddMoney(lowParam);
            Assert.AreEqual(expectedLowParamValue, balance.Money);

            string highParam = "999";
            decimal expectedHighParamValue = 1000.00M;
            balance.AddMoney(highParam);
            Assert.AreEqual(expectedHighParamValue, balance.Money);

            string outOfBoundsParam = "asdad";
            decimal expectedOutOfBoundsValue = 1000.00M;
            balance.AddMoney(outOfBoundsParam);
            Assert.AreEqual(expectedOutOfBoundsValue, balance.Money);

            string decimalParam = "2.33";
            decimal expectedDecimalValue = 1000.00M;
            balance.AddMoney(decimalParam);
            Assert.AreEqual(expectedDecimalValue, balance.Money);
        }

        [TestMethod]
        public void SubtractMoneyUntilSoldOutItemTests()
        {
            Balance balance = new Balance();
            VendingMachine vendingMachine = new VendingMachine();
            vendingMachine.FoodItemList.Add(new FoodItem("Yum") { ItemNo = "A1", Name = "Potato Chips", Price = 3.05M, Type = "Chip" });
            vendingMachine.FillStock();
            balance.AddMoney("100");

            string upperCaseParam = "A1";
            decimal expectedResult = 96.95M;
            balance.SubtractMoney(upperCaseParam, vendingMachine.FoodItemList, vendingMachine.StockValues);
            vendingMachine.MinusStock("Potato Chips");
            Assert.AreEqual(expectedResult, balance.Money);

            balance.SubtractMoney(upperCaseParam, vendingMachine.FoodItemList, vendingMachine.StockValues);
            vendingMachine.MinusStock("Potato Chips");
            balance.SubtractMoney(upperCaseParam, vendingMachine.FoodItemList, vendingMachine.StockValues);
            vendingMachine.MinusStock("Potato Chips");
            balance.SubtractMoney(upperCaseParam, vendingMachine.FoodItemList, vendingMachine.StockValues);
            vendingMachine.MinusStock("Potato Chips");
            balance.SubtractMoney(upperCaseParam, vendingMachine.FoodItemList, vendingMachine.StockValues);
            vendingMachine.MinusStock("Potato Chips");
            decimal expectedResult2 = 84.75M;
            Assert.AreEqual(expectedResult2, balance.Money);

            
            string expectedResult3 = "THIS ITEM IS SOLD OUT";
            string actualResult3 = balance.SubtractMoney(upperCaseParam, vendingMachine.FoodItemList, vendingMachine.StockValues);
            Assert.AreEqual(expectedResult3, actualResult3);

        }

        [TestMethod]
        public void DispenseMoneyTests()
        {
            Balance balance = new Balance();
            
            decimal expectedResult = 0;
            balance.AddMoney("3");
            balance.DispenseMoney();

            Assert.AreEqual(expectedResult, balance.Money);

            balance.AddMoney("332525");
            balance.DispenseMoney();

            Assert.AreEqual(expectedResult, balance.Money);
        }
    }
}
