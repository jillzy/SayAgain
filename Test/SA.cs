using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using SFML.Graphics;
using SFML.Window;
using SFML.System;
using Newtonsoft.Json;

namespace SayAgain {

    class SA : Game {

        public SA() : base(1920, 1080, "Say Again?") {
            window.KeyPressed += onKeyPressed;
            window.KeyReleased += onKeyReleased;
            window.MouseButtonPressed += onMouseButtonPressed;
            window.MouseButtonReleased += onMouseButtonReleased;
            window.MouseMoved += onMouseMoved;

        }


        public Character getMom() {
            return Mom;
        }

        public Character getDad() {
            return Dad;
        }

        public Character getAlexis() {
            return Alexis;
        }

        #region screen resize math
        private void screenHelper() {
            var DesktopX = (double)1920;
            var DesktopY = (double)1080;
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


            } else if (State.GetState() == "game" || State.GetState() == "tutorial") {
                ui_man.SweepButtons(e.X, e.Y, scaleFactorX, scaleFactorY);
                if (State.tooltip.init) {
                    State.tooltip.hover = State.tooltip.contains((int)(e.X * scaleFactorX), (int)(e.Y * scaleFactorY));
                }
                if (State.dialogueBox.init && State.dialogueBox.awaitInput) {
                    State.dialogueBox.hover = State.dialogueBox.cursorContains((int)(e.X * scaleFactorX), (int)(e.Y * scaleFactorY));
                }
                if (State.playerDialogueBox.init && State.playerDialogueBox.awaitInput) {
                    State.playerDialogueBox.hover = State.playerDialogueBox.cursorContains((int)(e.X * scaleFactorX), (int)(e.Y * scaleFactorY));
                }
            } else if (State.GetState() == "pause") {
                if (State.GetMenuState() == "pause") {
                    pauseMenu.SweepButtons(e.X, e.Y, scaleFactorX, scaleFactorY);
                } else if (State.GetMenuState() == "settings") {
                    settingsMenu.SweepButtons(e.X, e.Y, scaleFactorX, scaleFactorY);
                }
            }

            //ui_man.SweepButtons(e.X, e.Y, scaleFactorX, scaleFactorY);
        }

        private void onMouseButtonReleased(object sender, MouseButtonEventArgs e) {

            ManagerOfInput.onMouseButtonReleased();
            ui_man.applyTones((int)(e.X * scaleFactorX), (int)(e.Y * scaleFactorY), State.tooltip);
        }

        private void onMouseButtonPressed(object sender, MouseButtonEventArgs e) {

            if (State.GetMenuState() == "pause") {
                State.resetGameStuff = new Task(() => { return; });
            }

            ManagerOfInput.onMouseButtonPressed(e.X, e.Y);

            ManagerOfInput.GamePlay(State, buttons, e.X, e.Y, scaleFactorX, scaleFactorY);

            ManagerOfInput.MenuPlay(State, menus, e.X, e.Y);

            if (State.getGameTimer("game").Contains(e.X, e.Y, scaleFactorX, scaleFactorY)) {
                State.sound_man.playSFX("button");
                TimerAction();
                //State.getGameTimer("game").setCountDown(0);
            }

            if (State.dialogueBox.hover) {
                spaceCode();
            }
            if (State.playerDialogueBox.hover) {
                spaceCode();
            }
        }

        bool alexDimmed = false;
        private void onKeyReleased(object sender, KeyEventArgs e) {
            if (e.Code == Keyboard.Key.LControl && State.GetState() == "menu") jankId = "27";
        }


