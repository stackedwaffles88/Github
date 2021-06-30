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
          
        }
        private static int Budget = 0;
        private async void OnContinueButtonClicked(object sender, EventArgs e)
        {
            //set the budget to the text that
            Budget = int.Parse(BudgetInputTextbox.Text);
            ExpenseManager.Budget = Budget;

            //continue to the next page
            // move to expense page now
            await Navigation.PushModalAsync(new NavigationPage(new AddEditExpensePage { BindingContext = new Expense()}));
        }
    }
}
