using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.Audio;
using SFML.Window;
using SFML.System;
using System.Text.RegularExpressions;

//holds UI elements such as buttons, input fields, TextBoxes, etc
namespace Test {
    class UIManager {
        //constructor
        public UIManager() {
            /* TEMPORARY CODE REMOVE AND CLEAN LATER*/
            if (buttonOrder == 0) {
                tonez = new List<tone>() { tone.Blunt, tone.Indifferent, tone.Compassionate, tone.Hesitant };
            } else {
                tonez = new List<tone>() { tone.Compassionate, tone.Indifferent, tone.Blunt, tone.Hesitant };
                if (buttonOrder == 2) {
                    while (tonez == null || (tonez[0] == tone.Blunt || tonez[0] == tone.Compassionate || tonez[2] == tone.Blunt || tonez[2] == tone.Compassionate)) {

                        tonez = shuffleList(tonez);
                    }
                }
            }
            generateButtons();

            rootBackground = new RectangleShape(new Vector2f(SCREEN_WIDTH, SCREEN_HEIGHT * 0.19f));
            rootBackground.Position = new Vector2f(0, SCREEN_HEIGHT*0.81f);
            rootBackground.FillColor = new Color(67, 65, 69);
            rootBackground.OutlineThickness = 2;
            rootBackground.OutlineColor = Color.White;

            rootBackgroundBorder = new RectangleShape(new Vector2f(rootBackground.GetGlobalBounds().Width - 15, rootBackground.GetGlobalBounds().Height - 15));
            rootBackgroundBorder.Position = new Vector2f(rootBackground.GetGlobalBounds().Width/2 - rootBackgroundBorder.GetGlobalBounds().Width/2, rootBackground.GetGlobalBounds().Top + rootBackground.GetGlobalBounds().Height / 2 - rootBackgroundBorder.GetGlobalBounds().Height / 2);
            rootBackgroundBorder.FillColor = new Color(67, 65, 69);
            rootBackgroundBorder.OutlineThickness = 2;
            rootBackgroundBorder.OutlineColor = rootBackground.FillColor;


        }

        public List<tone> tonez;
        List<UIButton> buttons = new List<UIButton>(); //our tone buttons
        List<UITextBox> playerDialogues = new List<UITextBox>();
        public RectangleShape rootBackground, rootBackgroundBorder;
        static UInt32 SCREEN_WIDTH = VideoMode.DesktopMode.Width;
        static UInt32 SCREEN_HEIGHT = VideoMode.DesktopMode.Height;
        public int tutorialButtonIndex = 0;
        int buttonOrder = 0;
        string[] dialogueArray;
        List<Dictionary<string, bool>> availTutorialButtons = new List<Dictionary<string, bool>>() {
            new Dictionary<string, bool>() {
                { "Blunt", false },
                { "Indifferent", true },
                { "Compassionate", false },
                { "Hesitant", false }
            },
            new Dictionary<string, bool>() {
                { "Blunt", false },
                { "Indifferent", false },
                { "Compassionate", false },
                { "Hesitant", true }
            },
            new Dictionary<string, bool>() {
                { "Blunt", false },
                { "Indifferent", false },
                { "Compassionate", true },
                { "Hesitant", false }
            },
            new Dictionary<string, bool>() {
                { "Blunt", true },
                { "Indifferent", false },
                { "Compassionate", false },
                { "Hesitant", false }
            },
        };

        public bool wasMouseIn = false;


        public bool contains(UIButton button) {

            FloatRect rootBounds = rootBackground.GetGlobalBounds();
            FloatRect toneButton = button.getRectBounds();

            if (rootBounds.Left < toneButton.Left + toneButton.Width &&
   rootBounds.Left + rootBounds.Width > toneButton.Left &&
   rootBounds.Top < toneButton.Top + toneButton.Height &&
   rootBounds.Height + rootBounds.Top > toneButton.Top) {
                return true;
                // collision detected!
            }
            return false;
        }

