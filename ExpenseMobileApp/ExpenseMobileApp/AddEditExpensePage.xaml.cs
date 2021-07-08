using ExpenseMobileApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExpenseMobileApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddEditExpensePage : ContentPage
    {
        private int currentMonth;
        private int currentYear;
        public Expense expenseContext { get; set; }
        public AddEditExpensePage()
        {
            InitializeComponent();
            loadCategoryPicker();
            
        }

        public void loadCategoryPicker()
        {
            //load Category Picker with a list of strings
            var categoryList = new List<string>();
            categoryList.Add("Essentials");
            categoryList.Add("Dining");
            categoryList.Add("Retail");
            categoryList.Add("Entertainment");
            categoryList.Add("Bills");

            CategoryPicker.ItemsSource = categoryList;
        }
        protected override void OnAppearing()
        {
            //get the binding context 
            expenseContext = (Expense)BindingContext;
            //date picker should display date corresponding only to that month and year and not any value
            //if it is a new expense 
            currentMonth = expenseContext.Date.Month;
            currentYear = expenseContext.Date.Year;
            //set min max values for the picker
            ExpenseDatePicker.MinimumDate = new DateTime(currentYear, currentMonth, 1);
            ExpenseDatePicker.MaximumDate = new DateTime(currentYear, currentMonth, DateTime.DaysInMonth(currentYear,currentMonth));

            
            // this is bound to the user
        }

        private async void OnDoneButtonClicked(object sender, EventArgs e)
        {
            // update the expense list with this
            var expense = (Expense)BindingContext;
            ExpenseManager.AddModifyMonthlyExpense(currentMonth, currentYear, expenseContext, expense);
            //move to the expense display page to display the current month and year
            // pass month and year as binding context to the expensedisplaypage
            string yearMonth = $"{currentMonth}.{currentYear}";
            await Navigation.PushModalAsync(new NavigationPage(new ExpenseDisplayPage { BindingContext = yearMonth }));

        }

        private async void OnCancelButtonClicked(object sender, EventArgs e)
        {
            //cancel go back to the original page
            await Navigation.PopModalAsync();

        }

    }
}