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
        private static bool albumsinitialised = false;
        private static bool isImageLoaded = false;
        public static string TitleText { get; set; }
       
        public static void GetSelectedPhotos(ObservableCollection<Photo> selphotos)
        {
            selphotos.Clear();
            if (selectedPhotos != null)
            {
                selectedPhotos.ForEach(photo => selphotos.Add(photo));
            }
        }
       
        public static async Task<bool> GetPhotosAsync(ObservableCollection<Photo> photos)
        {
            bool retval = true;
            //how to get absolute path
            photos.Clear();
     
            //PhotoCollection.Clear();
            if (!initialised)
            {
                try
                {
                    StorageFolder picturesFolder = KnownFolders.PicturesLibrary;
                    IReadOnlyList<StorageFile> fileList = await picturesFolder.GetFilesAsync();

                    foreach (StorageFile file in fileList)
                    {
                        Photo newphoto = new Photo { imageFile = file.Path };

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

                    //add an album file to this folder

                    initialised = true;
                }
                catch (Exception e)
                {
                    initialised = false;
                }
            }
            PhotoCollection.ForEach(elem => photos.Add(elem));
            return retval;
        }

        public static async Task AddNewPhoto()
        {
            //
            var picker = new Windows.Storage.Pickers.FileOpenPicker();
            picker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;
            picker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.ComputerFolder;
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".jpeg");
            picker.FileTypeFilter.Add(".png");
            var files = await picker.PickMultipleFilesAsync();
            if (files != null)
            {
                foreach(StorageFile selfile in files)
                {
                    StorageFile file = await selfile.CopyAsync(KnownFolders.PicturesLibrary);
                    //this will make sure the file remains there for future too...
                    Photo newphoto = new Photo { imageFile = file.Path };

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
            }

        }
        public static async Task GetAlbums(ObservableCollection<Album> albumslist)
        {
            if (!albumsinitialised)
            {
                //also initialise the albums folder n keep them so that it is easy to display whenever the user asks for it
                StorageFolder storageFolder =
                    Windows.Storage.ApplicationData.Current.LocalFolder;
                StorageFolder albumsFolder;
                try
                {
                    albumsFolder = await storageFolder.GetFolderAsync("Albums");
                    //albums are there start getting them one by one and creat
                    var files = await albumsFolder.GetFilesAsync();
                    foreach (StorageFile file in files)
                    {
                        //get the index of the wextensiona nd trim it to get the album name only
                        //int index = file.Name.IndexOf(".", file.Name.Length - 1);
                        string albumname = Path.GetFileNameWithoutExtension(file.Name);
                       
                        //for each existing album - create an album record and load it in memory
                        // now for read the file and update the coverphoto and create an album record
                        string coverfile;

                        using (StreamReader sr = new StreamReader(file.Path))
                        {
                            //First write the path of the coverphotoimage
                            coverfile = sr.ReadLine();

                            sr.Close();

                        }
                        //load the thumbnailasync
                        //load the file with the path
                        StorageFolder picturesFolder = KnownFolders.PicturesLibrary;
                        StorageFile picfile = await picturesFolder.GetFileAsync(Path.GetFileName(coverfile));
                        
                        var thumbnail = await picfile.GetThumbnailAsync(ThumbnailMode.SingleItem);
                        BitmapImage thumbnailimage = new BitmapImage();
                        await thumbnailimage.SetSourceAsync(thumbnail);


                        Album newalbum = new Album { Name = albumname, CoverPhotoFile = coverfile, CoverImage = thumbnailimage };


                        albums.Add(newalbum);
                    }



                }
                catch (FileNotFoundException)
                {
                    //albumsFolder = await storageFolder.CreateFolderAsync("Albums");
                    //just create this folder and keep it ready so that albums can be added later

                }
                albumsinitialised = true;
            }

           


            albums.ForEach(album => albumslist.Add(album));

        }

        public static void GetAllPhotos(ObservableCollection<Photo> allphotos)
        {
            PhotoCollection.ForEach(photo => allphotos.Add(photo));
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
            //open the file and get the contents
            string path = $"{Windows.Storage.ApplicationData.Current.LocalFolder.Path}\\Albums\\{SelectedAlbum.Name}.txt";

            StreamReader sr = new StreamReader(path);
            string line;
            //Read the first line of text
            line = sr.ReadLine();
            //Continue to read until you reach end of file
            line = sr.ReadLine();
            while (line != null)
            {
                //first line is the coverphoto for the album
                //write the lie to console window
                
                //Read the next line
                
                //this is the first selected photo
                //check if any of the photocollection matches with this, if so add it tot he list
                foreach(Photo ph in PhotoCollection)
                {
                    if (line == ph.imageFile)
                    {
                        //add it to the selected photos/observable collection
                        photos.Add(ph);
                    }
                }
                line = sr.ReadLine();
            }
            //close the file
            sr.Close();
            
            //var albumphotos = PhotoCollection.Where(item => item.AlbumName == SelectedAlbum.Name).ToList();
            //albumphotos.ForEach(photo => photos.Add(photo));
        }

        public static async Task LoadAllPhotosAsync(ObservableCollection<Photo> photos)
        {
            photos.Clear();
            if (!isImageLoaded)
            {
                foreach (Photo ph in PhotoCollection)
                {
                    IRandomAccessStream fileStream = await ph.storagefile.OpenAsync(FileAccessMode.Read);
                    BitmapImage bitmapImage = new BitmapImage();
                    bitmapImage.SetSource(fileStream);
                    ph.image = bitmapImage;
                }
                isImageLoaded = true;
            }
            GetAllPhotos(photos);

        }

        public static async void AddAlbum(Album album)
        {
            //check if the album folder exists
            StorageFolder storageFolder =
                    Windows.Storage.ApplicationData.Current.LocalFolder;
            StorageFolder albumsFolder;
            try
            {
                albumsFolder = await storageFolder.GetFolderAsync("Albums");
            }
            catch (FileNotFoundException)
            {
                albumsFolder = albumsFolder = await storageFolder.CreateFolderAsync("Albums");

            }
            
            //add an album file to this folder
            string path = $"{albumsFolder.Path}//{album.Name}.txt";
            
            using (StreamWriter sw = new StreamWriter (path))
            {
                //First write the path of the coverphotoimage
                sw.WriteLine(album.CoverPhotoFile);
                foreach(Photo ph in selectedPhotos)
                {
                    sw.WriteLine(ph.imageFile);
                }
                sw.Close();

            }
            albums.Add(album);

        }
       

    }
}