        private void onKeyPressed(object sender, KeyEventArgs e) {
            if (e.Code == Keyboard.Key.Space) {
                spaceCode();
            }
            if (State.GetState() == "game" || State.GetState() == "tutorial") {
                #region button to apply tones
                if (State.playerDialogueBox.active == false && State.dialogueBox.active == false) {
                    if (e.Code == Keyboard.Key.Num1) ui_man.applyToneShortcut(buttons[0], State.tooltip);
                    else if (e.Code == Keyboard.Key.Num2) ui_man.applyToneShortcut(buttons[1], State.tooltip);
                    else if (e.Code == Keyboard.Key.Num3) ui_man.applyToneShortcut(buttons[2], State.tooltip);
                    else if (e.Code == Keyboard.Key.Num4) ui_man.applyToneShortcut(buttons[3], State.tooltip);
                }
                #endregion

                if (e.Code == Keyboard.Key.P) {
                    State.TogglePause();
                }
            }
        }

        private void spaceCode() {
            if (State.GetState() == "menu") State.SetState("tutorial");
            else if (State.GetState() == "game") {
                if (endGame) {
                    if (endGame2) {
                        if (State.dialogueBox.checkNext()) {
                            State.playerDialogueBox.loadNewDialogue("player", "Thanks for playing Say Again <Credits:> <Jill Yeung - Designer, John Chau - Developer> <Leo Gomez - Producer, Raman Nakarmi - Tester> <Michael Simpson - Test Coordinator, Koosha Seyvani - Social Media Coordinator> <Yuna Choe - Artist Coordinator, Vernon Wong - Writer> <Allayne Low - Artist, Lyn Kline - Artist, Courtney Chavez - Artist> <END> ");
                            State.playerDialogueBox.active = true;
                            State.playerDialogueBox.init = true;
                            State.dialogueBox.active = false;
                            State.dialogueBox.init = false;
                            State.playerDialogueBox.awaitInput = false;
                            fadeFlag = "in";
                            endGame2 = false;
                        }
                    } else { State.playerDialogueBox.checkNext(); return; }
                } else {
                    if (State.dialogueIndex == "player") {
                        State.advanceConversation(speaker, responseList, responseListNPC);


                    } else if (State.dialogueIndex == "root") {
                        revertEmotion(Mom); revertEmotion(Dad); revertEmotion(Alexis);

                        if (State.dialogueBox.getAwaitInput() == false && State.dialogueBox.printTime != 0) {
                            State.dialogueBox.printTime = 0;
                        }
                        if (State.getGameTimer("game").getCountDown() != 0.0) {
                            State.getGameTimer("game").setCountDown(0);
                            //TimerAction();
                            State.dialogueBox.active = false;
                            State.playerDialogueBox.active = false;
                        }

                        // Activate dialogueBox to display and be responsive, or switch to Root dialogue
                    } else if (State.dialogueIndex == "AI") {
                        State.advanceConversation(speaker, responseList, responseListNPC);
                        if (responseListNPC[0].finished == "finGame") endGame = true;

                    } else if (State.dialogueIndex == "interject") {
                        if (State.dialogueBox.getAwaitInput() == true) {
                            if (State.dialogueBox.checkNext()) {
                                if (responseListNPC[0].inext != "") speaker = responseListNPC[0].inext;

                                if (sman.getDialogueType() == "plotpoint") {
                                    responseListNPC = s.ChooseDialogPlot(Load.allp, sman.getCurrentNode(), npcPlotId, pTone);
                                    npcPlotId = incr(npcPlotId);
                                    playerPlotId = linkId(npcPlotId);
                                } else {
                                    responseListNPC = s.ChooseDialogTransition(Load.allt, bucket, npcTransitionId, currentTone.ToString());
                                    npcTransitionId = incr(npcTransitionId);
                                    playerTransitionId = linkId(npcTransitionId);
                                }

                                RES(responseListNPC);

                                State.advanceConversation(speaker, responseList, responseListNPC);
                                affect(responseListNPC[0].target, responseListNPC[0].FNC);
                                checkFades();
                                checkPlotChange();
                                resetCharacterFNC();
                                reRootPlayer();

                            }
                        } else if (State.dialogueBox.getAwaitInput() == false && State.dialogueBox.printTime != 0) {
                            State.dialogueBox.printTime = 0;
                        }
                    }
                }
            } else if (State.GetState() == "tutorial") {

                if (Int32.Parse(jankId) >= 27) {
                    playerPlotId = "2";
                    resetTransitionId();
                    ui_man.tutorialButtonIndex = 4;
                    ui_man.reset(responseList);
                    fadeFlag = "in";
                    ui_man.TooltipToggle(false, State.tooltip);
                    Mom.setHide(false);
                    Dad.setHide(false);
                    Arm.setHide(false);
                }
                if (State.dialogueIndex == "AI") {
                    if (State.dialogueBox.checkNext()) {

                        jankList = s.chooseJank(Load.Jankson, jankId, currentTone.ToString());
                        State.setResponseList(jankList);

                        State.advanceConversation("", null, null);
                        jankIncr();
                        limitTones();

                    }
                } else if (State.dialogueIndex == "root") {

                    if (State.getGameTimer("game").getCountDown() != 0.0) {
                        TimerAction();
                        //State.getGameTimer("game").setCountDown(0);
                    }

                } else if (State.dialogueIndex == "player") {

                    if (State.playerDialogueBox.checkNext()) {
                        jankList = s.chooseJank(Load.Jankson, jankId, currentTone.ToString());
                        State.setResponseList(jankList);

                        State.advanceConversation("", null, null);
                        jankIncr();
                        limitTones();
                    }

                }
                if (Int32.Parse(jankId) == 4) {
                    fadeFlag = "in";
                } else if (Int32.Parse(jankId) == 12) {
                    fadeFlag = "out";

                } else if (Int32.Parse(jankId) == 13) {
                    Dad.setHide(false);
                    Arm.setHide(false);
                    fadeFlag = "in";
                } else if (Int32.Parse(jankId) == 18) {
                    fadeFlag = "out";

                } else if (Int32.Parse(jankId) == 19) {
                    Mom.setHide(false);
                    fadeFlag = "in";
                }

            }
        }

