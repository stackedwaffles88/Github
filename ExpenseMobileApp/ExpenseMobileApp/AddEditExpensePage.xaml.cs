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
        private string oldCatSelection;
        public Expense expenseContext { get; set; }
        
        public AddEditExpensePage()
        {
            InitializeComponent();
            
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
            int daysallowed;
            if(DateTime.Now.Year == currentYear && DateTime.Now.Month == currentMonth)
            {
                daysallowed = DateTime.Now.Day;
            }
            else
            {
                daysallowed = DateTime.DaysInMonth(currentYear, currentMonth);
            }
            ExpenseDatePicker.MaximumDate = new DateTime(currentYear, currentMonth, daysallowed);
            CatSelection.Text = expenseContext.CategoryName;

            var catSelectiontapped = new TapGestureRecognizer();
            catSelectiontapped.Tapped += CatSelectiontapped_Tapped;
            CatSelection.GestureRecognizers.Clear();
            CatSelection.GestureRecognizers.Add(catSelectiontapped);



            // this is bound to the user
        }

        private async void CatSelectiontapped_Tapped(object sender, EventArgs e)
        {
            oldCatSelection = CatSelection.Text;
            
            var expense = (Expense)BindingContext;
            await Navigation.PushModalAsync(new NavigationPage(new CategoryChoice { BindingContext = expense }));

        }

        private async void OnDoneButtonClicked(object sender, EventArgs e)
        {
            //do the validations before proceeding
            if (String.IsNullOrEmpty(ExpenseAmountEntry.Text) || !Double.TryParse(ExpenseAmountEntry.Text, out _ ) || Double.Parse(ExpenseAmountEntry.Text) <=0 || String.IsNullOrEmpty(CatSelection.Text)) {
                await DisplayAlert("Error", "Please enter valid values for all entries", "OK");
                //display error message
            } else { 
            // update the expense list with this
            Expense expense = new Expense();
            //update the expense wiotht he new values user has selected
            expense.Name = ExpenseNameLabel.Text;
            expense.Amount = Double.Parse((ExpenseAmountEntry.Text));
            expense.CategoryName = CatSelection.Text;
            expense.Date = ExpenseDatePicker.Date;
            ExpenseManager.AddModifyMonthlyExpense(currentMonth, currentYear, expenseContext, expense);
            //move to the expense display page to display the current month and year
            // pass month and year as binding context to the expensedisplaypage
            //string yearMonth = $"{currentMonth}.{currentYear}";
            await Navigation.PopModalAsync();
            //await Navigation.PushModalAsync(new NavigationPage(new ExpenseDisplayPage { BindingContext = yearMonth }));

            }
        }

        private async void OnCancelButtonClicked(object sender, EventArgs e)
        {
            
            if (!String.IsNullOrEmpty(oldCatSelection))
            {
                expenseContext.CategoryName = oldCatSelection;
            }
            //cancel go back to the original page
            await Navigation.PopModalAsync();

        }

    }
}