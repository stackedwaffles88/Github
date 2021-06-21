using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWPPhotoGallery.Model;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;
using static System.Net.Mime.MediaTypeNames;

namespace UWPPhotoGallery
{
    public static class PhotoManager
    {
        public static Photo currentPhoto;
        public static string Context;
        public static List<Photo> selectedPhotos;
        public static List<Photo> PhotoCollection = new List<Photo>();
        public static List<Album> albums = new List<Album>();
        public static Album SelectedAlbum = new Album();
        private static bool initialised = false;
        
       
        public static void GetSelectedPhotos(ObservableCollection<Photo> selphotos)
        {
            selphotos.Clear();
            if (selectedPhotos != null)
            {
                selectedPhotos.ForEach(photo => selphotos.Add(photo));
            }
        }
       
        public static async Task GetPhotosAsync(ObservableCollection<Photo> photos)
        {

            //how to get absolute path
            photos.Clear();
            //PhotoCollection.Clear();
            if (!initialised)
            {
                
                StorageFolder picturesFolder = KnownFolders.PicturesLibrary;
                IReadOnlyList<StorageFile> fileList = await picturesFolder.GetFilesAsync();

                foreach (StorageFile file in fileList)
                {
                    Photo newphoto = new Photo{ imageFile = file.Path };
                                       
                    IRandomAccessStream fileStream = await file.OpenAsync(FileAccessMode.Read);
                    BitmapImage bitmapImage = new BitmapImage();
                    bitmapImage.SetSource(fileStream);
                    
                    newphoto.image = bitmapImage;
                
                    
                    var thumbnail = await file.GetThumbnailAsync(ThumbnailMode.SingleItem);

                    BitmapImage thumbnailimage = new BitmapImage();
                    await thumbnailimage.SetSourceAsync(thumbnail);
                    newphoto.Thumbnail = thumbnailimage;
                    newphoto.storagefile = file;
                    PhotoCollection.Add(newphoto);
                }
                initialised = true;
            }
            PhotoCollection.ForEach(elem => photos.Add(elem));
            return;
        }

        public static void GetAlbums(ObservableCollection<Album> albumslist)
        {
            albums.ForEach(album => albumslist.Add(album));

        }

        public static void SetAlbumNameinSelectedPhotos(string albumName)
        {
            //browse through the collections and set the ablum name property to the corresponding category
            //foreach(Photo photo in selectedphotos)
            foreach(Photo ph in selectedPhotos)
            {
                
                //run the loop the get the selectedphoto from the 
                foreach (Photo sel in PhotoCollection)
                {
                    if(ph.imageFile == sel.imageFile)
                    {
                        //set the album name to true
                        sel.AlbumName = albumName;
                    }
                }
            }
        }

        public static void GetPhotosForCurrentAlbumName(ObservableCollection<Photo> photos)
        {
            photos.Clear();
            var albumphotos = PhotoCollection.Where(item => item.AlbumName == SelectedAlbum.Name).ToList();
            albumphotos.ForEach(photo => photos.Add(photo));
        }

    }
}
