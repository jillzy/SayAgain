using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SFML.Graphics;
using SFML.Window;
using SFML.System;
using System.Drawing;
using Newtonsoft.Json;

namespace Test {

    class SA : Game {

        List<string> targets = new List<string>();


        public Character getMom()
        {
            return Mom;
        }

        public Character getDad()
        {
            return Dad;
        }

        public Character getAlexis()
        {
            return Alexis;
        }



        public SA() : base(VideoMode.DesktopMode.Width, VideoMode.DesktopMode.Height, "Say Again?") {
            window.KeyPressed += onKeyPressed;
            window.KeyReleased += onKeyReleased;
            window.MouseButtonPressed += onMouseButtonPressed;
            window.MouseButtonReleased += onMouseButtonReleased;
            window.MouseMoved += onMouseMoved;

        }

        #region screen resize math
        private void screenHelper() {
            var DesktopX = (double)VideoMode.DesktopMode.Width;
            var DesktopY = (double)VideoMode.DesktopMode.Height;
            var WindowX = (double)window.Size.X;
            var WindowY = (double)window.Size.Y;
            scaleFactorX = DesktopX / WindowX;
            scaleFactorY = DesktopY / WindowY;
        }
        #endregion

        private void onMouseMoved(object sender, MouseMoveEventArgs e) {
            ManagerOfInput.OnMouseMoved(State, e.X, e.Y);
            if (State.GetState() == "menu") {
                if (State.GetMenuState() == "start") {
                    startMenu.SweepButtons(e.X, e.Y, scaleFactorX, scaleFactorY);
                } else if (State.GetMenuState() == "settings") {
                    settingsMenu.SweepButtons(e.X, e.Y, scaleFactorX, scaleFactorY);
                }

            } else if (State.GetState() == "game") {
                ui_man.SweepButtons(e.X, e.Y, scaleFactorX, scaleFactorY);
            } else if (State.GetState() == "pause") {
                if (State.GetMenuState() == "pause") {
                    pauseMenu.SweepButtons(e.X, e.Y, scaleFactorX, scaleFactorY);
                } else if (State.GetMenuState() == "settings") {
                    settingsMenu.SweepButtons(e.X, e.Y, scaleFactorX, scaleFactorY);
                }
            }

            ui_man.SweepButtons(e.X, e.Y, scaleFactorX, scaleFactorY);
        }

        private void onMouseButtonReleased(object sender, MouseButtonEventArgs e) {

            ManagerOfInput.onMouseButtonReleased();

            if (playerChoice) {
                //ManagerOfInput.checkTargets(State, D_Man);
                if (D_Man.getAlex().Contains(e.X, e.Y) == true) {
                    currentContext = nextContextDict["Alex"];
                    loadDialogues();
                    playerChoice = false;
                    //COME BACK HERE
                } else if (D_Man.getMom().Contains(e.X, e.Y) == true) {
                    currentContext = nextContextDict["Mom"];
                    loadDialogues();
                    playerChoice = false;
                    //COME BACK HERE
                } else if (D_Man.getDad().Contains(e.X, e.Y) == true) {
                    currentContext = nextContextDict["Dad"];
                    loadDialogues();
                    playerChoice = false;
                    State.getGameTimer("game").resetTimer();
                    State.getGameTimer("game").startTimer();
                    //restart the timer
                    //COME BACK HERE
                }

                //d.getAlex().targetCheck(MouseX, MouseY);
                //d.getMom().targetCheck(MouseX, MouseY);
                //d.getDad().targetCheck(MouseX, MouseY);


                //}

            }
            ui_man.applyTones((int)(e.X * scaleFactorX), (int)(e.Y * scaleFactorY));
        }

        private void onMouseButtonPressed(object sender, MouseButtonEventArgs e) {

            ManagerOfInput.onMouseButtonPressed(e.X, e.Y);

            ManagerOfInput.GamePlay(State, buttons, e.X, e.Y, scaleFactorX, scaleFactorY);

            ManagerOfInput.MenuPlay(State, menus, e.X, e.Y);

            if (State.getGameTimer("game").Contains(e.X, e.Y, scaleFactorX, scaleFactorY) && State.getGameTimer("game").getStart()) {
                State.sound_man.playSFX("button");
                State.getGameTimer("game").setCountDown(0);
            }
        }

