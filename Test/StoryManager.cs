﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//greetdad, settype PP, 
namespace Test {
    class StoryManager {
        protected List<String> reachedPlotpoints;
        protected string dialogueType;
        public enum type { plotpoint, transition };
        //protected List<String> nextPreconditions;
        protected string currentNode;
        protected int numberOfChildren;

        static Dictionary<String, Tuple<List<String>, List<String>>> plot_dict
            = new Dictionary<String, Tuple<List<String>, List<String>>>();
        List<String> next_nodes = new List<String>();
        List<String> preconditions = new List<String>();

        void addNode(String s, List<String> n, List<String> p) {

            Tuple<List<String>, List<String>> value =
                new Tuple<List<String>, List<String>>(n, p);
            plot_dict.Add(s, value);
            clear();

        }

        public string getCurrentNode() {
            return currentNode;
        }

        void clear() {
            next_nodes = null;
            next_nodes = new List<String>();
            preconditions = null;
            preconditions = new List<String>();
        }


        public string getDialogueType() {
            return dialogueType;
        }

        public void setDialogueType(type t) {
            dialogueType = t.ToString();
        }

        public void setTypePlotNode() {
            dialogueType = type.plotpoint.ToString();
        }

        public void setTypeTransition() {
            dialogueType = "transition";
        }

        public bool findNextPossibleNodes() {
            numberOfChildren = 0;
            if (plot_dict[currentNode].Item1 != null) {
                foreach (var n in plot_dict[currentNode].Item1) {
                    List<String> nextPreconditions = new List<String>();
                    numberOfChildren += 1;
                    if (plot_dict[n].Item2 != null) {
                        foreach (var c in plot_dict[n].Item2) {
                            nextPreconditions.Add(c);
                            if (checkIfPreconSatisfied(nextPreconditions)) {
                                currentNode = n;
                                reachedPlotpoints.Add(currentNode);
                                return true;
                            }

                        }
                    }
                }
            }
            return false;
        }

        //ASSUMES that children dont have overlapping preconditions
        public bool checkIfPreconSatisfied(List<String> nextPreconditions) {

            foreach (var p in nextPreconditions) {
                //if p has a ' in it (multiple preconditions)
                //then separate the two conitions, parse for whether FNC or plot point requirement
                if (p.Contains(",") || true) {

                    string tmp = p.Replace(" ", String.Empty); //get rid of whitespace
                    var array = tmp.Split(',');
                    //for each thing in array satisfied, satisfied = true, else false and break
                    foreach (var k in array) {
                        if (!k.Contains(":")) {
                            //this means its not an FNC check
                            //means its a plotpoint check
                            if (!checkPastPlotPoint(k))//if false
                            {
                                return false;
                            }
                        } else {
                            var t = k.Replace(":", String.Empty);
                            if (!checkCharFNC(t)) {
                                return false;
                            }
                        }
                    }
                } else if (p.Contains(":")) {
                    //this means its not an FNC check
                    //means its a plotpoint check
                    if (!checkPastPlotPoint(p))//if false
                    {
                        return false;
                    }
                } else {
                    var t = p.Replace(":", String.Empty);
                    if (!checkCharFNC(p)) {
                        return false;
                    }
                }

            }
            return true;
        }

        public bool checkCharFNC(string s) {
            char character = s[0];
            Character mom = Program.getGame().getMom();
            Character dad = Program.getGame().getDad();
            Character alexis = Program.getGame().getAlexis();
            s = s.Substring(1);
            var range = s.Split('-');
            double high, low;
            // mom should not be null

            switch (character) {
                case 'M':

                    low = determineRange(range, mom.getFNCRange())[0];
                    high = determineRange(range, mom.getFNCRange())[1];
                    if (mom.getCurrentFNC() >= low && mom.getCurrentFNC() <= high) {

                        return true;
                    }

                    return false;

                case 'D':
                    low = determineRange(range, dad.getFNCRange())[0];
                    high = determineRange(range, dad.getFNCRange())[1];
                    if (dad.getCurrentFNC() >= low && dad.getCurrentFNC() <= high) {

                        return true;
                    }

                    return false;

                case 'A':
                    low = determineRange(range, alexis.getFNCRange())[0];
                    high = determineRange(range, alexis.getFNCRange())[1];
                    if (alexis.getCurrentFNC() >= low && alexis.getCurrentFNC() <= high) {
                        //add the new currentNode to the list of nodes we have been to
                        reachedPlotpoints.Add(currentNode);

                        return true;
                    }

                    return false;

            }
            return false;
        }


