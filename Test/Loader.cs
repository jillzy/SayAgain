using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test {
    public class Loader {
        //Creates and loads Json Objects
        public DialogueParsing playerDialogueObj1 = new DialogueParsing(@"../../player.json");
        public DialogueParsing NPCDialogueObj = new DialogueParsing(@"../../dad.json");
        public DialogueParsing newplayert = new DialogueParsing(@"../../newplayertt.json");
        public DialogueParsing newplayerp = new DialogueParsing(@"../../newplayerpp.json");
        public DialogueParsing allt = new DialogueParsing(@"../../alltt.json");
        public DialogueParsing allp = new DialogueParsing(@"../../allpp.json");
        public DialogueParsing Jankson = new DialogueParsing(@"../../jank.json");


        //Creates and loads music and sound files


        //Creates and loads art and sprites


        public Loader() { }
        ~Loader() { }
    }
}
