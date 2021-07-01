using System;
using System.Collections.Generic;
using System.Text;

namespace ExpenseMobileApp.Model
{
    public class MonthlyExpense
    {
        public int Budget { get; set; }

        public List<Expense> ExpenseList { get; set; }

        public int Month { get; set; }

        public int Balance
        {
            get
            {
                //calculate balace based on expenses
                int totalExp = 0;
                ExpenseList.ForEach(exp => totalExp += exp.Amount);
                return Budget - totalExp;
            }
        }
    }
}
