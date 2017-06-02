using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace SayAgain {
    public class DialogueParsing {
        public RootObject r = new RootObject();
        private string filename = "";
        private void MakeParse() {
            string json_file = System.IO.File.ReadAllText(filename);
            r = JsonConvert.DeserializeObject<RootObject>(json_file);
        }

        RootObject GetObject() { return r; }
        public DialogueParsing(string file) {
            filename = file;
            MakeParse();
        }
        public DialogueParsing() { }
        ~DialogueParsing() { }
    }

    public class DialogueObj {
        public string content;
        public string tone;
        public string plot;
        public string id;
        public string next;
        public double FNC;
        public int bucket;
        public string speaker;
        public string finished;
        public string inext;
        public List<string> target;


        //for npc plot point lines/// 8 string, 1 string list (dialogue object)
        public DialogueObj(string content, string tone, string plotpoint, string id, string FNC, string finished, string speaker, string InterjectionNext, List<string> target) {
            this.content = content; this.tone = tone; this.plot = plotpoint; this.id = id; this.FNC = double.Parse(FNC); this.finished = finished; this.speaker = speaker; this.inext = InterjectionNext;
            this.target = target;
        }

        //for npc transitino lines///6 string, 1 string list, 1 int (dialogue object)
        public DialogueObj(string c, string t, string id, string f, int b, string s, string ix, List<string> target) {
            this.content = c; this.tone = t; this.id = id; this.FNC = double.Parse(f); this.bucket = b; this.speaker = s; this.inext = ix;
            this.target = target;
        }

        //for player plot point lines///6 string, 1 string list (dialogue object)
        public DialogueObj(string c, string t, string p, string i, string f, string ix, List<string> target) {
            this.content = c; this.tone = t; this.plot = p; this.id = i; this.FNC = double.Parse(f); this.inext = ix; this.target = target;
        }

        //for player transition lines///5 string, 1 int, 1 string list (dialogue object)
        public DialogueObj(string c, string t, string i, string f, int b, string ix, List<string> target) {
            this.content = c; this.tone = t; this.id = i; this.FNC = double.Parse(f); this.bucket = b; this.inext = ix; this.target = target;
        }

        public DialogueObj() {
            content = ". . .";
            tone = "";
            plot = "";
            id = "";
            next = "";
            finished = "";
            speaker = "";
            inext = "";
            target = new List<string>();
            bucket = -(2 ^ 16);
            FNC = 2 ^ 16;
        }
        ~DialogueObj() { }
    }

    public class RootObject {
        public List<DialogueObj> Dialogues { get; set; }
    }

}
