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
        public List<CategoryItem> CatItem;
        public Expense expense { get; set; }
        public CategoryChoice()
        {
            InitializeComponent();

            CategoryItem ci = new CategoryItem();
            Expense ex = new Expense();
            ex.CategoryName = ci.CatName;
            ex.CategoryIcon = ci.CatIcon;
        }

        protected override void OnAppearing()
        {
            expense = (Expense)BindingContext;

            CategoryItem catitem = new CategoryItem();
            CatItem = new List<CategoryItem>
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

            CategoryIconView.ItemsSource = CatItem;


        }

        private async void CategoryIconView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //var selected = CategoryIconView.SelectedItem;
            //Expense ex = new Expense();
            //ex.CategoryName = selected.ToString();

            //await Navigation.PopModalAsync();


            var expense = (Expense)BindingContext;
            var selected = CategoryIconView.SelectedItem;
            await Navigation.PushModalAsync(new NavigationPage(new AddEditExpensePage
            { BindingContext = expense }));
        }
    }
}