using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using UWPPhotoGallery.Model;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
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
    public sealed partial class CollectionsPage : Page
    {
        //public ObservableCollection<Photo> photos = new ObservableCollection<Photo>();
        public ObservableCollection<Photo> photos { get; set; }

       
        

        public CollectionsPage()
        {
            this.InitializeComponent();//get files to load
            photos = new ObservableCollection<Photo>();
            PhotoManager.GetPhotosAsync(photos);
            //this.Loaded += CollectionsPage_Loaded;
          

        }

        private async void CollectionsPage_Loaded(object sender, RoutedEventArgs e)
        {
            
           
            
        }

        private void PhotoGrid_Tapped(object sender, TappedRoutedEventArgs e)
        {
         
            Photo selectedphoto = PhotoGrid.SelectedItem as Photo;
            PhotoManager.currentPhoto = selectedphoto;
            this.Frame.Navigate(typeof(Photoview));

        }
    }
}
