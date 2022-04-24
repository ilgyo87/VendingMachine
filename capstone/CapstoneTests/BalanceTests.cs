using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Capstone.Tests
{
    [TestClass]
    class BalanceTests
    {
        [TestMethod]
        public void AddMoneyTest()
        {
            Balance balance = new Balance();

            string lowParam = "1";
            string highParam = "999999";
            string outOfBoundsParam = "a";
            string decimalParam = "1.33";
            string decimalTwoParam = "1.00";

            decimal expectedLowParam = 1.00M;
            balance.AddMoney(lowParam);
            Assert.AreEqual(expectedLowParam, balance.Money);

            decimal expectedHighParam = 1000000.00M;
            balance.AddMoney(highParam);
            Assert.AreEqual(expectedHighParam, balance.Money);

            /*balance.AddMoney(outOfBoundsParam);


            Assert*/



        }
    }
}
