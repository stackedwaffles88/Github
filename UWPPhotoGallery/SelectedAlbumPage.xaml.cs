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
    public sealed partial class SelectedAlbumPage : Page
    {
        public Album currentAlbum;
        public ObservableCollection<Photo> currentPhotos = new ObservableCollection<Photo>();
        public SelectedAlbumPage()
        {
            this.InitializeComponent();
            currentAlbum = PhotoManager.SelectedAlbum;
            PhotoManager.GetPhotosForCurrentAlbumName(currentPhotos);
        }

        private void PhotoImage_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Photo selectedphoto = PhotoGrid.SelectedItem as Photo;
            PhotoManager.currentPhoto = selectedphoto;
            PhotoManager.Context = "Album";
            this.Frame.Navigate(typeof(Photoview));
        }
    }
}