        private void resetPlotId() {
            playerPlotId = "1";
            npcPlotId = "1";
        }
        private void resetTransitionId() {
            playerTransitionId = "1";
            npcTransitionId = "1";
        }

        private void limitTones() {
            if (jankId == "5") {
                ui_man.tutorialButtonIndex = 0;
                currentTone = tone.Root;
            } else if (jankId == "8") {
                ui_man.tutorialButtonIndex = 1;
                currentTone = tone.Root;
            } else if (jankId == "14") {
                ui_man.tutorialButtonIndex = 2;
                currentTone = tone.Root;
            } else if (jankId == "21") {
                ui_man.tutorialButtonIndex = 3;
                currentTone = tone.Root;
            } else if (Int32.Parse(jankId) > 22) {
                ui_man.tutorialButtonIndex = 4;
                currentTone = tone.Root;
            }


            if (Int32.Parse(jankId) < 27) {
                ui_man.reset(jankList);

            }
        }

        public void jankIncr() {
            int j = Int32.Parse(jankId);
            j++;
            jankId = j.ToString();
        }

        public void TimerAction() {
            currentTone = ui_man.getTone();
            if (currentTone != tone.Root) pTone = currentTone.ToString();
            loadDialogues();
        }

        #region id tracker
        string playerPlotId = "1";
        string playerTransitionId = "1";
        string npcPlotId = "1";
        string npcTransitionId = "1";

        string speaker = "dad";
        string jankId = "1";
        double bucket = 1;
        List<double> pastBuckets = new List<double>();
        string pTone = "";
        #endregion