        private void onKeyReleased(object sender, KeyEventArgs e) {
        }

        private void onKeyPressed(object sender, KeyEventArgs e) {
            if (State.GetState() == "game") {
                if (e.Code == Keyboard.Key.Space) {


                    // Activate playerDialogueBox to display and be responsive, or switch to AI dialogue
                    if (State.dialogueIndex == "player") {
                        State.advanceConversation(speaker, null, responseListNPC);

                        // Deactivate dialogueBox, Display playerDialogueBox, and submit tone 
                    } else if (State.dialogueIndex == "root") {
                        // Sets the timer to 0 which calls Timer Action to advance the Conversation with the new responseLists

                        if (State.dialogueBox.getAwaitInput() == false && State.dialogueBox.printTime != 0) {
                            State.dialogueBox.printTime = 0;
                        }

                        if (State.getGameTimer("game").getCountDown() != 0.0) {
                            State.getGameTimer("game").setCountDown(0);
                            State.dialogueBox.active = false;
                            State.playerDialogueBox.active = false;
                        }
                        // Activate dialogueBox to display and be responsive, or switch to Root dialogue
                    } else if (State.dialogueIndex == "AI") {
                        //Console.WriteLine("during: dialogue index = ai");

                        State.advanceConversation(speaker, responseList, responseListNPC);
                    } else if (State.dialogueIndex == "interject") {
                        if (State.dialogueBox.getAwaitInput() == true) {
                            //if (State.dialogueBox.checkNext())
                            //{
                            responseListNPC = s.ChooseDialog3(Load.NPCDialogueObj, 1, ncurrid2);

                            if (responseListNPC[0].speaker != "") {
                                speaker = responseListNPC[0].speaker;
                                Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~ Speaker:" + speaker);

                            }
                            int temp1 = Int32.Parse(ncurrid2);
                            temp1++;
                            ncurrid2 = temp1.ToString();

                            State.advanceConversation(speaker, responseList, responseListNPC);
                            //}
                        } else if (State.dialogueBox.getAwaitInput() == false && State.dialogueBox.printTime != 0) {
                            State.dialogueBox.printTime = 0;
                        }
                    }

                    //Console.WriteLine("~~~~~~~~~~~~~~~~~~~~ INDEX RIGHT AFTER ADVANCE CONVERSATION IN TIMERACTION: " + State.dialogueIndex);

                }

                #region button to apply tones
                if (State.getGameTimer("game").getStart()) {
                    if (e.Code == Keyboard.Key.Num1) ui_man.applyToneShortcut(buttons[0]);
                    else if (e.Code == Keyboard.Key.Num2) ui_man.applyToneShortcut(buttons[1]);
                    else if (e.Code == Keyboard.Key.Num3) ui_man.applyToneShortcut(buttons[2]);
                    else if (e.Code == Keyboard.Key.Num4) ui_man.applyToneShortcut(buttons[3]);
                }
                #endregion

                if (e.Code == Keyboard.Key.P) {
                    // Toggles game state between game and pause
                    State.TogglePause();
                }
            }
        }

        #region Timer Action Placeholder
        public void TimerAction() {
            updateTargetFNC();
            //update currentmademeories, currentmilestones, currenttone, currentcontext
            currentTone = ui_man.getTone();
            if (currentTone != tone.Root) updateCurrents(); //updates everything besides FNC
            loadDialogues();
        }
        #endregion

        public string getSpeaker()
        {
            return speaker;
        }

        public void getTargets()
        {
            /*Console.WriteLine("never prints this");
            targets = Load.NPCDialogueObj.r.Dialogues.ElementAt(0).target;
            Console.WriteLine(targets[0]);
            return targets;*/
            Console.WriteLine(Load.NPCDialogueObj.r.Dialogues.ElementAt(0).target.ToString());
            
            Console.WriteLine("fuck u");
        }
        public void updateTargetFNC() {

            //load tonal matrix
            //get targets from player
            //get context
            //load context matrix
            //meth;
              }

        #region update currents

        string pcurrid = "1";
        string ncurrid = "1";
        string ncurrid2 = "1";


