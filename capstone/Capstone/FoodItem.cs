using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone
{
    public class FoodItem
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Type { get; set; }
        public string ItemNo { get; set; }
        public string Message { get; }

        public FoodItem(string message)
        {
            Message = message;
        }
    }
}
