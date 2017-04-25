using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public class Selector
    {
        public List<DialogueObj> ChooseDialog(double fncPreReq, DialogueParsing r, List<string> memories, List<string>
                                              currentMilestones, tone currentTone, string currentContext)
        {
            //memory check
            bool memoriesCheck = false;
            int fncDirection = 0;
            int counter = 0;
            //check fnc direction
            if (fncPreReq > 0) fncDirection = 1; else if (fncPreReq < 0) fncDirection = -1; else fncDirection = 0;
            //checks for memories requirements first (ones with no memoriess are also added)
            List<DialogueObj> possibleChoices = new List<DialogueObj>();
            possibleChoices.Clear();
            //iterates through the json list
            for (int i = 0; i < r.r.Dialogues.Count; i++)
            {
                memoriesCheck = false;
                counter = 0;
                if (r.r.Dialogues.ElementAt(i).memories.Count == 1 && r.r.Dialogues.ElementAt(i).memories[0] == "")
                {
                    Console.WriteLine("I AM A CRIME AGAINST HUMANITY");
                    memoriesCheck = true;
                }
                else
                {
                    //iterates through any memoriess from json element
                    for (int a = 0; a < r.r.Dialogues.ElementAt(i).memories.Count; a++)
                    {
                        //iterates through currentMade memories
                        for (int e = 0; e < memories.Count; e++)
                        {
                            if (r.r.Dialogues.ElementAt(i).memories[a].CompareTo(memories[e]) == 0)
                            {
                                counter++;
                            }
                        }
                    }
                    //check to see if require memoriess are there
                    if (counter == r.r.Dialogues.ElementAt(i).memories.Count)
                    {
                        memoriesCheck = true;
                    }
                }
                //if present, add to list
                if (memoriesCheck)
                {
                    possibleChoices.Add(new DialogueObj(r.r.Dialogues.ElementAt(i).content,
                        r.r.Dialogues.ElementAt(i).tonalPreReq, r.r.Dialogues.ElementAt(i).context,
                        r.r.Dialogues.ElementAt(i).consequence, r.r.Dialogues.ElementAt(i).memories,
                        r.r.Dialogues.ElementAt(i).milestone, r.r.Dialogues.ElementAt(i).fncPreReq,
                        r.r.Dialogues.ElementAt(i).speaker, r.r.Dialogues.ElementAt(i).target,
                        r.r.Dialogues.ElementAt(i).nextContext));
                }
            }
            //checks for fncPreReq requirements for whats left

            for (int i = 0; i < possibleChoices.Count; i++)
            {
                var ListOneNotTwo = currentMilestones.Except(possibleChoices.ElementAt(i).milestone).ToList();

                if (possibleChoices.ElementAt(i).fncPreReq != fncPreReq)
                {
                    possibleChoices.Remove(possibleChoices.ElementAt(i));
                    i--;
                }

                /*
                //zero fnc
                if (fncDirection == 0 && possibleChoices.ElementAt(i).fncPreReq != 0)
                {  
                    possibleChoices.Remove(possibleChoices.ElementAt(i));
                    i--;
                }
                //positive fnc
                else if (fncDirection == 1 && (possibleChoices.ElementAt(i).fncPreReq > (int)fncPreReq || possibleChoices.ElementAt(i).fncPreReq < 0 ))
                {
                    possibleChoices.Remove(possibleChoices.ElementAt(i));
                    i--;
                }
                //negative fnc 
                else if (fncDirection == -1 && (possibleChoices.ElementAt(i).fncPreReq < (int)fncPreReq || possibleChoices.ElementAt(i).fncPreReq > 0))
                {
                    possibleChoices.Remove(possibleChoices.ElementAt(i));
                    i--;
                }

                */

                //possibleChoices.Ele

                //checks for milestone requirement

                else if (ListOneNotTwo.Count != 0)
                {
                    possibleChoices.Remove(possibleChoices.ElementAt(i));
                    i--;
                }

                //checks for required tone
                else if (!possibleChoices[i].tonalPreReq.Equals(currentTone.ToString()))
                {
                    possibleChoices.Remove(possibleChoices.ElementAt(i));
                    i--;
                }

                //checks current context
                else if (!possibleChoices[i].context.Equals(currentContext))
                {
                    possibleChoices.Remove(possibleChoices.ElementAt(i));
                    i--;
                }

            }

            //sends results or returns empty value
            if (possibleChoices.Count == 0)
            {
                possibleChoices.Add(new DialogueObj());

            }

            return possibleChoices;
        }
        public Selector() { }
        ~Selector() { }

        //Debugging printer
        //Console.WriteLine("number of possible choices = " + possibleChoices.Count);
        //for (int i = 0; i < possibleChoices.Count; i++)
        //{
        //    Console.WriteLine("One response = " + possibleChoices.ElementAt(i).content);
        //}
    }
}