        #region load dialogue new
        public void loadDialogues() {
            ui_man.rootBackgroundBorder.OutlineColor = ui_man.rootBackground.FillColor;
            if (State.GetState() == "game") {
                if (currentTone != tone.Root) {
                    mainDialogueLoop();
                }
            } else if (State.GetState() == "tutorial") {

                if (currentTone != tone.Root) {
                    State.tooltip.init = false;
                    jankList = s.chooseJank(Load.Jankson, jankId, currentTone.ToString());
                    State.setResponseList(jankList);

                    State.advanceConversation("", null, null);
                    State.getGameTimer("game").resetTimer();
                    jankIncr();
                } else {
                    State.getGameTimer("game").resetTimer();
                }
            }
        }
        #endregion

        private void mainDialogueLoop() {
            if (sman.getDialogueType() == "plotpoint") {
                responseList = s.ChooseDialogPlot(Load.newplayerp, sman.getCurrentNode(), playerPlotId, currentTone.ToString());
                responseListNPC = s.ChooseDialogPlot(Load.allp, sman.getCurrentNode(), npcPlotId, currentTone.ToString());
                updateIds(0);

                if (responseListNPC[0].finished == "fin") sman.setTypeTransition();
            } else {
                responseList = s.ChooseDialogTransition(Load.newplayert, bucket, playerTransitionId, currentTone.ToString());
                responseListNPC = s.ChooseDialogTransition(Load.allt, bucket, npcTransitionId, currentTone.ToString());
                updateIds(1);
            }

            int pickNPC = rnd.Next(0, responseListNPC.Count);
            int pickPlayer = rnd.Next(0, responseList.Count);

            if (responseListNPC[pickNPC].speaker != "") speaker = responseListNPC[pickNPC].speaker;
            State.playerDialogueBox.loadNewDialogue("player", responseList[pickPlayer].content);
            State.advanceConversation(speaker, responseList, responseListNPC);

            affect(responseList[pickPlayer].target, responseList[pickPlayer].FNC);
            affect(responseListNPC[pickNPC].target, responseListNPC[pickNPC].FNC);

            //debug RES
            //RES(responseList);
            //RES(responseListNPC);

            checkFades();
            checkPlotChange();
            resetCharacterFNC();
            reRootPlayer();
        }

        private void reRootPlayer() {
            Console.WriteLine(npcPlotId);
            Console.WriteLine(playerPlotId + " |||||| " + linkId(npcPlotId));
            Console.WriteLine(sman.getCurrentNode());
            Console.WriteLine(State.dialogueIndex);
            Console.WriteLine();

            currentTone = tone.Root;

            if (sman.getDialogueType() == "plotpoint") {
                responseList = s.ChooseDialogPlot(Load.newplayerp, sman.getCurrentNode(), playerPlotId, currentTone.ToString());
                playerPlotId = incr(playerPlotId);
            } else {
                responseList = s.ChooseDialogTransition(Load.newplayert, bucket, playerTransitionId, currentTone.ToString());
                playerTransitionId = incr(playerTransitionId);
            }

            ui_man.reset(responseList);
        }

        private string incr(string s) {
            int temp1 = Int32.Parse(s);
            temp1++;
            return temp1.ToString();
        }

        private void RES(List<DialogueObj> LDO) {
            if (LDO[0].content == "...") {
                Console.WriteLine("playerid: " + playerPlotId);
                Console.WriteLine("npcid: " + npcPlotId);
                Console.WriteLine("currentnode: " + sman.getCurrentNode());
            }
        }

        private void updateIds(int t) {
            //zero for plots
            if (t == 0) {
                npcPlotId = incr(npcPlotId);
                playerPlotId = incr(playerPlotId);
            } else {
                npcTransitionId = incr(npcTransitionId);
                playerTransitionId = incr(playerTransitionId);
            }
        }

        private void checkPlotChange() {
            if (sman.findNextPossibleNodes()) {
                resetPlotId();
                resetCharacterFNC();
                sman.setTypePlotNode();
            }
        }

