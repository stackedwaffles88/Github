using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWPSoundBar.Model;

namespace UWPSoundBar
{
    public static class SoundManager
    {
        
        public static void GetAllSounds(ObservableCollection<Sound> sounds)
        {
            //need to create all sounds and full it;
            var allsounds = CreateAllSound();
            sounds.Clear();
            
            allsounds.ForEach(sound => sounds.Add(sound));
            
        }
        public static void  GetFilteredSounds(ObservableCollection<Sound> sounds, SoundCategory cat)
        {
            sounds.Clear();
            var allsounds = CreateAllSound();
            var filteredsounds = allsounds.Where(sound => sound.category == cat).ToList();
            filteredsounds.ForEach(elem => sounds.Add(elem)) ;
        }
        private static List<Sound> CreateAllSound()
        {
            var allsounds = new List<Sound>();
            allsounds.Add(new Sound("Cat", SoundCategory.Animals));
            allsounds.Add(new Sound("Cow", SoundCategory.Animals));
            allsounds.Add(new Sound("Gun", SoundCategory.Cartoons));
            allsounds.Add(new Sound("Spring", SoundCategory.Cartoons));
            allsounds.Add(new Sound("Clock", SoundCategory.Taunts));
            allsounds.Add(new Sound("LOL", SoundCategory.Taunts));
            allsounds.Add(new Sound("Ship", SoundCategory.Warnings));
            allsounds.Add(new Sound("Siren", SoundCategory.Warnings));
            return allsounds;  
        }
    }
}
