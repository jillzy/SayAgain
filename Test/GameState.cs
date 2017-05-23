using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Window;
using SFML.System;
using System.Drawing;
namespace Test {
    class GameState {
        public GameState() {
            currentState = "menu";
            currentMenuState = "start";
            sound_man.init_music();
            playerDialogueBox = new DialogueBox(this, "PLAYER");
            dialogueBox = new DialogueBox(this, "AI");
            tooltip = new DialogueBox(this, "tooltip");
            dialogueBox.animationStart = true;
            dialogueBox.init = true;
        }
        string currentState;
        string currentMenuState;
        //Sound Manager
        public SoundManager sound_man = new SoundManager();
        Dictionary<string, GameTimer> DictGameTimer = new Dictionary<string, GameTimer>();
        //Jill's fields and variables
        public DialogueBox dialogueBox;
        public DialogueBox playerDialogueBox;
        public DialogueBox tooltip;
        public string dialogueIndex = "player";
        public bool interjection = false;
        public void advanceConversation(string speaker, List<DialogueObj> responseList, List<DialogueObj> responseListNPC) {
            if (currentState == "game") {
                if (dialogueIndex == "AI") {
                    playerDialogueBox.init = false;
                    playerDialogueBox.active = false;
                    dialogueBox.active = true;
                    if (dialogueBox.checkNext()) {

                        dialogueIndex = "root";
                        dialogueBox.active = false;
                        playerDialogueBox.active = false;
                    }
                } else if (dialogueIndex == "root") {
                    dialogueBox.init = false;
                    playerDialogueBox.init = true;
                    dialogueIndex = "player";
                } else if (dialogueIndex == "player") {
                    dialogueBox.init = false;
                    dialogueBox.active = false;
                    if (playerDialogueBox.active && !dialogueBox.active) {
                        if (playerDialogueBox.printTime != 0 && playerDialogueBox.getAnimationStart() && !playerDialogueBox.getAwaitInput()) {
                            playerDialogueBox.setPrintTime(0);
                        } else {
                            if (playerDialogueBox.checkNext()) {
                                playerDialogueBox.active = false;
                                dialogueBox.active = true;
                                dialogueBox.init = true;
                                if (responseListNPC[0].inext == "") {
                                    //if there is no interjector
                                    dialogueIndex = "AI";

                                    dialogueBox.loadNewDialogue(speaker, responseListNPC[0].content);
                                    sound_man.playChatter(speaker);

                                } else {

                                    //if there is an interjector
                                    dialogueIndex = "interject";
                                    dialogueBox.loadNewDialogue(speaker, responseListNPC[0].content);
                                }
                            }
                        }
                    }
                } else if (dialogueIndex == "interject") {

                    if (dialogueBox.checkNext()) {

                        dialogueBox.loadNewDialogue(speaker, responseListNPC[0].content);

                        if (responseListNPC[0].inext == "") {
                            dialogueIndex = "AI";

                        }

                    }
                }
                // DO STUFF HERE MEGA JANK INCOMING
            } else if (currentState == "tutorial") {

                if (jankList[0].id == "1") {
                    db_states('p');
                    tooltip.init = true;
                    tooltip.loadNewDialogue("tooltip1", "Press Space to Advance");
                    playerDialogueBox.loadNewDialogue("player", jankList[0].content);
                } else if (jankList[0].id == "2") {
                    tooltip.init = false;
                    db_states('p');
                    playerDialogueBox.loadNewDialogue("player", jankList[0].content);
                } else if (jankList[0].id == "3") {
                    db_states('a');
                    dialogueBox.loadNewDialogue("alex", jankList[0].content);
                   sound_man.playChatter("alex");
                } else if (jankList[0].id == "4") {
                    db_states('p');
                    playerDialogueBox.loadNewDialogue("player", jankList[0].content);
                } else if (jankList[0].id == "5") {
                    db_states('r');
                    tooltip.init = true;
                    tooltip.loadNewDialogue("tooltip2", "Drag tone to your dialogue");
                } else if (jankList[0].id == "6") {
                    db_states('p');
                    playerDialogueBox.loadNewDialogue("player", jankList[0].content);
                } else if (jankList[0].id == "7") {
                    db_states('a');
                    dialogueBox.loadNewDialogue("alex", jankList[0].content);
                    sound_man.playChatter("alex");
                } else if (jankList[0].id == "8") {
                    db_states('r');
                } else if (jankList[0].id == "9") {
                    db_states('p');
                    playerDialogueBox.loadNewDialogue("player", jankList[0].content);
                } else if (jankList[0].id == "10") {
                    db_states('a');
                    dialogueBox.loadNewDialogue("alex", jankList[0].content);
                    sound_man.playChatter("alex");
                } else if (jankList[0].id == "11") {
                    db_states('p');
                    playerDialogueBox.loadNewDialogue("player", jankList[0].content);
                } else if (jankList[0].id == "12") {
                    db_states('a');
                    dialogueBox.loadNewDialogue("dad", jankList[0].content);
                    sound_man.playChatter("dad");
                } else if (jankList[0].id == "13") {
                    db_states('p');
                    playerDialogueBox.loadNewDialogue("player", jankList[0].content);
                } else if (jankList[0].id == "14") {
                    db_states('r');
                } else if (jankList[0].id == "15") {
                    db_states('p');
                    playerDialogueBox.loadNewDialogue("player", jankList[0].content);
                } else if (jankList[0].id == "16") {
                    db_states('a');
                    dialogueBox.loadNewDialogue("dad", jankList[0].content);
                    sound_man.playChatter("dad");
                } else if (jankList[0].id == "17") {
                    db_states('p');
                    playerDialogueBox.loadNewDialogue("player", jankList[0].content);
                } else if (jankList[0].id == "18") {
                    db_states('p');
                    playerDialogueBox.loadNewDialogue("player", jankList[0].content);
                } else if (jankList[0].id == "19") {
                    db_states('a');
                    dialogueBox.loadNewDialogue("mom", jankList[0].content);
                    sound_man.playChatter("mom");
                } else if (jankList[0].id == "20") {
                    db_states('p');
                    playerDialogueBox.loadNewDialogue("player", jankList[0].content);
                } else if (jankList[0].id == "21") {
                    db_states('r');
                } else if (jankList[0].id == "22") {
                    db_states('p');
                    playerDialogueBox.loadNewDialogue("player", jankList[0].content);
                } else if (jankList[0].id == "23") {
                    db_states('a');
                    dialogueBox.loadNewDialogue("mom", jankList[0].content);
                    sound_man.playChatter("mom");
                } else if (jankList[0].id == "24") {
                    db_states('r');
                } else if (jankList[0].id == "25") {
                    db_states('p');
                    playerDialogueBox.loadNewDialogue("player", jankList[0].content);
                } else if (jankList[0].id == "26") {
                    db_states('a');
                    dialogueBox.loadNewDialogue("mom", jankList[0].content);
                    sound_man.playChatter("mom");
                } else if (jankList[0].id == "27") {
                    db_states('p');
                    playerDialogueBox.loadNewDialogue("player", jankList[0].content);
                } else {
                    db_states('a');
                    dialogueBox.loadNewDialogue("dad", "Hey, it's good to have you home.");
                    sound_man.playChatter("dad");

                    SetState("game");
                }
            }
        }