        private void affect(List<string> targs, double m) {
            int t = (int)clamp(m, -1, 1);
            if (responseListNPC[0].FNC == 0) t = 0;
            m = clamp(m, -10, 10);
            foreach (string s in targs) {
                if (s == "mom") {
                    Mom.changeFNC(m);
                    State.sound_man.loop_enqueue(s, t);
                    respondEmotionally(Mom, t);
                } else if (s == "-mom") {
                    Mom.changeFNC(-m);
                    State.sound_man.loop_enqueue(s.Substring(1, s.Length-1), t);
                    respondEmotionally(Mom, t);
                } else if (s == "dad") {
                    Dad.changeFNC(m);
                    State.sound_man.loop_enqueue(s, t);
                    respondEmotionally(Dad, t);
                } else if (s == "-dad") {
                    Dad.changeFNC(-m);
                    State.sound_man.loop_enqueue(s.Substring(1, s.Length-1), t);
                    respondEmotionally(Dad, t);
                } else if (s == "alex") {
                    Alexis.changeFNC(m);
                    State.sound_man.loop_enqueue(s, t);
                    respondEmotionally(Alexis, t);
                } else if (s == "-alex") {
                    Alexis.changeFNC(-m);
                    State.sound_man.loop_enqueue(s.Substring(1, s.Length-1), t);
                    respondEmotionally(Alexis, t);
                }
            }
        }

        private void revertEmotion(Character c) {
            if (c.fncState() == "coop") c.setSpriteEmotion(Character.spriteEmotion.happy);
            else if (c.fncState() == "frust") c.setSpriteEmotion(Character.spriteEmotion.angry);
            else if (c.fncState() == "neut") c.setSpriteEmotion(Character.spriteEmotion.neutral);
        }

        private void respondEmotionally(Character c, int t) {
            if (t == 1) c.setSpriteEmotion(Character.spriteEmotion.happy);
            else if (t == -1) c.setSpriteEmotion(Character.spriteEmotion.angry);
            else c.setSpriteEmotion(Character.spriteEmotion.neutral);
        }
        private void checkFades() {
            if (responseListNPC[0].id == "5" && responseListNPC[0].plot == "DadInterjects2") {
                alexDimmed = true;
            } else if (responseListNPC[0].id == "13" && responseListNPC[0].plot == "MomInterjects3") {
                alexDimmed = false;
            } else if (responseListNPC[0].id == "1" && responseListNPC[0].plot == "BadEnding1") {
                alexDimmed = false;
            } else if (responseListNPC[0].id == "23" && responseListNPC[0].plot == "MomAdmitsJob" && chance(20)) {
                endGame = true;
            }
        }

        Boolean chance(int n) {
            Random r = new Random();
            return (r.Next(0, n) == 0);
        }

        private void resetCharacterFNC() {
            Mom.setCurrentFNC(0);
            Dad.setCurrentFNC(0);
            Alexis.setCurrentFNC(0);
        }

