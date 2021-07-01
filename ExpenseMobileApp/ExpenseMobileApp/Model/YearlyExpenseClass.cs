using System;
using System.Collections.Generic;
using System.Text;

namespace ExpenseMobileApp.Model
{
    public class YearlyExpense

    {
 
        public int Year { get; set; }

        public  List<MonthlyExpense> monthlyExpenseList = new List<MonthlyExpense>();
    }
}
