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
        //public static SA current;

        public Character Mom, Dad, Alexis;
        public View fullScreenView, charView;
        //Character mom, alexis, dad;



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

        //if (D_Man.getAlex().Contains(e.X, e.Y)) {
        //    D_Man.getAlex().setHover(true);
        //} else if (D_Man.getMom().Contains(e.X, e.Y)) {
         //   D_Man.getMom().setHover(true);
        //} else if (D_Man.getDad().Contains(e.X, e.Y)) {
        //    D_Man.getDad().setHover(true);
        //} else {
        //    D_Man.getAlex().setHover(false);
        //    D_Man.getMom().setHover(false);
        //    D_Man.getDad().setHover(false);
        //}

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
                    if (dialogueBox.printTime != 0 && dialogueBox.getAnimationStart() && !dialogueBox.getAwaitInput())
                    {
                        dialogueBox.setPrintTime(0);
                    }
                    else {
                        dialogueBox.acknowledge();
                    }

                    if (playerDialogueBox.printTime != 0 && playerDialogueBox.getAnimationStart() && !playerDialogueBox.getAwaitInput())
                    {
                        playerDialogueBox.setPrintTime(0);
                    }
                    else
                    {
                        playerDialogueBox.acknowledge();
                    }
                }

                if (e.Code == Keyboard.Key.N) {
                    dialogueBox.checkNext();
                }

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

        public void updateTargetFNC() {

            //load tonal matrix
            //get targets from player
            //get context
            //load context matrix
            //meth;
        }

        string pcurrid = "1";
        string ncurrid = "1";

        //after timer runs out update the current stuff
        private void updateCurrents() {
            int temp2 = Int32.Parse(pcurrid);
            int temp1 = Int32.Parse(ncurrid);

            temp2++;
            if (temp2 % 2 == 0 && temp2 > 2)
            {
                temp1++;
            }

            ncurrid = temp1.ToString();
            pcurrid = temp2.ToString();

            if (responseList.ElementAt(0).next != "") pcurrid = responseList.ElementAt(0).next;
            if (responseListNPC.ElementAt(0).next != "") ncurrid = responseListNPC.ElementAt(0).next;
        }
        #endregion



        public void loadDialogues() {

            if (currentTone != tone.Root)
            {

                responseList = s.ChooseDialog(Load.playerDialogueObj1, pcurrid, currentTone.ToString());
                responseListNPC = s.ChooseDialog(Load.NPCDialogueObj, ncurrid, currentTone.ToString());
                ui_man.dialogueLoadOrder(State, playerDialogueBox, dialogueBox, responseList, responseListNPC, playerChoice);
                loadedAIDialogueOnce = true;

                updateCurrents();

                responseList = s.ChooseDialog(Load.playerDialogueObj1, pcurrid, tone.Root.ToString());

                ui_man.reset(responseList);

            }
            else
            {

                State.getGameTimer("game").resetTimer();
                State.getGameTimer("game").startTimer();

            }
        }


        protected override void Initialize() {
            sman = new StoryManager();
            sman.print();

            backwall = new Sprite(new Texture("../../Art/UI_Art/buttons n boxes/backwall.png"));
            flower = new Sprite(new Texture("../../Art/UI_Art/buttons n boxes/flower.png"));
            lamp = new Sprite(new Texture("../../Art/UI_Art/buttons n boxes/lamp.png"));
            pictures = new Sprite(new Texture("../../Art/UI_Art/buttons n boxes/pictures.png"));
            table = new Sprite(new Texture("../../Art/UI_Art/buttons n boxes/table.png"));
            
            backwall.Scale = new Vector2f(SCREEN_WIDTH / backwall.GetGlobalBounds().Width, SCREEN_HEIGHT / backwall.GetGlobalBounds().Height);
            flower.Scale = new Vector2f(SCREEN_WIDTH / flower.GetGlobalBounds().Width, SCREEN_HEIGHT / flower.GetGlobalBounds().Height);
            lamp.Scale = new Vector2f(SCREEN_WIDTH / lamp.GetGlobalBounds().Width, SCREEN_HEIGHT / lamp.GetGlobalBounds().Height);
            pictures.Scale = new Vector2f(SCREEN_WIDTH / pictures.GetGlobalBounds().Width, SCREEN_HEIGHT / pictures.GetGlobalBounds().Height);
            table.Scale = new Vector2f(SCREEN_WIDTH / table.GetGlobalBounds().Width, SCREEN_HEIGHT / table.GetGlobalBounds().Height);

            table.Position = new Vector2f(0, -200);
            flower.Position = new Vector2f(0, -200);

            toneBar = new Sprite(new Texture("../../Art/UI_Art/buttons n boxes/tonebar.png"));
            toneBar.Position = new Vector2f(6,(float)(SCREEN_HEIGHT*0.735));
            toneBar.Scale = new Vector2f(SCREEN_WIDTH/1920, SCREEN_HEIGHT/1080);

            textBackground = new RectangleShape(new Vector2f(SCREEN_WIDTH, SCREEN_HEIGHT / 5));
            textBackground.Position = new Vector2f(0, SCREEN_HEIGHT - (float)(SCREEN_HEIGHT * 0.19));
            textBackground.FillColor = new Color(67, 65, 69);
            textBackground.OutlineColor = Color.White;
            textBackground.OutlineThickness = 2;

            //Originally in LoadContent/////////////////////////////////////////////////////////////////////////////////
            // Create Character states

            responseList = s.ChooseDialog(Load.playerDialogueObj1, pcurrid, currentTone.ToString());
            responseListNPC = s.ChooseDialog(Load.NPCDialogueObj, ncurrid, currentTone.ToString());

            string FirstDialogue = responseList[0].content;
            ui_man.produceTextBoxes2(FirstDialogue);
            //timeflag
            State.addTimer("game", 10, new Action(() => { TimerAction(); }));
            /////////////////////////////////////////////////////////////////////////////////////////////////////////

            fullScreenView = window.DefaultView;
            fullScreenView.Viewport = new FloatRect(0, 0, 1, 1);
            window.SetView(fullScreenView);

            dialogueBox = new DialogueBox(State, "AI");
            //dialogueBox.loadNewDialogue("dad", "Hey! It’s great having you back home.");

            playerDialogueBox = new DialogueBox(State, "PLAYER");

            buttons = ui_man.getButtons();
            menus.Add(startMenu); menus.Add(settingsMenu); menus.Add(pauseMenu);


            Mom = new Mom();
            Mom.setSpriteEmotion(Character.spriteEmotion.happy);
            Mom.active(true);
            Mom.state.setMood(5f);
            Console.WriteLine(Mom.getCurrentFNC());

            Alexis = new Alex();
            Alexis.setSpriteEmotion(Character.spriteEmotion.angry);
            Alexis.active(true);

            Dad = new Dad();
            Dad.setSpriteEmotion(Character.spriteEmotion.happy);
            Dad.active(true);
        }

        private void LoadInitialPreReqs() {

            currentMadeMemories.Add("");

            currentMilestones.Add("");


            currentContext = "AlexTalksPlayer";

            FNC = 0;
        }
        bool yolo = true;
        protected override void Update() {
            screenHelper();
            if (State.GetState() == "game" && yolo == true) {
                dialogueBox.loadNewDialogue("dad", "Hey! It’s great having you back home.");
                yolo = false;
            }
            State.sound_man.soundUpdate(settingsMenu.getSoundToggle());
            if (State.GetState() == "game") {
                //if (startOnce) {
                //    State.getGameTimer("game").startTimer();
                //    startOnce = false;
                //}

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
                                    if (!playerDialogues[j].Contains((int)(MouseCoord[0]*scaleFactorX), (int)(MouseCoord[1]*scaleFactorY))) {
                                        // Mouse has now left the UI Textbox so set it to false
                                        playerDialogues[j].setMouseWasIn(false);
                                        // Reset the color to match its previous color
                                        playerDialogues[j].setBoxColor(playerDialogues[j].getBoxColor("prev"));
                                        // Update the rest of the buttons in the cluster
                                        ui_man.updateClusterColors(playerDialogues[j], playerDialogues, playerDialogues[j].getBoxColor("prev"), false);

                                    }

                                    // If mouse just came from outside the UI Textbox
                                } else {
                                    if (playerDialogues[j].Contains((int)(MouseCoord[0] * scaleFactorX), (int)(MouseCoord[1] * scaleFactorY))) {
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
                // ui_man.dialogueLoadOrder(State, playerDialogueBox, dialogueBox, responseList, responseListAlex);
                if (playerDialogueBox.getAnimationStart() == false && loadedAIDialogueOnce == true && playerDialogueBox.getAwaitInput() == false) {
                    //add another condition to see if the User has pressed space on player dialogue
                    //sound starts on npc dialogue start
                    State.sound_man.playSFX("chatter");

                    if (responseListNPC[0].content != "returned empty string") {
                        dialogueBox.setInit(true);
                        dialogueBox.loadNewDialogue("dad", responseListNPC.ElementAt(0).content); //this makes the timer happen after the animation is done
                        loadedAIDialogueOnce = false;
                        if (dialogueBox.getAnimationStart() == false) {
                            loadedAIDialogueOnce = true;
                        }
                    }

                }

            } else if (State.GetState() == "pause") {
                State.getGameTimer("game").PauseTimer();

            }


        }
        //Ensures that AI dialogue doesnt get loaded more than once per timer done
        bool loadedAIDialogueOnce = false;

        bool playerChoice = false;

        protected override void Draw() {

            window.Clear(clearColor);

            if (State.GetState() == "game")
            {
                dialogueBox.setInit(true);
            }

            window.SetView(fullScreenView);
            if (State.GetState() == "menu") {
                if (State.GetMenuState() == "start") {
                    window.Draw(startMenu);
                } else {
                    window.Draw(settingsMenu);
                }
            } else {
                window.Draw(backwall);
                window.Draw(pictures);
                window.Draw(lamp);
                window.Draw(Mom);
                window.Draw(Alexis);
                window.Draw(Dad);
                window.Draw(table);
                window.Draw(flower);
                
                //Draw text box background box
              

                var dialogues = ui_man.getPlayerDialogues();

               

                var buttons = ui_man.getButtons();

                if (!playerDialogueBox.active && !dialogueBox.active)
                {

                    window.Draw(textBackground);
                    for (var i = 0; i < dialogues.Count; i++)
                    {
                        window.Draw(dialogues[i]);
                    }
                    window.Draw(toneBar);
                    for (var i = 0; i < buttons.Count; i++)
                    {
                        window.Draw(buttons[i]);
                    }
                    window.Draw(State.getGameTimer("game")); //this is the timer circle
                }
                
                if (playerChoice) {
                    window.Draw(D_Man);
                }
                
                if (!dialogueBox.active)
                {
                    window.Draw(playerDialogueBox);
                }
                if (!playerDialogueBox.active)
                {
                    window.Draw(dialogueBox);
                }

                if (State.GetState() == "pause") {

                    pauseMenu.DrawPauseBG(window);
                    if (State.GetMenuState() == "pause") {
                        window.Draw(pauseMenu);
                    } else if (State.GetMenuState() == "settings") {
                        window.Draw(settingsMenu);

                    }

                }
                
            }


        }


    }
}
