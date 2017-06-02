using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SayAgain {
    class OldSelector {
        
        //plot point dialogue
        public List<DialogueObj> ChooseDialogPlot(DialogueParsing r, string currNode, string id, string t) {
            int countdown = -1;
            List<DialogueObj> responseList = new List<DialogueObj>();
            var best = new DialogueObj();
            for (int i = 0; i < r.r.Dialogues.Count; i++) {
                var curr = r.r.Dialogues[i];
                if (curr.plot == currNode && id == curr.id && (curr.tone == t || curr.tone == "Default")) {
                    responseList.Add(curr);
                    countdown = 3;
                }
                if (countdown-- == 0) return responseList;
            }

            if (responseList.Count == 0) responseList.Add(best);
            return responseList;
        }

        //transition
        public List<DialogueObj> ChooseDialogTransition(DialogueParsing r, double b, string id, string t) {
            int countdown = -1;
            List<DialogueObj> responseList = new List<DialogueObj>();
            var best = new DialogueObj();
            for (int i = 0; i < r.r.Dialogues.Count; i++) {
                var curr = r.r.Dialogues[i];
                if (b == curr.bucket && curr.id == id && (curr.tone == t || curr.tone == "Default")) {
                    responseList.Add(curr);
                    countdown = 3;
                }
                if (countdown-- == 0) return responseList;
            }
            if (responseList.Count == 0) responseList.Add(best);
            return responseList;
        }

        public List<DialogueObj> chooseJank(DialogueParsing r, string id, string t) {
            List<DialogueObj> responseList = new List<DialogueObj>();
            var best = new DialogueObj();
            for (int i = 0; i < r.r.Dialogues.Count; i++) {
                var curr = r.r.Dialogues[i];
                if (curr.id == id && (curr.tone == t || curr.tone == "Default")) {

                    responseList.Add(curr);
                    return responseList;
                }
            }
            responseList.Add(best);
            return responseList;
        }
        public OldSelector() { }
        ~OldSelector() { }

        //printStuff(curr,currNode,id,t);
        private void printStuff(DialogueObj d, string c_node, string c_id, string c_tone) {
            Console.WriteLine("current dialogue object fields: " + d.id + " , " + d.plot);
            Console.WriteLine("current varibles to be matched: " + c_id + " , " + c_node + " , " + c_tone);
        }
    }
}
