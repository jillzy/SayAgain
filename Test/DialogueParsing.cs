using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace Test
{
    public class DialogueParsing
    {
        public RootObject r = new RootObject();
        private string filename = "";
        private void MakeParse()
        {
            string json_file = System.IO.File.ReadAllText(filename);
            r = JsonConvert.DeserializeObject<RootObject>(json_file);
        }

        RootObject GetObject() { return r; }
        public DialogueParsing(string file)
        {
            filename = file;
            MakeParse();
        }
        public DialogueParsing() { }
        ~DialogueParsing() { }
    }

    public class DialogueObj
    {
        public string content { get; set; }
        public string tonalPreReq { get; set; }
        public string context { get; set; }
        public string consequence { get; set; }
        public List<string> memories { get; set; }
        public List<string> milestone { get; set; }
        public int fncPreReq { get; set; }
        public string speaker { get; set; }
        public List<int> target { get; set; }
        public string nextContext { get; set; }

        public DialogueObj(string newContent, string newTonalPreReq, string newContext, string newConsequence, List<string> newMemories, List<string> newMilestone, int newFncPreReq, string newSpeaker, List<int> newTarget, string newNextContext)
        {
            content = newContent; tonalPreReq = newTonalPreReq; context = newContext; consequence = newConsequence; memories = newMemories; milestone = newMilestone; fncPreReq = newFncPreReq; speaker = newSpeaker; target = newTarget; nextContext = newNextContext;
        }

        public DialogueObj() {
            content = "returned empty string";
            tonalPreReq = "";
            context = "";
            consequence = "";
            nextContext = "Greetings";
            memories = new List<string>();
            milestone = new List<string>();
            memories.Add("");
            milestone.Add("");
            
        }
        ~DialogueObj() { }
    }

    public class RootObject
    {
        public List<DialogueObj> Dialogues { get; set; }
    }

}
