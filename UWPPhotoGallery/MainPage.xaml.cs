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
        public string TitleTextboxText;
        public MainPage()
        {
            this.InitializeComponent();
            this.Loaded += MainPage_Loaded;
            
        }

        private void MainPage_Loaded(object sender, RoutedEventArgs e)

        {
            PhotoManager.TitleText = "All Photos";
            TitleTextboxText = PhotoManager.TitleText;
            this.MainFrame.Navigate(typeof(CollectionsPage));
            //PhotoGallerySplitviewContent.Navigate();
        }

        /*protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            menu.Clear();
            menu.Add(new ListItem { PageLink = typeof(CollectionsPage), MenuText = typeof(CollectionsPage).Name, MenuIcon = "Assets/Icons/GalleryMenuIcon.png" });
            menu.Add(new ListItem { PageLink = typeof(AlbumsPage), MenuText = typeof(AlbumsPage).Name, MenuIcon = "Assets/Icons/AlbumMenuIcon.png" });

        }

        private void listmenu_ItemClick(object sender, ItemClickEventArgs e)
        {
            //if(listmenu.selection)
            var naviitem = e.ClickedItem as ListItem;
            //based on the listitem selection
            PhotoManager.Context = naviitem.MenuText;

            //PhotoGallerySplitviewContent.Navigate(naviitem.PageLink);
        }
        */
        private void BackItm_Tapped(object sender, TappedRoutedEventArgs e)
        {
            PhotoManager.Context = "CollectionsPage";
            this.MainFrame.Navigate(typeof(CollectionsPage));
        }

        private void AlbumsNewFlyout_Click(object sender, RoutedEventArgs e)
        {
            //user wants to create new album
            //navigatet o addnew albums page
            this.MainFrame.Navigate(typeof(AddPhotostoAlbumPage));

        }

        private void AlbumsEditFlyout_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EditItm_Tapped(object sender, TappedRoutedEventArgs e)
        {

        }

        private void AlbumsItm_Tapped(object sender, TappedRoutedEventArgs e)
        {
            //navigate to albums page
            this.MainFrame.Navigate((typeof(AlbumsPage)));
        }

        private async void PhotosNewFlyout_Click(object sender, RoutedEventArgs e)
        {
            //simply call photomanager to add the photos - let photo manager add photo and then you can display that in the collections page
            await PhotoManager.AddNewPhoto();
            PhotoManager.Context = "CollectionsPage";
            this.MainFrame.Navigate(typeof(CollectionsPage));


        }
    }
}