        protected override void Initialize() {
            screenHelper();

            deltaClock = new Clock();
            deltaTime = new Time();

            splash = new Sprite(new Texture("../../Art/UI_Art/buttons n boxes/wut.png"));
            playerfood = new Sprite(new Texture("../../Art/UI_Art/buttons n boxes/playerfood.png"));
            backwall = new Sprite(new Texture("../../Art/UI_Art/buttons n boxes/backwall.png"));
            flower = new Sprite(new Texture("../../Art/UI_Art/buttons n boxes/flowershadow.png"));
            lamp = new Sprite(new Texture("../../Art/UI_Art/buttons n boxes/lamp.png"));
            pictures = new Sprite(new Texture("../../Art/UI_Art/buttons n boxes/pictures.png"));

            table = new Sprite(new Texture("../../Art/UI_Art/buttons n boxes/newletable.png"));
            cups = new Sprite(new Texture("../../Art/UI_Art/buttons n boxes/cups.png"));
            plates = new Sprite(new Texture("../../Art/UI_Art/buttons n boxes/platesfinal.png"));
            wallWindow = new Sprite(new Texture("../../Art/UI_Art/buttons n boxes/window.png"));


            splash.Scale = new Vector2f(1.755f, 1.755f);

            playerfood.Scale = new Vector2f((float)((SCREEN_WIDTH / playerfood.GetGlobalBounds().Width) * 0.23), (float)((SCREEN_HEIGHT / playerfood.GetGlobalBounds().Height) * 0.23));
            backwall.Scale = new Vector2f(SCREEN_WIDTH / backwall.GetGlobalBounds().Width, SCREEN_HEIGHT / backwall.GetGlobalBounds().Height);
            flower.Scale = new Vector2f((float)((SCREEN_WIDTH / flower.GetGlobalBounds().Width) * 0.8), (float)((SCREEN_HEIGHT / flower.GetGlobalBounds().Height) * 0.8));
            lamp.Scale = new Vector2f(SCREEN_WIDTH / lamp.GetGlobalBounds().Width, SCREEN_HEIGHT / lamp.GetGlobalBounds().Height);
            cups.Scale = new Vector2f(SCREEN_WIDTH / cups.GetGlobalBounds().Width, SCREEN_HEIGHT / cups.GetGlobalBounds().Height);
            plates.Scale = new Vector2f(SCREEN_WIDTH / plates.GetGlobalBounds().Width, SCREEN_HEIGHT / plates.GetGlobalBounds().Height);
            pictures.Scale = new Vector2f(SCREEN_WIDTH / pictures.GetGlobalBounds().Width, SCREEN_HEIGHT / pictures.GetGlobalBounds().Height);
            table.Scale = new Vector2f(SCREEN_WIDTH / table.GetGlobalBounds().Width, SCREEN_HEIGHT / table.GetGlobalBounds().Height);
            wallWindow.Scale = new Vector2f(SCREEN_WIDTH / wallWindow.GetGlobalBounds().Width, SCREEN_HEIGHT / wallWindow.GetGlobalBounds().Height);

            // splash.Position = new Vector2f(0, 0);


            table.Position = new Vector2f(0, (float)((SCREEN_HEIGHT * -0.02) + 20));
            cups.Position = new Vector2f(0, (float)(SCREEN_HEIGHT * -0.12));
            plates.Position = new Vector2f(0, (float)(SCREEN_HEIGHT * -0.12));
            playerfood.Position = new Vector2f((SCREEN_WIDTH / 2) - (playerfood.GetGlobalBounds().Width / 2), 820);
            flower.Position = new Vector2f((SCREEN_WIDTH / 2) - (flower.GetGlobalBounds().Width / 2), (float)(SCREEN_HEIGHT / 17));

            toneBar = new Sprite(new Texture("../../Art/UI_Art/buttons n boxes/tonebar.png"));
            toneBar.Position = new Vector2f(6, (float)(SCREEN_HEIGHT * 0.735));
            toneBar.Scale = new Vector2f(SCREEN_WIDTH / 1920, SCREEN_HEIGHT / 1080);

            textBackground = new RectangleShape(new Vector2f(SCREEN_WIDTH, SCREEN_HEIGHT / 5));
            textBackground.Position = new Vector2f(0, SCREEN_HEIGHT - (float)(SCREEN_HEIGHT * 0.19));
            textBackground.FillColor = new Color(67, 65, 69);
            textBackground.OutlineColor = Color.White;
            textBackground.OutlineThickness = 2;

            // Create Character states

            jankList = s.chooseJank(Load.Jankson, jankId, currentTone.ToString());
            State.setResponseList(jankList);
            jankIncr();
            jankList = s.chooseJank(Load.Jankson, jankId, currentTone.ToString());

            //jumpflag1
            responseListNPC = s.ChooseDialogPlot(Load.allp, sman.getCurrentNode(), npcPlotId, "Default");
            responseList = s.ChooseDialogPlot(Load.newplayerp, sman.getCurrentNode(), playerPlotId, "Root");

            //ui_man.produceTextBoxes(responseList[0].content);
            //timeflag
            State.addTimer("game", 10, new Action(() => { TimerAction(); }));
            /////////////////////////////////////////////////////////////////////////////////////////////////////////

            buttons = ui_man.getButtons();
            menus.Add(startMenu); menus.Add(settingsMenu); menus.Add(pauseMenu);

            State.resetGameStuff = new Task(() => { return; });
            Mom = new Mom();
            Mom.setSpriteEmotion(Character.spriteEmotion.happy);
            Mom.active(true);
            Mom.setTalking(false);

            Alexis = new Alex();
            Alexis.setSpriteEmotion(Character.spriteEmotion.angry);
            Alexis.active(true);
            Alexis.setTalking(false);

            Dad = new Dad();
            Dad.setSpriteEmotion(Character.spriteEmotion.happy);
            Dad.active(true);
            Dad.setTalking(false);

            Arm = new Arm();
            Arm.setSpriteEmotion(Character.spriteEmotion.neutral);
            Arm.setArmPosition(Dad.getArmPosition());
            Arm.active(true);

            blackness.FillColor = Color.Black;
            blackness.Position = new Vector2f(0, 0);
            State.sound_man.init_music();
        }

