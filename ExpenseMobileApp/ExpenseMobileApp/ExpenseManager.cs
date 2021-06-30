using ExpenseMobileApp.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ExpenseMobileApp
{
    public static class ExpenseManager
    {
        public static List<Expense> Expenses = new List<Expense>();

        public static int Budget { get; set; }

        public static void AddExpense(Expense expense)
        {
            DateTime date = expense.Date.Date;
            Expenses.Add(expense);
        }

        public static int Balance
        {
            get
            {
                //calculate balace based on expenses
                int totalExp = 0;
                Expenses.ForEach(exp => totalExp += exp.Amount);
                return Budget - totalExp;
            }
        }

    }

}
