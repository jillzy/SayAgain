using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Audio;

namespace Test
{
    class SoundManager
    {
        //constructor
        public SoundManager()
        {
            song_dict = new Dictionary<string, string>() { { "Dad", "../../Sounds/sayagain-loop1.wav" },
                                                           { "Mom","../../Sounds/sayagain-loop2.wav" },
                                                           { "Alex", "" } };
            sfx_dict = new Dictionary<string, SoundBuffer>() { { "chatter", new SoundBuffer("../../Sounds/chatter.wav") },
                                                               { "button", new SoundBuffer("../../Sounds/button.wav")} };
            current = "None";
            next = "None";
            sound = new Sound();
        }

        //fields
        Sound sound;
        Music song;
        private String current;
        private String next;
        public Dictionary<String, String> song_dict;
        public Dictionary<String, SoundBuffer> sfx_dict;
        public bool soundpause = false;

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
            //load the click sound object
            if (!soundpause) {
                sound.SoundBuffer = sfx_dict[soundName];
                sound.Play();
            }
           return;
        }

        public void playMusic(string musicname)
        {
            if (current != musicname)
            {
                if (current != "None" && song.Status == SoundStatus.Playing)
                {
                    song.Stop();
                }
                song = new Music(song_dict[musicname]);
                song.Volume = 0;
                song.Play();
                song.Loop = true;
                current = musicname;
            } 

            return;
        }

        public void transitionSong(String musicName)
        {
            song.Loop = false;
            next = musicName;
        }

        public void soundUpdate(bool soundToggle)
        {
            if (!soundToggle && !this.soundpause)
            {
                this.song.Pause();
                soundpause = true;
            }
            else if (soundToggle && this.soundpause)
            {
                this.song.Play();
                soundpause = false;
            }
            else if (song.Status == SoundStatus.Stopped && !soundpause)
            {
                playMusic(next);
            }
        }
    }
}