        //after timer runs out update the current stuff
        private void updateCurrents() {
            int temp2 = Int32.Parse(pcurrid);
            int temp1 = Int32.Parse(ncurrid);

            temp2++;
            if (temp2 % 2 == 0 && temp2 > 2) {
                temp1++;
            }

            ncurrid = temp1.ToString();
            pcurrid = temp2.ToString();

            if (responseList.ElementAt(0).next != "") pcurrid = responseList.ElementAt(0).next;
            if (responseListNPC.ElementAt(0).next != "") ncurrid = responseListNPC.ElementAt(0).next;
        }
        #endregion

        string speaker = "dad";

        #region load dialogue new
        public void loadDialogues() {
            if (currentTone != tone.Root) {
                // Load playerDialogueBox with the new content from responseList
                State.playerDialogueBox.loadNewDialogue("player", responseList[0].content);

                // Update response Lists with the recently used tone
                responseList = s.ChooseDialog(Load.playerexpo, pcurrid, currentTone.ToString());
                if (sman.testPlotPoint(sman.getDialogueType())) {
                    //Console.WriteLine("hello babby");
                    Load.NPCDialogueObj = Load.dadp;
                    responseListNPC = s.ChooseDialog2(Load.NPCDialogueObj, sman.getCurrentNode(), ncurrid);
                    if (responseListNPC[0].finished == "fin") sman.setTypeTransition();
                } else {
                    Load.NPCDialogueObj = Load.npcexpo;
                    var rnd = new Random();
                    //Console.WriteLine("por que: " + ncurrid);
                    //responseListNPC = s.ChooseDialog3(Load.NPCDialogueObj, (double)(rnd.Next(0, 2)), ncurrid);
                    responseListNPC = s.ChooseDialog3(Load.NPCDialogueObj, 1, ncurrid2);
                    int temp1 = Int32.Parse(ncurrid2);
                    temp1++;
                    ncurrid2 = temp1.ToString();
                    

                }

                if (responseListNPC[0].speaker != "") {
                    speaker = responseListNPC[0].speaker;

                }

                State.playerDialogueBox.loadNewDialogue("player", responseList[0].content);
                State.advanceConversation(speaker, responseList, responseListNPC);

                updateCurrents();

                responseList = s.ChooseDialog(Load.playerexpo, pcurrid, tone.Root.ToString());

                ui_man.reset(responseList);
            } else {
                State.getGameTimer("game").resetTimer();
                State.getGameTimer("game").startTimer();

            }
        }
        #endregion