        public static double clamp(double value, double min, double max) {
            return (value < min) ? min : (value > max) ? max : value;
        }

        private void LoadInitialPreReqs() {

            currentMadeMemories.Add("");

            currentMilestones.Add("");

            currentContext = "AlexTalksPlayer";

            FNC = 0;
        }

        private string linkId(string s) {
            int temp = ((int.Parse(s) * 2) - 1);
            return temp.ToString();
        }
        protected override void Update() {
            screenHelper();
            State.sound_man.update_music();

            if (State.dialogueBox.active == true) {
                if (State.dialogueBox.currSpeaker == "alex") {
                    Alexis.setTalking(true);
                    Dad.setTalking(false);
                    Mom.setTalking(false);
                } else if (State.dialogueBox.currSpeaker == "dad") {
                    Alexis.setTalking(false);
                    Dad.setTalking(true);
                    Mom.setTalking(false);
                } else if (State.dialogueBox.currSpeaker == "mom") {
                    Alexis.setTalking(false);
                    Dad.setTalking(false);
                    Mom.setTalking(true);
                }

            } else {
                Alexis.setTalking(false);
                Dad.setTalking(false);
                Mom.setTalking(false);
            }

            if (State.GetState() == "game" || State.GetState() == "tutorial") {
                if (fadeFlag != "") {
                    if (alphaBlack + (fadeFloat * (fadeFlag == "in" ? -1 : 1)) * deltaTime.AsSeconds() <= 255 && alphaBlack + (fadeFloat * (fadeFlag == "in" ? -1 : 1)) * deltaTime.AsSeconds() >= 0) {

                        alphaBlack += (fadeFloat * (fadeFlag == "in" ? -1 : 1)) * deltaTime.AsSeconds();
                    } else {

                        fadeFlag = "";
                    }
                }

                blackness.FillColor = new Color(0, 0, 0, (byte)alphaBlack);

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
                        if (buttons[i].GetSelected() && !buttons[i].getDisabled()) {
                            // Move the button around the screen
                            buttons[i].translate(MouseCoord[0], MouseCoord[1], window.Size.X, window.Size.Y);

                            // Check collision with UI Textboxes
                            // Loop through UI Textboxes

                            if (ui_man.wasMouseIn) {
                                // If the mouse just came from inside a UI Textbox
                                if (!ui_man.contains(buttons[i])) {
                                    // Mouse has now left the UI Textbox so set it to false
                                    ui_man.wasMouseIn = false;
                                    // Reset the color to match its previous color
                                    playerDialogues[0].setBoxColor(playerDialogues[0].getBoxColor("prev"));
                                    ui_man.rootBackgroundBorder.OutlineColor = ui_man.rootBackground.FillColor;
                                    // Update the rest of the buttons in the cluster
                                    ui_man.updateClusterColors(playerDialogues[0], playerDialogues, playerDialogues[0].getBoxColor("prev"), false);
                                }
                            } else {
                                // If mouse just came from outside the UI Textbox
                                if (ui_man.contains(buttons[i])) {
                                    ui_man.wasMouseIn = true;
                                    // Update previous color to current color of the UI Textbox
                                    playerDialogues[0].setPrevColor(playerDialogues[0].getBoxColor("curr"));
                                    // Update current color to selected tonal button color
                                    playerDialogues[0].setBoxColor(buttons[i].getTonalColor());
                                    ui_man.rootBackgroundBorder.OutlineColor = buttons[i].getTonalColor();
                                    // Update the rest of the buttons in the cluster
                                    ui_man.updateClusterColors(playerDialogues[0], playerDialogues, buttons[i].getTonalColor(), true);
                                }
                            }

                        }
                    }

                }

            } else if (State.GetState() == "pause") {
                //State.getGameTimer("game").PauseTimer();

            }
            deltaTime = deltaClock.Restart();

        }
        //Ensures that AI dialogue doesnt get loaded more than once per timer done
        //bool loadedAIDialogueOnce = false;
        //bool playerChoice = false;

        RectangleShape blackness = new RectangleShape(new Vector2f(SCREEN_WIDTH, SCREEN_HEIGHT));
        float alphaBlack = 255;
        String fadeFlag = "";
        float fadeFloat = 300;
        bool endGame = false;
        bool endGame2 = true;

        protected override void Draw() {

            window.Clear(clearColor);
            //window.SetView(fullScreenView);
            if (State.GetState() == "menu") {
                if (State.GetMenuState() == "start") {
                    window.Draw(splash);
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
                window.Draw(Dad);
                if (!alexDimmed) window.Draw(Alexis);
                window.Draw(table);
                window.Draw(Arm);
                window.Draw(plates);
                window.Draw(cups);
                window.Draw(flower);
                window.Draw(playerfood);

                var dialogues = ui_man.getPlayerDialogues();

                var buttons = ui_man.getButtons();

                if (!State.playerDialogueBox.active) {
                    window.Draw(State.dialogueBox);
                }

                window.Draw(blackness);


                if (!State.dialogueBox.active) {
                    //window.Draw(textBackground); // Account for fixed height of player dialogue box (makes sure there isnt a gap below the PDB)
                    window.Draw(ui_man.rootBackground);
                    window.Draw(State.playerDialogueBox);
                }

                if (!State.playerDialogueBox.active && !State.dialogueBox.active) {

                    //window.Draw(textBackground);
                    window.Draw(ui_man.rootBackground);
                    window.Draw(ui_man.rootBackgroundBorder);

                    for (var i = 0; i < dialogues.Count; i++) {
                        window.Draw(dialogues[i]);
                    }
                    if (State.dialogueIndex != "player") window.Draw(toneBar);
                    for (var i = 0; i < buttons.Count; i++) {
                        window.Draw(buttons[i]);
                    }
                    window.Draw(State.getGameTimer("game")); //this is the speak button
                }

                window.Draw(State.tooltip);
                if (State.GetState() == "pause") {
                    pauseMenu.DrawPauseBG(window);
                    if (State.GetMenuState() == "pause") {
                        window.Draw(pauseMenu);
                    } else if (State.GetMenuState() == "settings") {
                        window.Draw(settingsMenu);

                    }

                }

                if (debugInfo) {
                    Text AI_DB = new Text(//"LoadAIOnce: " + loadedAIDialogueOnce + "\n" +
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
