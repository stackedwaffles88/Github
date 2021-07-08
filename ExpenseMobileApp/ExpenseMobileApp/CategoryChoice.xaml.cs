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
    public partial class CategoryChoice : ContentPage
    {

        public List<CategoryItem> CatItems;
        public Expense expense { get; set; }


        public CategoryChoice()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            expense = (Expense)BindingContext;

            CategoryItem catitem = new CategoryItem();
            CatItems = new List<CategoryItem>
            {
                new CategoryItem
                {
                    CatName = "Bills",
                    CatIcon = "bill.png"
                },
                new CategoryItem
                {
                    CatName = "Dining",
                    CatIcon = "dining.png"
                },
                new CategoryItem
                {
                    CatName = "Entertainment",
                    CatIcon = "entertainment.png"
                },
                new CategoryItem
                {
                    CatName = "Essentials",
                    CatIcon = "essentials.png"
                },
                new CategoryItem
                {
                    CatName = "Retail",
                    CatIcon = "retail.png"
                },
            };

            CategoryIconView.ItemsSource = CatItems;


        }

        private async void CategoryIconView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var selected = CategoryIconView.SelectedItem as CategoryItem;
            expense.CategoryName = selected.CatName;

            await Navigation.PushModalAsync(new NavigationPage(new AddEditExpensePage
            { BindingContext = expense }));
        }

        private void Entry_Focused(object sender, FocusEventArgs e)
        {

        }

    }
}