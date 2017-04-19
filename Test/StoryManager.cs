using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//greetdad, settype PP, 
namespace Test
{
    class StoryManager
    {
        protected List<String> reachedPlotpoints;
        protected string dialogueType;
        public enum type { plotpoint, transition };
        //protected List<String> nextPreconditions;
        protected string currentNode;
        protected int numberOfChildren;

        static Dictionary<String, Tuple<List<String>, List<String>>> plot_dict
            = new Dictionary < String, Tuple<List<String>, List<String>>>();
        List<String> next_nodes = new List<String>();
        List<String> preconditions = new List<String>();

        void addNode(String s, List<String> n, List<String> p)
        {

            Tuple<List<String>, List<String>> value =
                new Tuple<List<String>, List<String>>(n, p);
            plot_dict.Add(s, value);
            clear();

        }

        public void print()
        {
           /* foreach (var kvp in plot_dict)
            {

                Console.WriteLine(kvp.Key);
                if (!kvp.Value.Item1[0].Equals(null))
                {
                    Console.WriteLine(kvp.Value.Item1[0]);
                }
                if (!kvp.Value.Item1[1].Equals(null)) { 
                    Console.WriteLine(kvp.Value.Item1[1]);
                }

          
            }
  */
        }
        
        public string getCurrentNode()
        {
            return currentNode;
        }

        void clear()
        {
            next_nodes = null;
            next_nodes = new List<String>();
            preconditions = null;
            preconditions = new List<String>();
        }


        
        public string getDialogueType()
        {
            return dialogueType;
        }

        public void setDialogueType(type t)
        {
            dialogueType = t.ToString();
        }

        public void setTypePlotNode()
        {
            dialogueType = type.transition.ToString();
        }

        public void setTypeTransition()
        {
            dialogueType = "transition";
        }

        public void findNextPossibleNodes()
        {
            numberOfChildren = 0;
            Console.WriteLine("Current node: " + currentNode);
            if (plot_dict[currentNode].Item1 != null)
            {
                //the string name of each child node
                foreach (var n in plot_dict[currentNode].Item1) //each child
                {
                    List<String> nextPreconditions = new List<String>();
                    numberOfChildren += 1;
                    Console.WriteLine("Possible Next Node: " + n);
                    if (plot_dict[n].Item2 != null)
                    {
                        foreach (var c in plot_dict[n].Item2) //the precondition of each child
                        {
                            Console.WriteLine("With preconditions: " + c);

                            nextPreconditions.Add(c);
   
                            if (checkIfPreconSatisfied(nextPreconditions)) //if true
                            {
                                currentNode = n;//current node is set to child node 
                                reachedPlotpoints.Add(currentNode);
                            }

                        }
                    }
                }
            }
            
        }

    //ASSUMES that children dont have overlapping preconditions
        public bool checkIfPreconSatisfied(List<String> nextPreconditions)
        {
            
            foreach (var p in nextPreconditions)
            {
                //if p has a ' in it (multiple preconditions)
                //then separate the two conitions, parse for whether FNC or plot point requirement
                //Console.WriteLine(p);
                if (p.Contains(","))
                {
                    string tmp = p.Replace(" ", String.Empty); //get rid of whitespace
                    var array = tmp.Split(',');
                    //for each thing in array satisfied, satisfied = true, else false and break
                    foreach (var k in array)
                    {
                        if (!k.Contains(":"))
                        {
                            //this means its not an FNC check
                            //means its a plotpoint check
                            if (!checkPastPlotPoint(k))//if false
                            {
                                return false;
                            }
                        }
                        else
                        {
                            var t = k.Replace(":", String.Empty);
                            if (!checkCharFNC(t))
                            {
                                return false;
                            }
                        }
                    }
                }
                else if (p.Contains(":"))
                {
                    //this means its not an FNC check
                    //means its a plotpoint check
                    if (!checkPastPlotPoint(p))//if false
                    {
                        return false;
                    }
                }
                else
                {
                    var t = p.Replace(":", String.Empty);
                    if (!checkCharFNC(p))
                    {
                        return false;
                    }
                }

            }
            return true;
        }
        
