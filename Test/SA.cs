﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SFML.Graphics;
using SFML.Window;
using SFML.System;
using System.Drawing;

namespace Test {



    class SA : Game {
        public SA() : base(VideoMode.DesktopMode.Width, VideoMode.DesktopMode.Height, "Say Again?", Color.Magenta) {

            window.KeyPressed += onKeyPressed;
            window.KeyReleased += onKeyReleased;
            window.MouseButtonPressed += onMouseButtonPressed;
            window.MouseButtonReleased += onMouseButtonReleased;
            window.MouseMoved += onMouseMoved;

        }

        private void onMouseMoved(object sender, MouseMoveEventArgs e) {
            ManagerOfInput.OnMouseMoved(State, e.X, e.Y);

        }

        private void onMouseButtonReleased(object sender, MouseButtonEventArgs e) {

            ManagerOfInput.onMouseButtonReleased();

            ManagerOfInput.checkTargets(State, Alex, Mom, Dad);

            ui_man.applyTones(e.X, e.Y);
        }

        private void onMouseButtonPressed(object sender, MouseButtonEventArgs e) {

            ManagerOfInput.onMouseButtonPressed(e.X, e.Y);

            ManagerOfInput.GamePlay(State, buttons, e.X, e.Y);

            ManagerOfInput.MenuPlay(State, menus, e.X, e.Y);
        }

        private void onKeyReleased(object sender, KeyEventArgs e) {
        }

        private void onKeyPressed(object sender, KeyEventArgs e) {


            if (e.Code == Keyboard.Key.Space) {
                dialogueBox.setPrintTime(0);
            }

            if (e.Code == Keyboard.Key.N) {
                dialogueBox.checkNext();
            }

            if (e.Code == Keyboard.Key.P) {
                // Toggles game state between game and pause
                State.TogglePause();

            }

            if (e.Code == Keyboard.Key.A || e.Code == Keyboard.Key.M || e.Code == Keyboard.Key.D) {
                init = true;
                dialogueBox.createCharacterDB(e);
            }
        }

        protected override void LoadContent() {

            // Create Character states
            Alex = new CharacterState("alex");
            Mom = new CharacterState("mom");
            Dad = new CharacterState("dad");


            currentMadeMemories.Add("");
            currentMilestones.Add("");

            // Test nums for a "school" context
            double[] nums = { -1, 2, 3, 4,
                               1, 2, 3, 4,
                               1, 2, 3, 4, };
            // Initialize cf to new context
            cf = new ContextFilter("school", nums);



            responseList = s.ChooseDialog(FNC, Load.sampleDialogueObj, currentMadeMemories, currentMilestones, currentTone, currentContext);


            string FirstDialogue = responseList[0].content;
            //Console.WriteLine("First Line: " +FirstDialogue);
            ui_man.produceTextBoxes2(FirstDialogue);


            //player manipulated sentences, 4testing
            string test = "my name is raman! my name is michael. my name is john? my name is jill. my name is yuna. my name is leo. my name is koosha.";
            //ui_man.produceTextBoxes2(test);

            // Create game timers
            State.addTimer("game", 2, new Action(() => { wrapper(); }));

        }

        private void wrapper() {
            //update currentmademeories, currentmilestones, currenttone, currentcontext
            updateCurrents();
            responseList = s.ChooseDialog(FNC, Load.sampleDialogueObj, currentMadeMemories, currentMilestones, currentTone, currentContext);
            ui_man.reset(Alex, Mom, Dad, responseList);
            //ui_man.reset(Alex, Mom, Dad, responseList, currentMadeMemories, Load.sampleDialogueObj);
        }

        //after timer runs out update the current stuff
        private void updateCurrents() {
            if (!responseList.ElementAt(0).nextContext.Equals("")) {
                currentContext = responseList.ElementAt(0).nextContext;
            }
            currentMilestones.Add(responseList.ElementAt(0).consequence);
            currentTone = ui_man.getTone();
            FNC = 0;
        }

        protected override void Initialize() {
            /*Texture texture;
            FileStream f = new FileStream("../../Art/angrymom.png", FileMode.Open);
            texture = new Texture(f);*/
            fullScreenView = window.DefaultView;
            fullScreenView.Viewport = new FloatRect(0, 0, 1, 1);
            window.SetView(fullScreenView);
            dialogueBox = new DialogueBox(0, 0, 710, 150);


            buttons = ui_man.getButtons();
            menus.Add(startMenu); menus.Add(settingsMenu); menus.Add(pauseMenu);
        }

