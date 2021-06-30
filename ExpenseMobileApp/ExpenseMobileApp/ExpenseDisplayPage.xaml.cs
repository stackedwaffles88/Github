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
            //get all expenses and set data cotext
            ExpenseListview.ItemsSource = ExpenseManager.Expenses;
            Budgetmoney.Text = ExpenseManager.Budget.ToString();
            balance = ExpenseManager.Balance;
            BalanceDisplay.Text = balance.ToString();
            //ExpenseListview.SetBinding(ListView.FooterProperty, balance);
           
        }

        private async void OnAddExpenseClicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new AddEditExpensePage { BindingContext = new Expense() }));

        }
    }
}