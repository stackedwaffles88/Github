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
    public sealed partial class Photoview : Page
    {
        public Photo currentphoto = PhotoManager.currentPhoto;
        public ObservableCollection<Photo> photos;
        public Photoview()
        {
            this.InitializeComponent();
            photos = new ObservableCollection<Photo>();
            if (PhotoManager.Context == null)
            {
                PhotoManager.GetPhotosAsync(photos);

            }
            else
            {
                //we need to get the selected photos
                PhotoManager.GetSelectedPhotos(photos);

            }
            PhotoFlipView.SelectedItem = PhotoManager.currentPhoto;
            this.Loaded += Photoview_Loaded;
        }

        private void Photoview_Loaded(object sender, RoutedEventArgs e)
        {
            //set data for the flipview
            
        }

        
    }
}