        protected override void Initialize() {
            sman = new StoryManager();
            ///sneaky deeky/

            ///

            backwall = new Sprite(new Texture("../../Art/UI_Art/buttons n boxes/backwall.png"));
            flower = new Sprite(new Texture("../../Art/UI_Art/buttons n boxes/flowershadow.png"));
            lamp = new Sprite(new Texture("../../Art/UI_Art/buttons n boxes/lamp.png"));
            pictures = new Sprite(new Texture("../../Art/UI_Art/buttons n boxes/pictures.png"));

            table = new Sprite(new Texture("../../Art/UI_Art/buttons n boxes/newletable.png"));
            wallWindow = new Sprite(new Texture("../../Art/UI_Art/buttons n boxes/window.png"));

            backwall.Scale = new Vector2f(SCREEN_WIDTH / backwall.GetGlobalBounds().Width, SCREEN_HEIGHT / backwall.GetGlobalBounds().Height);
            flower.Scale = new Vector2f((float)((SCREEN_WIDTH / flower.GetGlobalBounds().Width) * 0.8), (float)((SCREEN_HEIGHT / flower.GetGlobalBounds().Height) * 0.8));
            lamp.Scale = new Vector2f(SCREEN_WIDTH / lamp.GetGlobalBounds().Width, SCREEN_HEIGHT / lamp.GetGlobalBounds().Height);
            pictures.Scale = new Vector2f(SCREEN_WIDTH / pictures.GetGlobalBounds().Width, SCREEN_HEIGHT / pictures.GetGlobalBounds().Height);
            table.Scale = new Vector2f(SCREEN_WIDTH / table.GetGlobalBounds().Width, SCREEN_HEIGHT / table.GetGlobalBounds().Height);
            wallWindow.Scale = new Vector2f(SCREEN_WIDTH / wallWindow.GetGlobalBounds().Width, SCREEN_HEIGHT / wallWindow.GetGlobalBounds().Height);

            table.Position = new Vector2f(0, (float)(SCREEN_HEIGHT * -0.15));
            flower.Position = new Vector2f((SCREEN_WIDTH / 2) - (flower.GetGlobalBounds().Width / 2), 0);

            toneBar = new Sprite(new Texture("../../Art/UI_Art/buttons n boxes/tonebar.png"));
            toneBar.Position = new Vector2f(6, (float)(SCREEN_HEIGHT * 0.735));
            toneBar.Scale = new Vector2f(SCREEN_WIDTH / 1920, SCREEN_HEIGHT / 1080);

            textBackground = new RectangleShape(new Vector2f(SCREEN_WIDTH, SCREEN_HEIGHT / 5));
            textBackground.Position = new Vector2f(0, SCREEN_HEIGHT - (float)(SCREEN_HEIGHT * 0.19));
            textBackground.FillColor = new Color(67, 65, 69);
            textBackground.OutlineColor = Color.White;
            textBackground.OutlineThickness = 2;

            //Originally in LoadContent/////////////////////////////////////////////////////////////////////////////////
            // Create Character states

            responseList = s.ChooseDialog(Load.playerexpo, pcurrid, currentTone.ToString());
            responseListNPC = s.ChooseDialog(Load.NPCDialogueObj, ncurrid, currentTone.ToString());

            ui_man.produceTextBoxes(responseList[0].content);
            //timeflag
            State.addTimer("game", 10, new Action(() => { TimerAction(); }));
            State.addTimer("cursor", 1, null);
            /////////////////////////////////////////////////////////////////////////////////////////////////////////

            fullScreenView = window.DefaultView;
            fullScreenView.Viewport = new FloatRect(0, 0, 1, 1);
            window.SetView(fullScreenView);

            buttons = ui_man.getButtons();
            menus.Add(startMenu); menus.Add(settingsMenu); menus.Add(pauseMenu);


            Mom = new Mom();
            Mom.setSpriteEmotion(Character.spriteEmotion.happy);
            Mom.active(true);
            Mom.state.setMood(5f);
            Mom.setTalking(true);
            //Console.WriteLine(Mom.state.getMood());

            Alexis = new Alex();
            Alexis.setSpriteEmotion(Character.spriteEmotion.angry);
            Alexis.active(true);

            Dad = new Dad();
            Dad.setSpriteEmotion(Character.spriteEmotion.happy);
            Dad.active(true);
            Dad.setTalking(true);

            Arm = new Arm();
            Arm.setSpriteEmotion(Character.spriteEmotion.neutral);
            Arm.setArmPosition(Dad.getArmPosition());
            Arm.active(true);

        }