        public List<double> determineRange(String[] range, double[] charFNC) {
            //HF-MF-LF-LN-MN-HN-LC-MC-HC
            double low, high;
            List<double> result = new List<double>();

            switch (range[0]) {
                case "HF":
                    low = charFNC[0];
                    result.Add(low);
                    break;
                case "MF":
                    low = charFNC[1];
                    result.Add(low);
                    break;
                case "LF":
                    low = charFNC[2];
                    result.Add(low);
                    break;
                case "LN":
                    low = charFNC[3];
                    result.Add(low);
                    break;
                case "MN":
                    low = charFNC[4];
                    result.Add(low);
                    break;
                case "HN":
                    low = charFNC[5];
                    result.Add(low);
                    break;
                case "LC":
                    low = charFNC[6];
                    result.Add(low);
                    break;
                case "MC":
                    low = charFNC[7];
                    result.Add(low);
                    break;
                case "HC":
                    low = charFNC[8];
                    result.Add(low);
                    break;
            }
            switch (range[1]) {
                case "HF":
                    high = charFNC[1];
                    result.Add(high);
                    break;
                case "MF":
                    high = charFNC[2];
                    result.Add(high);
                    break;
                case "LF":
                    high = charFNC[3];
                    result.Add(high);
                    break;
                case "LN":
                    high = charFNC[4];
                    result.Add(high);
                    break;
                case "MN":
                    high = charFNC[5];
                    result.Add(high);
                    break;
                case "HN":
                    high = charFNC[6];
                    result.Add(high);
                    break;
                case "LC":
                    high = charFNC[7];
                    result.Add(high);
                    break;
                case "MC":
                    high = charFNC[8];
                    result.Add(high);
                    break;
                case "HC":
                    high = charFNC[9];
                    result.Add(high);
                    break;
            }

            return result;


        }
        public bool checkPastPlotPoint(string p) {
            foreach (var plotpoint in reachedPlotpoints) {
                if (p == plotpoint) {
                    return true;
                }
            }
            return false;
        }

        public StoryManager() {
            //nextPreconditions = new List<String> ();

            currentNode = "DadGreetsPlayer";
            setDialogueType(type.plotpoint);
            reachedPlotpoints = new List<String>();
            reachedPlotpoints.Add(currentNode);

            next_nodes.Add("PlayerSupportsDad");
            next_nodes.Add("PlayerDefendsMom");
            addNode("DadGreetsPlayer", next_nodes, preconditions);

            preconditions.Add("M: HF-HF");
            next_nodes.Add("PlayerSupportsDad2");
            addNode("PlayerSupportsDad", next_nodes, preconditions);

            preconditions.Add("M: HC-HC");
            next_nodes.Add("MomAdmitsJob");
            addNode("AlexChangesSubject", next_nodes, preconditions);

            preconditions.Add("M: HF-HF");
            next_nodes.Add("AlexChangesSubject");
            addNode("PlayerSupportsDad2", next_nodes, preconditions);

            preconditions.Add("M: HC-HC");
            next_nodes.Add("MomAdmitsJob");
            addNode("PlayerDefendsMom", next_nodes, preconditions);

            preconditions.Add("M: HC-HC");
            next_nodes.Add("MomReconcilesDad");
            addNode("MomAdmitsJob", next_nodes, preconditions);

            preconditions.Add("M: HC-HC");
            next_nodes.Add("DadSeesHisFault");
            next_nodes.Add("DadNoSeesHisFault");
            addNode("MomReconcilesDad", next_nodes, preconditions);

            preconditions.Add("CAT");
            next_nodes.Add("boooom baby boom");
            addNode("DadNoSeesHisFault", next_nodes, preconditions);

            preconditions.Add("DOG");
            next_nodes.Add("boooom baby boom");
            addNode("DadSeesHisFault", next_nodes, preconditions);
        }

    }
}
