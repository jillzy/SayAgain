using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;
using SFML.System;
using System.Drawing;

namespace Test
{
    class GameState
    {
        public GameState()
        {
            currentState = "menu";
            currentMenuState = "start";
        }

        string currentState;
        string currentMenuState;
        Dictionary<string, GameTimer> DictGameTimer = new Dictionary<string, GameTimer>();

        //Timer for keeping track of time given to the player

        public GameTimer getGameTimer(string tag)
        {
            return DictGameTimer[tag];
        }

        public void addTimer(string name, double initTime, Action T)
        {
            if (DictGameTimer.ContainsKey(name))
            {
                DictGameTimer[name] = new GameTimer(name, initTime, T);
            }
            else
            {
                DictGameTimer.Add(name, new GameTimer(name, initTime, T));
            }
        }

        public string GetState()
        {
            return currentState;
        }


        public void SetState(string state)
        {
            if (state != "menu" && state != "game" && state != "pause")
            {
                throw new FormatException();
            }
            if (state == "game" && currentMenuState == "start")
            {
                DictGameTimer["game"].setOldGameTime((DateTime.Now.Ticks / 10000) / 1000);
            }


            currentState = state;
        }

        public string GetMenuState()
        {
            return currentMenuState;
        }

        public void SetMenuState(string state)
        {
            if (state != "start" && state != "settings" && state != "pause")
            {
                throw new FormatException();
            }
            currentMenuState = state;
        }

        public void updateTimerz()
        {

            //Console.WriteLine("GET START IS TRUE HERE: "+DictGameTimer["game"].getCountDown());
            foreach (var pair in DictGameTimer)
            {
                //pair.Value is a gameTimer
                //pair.Key is the label of the game Timer;
                if (pair.Value.getStart())
                {
                    //Console.WriteLine("GET START IS TRUE HERE");
                    pair.Value.updateTimer();

                }
                else
                {
                    if (pair.Value.getCountDown() == 0)
                    {
                        //DO STUFF BEFORE RESTARTING
                        //Process Player dialogue
                        pair.Value.doTask();
                        pair.Value.restartTimer();
                    }
                    
                }
            }
        }

        public void stopTimerz(string key) {
            DictGameTimer[key].stopTimer();
        }

        public void startTimer(string key)
        {
            DictGameTimer[key].startTimer();
        }

        // Handle Menu Traversal and Game Launching
        public void updateMenuState(int[] mouseCoords, List<MenuButton> buttons, List<Tuple<string, string, Task>> mappings)
        {
            // Loop through current menu's buttons
            for (var i = 0; i < buttons.Count; i++)
            {
                // If mouse position is over current button
                if (buttons[i].Contains(mouseCoords[0], mouseCoords[1]))
                {
                    // Find what this button is suppose to do
                    for (var j = 0; j < mappings.Count; j++)
                    {
                        // Found button being clicked
                        if (buttons[i].getMenuButtonText().DisplayedString == mappings[j].Item1)
                        {
                            // Do button action
                            mappings[j].Item3.Start();

                            // Change either game state or menu state based off of button's target state
                            if (mappings[j].Item2 == "game")
                            {
                                SetState(mappings[j].Item2);
                            }
                            else if (mappings[j].Item2 == "menu")
                            {
                                SetState(mappings[j].Item2);
                                SetMenuState("start");
                            }
                            else
                            {
                                SetMenuState(mappings[j].Item2);
                            }

                            break;
                        }
                    }
                    break;
                }
            }
        }

        public void TogglePause()
        {
            if (GetState() == "pause")
            {
                SetState("game");
                SetMenuState("start");
            }
            else if (GetState() == "game")
            {
                SetState("pause");
                SetMenuState("pause");
            }
        }

    }
}
