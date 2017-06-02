using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using SFML.Audio;
using SFML.System;

namespace SayAgain {
    class SoundManager {
        //constructor
        public SoundManager() {

            m_queue = new Queue<String>();
            time_left = Time.FromSeconds(1);

            sound_dict = new Dictionary<string, SoundBuffer>() { { "button", new SoundBuffer("../../Sounds/button.wav") } };

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

            loops = new Dictionary<string, List<string>>() { { "basic",new List<string>()  { "../../Sounds/basic.OGG" } },
                                                         { "alex",new List<string>()  {"../../Sounds/harmonica-sad.OGG" ,
                                                                                        "../../Sounds/harmonica-neutral.OGG"
                                                                                        , "../../Sounds/harmonica-happy.OGG" }} ,
                                                         { "dad",new List<string>()  {"../../Sounds/sax-sad.OGG" ,
                                                                                        "../../Sounds/sax-neutral.OGG"
                                                                                        , "../../Sounds/sax-happy.OGG" }},
                                                         { "mom",new List<string>()  {"../../Sounds/key-sad.OGG" ,
                                                                                        "../../Sounds/key-neutral.OGG"
                                                                                        , "../../Sounds/key-happy.OGG" }} };
        }

        private Music current;
        private Time time_left;

        private Dictionary<String, SoundBuffer> sound_dict;
        private Queue<String> m_queue;
        public bool soundpause = false;
        private Dictionary<string, List<SoundBuffer>> chatter_dict;
        private Sound chatter;
        private Sound SFX;
        private Dictionary<string, List<string>> loops;


        public void toggleSoundPause() {
            soundpause = !soundpause;
            current.Volume = (soundpause ? 0 : 100);
        }

        //methods


        public void playSFX(String soundName)
        {
            SFX = new Sound(sound_dict[soundName]);
            SFX.Volume = (soundpause ? 0 : 100);
            SFX.Play();
        }

        public void playChatter(String person) {
            Random r = new Random();
            chatter = new Sound(chatter_dict[person][r.Next(chatter_dict[person].Count)]);
            chatter.Volume = (soundpause ? 0 : 100);
            chatter.Play();
        }


        public void init_music()
        {
            current = new Music(loops["basic"][0]);
            current.Volume = (soundpause ? 0 : 100);
            current.Play();
        }
        public void update_music()
        {
            if (current.PlayingOffset >  time_left && !m_queue.Any())
            {
                //Random r = new Random();
                //List<string> values = Enumerable.ToList(loops.Values);
                m_queue.Enqueue(loops["basic"][0]);
                
            }

            if (current.Status == SoundStatus.Stopped && !soundpause)
            {

                current = new Music(m_queue.Dequeue());
                current.Volume = (soundpause ? 0 : 100);
                current.Play();
            }

            if (current.Status == SoundStatus.Stopped) {



                current = new Music(m_queue.Dequeue());
                current.Play();
            }
        }

        public void loop_enqueue(string speaker, int change)
        {
            if (speaker[0] == '-') speaker = speaker.Substring(1, speaker.Length-1);

            if (change < 0)
                m_queue.Enqueue(loops[speaker][0]);
            else if (change == 0 )
                m_queue.Enqueue(loops[speaker][1]);
            else
                m_queue.Enqueue(loops[speaker][2]);

            while (m_queue.Count > 2) m_queue.Dequeue();
        }
    }
}
