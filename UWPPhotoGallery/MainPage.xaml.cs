using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UWPPhotoGallery.Model;
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

namespace UWPPhotoGallery
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private ObservableCollection<ListItem> menu = new ObservableCollection<ListItem>();
        public MainPage()
        {
            this.InitializeComponent();
            this.Loaded += MainPage_Loaded;
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            PhotoGallerySplitviewContent.Navigate(typeof(CollectionsPage));
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            menu.Clear();
            menu.Add(new ListItem { PageLink = typeof(CollectionsPage), MenuText = typeof(CollectionsPage).Name, MenuIcon = "Assets/Icons/GalleryMenuIcon.png" });
            menu.Add(new ListItem { PageLink = typeof(AlbumsPage), MenuText = typeof(AlbumsPage).Name, MenuIcon = "Assets/Icons/AlbumMenuIcon.png" });

        }

        private void listmenu_ItemClick(object sender, ItemClickEventArgs e)
        {
            //if(listmenu.selection)
            var naviitem = e.ClickedItem as ListItem;

            PhotoGallerySplitviewContent.Navigate(naviitem.PageLink);
        }
    }
}
