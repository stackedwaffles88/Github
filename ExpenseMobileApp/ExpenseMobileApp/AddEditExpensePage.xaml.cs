using ExpenseMobileApp.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
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
        }

        protected override void OnAppearing()
        {
            //get the binding context 
            expense = (Expense)BindingContext;

            var catSelectiontapped = new TapGestureRecognizer();
            catSelectiontapped.Tapped += CatSelectiontapped_Tapped;
            CatSelection.GestureRecognizers.Add(catSelectiontapped);

            CategoryItem ci = new CategoryItem();
            string CatPic = ci.CatIcon;

            //CatIconBtn.Source = CatPic;


        }

        private async void CatSelectiontapped_Tapped(object sender, EventArgs e)
        {
            var expense = (Expense)BindingContext;
            await Navigation.PushModalAsync(new NavigationPage(new CategoryChoice { BindingContext = expense }));

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
            await Navigation.PushModalAsync(new NavigationPage(new ExpenseDisplayPage()));

        }

    }
}