        public void generateButtons() {
            buttons.Clear();
            int xPos = (int)SCREEN_WIDTH / tonez.Count;
            for (int i = 0; i < tonez.Count; i++) {
                buttons.Add(new UIButton(xPos / 2 + i * xPos, (float)(SCREEN_HEIGHT - SCREEN_HEIGHT * 0.26), tonez[i]));
                //Console.WriteLine(i + " : " + buttons[buttons.Count - 1].getRectBounds());
                if (tutorialButtonIndex < availTutorialButtons.Count()) {
                    buttons[i].setDisabled(!availTutorialButtons[tutorialButtonIndex][tonez[i].ToString()]);
                }
            }
            //tutorialButtonIndex++;
        }

        //methods
        public List<UIButton> getButtons() {
            return buttons;
        }

        public List<UITextBox> getPlayerDialogues() {
            return playerDialogues;
        }

        private List<tone> shuffleList(List<tone> inputList) {
            var rand = new Random();
            for (int i = inputList.Count - 1; i >= 0; i--) {

                tone tmp = inputList[i];
                int randomIndex = rand.Next(i + 1);

                //Swap elements
                inputList[i] = inputList[randomIndex];
                inputList[randomIndex] = tmp;
            }
            return inputList;
        }

        #region SweepButtons
        public void SweepButtons(int x, int y, double scalex, double scaley) {
            var buttons = getButtons();
            for (var i = 0; i < buttons.Count; i++) {
                buttons[i].setHover((int)(x * scalex), (int)(y * scaley));
            }
        }
        #endregion

        public List<UITextBox> produceTextBoxes(string Dialogue) {
            dialogueArray = Dialogue.Split(' ');
            List<string> words = new List<string>();
            foreach (var dialogue in dialogueArray) {

                ////Console.WriteLine(dialogue);
            }

            string tempString = dialogueArray[0];
            Font tempFont = new Font("../../Art/UI_Art/fonts/ticketing/TICKETING/ticketing.ttf");
            uint x = 15;
            uint y = SCREEN_HEIGHT - ((SCREEN_HEIGHT / 5)) + 10;
            int cluster = 0;

            int length = 0;
            if (dialogueArray.Length > 1) {
                length = dialogueArray.Length - 1;
            } else {
                length = dialogueArray.Length;
            }

            ////Console.WriteLine("THE LENGTH OF THE DIALOGUE IS: " + dialogueArray.Length);

            if (dialogueArray.Length != 1) {

                for (int word = 1; word < dialogueArray.Length; word++) {
                    Text tempText = new Text(tempString + ' ' + dialogueArray[word], tempFont, getFontSize()); //current word + the next one after
                    if (tempText.GetGlobalBounds().Width + 10 >= SCREEN_WIDTH) {

                        //time to go the next line
                        ////Console.WriteLine("NEXT LINE TIME!!!");
                        playerDialogues.Add(new UITextBox(x, y, tempString, cluster));
                        y += (uint)tempText.GetGlobalBounds().Height + 10;
                        tempString = dialogueArray[word];
                        if (word == dialogueArray.Length - 1) {

                            playerDialogues.Add(new UITextBox(x, y, tempString, cluster));
                            y += (uint)tempText.GetGlobalBounds().Height + 10;
                        }

                    } else {

                        ////Console.WriteLine("tempString before adding the next word: " + tempString);
                        tempString += ' ';
                        tempString += dialogueArray[word];
                        ////Console.WriteLine("tempString after adding the next word: " + tempString);
                        if (word == dialogueArray.Length - 1) {

                            playerDialogues.Add(new UITextBox(x, y, tempString, cluster));
                            y += (uint)tempText.GetGlobalBounds().Height + 10;
                        }
                    }

                }
            } else if (dialogueArray.Length == 1) {

                playerDialogues.Add(new UITextBox(x, y, tempString, cluster));
            }

            return playerDialogues;

        }
        private uint getFontSize() {

            return (uint)((SCREEN_WIDTH / 1920) * 80);
        }

        public void reset(List<DialogueObj> responseList) {
            bool gotTone = false;
            tone Tone = tone.Root;

            foreach (var dialogue in playerDialogues) {
                if (dialogue.getAffected() && !gotTone) {
                    ////Console.WriteLine(dialogue.getTone());

                    Tone = dialogue.getTone();
                    gotTone = true;
                }

                dialogue.setAffected(false);
                dialogue.setTone(tone.Root);
            }
            playerDialogues.Clear();

            produceTextBoxes(responseList.ElementAt(0).content);

            //Console.WriteLine("I AM PRODUCING THE CONTENTS OF: " + responseList[0].content);

            generateButtons();
        }