        protected override void Update() {
            if (State.GetState() == "game") {
                // Update the game timerz
                State.updateTimerz();

                // Get the current UI Textboxes from the UI Manager
                var playerDialogues = ui_man.getPlayerDialogues();

                // Get the mouse coordinates from Input Manager
                var MouseCoord = ManagerOfInput.GetMousePos();

                // If the mouse is currently dragging
                if (ManagerOfInput.GetMouseDown()) {
                    // Get tonal buttons from UI Manager
                    var buttons = ui_man.getButtons();

                    // Loop through buttons
                    for (var i = 0; i < buttons.Count; i++) {
                        // Find button currently being interacted with
                        if (buttons[i].GetSelected()) {
                            // Move the button around the screen
                            buttons[i].translate(MouseCoord[0], MouseCoord[1]);

                            // Check collision with UI Textboxes
                            // Loop through UI Textboxes
                            for (var j = 0; j < playerDialogues.Count; j++) {
                                // If the mouse just came from inside a UI Textbox
                                if (playerDialogues[j].wasMouseIn()) {
                                    if (!playerDialogues[j].Contains(MouseCoord[0], MouseCoord[1])) {
                                        // Mouse has now left the UI Textbox so set it to false
                                        playerDialogues[j].setMouseWasIn(false);
                                        // Reset the color to match its previous color
                                        playerDialogues[j].setBoxColor(playerDialogues[j].getBoxColor("prev"));
                                        // Update the rest of the buttons in the cluster
                                        ui_man.updateClusterColors(playerDialogues[j], playerDialogues, playerDialogues[j].getBoxColor("prev"), false);
                                    }

                                    // If mouse just came from outside the UI Textbox
                                } else {
                                    if (playerDialogues[j].Contains(MouseCoord[0], MouseCoord[1])) {
                                        // Mouse is now inside a UI Textbox, so set it to true
                                        playerDialogues[j].setMouseWasIn(true);
                                        // Update previous color to current color of the UI Textbox
                                        playerDialogues[j].setPrevColor(playerDialogues[j].getBoxColor("curr"));
                                        // Update current color to selected tonal button color
                                        playerDialogues[j].setBoxColor(buttons[i].getTonalColor());
                                        // Update the rest of the buttons in the cluster
                                        ui_man.updateClusterColors(playerDialogues[j], playerDialogues, buttons[i].getTonalColor(), true);
                                    }
                                }

                            }

                        }
                    }

                }

            } else if (State.GetState() == "pause") {
                State.getGameTimer("game").PauseTimer();

            }


        }

        protected override void Draw() {
            window.Clear(clearColor);
            if (init) {
                window.Draw(dialogueBox);

            }
            window.SetView(fullScreenView);
            if (State.GetState() == "menu") {
                if (State.GetMenuState() == "start") {
                    window.Draw(startMenu);
                } else {
                    window.Draw(settingsMenu);
                }


            } else {
                //Draw text box background box
                RectangleShape textBackground = new RectangleShape(new Vector2f(SCREEN_WIDTH, SCREEN_HEIGHT / 5));
                textBackground.Position = new Vector2f(0, SCREEN_HEIGHT - (SCREEN_HEIGHT / 5));
                textBackground.FillColor = Color.Black;
                window.Draw(textBackground);

                var dialogues = ui_man.getPlayerDialogues();

                for (var i = 0; i < dialogues.Count; i++) {
                    window.Draw(dialogues[i]);
                }
                var buttons = ui_man.getButtons();

                for (var i = 0; i < buttons.Count; i++) {
                    window.Draw(buttons[i]);
                }

                window.Draw(Alex);
                window.Draw(Mom);
                window.Draw(Dad);

                if (State.GetState() == "pause") {
                    pauseMenu.DrawBG(window);
                    if (State.GetMenuState() == "pause") {
                        window.Draw(pauseMenu);
                    } else if (State.GetMenuState() == "settings") {
                        window.Draw(pauseMenu);
                    }

                }
                window.Draw(State.getGameTimer("game")); //this is the timer circle
            }


        }
    }
}
