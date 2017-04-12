using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using CSCore;
using CSCore.SoundOut;
using CSCore.Codecs;
using CSCore.Streams;

namespace Test
{
    class SoundManager
    {
        //constructor
        public SoundManager()
        {
            //song_dict = new Dictionary<string, string>() { { "Dad", "../../Sounds/sayagain-loop1.wav" },
            //                                               { "Mom","../../Sounds/sayagain-loop2.wav" },
            //                                               { "Alex", "" } };
            //sfx_dict = new Dictionary<string, SoundBuffer>() { { "chatter", new SoundBuffer("../../Sounds/chatter.wav") },
            //                                                  { "button", new SoundBuffer("../../Sounds/button.wav")} };
            current = "None";
            next = "None";
            //sound = new Sound();
            soundSource = GetSoundSource();
            //SoundOut implementation which plays the sound
            soundOut = GetSoundOut();
        }

        //fields
        //Sound sound;
        //Music song;
        IWaveSource soundSource;
        ISoundOut soundOut;
        private String current;
        private String next;
        //public Dictionary<String, String> song_dict;
        //public Dictionary<String, SoundBuffer> sfx_dict;
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
        }

        public void playMusic(string musicname)
        {
            PlayASound();
        }

        public void transitionSong(String musicName)
        {

        }

        public void soundUpdate(bool soundToggle)
        {

        }

        [STAThread]
        private void PlayASound()
        {
            //Tell the SoundOut which sound it has to play
            soundOut.Initialize(soundSource);
            //Play the sound
            soundOut.Play();

            //Thread.Sleep(2000);

            //Stop the playback
            //soundOut.Stop();
        }

        public static void ClearCurrentConsoleLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, Console.CursorTop);
            for (int i = 0; i < Console.WindowWidth; i++)
                Console.Write(" ");
            Console.SetCursorPosition(0, currentLineCursor);
        }
    

        private ISoundOut GetSoundOut()
        {
            if (WasapiOut.IsSupportedOnCurrentPlatform)
                return new WasapiOut();
            else
                return new DirectSoundOut();
        }

        private IWaveSource GetSoundSource()
        {
            //return any source ... in this example, we'll just play a mp3 file
            return CodecFactory.Instance.GetCodec(@"../../Sounds/sayagain-loop2.wav");
        }
    }
}
