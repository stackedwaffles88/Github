using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UWPPhotoGallery.Model;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace UWPPhotoGallery
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddAlbumPage : Page
    {
        public ObservableCollection<Photo> selectedPhotos;
        public AddAlbumPage()
        {
            this.InitializeComponent();
            selectedPhotos = new ObservableCollection<Photo>();
            PhotoManager.GetSelectedPhotos(selectedPhotos);
        }

        

       
        private void DoneButton_Click(object sender, RoutedEventArgs e)
        {
            Photo Coverphoto = (Photo)SelectedPhotoGrid.SelectedItem;
            if(Coverphoto == null)
            {
                //pick any image from the seelcted photos
                Coverphoto = selectedPhotos[0];
            }
            //add this album
            Album newalbum = new Album
            {
                CoverPhotoFile = Coverphoto.imageFile,
                Name = AlbumnameTextbox.Text,
                CoverImage = (BitmapImage)Coverphoto.Thumbnail
                
            };
            
          
            PhotoManager.albums.Add(newalbum);
            PhotoManager.SetAlbumNameinSelectedPhotos(AlbumnameTextbox.Text);
            this.Frame.Navigate(typeof(AlbumsPage));

        }
    }
}