        List<DialogueObj> jankList;
        public void setResponseList(List<DialogueObj> jankList) {
            this.jankList = jankList;
        }

        private void die(string s) {
            dialogueIndex = s;
        }

        public void db_states(char s) {
            switch (s) {
                case 'p': {
                        die("player");
                        playerDialogueBox.active = true;
                        playerDialogueBox.init = true;
                        dialogueBox.init = false;
                        dialogueBox.active = false;
                        break;
                    }
                case 'a': {
                        die("AI");
                        playerDialogueBox.active = false;
                        playerDialogueBox.init = false;
                        dialogueBox.init = true;
                        dialogueBox.active = true;
                        break;
                    }
                case 'r': {
                        die("root");
                        playerDialogueBox.active = false;
                        playerDialogueBox.init = false;
                        dialogueBox.init = true;
                        dialogueBox.active = false;
                        //getGameTimer("game").startTimer();
                        break;
                    }
                default: break;
            }
        }

        //Timer for keeping track of time given to the player
        public GameTimer getGameTimer(string tag) {
            return DictGameTimer[tag];
        }
        public void addTimer(string name, double initTime, Action T) {
            if (DictGameTimer.ContainsKey(name)) {
                DictGameTimer[name] = new GameTimer(name, initTime, T);
            } else {
                DictGameTimer.Add(name, new GameTimer(name, initTime, T));
            }
        }
        public string GetState() {
            return currentState;
        }
        public void SetState(string state) {
            if (state != "menu" && state != "game" && state != "pause" && state != "tutorial") {
                throw new FormatException();
            }
            currentState = state;
            if (state == "tutorial" && currentMenuState == "start") {
                advanceConversation("", null, null);
            }

        }
        public string GetMenuState() {
            return currentMenuState;
        }
        public void SetMenuState(string state) {
            if (state != "start" && state != "settings" && state != "pause") {
                throw new FormatException();
            }
            currentMenuState = state;
        }
        public void updateTimerz() {
            foreach (var pair in DictGameTimer) {
                //pair.Value is a gameTimer
                //pair.Key is the label of the game Timer;
                if (pair.Value.getStart()) {
                    pair.Value.updateTimer();
                } else {
                    if (pair.Value.getCountDown() == 0) {
                        //DO STUFF BEFORE RESTARTING
                        //Process Player dialogue
                        if (pair.Value != null) {
                            pair.Value.doTask();
                        }
                    }
                }
            }
        }
        public void stopTimerz(string key) {
            DictGameTimer[key].stopTimer();
        }
        public void startTimer(string key) {
            DictGameTimer[key].startTimer();
        }
        public void resetTimer(string key) {
            DictGameTimer[key].resetTimer();
        }
        // Handle Menu Traversal and Game Launching
        public void updateMenuState(int[] mouseCoords, List<MenuButton> buttons, List<Tuple<string, string, Task>> mappings) {
            // Loop through current menu's buttons
            for (var i = 0; i < buttons.Count; i++) {
                // If mouse position is over current button
                if (buttons[i].Contains(mouseCoords[0], mouseCoords[1])) {
                    // Find what this button is suppose to do
                    for (var j = 0; j < mappings.Count; j++) {
                        // Found button being clicked
                        if (buttons[i].getMenuButtonContent() == mappings[j].Item1) {
                            // Do button action
                            mappings[j].Item3.Start();
                            // Change either game state or menu state based off of button's target state
                            if (mappings[j].Item2 == "tutorial") {
                                SetState(mappings[j].Item2);
                                //DictGameTimer["game"].startTimer();
                            } else if (mappings[j].Item2 == "menu") {
                                SetState(mappings[j].Item2);
                                SetMenuState("start");
                            } else {
                                SetMenuState(mappings[j].Item2);
                            }
                            break;
                        }
                    }
                    break;
                }
            }
        }
        public void TogglePause() {
            if (GetState() == "pause") {
                SetState("game");
                SetMenuState("start");
            } else if (GetState() == "game") {
                SetState("pause");
                SetMenuState("pause");
            }
        }
    }
}