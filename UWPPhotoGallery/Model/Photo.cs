using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace UWPPhotoGallery.Model
{
    public class Photo
    {
        public ImageSource Thumbnail;
        public string AlbumName;
        public string imageFile { get; set; }
        public string test { get; set; }
        public ImageSource  image { get; set; }
        public StorageFile storagefile { get; set; }
        
        public BitmapImage thumbnailbitmap { get; set; }

        public DateTime DateTaken { get; set; }



    }
}
