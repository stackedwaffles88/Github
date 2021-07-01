using ExpenseMobileApp.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ExpenseMobileApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            ExpenseManager.InitializeData();
          
        }

        protected async override void OnAppearing()
        {
            //Decide what template to display based on condition
            if (ExpenseManager.IsMonthInitialised(DateTime.Now.Year, DateTime.Now.Month))
            {
                await Navigation.PushModalAsync(new NavigationPage(new ExpenseDisplayPage() ));
            }
        }
        private static int Budget = 0;
        private async void OnContinueButtonClicked(object sender, EventArgs e)
        {
            //set the budget to the text that
            Budget = int.Parse(BudgetInputTextbox.Text);
            ExpenseManager.InitializeMonthlyBudget(Budget, DateTime.Now.Year, DateTime.Now.Month);
            
            //continue to the next page
            // move to expense page now
            await Navigation.PushModalAsync(new NavigationPage(new AddEditExpensePage { BindingContext = new Expense()}));
        }
    }
}
