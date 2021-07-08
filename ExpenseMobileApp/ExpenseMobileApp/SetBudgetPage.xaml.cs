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
    public partial class SetBudgetPage : ContentPage
    {
        private string context;
        public SetBudgetPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing()
        {
            context = (string)BindingContext;
            string[] info = context.Split('.');

            BudgetInputTextbox.Text = ExpenseManager.GetMonthlyBudget(int.Parse(info[1]), int.Parse(info[0])).ToString();
            YearMonthLabel.Text = $"{Enum.GetName(typeof(Months), int.Parse(info[0])-1)} {info[1]}";

        }

        private async void OnContinueButtonClicked(object sender, EventArgs e)
        {
            string inputstr = BudgetInputTextbox.Text;
            //displays an error message if no budget amount is specified
            if (String.IsNullOrEmpty(inputstr) || !int.TryParse(inputstr, out _) || int.Parse(inputstr) <=0)
            {
                await DisplayAlert("Error", "Please Enter A valid Amount", "OK");
            }
            else
            {
                string[] info = context.Split('.');
               
                //set the budget to the text contents
                int Budget = int.Parse(BudgetInputTextbox.Text);
                ExpenseManager.SetMonthlyBudget(Budget, int.Parse(info[1]), int.Parse(info[0]));

                // move to expense page
                //await Navigation.PushModalAsync(new NavigationPage(new ExpenseDisplayPage { BindingContext = context }));
                await Navigation.PopModalAsync();

            }
        }

        private async void OnCancelButtonClicked(object sender, EventArgs e)
        {
            //displays an error message if no budget amount is specified
            await Navigation.PopModalAsync();
        }
    }
}