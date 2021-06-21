using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace FitnessApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private ObservableCollection<NavigationItem> menu = new ObservableCollection<NavigationItem>();
        public MainPage()
        {
            this.InitializeComponent();
            this.Loaded += MainPage_Loaded;
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            //once main page loaded, 
            listmenu.SelectedIndex = 0;
            //By defualt always display measuremetns  page
            var naviitem = listmenu.SelectedItem as NavigationItem;

            FitnesssplitviewContent.Navigate(naviitem.PageLink);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            menu.Clear();
            menu.Add(new NavigationItem { PageLink = typeof(MeasurementsPage), MenuText = typeof(MeasurementsPage).Name, MenuIcon = "\xE890" });
            menu.Add(new NavigationItem { PageLink = typeof(TrendsPage), MenuText = typeof(TrendsPage).Name, MenuIcon = "\xE908" });
            menu.Add(new NavigationItem { PageLink = typeof(ProfilePage), MenuText = typeof(ProfilePage).Name, MenuIcon = "\xE77B" });
            
        }
        private void listmenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
            //once the selection is changed display respective page
        {
            var naviitem = listmenu.SelectedItem as NavigationItem;

            FitnesssplitviewContent.Navigate(naviitem.PageLink);

        }

        private void FitnesssplitviewContent_Navigated(object sender, NavigationEventArgs e)
        {

        }
        

    }
}
