using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using ExpenseMobileApp.Model;

namespace ExpenseMobileApp.ViewModel
{
   public class PieChartViewerImpl
    {
        public ObservableCollection<CategoryExpensePie> Data { get; set; }

        public PieChartViewerImpl(Dictionary<string, double> expensesbyCategory)
        {
            Data = new ObservableCollection<CategoryExpensePie>();
            foreach(KeyValuePair<string, double> entry in expensesbyCategory)
            {
                CategoryExpensePie randomObj = new CategoryExpensePie()
                {
                    CategoryName = entry.Key,
                    AmountForCategory = entry.Value
                };
                Data.Add(randomObj);
            }
        }
    }
}



