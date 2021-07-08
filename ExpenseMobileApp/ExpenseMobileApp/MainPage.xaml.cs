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
            string yearMonth = $"{DateTime.Now.Month}.{DateTime.Now.Year}";
            //Decide what template to display based on condition
            if (ExpenseManager.IsMonthInitialised(DateTime.Now.Year, DateTime.Now.Month))
            {
               
                await Navigation.PushModalAsync(new NavigationPage(new ExpenseDisplayPage { BindingContext = yearMonth } ));
            }
            else
            {
                await Navigation.PushModalAsync(new NavigationPage(new SetBudgetPage { BindingContext = yearMonth }));
            }
        }
        private static int Budget = 0;
        private async void OnContinueButtonClicked(object sender, EventArgs e)
        {
            //displays an error message if no budget amount is specified
            if (BudgetInputTextbox.Text == null)
            {
                await DisplayAlert("Error", "Please Enter An Amount", "OK");
            }
            else
            {
                //set the budget to the text contents
                Budget = int.Parse(BudgetInputTextbox.Text);
                ExpenseManager.SetMonthlyBudget(Budget, DateTime.Now.Year, DateTime.Now.Month);

                // move to expense page
                await Navigation.PushModalAsync(new NavigationPage(new AddEditExpensePage { BindingContext = new Expense { Date = DateTime.Now } }));

            }
        }

        private async void RadioExample_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new StartPage()));

        }
    }
}
