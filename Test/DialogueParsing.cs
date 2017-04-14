﻿using System;
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
        public string content { get; set; }
        public string tone { get; set; }
        public string id { get; set; }
        public string next { get; set; }
        public double FNC { get; set; }

        public DialogueObj(string newContent, string newTonalPreReq, string id, string next) {
            content = newContent; tone = newTonalPreReq; this.id = id; this.next = next; this.FNC = 2 ^ 16;
        }

        public DialogueObj(string newContent, string newTonalPreReq, string id, string next, string FNC) {
            content = newContent; tone = newTonalPreReq; this.id = id; this.next = next; this.FNC = double.Parse(FNC);
        }

        public DialogueObj() {
            content = "returned empty string";
            tone = "";
            id = "";
            next = "";
            FNC = 2 ^ 16;
        }
        ~DialogueObj() { }
    }

    public class RootObject {
        public List<DialogueObj> Dialogues { get; set; }
    }

}
