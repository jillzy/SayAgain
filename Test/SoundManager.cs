using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using SFML.Audio;

namespace Test
{
    class SoundManager
    {
        //constructor
        public SoundManager()
        {
            sound_dict = new Dictionary<string, string>() { { "loop", "../../Sounds/jazz-loop.wav" },
                                                          { "button","../../Sounds/button.wav"} };
            chatter_dict = new Dictionary<string, List<SoundBuffer>>() { { "dad",new List<SoundBuffer>() {new SoundBuffer( "../../Sounds/dad/dad1.wav"),
                                                                                                 new SoundBuffer("../../Sounds/dad/dad2.wav"),
                                                                                                new SoundBuffer( "../../Sounds/dad/dad3.wav"),
                                                                                                 new SoundBuffer("../../Sounds/dad/dad4.wav")} },
                                                                    {"alex",new List<SoundBuffer>() {new SoundBuffer( "../../Sounds/alex/alex1.wav"),
                                                                                                 new SoundBuffer("../../Sounds/alex/alex2.wav"),
                                                                                                 new SoundBuffer("../../Sounds/alex/alex3.wav"),
                                                                                                 new SoundBuffer("../../Sounds/alex/alex4.wav")} },
                                                                    { "mom",new List<SoundBuffer>() {new SoundBuffer( "../../Sounds/mom/mom1.wav"),
                                                                                                 new SoundBuffer("../../Sounds/mom/mom2.wav"),
                                                                                                new SoundBuffer( "../../Sounds/mom/mom3.wav")} }};
            Step = 1;
            next = "None";
        }

        Music music;

        private String next;
        private Dictionary<String, String> sound_dict;
        public bool soundpause = false;
        private int Step;
        private List<string> music_blocks;
        private Dictionary<string, List<SoundBuffer>> chatter_dict;
        private Sound chatter;
        private Sound SFX;

        public bool getSoundPause()
        {
            return soundpause;
        }
        public void setSoundPause(bool b)
        {
            soundpause = b;
        }

        //methods

        public void playSFX(String soundName)
        {
            SoundBuffer buffer = new SoundBuffer(sound_dict[soundName]);
            SFX = new Sound(buffer);
            SFX.Play();
        }

        public void playChatter(String person)
        {
            Random r = new Random();
            chatter = new Sound(chatter_dict[person][r.Next(chatter_dict[person].Count)]);
            chatter.Play();
        }

        public void init_music()
        {
            music_blocks = new List<string> { "loop", "loop", "loop","loop" };
            Step = 0;
            music = new Music(sound_dict[music_blocks[Step]]);
            music.Play();
        }
        public void update_music()
        {
            if (music.Status == SoundStatus.Stopped )
            {
                if (++Step > 3) Step = 0;

                music = new Music(sound_dict[music_blocks[Step]]);
                music.Play();
            }

        }
    }
}
