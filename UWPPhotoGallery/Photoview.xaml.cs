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
    public sealed partial class Photoview : Page
    {
        public Photo currentphoto = PhotoManager.currentPhoto;
        public ObservableCollection<Photo> photos;
        public Photoview()
        {
            this.InitializeComponent();
            photos = new ObservableCollection<Photo>();
            
            this.Loaded += Photoview_Loaded;
        }

        private void Photoview_Loaded(object sender, RoutedEventArgs e)
        {
            //set data for the flipview
            //load photos asynchronously
            //this page will either load the full collection or the selected album from 
            try
            {
                LoadingRing.IsActive = true;
                if (PhotoManager.Context == null)
                {
                    PhotoManager.GetAllPhotos(photos);


                }
                else
                {
                    //we need to get the selected photos
                    PhotoManager.GetSelectedPhotos(photos);

                }
    }
            finally
            {
                LoadingRing.IsActive = false;
            }
            //PhotoFlipView.Visibility = Visibility.Visible;
            PhotoFlipView.SelectedItem = PhotoManager.currentPhoto;

        }

        private void PhotoFlipView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //just try changing the image source here
            
        }
    }
}
