using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace Avoid
{
    class AudioManager
    {
        Dictionary<String, SoundPlayer> sounds;
        public AudioManager()
        {
            sounds = new Dictionary<string, SoundPlayer>();
            sounds["background"] = new SoundPlayer("background.wav");
        }

        public SoundPlayer this[String index]{
            get{
                return sounds[index];
            }
        }
    }
}
