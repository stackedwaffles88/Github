using System;
using System.Collections.Generic;
using System.Text;

namespace ExpenseMobileApp.Model
{
    //public enum Categories
    //{
    //    Essentials,
    //    Dining,
    //    Retail,
    //    Bills,
    //    Entertainment
    //}

    public enum CatFiles
    {

    }
    public class Expense
    {
        public string Name { get; set; }
        public int Amount { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;
            

        public string CategoryName { get; set; }
        
        public string CategoryIcon { get; set; }

        //public Expense(string name, Categories category)
        //{
        //    Name = name;
        //    CategoryName = category;
        //    CategoryIcon = 
        //}
    }
}

