using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace UWPPhotoGallery.Model
{
    public class ImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            BitmapImage image = null;

            if (value != null)
            {
                if (value.GetType() != typeof(BitmapImage))
                {
                    throw new ArgumentException("Expected a thumbnail");
                }
                if (targetType != typeof(ImageSource))
                {
                    throw new ArgumentException("What are you trying to convert to here?");
                }
                image = new BitmapImage();
                //image.sou
                //((BitmapImage)value).source
            }
            return (image);
        }
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
    
}
