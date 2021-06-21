using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace UWPPhotoGallery.Model
{
    public class Album
    {
        public string Name { get; set; }
        public string CoverPhotoFile { get; set; }
        public BitmapImage CoverImage { get; set; }
    }
}
