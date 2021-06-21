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


// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace UWPPhotoGallery
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AlbumsPage : Page
    {
        public ObservableCollection<Album> albums;
        public AlbumsPage()
        {
            this.InitializeComponent();
            albums = new ObservableCollection<Album>();
            //update this
            PhotoManager.GetAlbums(albums);

        }

        private void PhotoGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void PhotoGrid_Tapped(object sender, TappedRoutedEventArgs e)
        {
            //now display the collection from this list.
            //set current album and move to the collections page to display the list
            //get the selected item first
            var album = (Album)PhotoGrid.SelectedItem;
            PhotoManager.SelectedAlbum = album;
            // more on to the next page
            this.Frame.Navigate(typeof(SelectedAlbumPage));
        }

        private void AddNewAlbumButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            //go to the other page
            this.Frame.Navigate(typeof(AddPhotostoAlbumPage));


        }
    }
}