        public tone getTone() {
            return playerDialogues[0].getTone();
        }

        public void updateClusterColors(UITextBox self, List<UITextBox> playerDialogues, Color c, bool f) {

            int cluster = self.getCluster();
            for (int i = 0; i < playerDialogues.Count; i++) {

                if (playerDialogues[i].getCluster() == cluster && playerDialogues[i] != self) {
                    if (!f) {
                        playerDialogues[i].setBoxColor(playerDialogues[i].getBoxColor("prev"));

                        //playerDialogues[i].setMouseWasIn(false);
                    } else {
                        playerDialogues[i].setPrevColor(playerDialogues[i].getBoxColor("curr"));
                        playerDialogues[i].setBoxColor(c);

                        //playerDialogues[i].setMouseWasIn(true);
                    }
                }

            }
        }



        #region SA_applyTones
        public void applyTones(int x, int y, DialogueBox theTip) {
            // Applying tones to Text Boxes
            for (var i = 0; i < buttons.Count; i++) {
                if (buttons[i].GetSelected()) {
                    //CHECK MATRIX BS
                    // Move to character state
                    //double[,] final = tfx.MatrixMult(tfx, cf);
                    ////Console.WriteLine(final[2, 3]);
                    ////Console.WriteLine("HEY THE BUTTON I AM DRAGGING IS: " + buttons[i].getTone().ToString());
                    // Get UI Text Boxes
                    var playerDialogues = this.getPlayerDialogues();

                    for (var j = 0; j < playerDialogues.Count; j++) {
                        var boxBounds = playerDialogues[j].getBoxBounds();
                        //change color if the button is hovering over the textbox

                        if (contains(buttons[i])) {

                            for (int k = 0; k < playerDialogues.Count; k++) {
                                playerDialogues[k].setPrevColor(playerDialogues[k].getBoxColor("curr"));
                                playerDialogues[k].setBoxColor(buttons[i].getTonalColor());
                                playerDialogues[k].setAffected(true);
                                playerDialogues[k].setTone(buttons[i].getTone());


                                if (theTip.init == true) {

                                    theTip.loadNewDialogue("tooltip3", "Click/Space to Speak");
                                }
                                ////Console.WriteLine("MY TONE IS: " + playerDialogues[0].getTone());
                                
                                //IF THE PLAYER DRAGGED IN BLUNT
                                //HAVE THE TARGET CHARS REACT ANGRILY
                                if (playerDialogues[0].getTone() == tone.Blunt)
                                {
                                    //only pgets called when dragged!! keys will not work
                                   // Program.getGame().getTargets();
                                    //Console.WriteLine("hello");
                                


                                    //applyReactionToBlunt(Program.getGame().getTargets());
                                }
                            }
                            break;
                        }
                    }
                    buttons[i].snapBack();
                    buttons[i].SetSelected(false);
                    break;
                }
            }
        }
        #endregion

        public virtual void applyReactionToBlunt(List<string> t)
        {
            foreach (var c in t)
            {
                if (string.Equals(c, "mom", StringComparison.OrdinalIgnoreCase))
                {
                    Program.getGame().getMom().setSpriteEmotion(Character.spriteEmotion.angry);
                }
                else if (string.Equals(c, "dad", StringComparison.OrdinalIgnoreCase))
                {
                    Program.getGame().getDad().setSpriteEmotion(Character.spriteEmotion.angry);
                }
                else if (string.Equals(c, "alex", StringComparison.OrdinalIgnoreCase))
                {
                    Program.getGame().getAlexis().setSpriteEmotion(Character.spriteEmotion.angry);
                }
            }
        }


        public void applyToneShortcut(UIButton button, DialogueBox theTip) {
            if (button.getDisabled() == false) {
                for (int i = 0; i < playerDialogues.Count; i++) {
                    rootBackgroundBorder.OutlineColor = button.getTonalColor();
                    playerDialogues[i].setPrevColor(playerDialogues[i].getBoxColor("curr"));
                    playerDialogues[i].setBoxColor(button.getTonalColor());
                    playerDialogues[i].setTone(button.getTone());


                    if (theTip.init == true) {

                        theTip.loadNewDialogue("tooltip3", "be my baby daddy pls");
                    }
                }
            }
        }
    }
}