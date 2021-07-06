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
        public Expense expense { get; set; }
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
            expense = (Expense)BindingContext;


            
            // this is bound to the user
        }

        private async void OnDoneButtonClicked(object sender, EventArgs e)
        {
            // update the expense list with this
            var expense = (Expense)BindingContext;
            ExpenseManager.AddMonthlyExpense(DateTime.Now.Month, DateTime.Now.Year, expense);

            await Navigation.PushModalAsync(new NavigationPage(new ExpenseDisplayPage()));

        }

        private async void OnCancelButtonClicked(object sender, EventArgs e)
        {
            //cancel go back to the original page
            await Navigation.PopModalAsync();

        }

    }
}