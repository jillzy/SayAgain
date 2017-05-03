using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test {
    class OldSelector {
        public List<DialogueObj> ChooseDialog(DialogueParsing r, string now, string currTone) {
            List<DialogueObj> responseList = new List<DialogueObj>();
            for (int i = 0; i < r.r.Dialogues.Count; i++) {
                var curr = r.r.Dialogues[i];
                if (curr.id == now && currTone == curr.tone) {
                    responseList.Add(curr);
                    return responseList;
                }
            }

            //sends results or returns empty value
            if (responseList.Count == 0) {
                responseList.Add(new DialogueObj());
            }

            return responseList;
        }

        //plot point dialogue
        public List<DialogueObj> ChooseDialog2(DialogueParsing r, string currNode, string id, string t) {
            List<DialogueObj> responseList = new List<DialogueObj>();
            var best = new DialogueObj();
            for (int i = 0; i < r.r.Dialogues.Count; i++) {
                var curr = r.r.Dialogues[i];
                if (curr.plotpoint == currNode && id == curr.id && curr.tone == t) {
                    responseList.Add(curr);
                    return responseList;

                }
            }
            responseList.Add(best);
            return responseList;
        }

        //transition
        public List<DialogueObj> ChooseDialog3(DialogueParsing r, double b, string id, string t) {
            List<DialogueObj> responseList = new List<DialogueObj>();
            var best = new DialogueObj();
            for (int i = 0; i < r.r.Dialogues.Count; i++) {
                var curr = r.r.Dialogues[i];
                if (b == curr.bucket && curr.id == id && (curr.tone == t || curr.tone == "Default")) {

                    responseList.Add(curr);
                    return responseList;
                }
            }
            responseList.Add(best);

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
    }
}
