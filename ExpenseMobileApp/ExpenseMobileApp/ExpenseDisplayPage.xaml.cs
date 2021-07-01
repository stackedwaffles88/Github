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
            //get all expenses and set data cotext
            ExpenseManager.GetMonthlyExpenses(DateTime.Now.Month,DateTime.Now.Year, ref monthlyExpense);
            ExpenseListview.ItemsSource = monthlyExpense.ExpenseList;
            Budgetmoney.Text = monthlyExpense.Budget.ToString();
            balance = monthlyExpense.Balance;
            BalanceDisplay.Text = balance.ToString();
            //ExpenseListview.SetBinding(ListView.FooterProperty, balance);
           
        }

        private async void OnAddExpenseClicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new AddEditExpensePage { BindingContext = new Expense() }));

        }
    }
}