        public bool checkCharFNC(string s)
        {
            Console.WriteLine("checkCharFNC()");
            char character = s[0];
            switch (character) {
                case 'M':
                    return false;
                    
                case 'D':
                    return false;
                    
                case 'A':
                    return false;
                    
            }
            return false;
        }

        public bool checkPastPlotPoint(string p)
        {
            Console.WriteLine("checkPastPlotPoint()");
            //if p exists in list of reached plotpoints
            //return true;
            //else
            //return false;
            foreach (var plotpoint in reachedPlotpoints)
            {
                if (p == plotpoint)
                {
                    return true;
                }
            }
            return false;
        }

        public StoryManager() {


            //nextPreconditions = new List<String> ();
            
            currentNode = "MomTellsPlayerTalkToAlex";
            setDialogueType(type.plotpoint);
            reachedPlotpoints = new List<String>();
            reachedPlotpoints.Add(currentNode);
            reachedPlotpoints.Add("DadApologizesMom");


        //TODO: all blow up nodes reachable from any point

        next_nodes.Add("MomTellsPlayerTalkToAlex");
            next_nodes.Add("MomAdmitsJob");
            addNode("GreetMom", next_nodes, preconditions);

            next_nodes.Add("MomReconcilesDad");
            next_nodes.Add("MomBlowsUp");
            next_nodes.Add("GreetDad");
            next_nodes.Add("GreetAlex");
            preconditions.Add("M: LF-MF");
            addNode("MomTellsPlayerTalkToAlex", next_nodes, preconditions);
            
            preconditions.Add("M: MC-HC");
            preconditions.Add("AlexAdmitsNeglect");
            preconditions.Add("DadAccusesMom");
            addNode("MomAdmitsJob", next_nodes, preconditions);


            next_nodes.Add("DadAccusesMom");
            next_nodes.Add("DadBlowsUp");
            addNode("GreetDad", next_nodes, preconditions);

            preconditions.Add("D: MC-HC");
            next_nodes.Add("MomBlowsUp");
            next_nodes.Add("DadApologizesMom");
            addNode("DadAccusesMom", next_nodes, preconditions);

            preconditions.Add("D: HC");
            preconditions.Add("MomAdmitsJob, D: LN-HC");
            next_nodes.Add("DadApologizesAlex");
            addNode("DadApologizesMom", next_nodes, preconditions);

            preconditions.Add("AlexAdmitsNeglect, D: LC-HC");
            preconditions.Add("D: HC");
            addNode("DadApologizesAlex", next_nodes, preconditions);

            preconditions.Add("DadApologizesMom, M: LN-HC");
            next_nodes.Add("AlexAdmitsNeglect");
            addNode("MomReconcilesDad", next_nodes, preconditions);

            next_nodes.Add("AlexAdmitsNeglect");
            next_nodes.Add("GreetMom");
            next_nodes.Add("GreetDad");
            addNode("GreetAlex", next_nodes, preconditions);

            preconditions.Add("A: LC-HC");
            next_nodes.Add("AlexReconcilesPlayer");
            addNode("AlexAdmitsNeglect", next_nodes, preconditions);

            preconditions.Add("A: LC-HC");
            next_nodes.Add("AlexReconcilesMom");
            addNode("AlexReconcilesPlayer", next_nodes, preconditions);

            
            preconditions.Add("A: LC-HC, MomAdmitsJob");
            next_nodes.Add("AlexReconcilesDad");
            addNode("AlexReconcilesMom", next_nodes, preconditions);

            preconditions.Add("A: LC-HC, DadApologizesAlex");
            addNode("AlexReconcilesDad", next_nodes, preconditions);

            preconditions.Add("D: HF");
            addNode("DadBlowsUp", next_nodes, preconditions);

            preconditions.Add("A: HF");
            addNode("AlexBlowsUp", next_nodes, preconditions);

            preconditions.Add("M: HF");
            addNode("MomBlowsUp", next_nodes, preconditions);

            findNextPossibleNodes();
        }

    }
}
