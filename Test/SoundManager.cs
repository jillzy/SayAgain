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
            sound_dict = new Dictionary<string, string>() { { "Dad", "../../Sounds/sayagain-loop1.wav" },
                                                          { "Mom","../../Sounds/sayagain-loop2.wav" },
                                                          { "Alex", "" },
                                                          { "chatter","../../Sounds/chatter.wav" },
                                                              { "button","../../Sounds/button.wav"} };
            //sfx_dict = new Dictionary<string, SoundBuffer>() { { "chatter", new SoundBuffer("../../Sounds/chatter.wav") },
            //                                                  { "button", new SoundBuffer("../../Sounds/button.wav")} };
            current = "None";
            next = "None";
            //sound = new Sound();
            //soundSource = GetSoundSource();
            //SoundOut implementation which plays the sound
            soundOut = GetSoundOut();
            soundOut2 = GetSoundOut();
        }

        //fields
        //Sound sound;
        //Music song;
        //IWaveSource soundSource;
        ISoundOut soundOut;
        ISoundOut soundOut2;
        private String current;
        private String next;
        public Dictionary<String, String> sound_dict;
        //public Dictionary<String, IWaveSource> sfx_dict;
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
            IWaveSource soundSource = GetSoundSource(soundName);
            soundOut2.Stop();
            PlayASound(soundName,soundSource,soundOut2);
        }

        public void playMusic(string musicname)
        {
            IWaveSource soundSource = GetSoundSource(musicname);
            soundOut.Stop();
            PlayASound(musicname, soundSource,soundOut);
        }

        public void transitionSong(String musicName)
        {

        }

        public void soundUpdate(bool soundToggle)
        {

        }

        [STAThread]
        private void PlayASound(String name, IWaveSource soundSource, ISoundOut thisSound)
        {
            //Tell the SoundOut which sound it has to play
            //thisSound.Stop();
            thisSound.Initialize(soundSource);
            //Play the sound
            thisSound.Play();

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

        private IWaveSource GetSoundSource(String name)
        {
            //return any source ... in this example, we'll just play a mp3 file
            return CodecFactory.Instance.GetCodec(sound_dict[name]);
        }

        private IWaveSource CreateASineSource()
        {
            double frequency = 500 ;
            double amplitude = 0.6;
            double phase = 0.0;
            //Create a ISampleSource
            ISampleSource sampleSource = new SineGenerator(frequency, amplitude, phase);

            //Convert the ISampleSource into a IWaveSource
            return sampleSource.ToWaveSource();
        }
    }
}
