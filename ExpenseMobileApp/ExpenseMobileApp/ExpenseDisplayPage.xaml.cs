using ExpenseMobileApp.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using ExpenseMobileApp.ViewModel;

namespace ExpenseMobileApp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExpenseDisplayPage : ContentPage
    {
        private int currentMonth ;
        private int currentYear;
        private enum Months
        {
            January,
            February,
            March,
            April,
            May,
            June,
            July,
            August,
            September,
            October,
            November,
            December
        };
        public int balance { get; set; }
        public ExpenseDisplayPage()
        {
            InitializeComponent();
            //load month and year picker
            MonthPicker.ItemsSource = Enum.GetNames(typeof(Months)).ToList();
            int currentyear = DateTime.Now.Year;
            
            YearPicker.ItemsSource = new List<int> { currentyear, currentyear - 1, currentyear - 2 };
        }
        protected override void OnAppearing()
        {
            var  context = (string)BindingContext;
            string[] info = context.Split('.');
            currentMonth = int.Parse(info[0]);
            currentYear = int.Parse(info[1]);

            MonthlyExpense monthlyExpense = new MonthlyExpense();

            //get all expenses and set data context
            ExpenseManager.GetMonthlyExpenses(currentMonth, currentYear, ref monthlyExpense);
            if (monthlyExpense.Month >0)
            {
                ExpenseListview.ItemsSource = monthlyExpense.ExpenseList.OrderByDescending(x => x.Date);
                MonthBudget.Text = monthlyExpense.Budget.ToString("C", CultureInfo.CurrentCulture);
               
                balance = monthlyExpense.Balance;
                BalanceDisplay.Text = balance.ToString("C", CultureInfo.CurrentCulture);
                EditDeleteStack.IsVisible = false;
                //neee to change the label text
                

            }
            MonthPicker.SelectedIndex = currentMonth - 1;
            YearPicker.SelectedItem = currentYear;

            MonthPicker.SelectedIndexChanged += MonthPicker_SelectedIndexChanged;
            YearPicker.SelectedIndexChanged += YearPicker_SelectedIndexChanged;
            if (monthlyExpense.Budget <= 0)
            {
                //disable + button
                AddExpenseButton.IsVisible = false;
            }
            EditDeleteStack.IsVisible = false;

            EditBudget.Source = $"{Environment.CurrentDirectory}/Resources/Editbutton.png";

            //if you are displaying for current month then disable the > button
            if (currentMonth  == DateTime.Now.Month && currentYear == DateTime.Now.Year)
            {
                NextMonthBtn.IsEnabled = false;
            }
            else
            {
                NextMonthBtn.IsEnabled = true;
            }
            ExpenseListview.SelectedItem = null;
        }

       

        private async void OnAddExpenseClicked(object sender, EventArgs e)
        {
            await Navigation.PushModalAsync(new NavigationPage(new AddEditExpensePage { BindingContext = new Expense { Date = new DateTime(currentYear, currentMonth, 1)} }));

        }

        private void ExpenseListview_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EditDeleteStack.IsVisible = true;
            
        }

        private async void DeleteExpense_Clicked(object sender, EventArgs e)
        {
           // = null;
            var expense = (Expense)ExpenseListview.SelectedItem;
            string yearMonth = $"{currentMonth}.{currentYear}";
            ExpenseManager.DeleteMonthlyExpense(currentMonth, currentYear, expense);
            await Navigation.PushModalAsync(new NavigationPage(new ExpenseDisplayPage { BindingContext = yearMonth }));

        }

        private async void EditExpense_Clicked(object sender, EventArgs e)
        {
            //ExpenseListview.SelectedItem = null;
            var expense = (Expense)ExpenseListview.SelectedItem;
            
            await Navigation.PushModalAsync(new NavigationPage(new AddEditExpensePage
            { BindingContext = expense  }));
        }

        private void CancelSelection_Clicked(object sender, EventArgs e)
        {
            ExpenseListview.SelectedItem = null;
            EditDeleteStack.IsVisible = false;
        }

        private async void PreviousMonthBtn_Clicked(object sender, EventArgs e)
        {
            string yearMonth;
            if (currentMonth != 12)
            {
                yearMonth = $"{currentMonth - 1}.{currentYear}";
            }
            else
            {
                yearMonth = $"1.{currentYear - 1}";
            }
            await Navigation.PushModalAsync(new NavigationPage(new ExpenseDisplayPage{ BindingContext = yearMonth }));
        }

        private async void NextMonthBtn_Clicked(object sender, EventArgs e)
        {
            string yearMonth;
            if (currentMonth != 12)
            {
                yearMonth = $"{currentMonth + 1}.{currentYear}";
            }
            else
            {
                yearMonth = $"1.{currentYear + 1}";
            }
            await Navigation.PushModalAsync(new NavigationPage(new ExpenseDisplayPage { BindingContext = yearMonth }));
        }

        private async void MonthPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedmonth = MonthPicker.SelectedIndex +1 ;//0 based index
            var selectedYear = (int)YearPicker.SelectedItem;
            string yearMonth = $"{selectedmonth}.{selectedYear}";
            
            await Navigation.PushModalAsync(new NavigationPage(new ExpenseDisplayPage { BindingContext = yearMonth }));
        }

        private async void YearPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedmonth = MonthPicker.SelectedIndex + 1;//0 based index
            var selectedYear = (int)YearPicker.SelectedItem;
            string yearMonth = $"{selectedmonth}.{selectedYear}";

            await Navigation.PushModalAsync(new NavigationPage(new ExpenseDisplayPage { BindingContext = yearMonth }));

        }

        private async void EditBudget_Clicked(object sender, EventArgs e)
        {
            //move on to set budget page with current month and year

            var selectedmonth = MonthPicker.SelectedIndex + 1;//0 based index
            var selectedYear = (int)YearPicker.SelectedItem;
            string yearMonth = $"{selectedmonth}.{selectedYear}";
            await Navigation.PushModalAsync(new NavigationPage(new SetBudgetPage { BindingContext = yearMonth }));
        }

        private async void ViewExpensesInPie_Clicked(object sender, EventArgs e)
        {
            MonthlyExpense monthlyExpense = new MonthlyExpense();

            //get all expenses and set data context
            ExpenseManager.GetMonthlyExpenses(currentMonth, currentYear, ref monthlyExpense);

            Dictionary<string, int> ExpensesbyCategory = new Dictionary<string, int>();
            foreach (Expense item in monthlyExpense.ExpenseList)
            {
                if (ExpensesbyCategory.ContainsKey(item.CategoryName))
                {
                    int existingAmount = ExpensesbyCategory[item.CategoryName];
                    int newAmount = existingAmount + item.Amount;
                    ExpensesbyCategory.Remove(item.CategoryName);
                    ExpensesbyCategory.Add(item.CategoryName, newAmount);
                }
                else
                {
                    ExpensesbyCategory.Add(item.CategoryName, item.Amount);
                }
            }

            await Navigation.PushModalAsync(new NavigationPage 
              (new PieChrtView { BindingContext = new PieChartViewerImpl(ExpensesbyCategory) }));
        
        }
    }
}