using ExpenseMobileApp.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExpenseMobileApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExpenseDisplayPage : ContentPage
    {
        public int balance { get; set; }
        public ExpenseDisplayPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            MonthlyExpense monthlyExpense = new MonthlyExpense();

            //get all expenses and set data context
            ExpenseManager.GetMonthlyExpenses(DateTime.Now.Month,DateTime.Now.Year, ref monthlyExpense);
            ExpenseListview.ItemsSource = monthlyExpense.ExpenseList.OrderByDescending(x => x.Date);
            MonthBudget.Text = monthlyExpense.Budget.ToString("C", CultureInfo.CurrentCulture);
            balance = monthlyExpense.Balance;
            BalanceDisplay.Text = balance.ToString("C", CultureInfo.CurrentCulture);
            EditDeleteStack.IsVisible = false;

        }

        private async void OnAddExpenseClicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new AddEditExpensePage { BindingContext = new Expense() }));

        }

        private void ExpenseListview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EditDeleteStack.IsVisible = true;
            
        }

        private async void DeleteExpense_Clicked(object sender, EventArgs e)
        {
            ExpenseListview.SelectedItem = null;
            var expense = (Expense)BindingContext;
            ExpenseManager.DeleteMonthlyExpense(DateTime.Now.Month, DateTime.Now.Year, expense);
            await Navigation.PushModalAsync(new NavigationPage(new ExpenseDisplayPage()));

        }

        private async void EditExpense_Clicked(object sender, EventArgs e)
        {
            ExpenseListview.SelectedItem = null;
            await Navigation.PushModalAsync(new NavigationPage(new AddEditExpensePage
            { BindingContext = (Expense)ExpenseListview.SelectedItem }));
        }

        private void CancelSelection_Clicked(object sender, EventArgs e)
        {
            ExpenseListview.SelectedItem = null;
            EditDeleteStack.IsVisible = false;
        }

        private async void PreviousMonthBtn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new ExpenseDisplayPage()));
        }

        private async void NextMonthBtn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new ExpenseDisplayPage()));
        }
    }
}