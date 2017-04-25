using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace Test {
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
        public string plotpoint;
        public string id;
        public string next;
        public double FNC;
        public double bucket;
        public string speaker;
        public string finished;
        public string inext;

        public DialogueObj(string newContent, string newTonalPreReq, string id, string next) { //for linked list reading
            content = newContent; tone = newTonalPreReq; this.id = id; this.next = next; this.FNC = 2 ^ 16;
        }

        public DialogueObj(string content, string tone, string plotpoint, string id, string FNC, string finished, string speaker, string InterjectionNext) { //for plot point lines
            this.content = content; this.tone = tone; this.plotpoint = plotpoint; this.id = id; this.FNC = double.Parse(FNC); this.finished = finished; this.speaker = speaker; this.inext = InterjectionNext;
        }

        public DialogueObj(string c, string t, string id, string f, string b, string s, string ix){ //for transitino lines
            this.content = c; this.tone = t; this.id = id; this.FNC = double.Parse(f); this.bucket = double.Parse(b); this.speaker = s; this.inext = ix;
        }

        public DialogueObj()
        {
            content = "returned empty string";
            tone = "";
            plotpoint = "";
            id = "";
            next = "";
            finished = "";
            speaker = "";
            inext = "";
            bucket = -(2 ^ 16);
            FNC = 2 ^ 16;
        }
        ~DialogueObj() { }
    }

    public class RootObject {
        public List<DialogueObj> Dialogues { get; set; }
    }

}
