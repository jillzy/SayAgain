﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Test {
    class InputManager {
        public InputManager() {

        }

        //fields
        int MouseX;
        int MouseY;
        bool MouseDown = false;
        bool MouseRelease = false;
        bool MouseMove = false;

        //check game and mouse pressed->set position
        public void MouseMoveCheck(string state, int x, int y) {
            if (state == "game") {
                if (this.GetMouseDown()) {
                    this.SetMouseMove(true);
                    this.SetMousePos(x, y);
                }
            }
        }

        public void PKeyCheck(GameState State) {
            if (State.GetState() == "pause") {
                State.SetState("game");
                State.SetMenuState("start");
            } else if (State.GetState() == "game") {
                State.SetState("pause");
                State.SetMenuState("pause");
            }
        }

        public void MouseReleasedCheck(string state, UIManager ui, ToneEffects tfx, ContextFilter cf) {
            this.SetMouseMove(false);
            this.SetMouseDown(false);
            this.SetMouseRelease(true);

            var MouseCoord = this.GetMousePos();
            var buttons = ui.getButtons();

            if (state == "game") {
                for (var i = 0; i < buttons.Count; i++) {
                    if (buttons[i].GetSelected()) {
                        //CHECK MATRIX BS
                        double[,] final = tfx.MatrixMult(tfx, cf);
                        //Console.WriteLine(final[2, 3]);


                        var playerDialogues = ui.getPlayerDialogues();

                        for (var j = 0; j < playerDialogues.Count; j++) {

                            var boxBounds = playerDialogues[j].getBoxBounds();
                            //change color if the button is hovering over the textbox

                            if (playerDialogues[j].Contains(MouseCoord[0], MouseCoord[1])) {
                                playerDialogues[j].setPrevColor(playerDialogues[j].getBoxColor("curr"));
                                playerDialogues[j].setBoxColor(buttons[i].getTonalColor());

                                playerDialogues[j].changeDialogue(buttons[i].getNewDialogue());
                                playerDialogues[j].setAffected(true);
                                ui.updateText(j, buttons[i].getNewDialogue());
                                break;
                            }

                        }
                        buttons[i].snapBack();
                        buttons[i].SetSelected(false);
                        break;

                    }
                }
            } else if (state == "pause")//If game is paused
            {
                for (var i = 0; i < buttons.Count; i++) {
                    if (buttons[i].GetSelected()) {
                        buttons[i].snapBack();
                        buttons[i].SetSelected(false);
                    }
                }
            }


        }

        public void MouseClickedCheck(GameState State, UIManager ui, StartMenu sta, StartMenu pau, StartMenu set, int x, int y) {
            this.SetMousePos(x, y);
            this.SetMouseRelease(false);
            this.SetMouseDown(true);

            if (State.GetState() == "game") {
                var buttons = ui.getButtons();
                for (var i = 0; i < buttons.Count; i++) {
                    if (buttons[i].Contains(x, y)) {
                        var bounds = buttons[i].getRectBounds();
                        buttons[i].SetMouseOffset(x - (int)bounds.Left, y - (int)bounds.Top);
                        buttons[i].SetSelected(true);
                    }
                }

                this.SetMousePos(x, y);
                this.SetMouseRelease(false);
                this.SetMouseDown(true);
            } else if (State.GetState() == "menu") {
                // Menu Traversal Logic
                if (State.GetMenuState() == "start") //If Current Menu State is the Start Menu
                {
                    // Pass the current menu's buttons, along with a list of tuples symbolizing:
                    //      Tuple(ButtonText, TargetState, AnonymousFunction)
                    updateMenuState(State, sta.getMenuButtons(), new List<Tuple<string, string, Task>> {
                        new Tuple<string, string, Task>("Game Start", "game", new Task(() => {})),
                        new Tuple<string, string, Task>("Settings", "settings", new Task(() => {}))
                    });

                } else if (State.GetMenuState() == "settings") //If Current Menu State is the Settings Menu
                  {
                    updateMenuState(State, set.getMenuButtons(), new List<Tuple<string, string, Task>> {
                        new Tuple<string, string, Task>("8K GAMING", "settings", new Task(() => {})),
                        new Tuple<string, string, Task>("<- Back", "start", new Task(() => {}))
                    });

                }
            } else if (State.GetState() == "pause") {
                Console.WriteLine(State.GetState());
                if (State.GetMenuState() == "pause") {
                    updateMenuState(State, pau.getMenuButtons(), new List<Tuple<string, string, Task>> {
                        new Tuple<string, string, Task>("Back to Game", "game", new Task(() => {})),
                        new Tuple<string, string, Task>("Settings", "settings", new Task(() => {})),
                        new Tuple<string, string, Task>("Quit", "menu", new Task(() => {}))
                    });
                } else if (State.GetMenuState() == "settings") {
                    updateMenuState(State, set.getMenuButtons(), new List<Tuple<string, string, Task>> {
                        new Tuple<string, string, Task>("8K GAMING", "settings", new Task(() => {})),
                        new Tuple<string, string, Task>("<- Back", "pause", new Task(() => {}))
                    });
                }
            }
        }

        // Handle Menu Traversal and Game Launching
        private void updateMenuState(GameState State, List<MenuButton> buttons, List<Tuple<string, string, Task>> mappings) {
            // Get Mouse Position
            var MousePos = this.GetMousePos();

            // Loop through current menu's buttons
            for (var i = 0; i < buttons.Count; i++) {
                // If mouse position is over current button
                if (buttons[i].Contains(MousePos[0], MousePos[1])) {
                    // Find what this button is suppose to do
                    for (var j = 0; j < mappings.Count; j++) {
                        // Found button being clicked
                        if (buttons[i].getMenuButtonText().DisplayedString == mappings[j].Item1) {
                            // Do button action
                            mappings[j].Item3.Start();

                            // Change either game state or menu state based off of button's target state
                            if (mappings[j].Item2 == "game") {
                                State.SetState(mappings[j].Item2);
                            } else if (mappings[j].Item2 == "menu") {
                                State.SetState(mappings[j].Item2);
                                State.SetMenuState("start");
                            } else {
                                State.SetMenuState(mappings[j].Item2);
                            }

                            break;
                        }
                    }
                    break;

                }
            }
        }





        /////////////////////////////////////////////////BUILT-IN
        private void SetMousePos(int x, int y) {
            MouseX = x;
            MouseY = y;
        }

        public int[] GetMousePos() {
            return new int[] { MouseX, MouseY };
        }

        private void SetMouseDown(bool value) {
            MouseDown = value;
        }

        public bool GetMouseDown() {
            return MouseDown;
        }

        private void SetMouseRelease(bool value) {
            MouseRelease = value;
        }

        private bool GetMouseRelease() {
            return MouseRelease;
        }

        private void SetMouseMove(bool value) {
            MouseMove = value;
        }

        private bool GetMouseMove() {
            return MouseMove;
        }

        private void printMouseStuff() {
            Console.WriteLine(MouseDown + ", " + MouseMove + ", " + MouseRelease);
        }

        private bool CheckCollision(FloatRect bounds) {
            if (MouseX >= bounds.Left && MouseX <= bounds.Left + bounds.Width && MouseY >= bounds.Top && MouseY <= bounds.Top + bounds.Height) {
                return true;
            }
            return false;
        }
        //////////////////////////////////////////////////////////////////////BUILT-IN
    }
}
