using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UWPSoundBar.Model;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UWPSoundBar
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private ObservableCollection<Sound> sounds;
        private List<MenuItem> MenuItems;
        public MainPage()
        {
            this.InitializeComponent();
            sounds = new ObservableCollection<Sound>();
            SoundManager.GetAllSounds(sounds);
            //Create menuitems to load into listview
            MenuItems = new List<MenuItem> {
                new MenuItem
                {
                    Category = SoundCategory.Animals,
                    IconFile = "/Assets/Icons/animals.png"
                },
                new MenuItem
                {
                    Category = SoundCategory.Cartoons,
                    IconFile = "/Assets/Icons/cartoon.png",

                },
                new MenuItem
                {
                    Category = SoundCategory.Taunts,
                    IconFile = "/Assets/Icons/taunt.png",

                },
                new MenuItem
                {
                    Category = SoundCategory.Warnings,
                    IconFile = "/Assets/Icons/warning.png",

                }
                };

        }

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            //display listview and the pane;
            SoundCategorySplitview.IsPaneOpen = !SoundCategorySplitview.IsPaneOpen;
            
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            SoundManager.GetAllSounds(sounds);
            BackButton.Visibility = Visibility.Collapsed;
            CategoryTextBlock.Text = "All Sounds";
        }

        private void SoundCategoryListview_ItemClick(object sender, ItemClickEventArgs e)
        {
            var menu = (MenuItem)e.ClickedItem;
            SoundManager.GetFilteredSounds(sounds, menu.Category);
            CategoryTextBlock.Text = menu.Category.ToString();
            BackButton.Visibility = Visibility.Visible;
        }

        private void SoundGrid_ItemClick(object sender, ItemClickEventArgs e)
        {
            var sound = (Sound)e.ClickedItem;
            SoundPlayer.Source = new Uri(this.BaseUri, sound.AudioFile);
            //SoundPlayer.Source = new Uri((.AudioFile);
        }
    }
}
