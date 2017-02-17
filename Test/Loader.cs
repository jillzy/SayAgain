using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Audio;

namespace Test
{
    public class Loader
    {
        //Creates and loads Json Objects
        public DialogueParsing playerDialogueObj1 = new DialogueParsing(@"../../playerTutorial.json");
        public DialogueParsing alexDialogueObj1 = new DialogueParsing(@"../../alexTutorial.json");
        public DialogueParsing sampleDialogueObj = new DialogueParsing(@"../../sampleJSON.JSON");

        //Creates and loads music and sound files
        public Music core_music = new Music(@"../../sounds/sayagain-loop-core.wav");
        
        //Creates and loads art and sprites


        public Loader() { }
        ~Loader() { }
    }
}
