using System;
using System.Collections.Generic;
using System.Text;

namespace ExpenseMobileApp.Model
{
    public  class Expense
    {
        public string Name { get; set; }
        public int Amount { get; set; }

        public DateTime Date { get; set; } = new DateTime(2021, 7, 1);
            

        public string CategoryName { get; set; }

        public string CategoryIcon { get; set; }

    }
}

