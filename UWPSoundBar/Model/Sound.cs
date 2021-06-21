using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UWPSoundBar.Model
{
    public enum SoundCategory
    {
        Animals,
        Cartoons,
        Taunts,
        Warnings
    }
    public class Sound
    {
        public string Name { get; set; }
        public SoundCategory category { get; set; }
        public string AudioFile { get; set; }
        public string ImageFile { get; set; }
        public Sound( string na, SoundCategory cat)
        {
            Name = na;
            category = cat;
            AudioFile = $"Assets/Audio/{category}/{Name}.wav";
            ImageFile = $"Assets/Images/{category}/{Name}.png";

        }

    }
}