        private void LoadInitialPreReqs() {

            currentMadeMemories.Add("");

            currentMilestones.Add("");

            currentContext = "AlexTalksPlayer";

            FNC = 0;
        }
        protected override void Update() {




            screenHelper();

            State.sound_man.soundUpdate(settingsMenu.getSoundToggle());
            if (State.GetState() == "game") {

                if (playerChoice && State.getGameTimer("game").getStart()) {
                    State.getGameTimer("game").stopTimer();
                } //jank fix

                // Update the game timerz
                State.updateTimerz();

                // Get the current UI Textboxes from the UI Manager
                var playerDialogues = ui_man.getPlayerDialogues();

                // Get the mouse coordinates from Input Manager
                var MouseCoord = ManagerOfInput.GetMousePos();

                // If the mouse is currently dragging
                if (ManagerOfInput.GetMouseDown()) {

                    // Loop through buttons
                    for (var i = 0; i < buttons.Count; i++) {
                        // Find button currently being interacted with
                        if (buttons[i].GetSelected()) {
                            // Move the button around the screen
                            buttons[i].translate(MouseCoord[0], MouseCoord[1], window.Size.X, window.Size.Y);

                            // Check collision with UI Textboxes
                            // Loop through UI Textboxes
                            for (var j = 0; j < playerDialogues.Count; j++) {
                                // If the mouse just came from inside a UI Textbox
                                if (playerDialogues[j].wasMouseIn()) {
                                    if (!playerDialogues[j].Contains(buttons[i])) {
                                        // Mouse has now left the UI Textbox so set it to false
                                        playerDialogues[j].setMouseWasIn(false);
                                        // Reset the color to match its previous color
                                        playerDialogues[j].setBoxColor(playerDialogues[j].getBoxColor("prev"));
                                        // Update the rest of the buttons in the cluster
                                        ui_man.updateClusterColors(playerDialogues[j], playerDialogues, playerDialogues[j].getBoxColor("prev"), false);

                                    }

                                    // If mouse just came from outside the UI Textbox
                                } else {
                                    if (playerDialogues[j].Contains(buttons[i])) {
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
        //Ensures that AI dialogue doesnt get loaded more than once per timer done

        bool playerChoice = false;

        protected override void Draw() {

            window.Clear(clearColor);

            window.SetView(fullScreenView);
            if (State.GetState() == "menu") {
                if (State.GetMenuState() == "start") {
                    window.Draw(startMenu);
                } else {
                    window.Draw(settingsMenu);
                }
            } else {
                window.Draw(backwall);

                window.Draw(wallWindow);
                window.Draw(pictures);
                window.Draw(lamp);
                window.Draw(Mom);
                window.Draw(Alexis);
                window.Draw(Dad);
                window.Draw(table);
                window.Draw(Arm);
                window.Draw(flower);

                //Draw text box background box

                var dialogues = ui_man.getPlayerDialogues();

                var buttons = ui_man.getButtons();

                if (!State.dialogueBox.active) {
                    window.Draw(textBackground); // Account for fixed height of player dialogue box (makes sure there isnt a gap below the PDB)
                    window.Draw(State.playerDialogueBox);
                }
                if (!State.playerDialogueBox.active) {
                    window.Draw(State.dialogueBox);
                }

                if (!State.playerDialogueBox.active && !State.dialogueBox.active) {

                    window.Draw(textBackground);

                    for (var i = 0; i < dialogues.Count; i++) {
                        window.Draw(dialogues[i]);
                    }
                    if (State.dialogueIndex != "player") window.Draw(toneBar);
                    for (var i = 0; i < buttons.Count; i++) {
                        window.Draw(buttons[i]);
                    }
                    window.Draw(State.getGameTimer("game")); //this is the speak button
                }
                if (playerChoice) {
                    window.Draw(D_Man);
                }

                if (State.GetState() == "pause") {

                    pauseMenu.DrawPauseBG(window);
                    if (State.GetMenuState() == "pause") {
                        window.Draw(pauseMenu);
                    } else if (State.GetMenuState() == "settings") {
                        window.Draw(settingsMenu);

                    }

                }

                if (debugInfo) {
                    Text AI_DB = new Text("LoadAIOnce: "  + "\n" +
                                          "AI_DB - animStart: " + State.dialogueBox.getAnimationStart() + "\n" +
                                          "        awaitInput: " + State.dialogueBox.getAwaitInput() + "\n" +
                                          "        dialoguePanesLength: " + State.dialogueBox.dialoguePanes.Count + "\n" +
                                          "        init: " + State.dialogueBox.init + "\n" +
                                          "        active: " + State.dialogueBox.active, new Font("../../Art/UI_Art/fonts/ticketing/TICKETING/ticketing.ttf"), 20);

                    Text P_DB = new Text("P_DB - animStart: " + State.playerDialogueBox.getAnimationStart() + "\n" +
                                          "        awaitInput: " + State.playerDialogueBox.getAwaitInput() + "\n" +
                                          "        dialoguePanesLength: " + State.playerDialogueBox.dialoguePanes.Count + "\n" +
                                          "        init: " + State.playerDialogueBox.init + "\n" +
                                          "        active: " + State.playerDialogueBox.active, new Font("../../Art/UI_Art/fonts/ticketing/TICKETING/ticketing.ttf"), 20);
                    AI_DB.Position = new Vector2f(SCREEN_WIDTH - (AI_DB.GetGlobalBounds().Width + 50), 50);
                    P_DB.Position = new Vector2f(SCREEN_WIDTH - (P_DB.GetGlobalBounds().Width + 50), 200);
                    AI_DB.Color = Color.White;
                    P_DB.Color = Color.White;
                    window.Draw(AI_DB);
                    window.Draw(P_DB);
                }
            }


        }


    }
}