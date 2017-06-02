using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SayAgain {
    public class Loader {
        //Creates and loads Json Objects
        public DialogueParsing newplayerp = new DialogueParsing(@"../../Json/newplayerpp.json");
        public DialogueParsing newplayert = new DialogueParsing(@"../../Json/newplayertt.json");
        public DialogueParsing allt = new DialogueParsing(@"../../Json/alltt.json");
        public DialogueParsing allp = new DialogueParsing(@"../../Json/allpp.json");
        public DialogueParsing Jankson = new DialogueParsing(@"../../Json/jank.json");


        //Creates and loads music and sound files


        //Creates and loads art and sprites


        public Loader() { }
        ~Loader() { }
    }
}
