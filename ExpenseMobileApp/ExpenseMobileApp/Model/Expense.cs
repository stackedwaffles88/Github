using System;
using System.Collections.Generic;
using System.Text;

namespace ExpenseMobileApp.Model
{
    public class Expense
    {
        public string Name { get; set; }
        public double Amount { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;
            

        public string CategoryName { get; set; }
        
        public string CategoryIcon { get; set; }

    }
}

