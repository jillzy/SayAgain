using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using SFML.Graphics;
using SFML.Window;
using SFML.System;
using System.Drawing;
//eventually make textbox into class for whole dialogue box (including name box)

namespace Test
{
    class DialogueBox : Drawable
    {

        static UInt32 SCREEN_WIDTH = VideoMode.DesktopMode.Width;
        static UInt32 SCREEN_HEIGHT = VideoMode.DesktopMode.Height;
        Vector2f scale = new Vector2f(SCREEN_WIDTH / 1920, SCREEN_HEIGHT / 1080);
        private Text name, dialogue;
        Task currentTask;
        uint dialogueFontSize = 40;
        uint nameFontSize = 55;

        static Dictionary<string, Sprite> spriteDict = new Dictionary<string, Sprite>();

        public bool init = false;
        string tag; //AI or player

        public bool animationStart = false;
        public bool awaitInput = false;

        CancellationTokenSource cts;
        public List<Text> dialoguePanes = new List<Text>();
        public int printTime = 30;
        public bool active = false;
        public int elementIndex = 0;
        public bool done = false;
        GameState state;

        Sprite dialogueBoxSprite;

        public CircleShape cursor;
        public float OGcursorX;
        public float OGcursorY;
        public float FcursorX;
        public float FcursorY;
        float iterator = 0.5f;
        public Boolean lastDialogue = false;

        Font speechFont = new Font("../../Art/UI_Art/fonts/ticketing/TICKETING/ticketing.ttf");

        //public void acknowledge()
        //{
        //    if (awaitInput)
        //    {
        //        if (tag == "AI")
        //        {
        //            state.startTimer("game");
        //        }
        //        active = false;
        //        awaitInput = false;
        //        init = false;
        //    }
        //}

        public string getDialogueText()
        {
            return dialogue.DisplayedString;
        }

        public void setInit(bool b)
        {
            init = b;
        }

        public bool getAwaitInput()
        {
            return awaitInput;
        }

        //public void forward()
        //{
        //    if (currentTask == null || currentTask.IsCompleted)
        //    {
        //        getNext();
        //        checkEnd();
        //    }
        //}

        public void setPrintTime(int i)
        {
            printTime = i;
        }
        public int getElementIndex()
        {
            return elementIndex;
        }

        public bool getAnimationStart()
        {
            return animationStart;
        }

        public bool checkNext()
        {
            if (elementIndex < dialoguePanes.Count)
            {
               // Console.WriteLine("\n---------- CHECK NEXT");
                if (cts != null)
                {
                    cts.Cancel();
                }
                cts = new CancellationTokenSource();
                currentTask = Task.Run(async () =>
                {
                    printTime = 30;
                    await animateText(cts.Token);
                }, cts.Token);

                return false;
            } else
            {
                if(tag == "AI")
                {
                    state.startTimer("game");
                }
                
                awaitInput = false;

                return true;
            }
        }

        public void loadNewDialogue(string speaker, string content)
        {
            if (speaker == "alex")
            {
                dialogueBoxSprite = spriteDict["right"];
                dialogueBoxSprite.Position = new Vector2f(SCREEN_WIDTH / 2 - (dialogueBoxSprite.GetGlobalBounds().Width / 2), SCREEN_HEIGHT / 5);

                name = new Text(speaker.ToUpper(), speechFont, nameFontSize);
                name.Position = new Vector2f(dialogueBoxSprite.GetGlobalBounds().Left + ((118 * scale.X) - name.GetGlobalBounds().Width / 2), dialogueBoxSprite.GetGlobalBounds().Top + ((22 * scale.Y) - name.GetGlobalBounds().Height));
                dialogue = new Text(content, speechFont, dialogueFontSize);
                dialogue.Position = new Vector2f(dialogueBoxSprite.GetGlobalBounds().Left + (uint)(SCREEN_WIDTH * 0.004), dialogueBoxSprite.GetGlobalBounds().Top + (uint)(SCREEN_HEIGHT * 0.046));

            }
            else if (speaker == "dad")
            {
                dialogueBoxSprite = spriteDict["left"];
                dialogueBoxSprite.Position = new Vector2f((float)(SCREEN_WIDTH * 0.21) - (dialogueBoxSprite.GetGlobalBounds().Width / 2), (float)(SCREEN_HEIGHT * 0.19) - (dialogueBoxSprite.GetGlobalBounds().Height / 2));

                name = new Text(speaker.ToUpper(), speechFont, nameFontSize);
                name.Position = new Vector2f(dialogueBoxSprite.GetGlobalBounds().Left + ((118 * scale.X) - name.GetGlobalBounds().Width / 2), dialogueBoxSprite.GetGlobalBounds().Top + ((22 * scale.Y) - name.GetGlobalBounds().Height));
                dialogue = new Text(content, speechFont, dialogueFontSize);
                dialogue.Position = new Vector2f(dialogueBoxSprite.GetGlobalBounds().Left + (uint)(SCREEN_WIDTH * 0.004), dialogueBoxSprite.GetGlobalBounds().Top + (uint)(SCREEN_HEIGHT * 0.046));

            }
            else if (speaker == "mom")
            {
                dialogueBoxSprite = spriteDict["right"];
                dialogueBoxSprite.Position = new Vector2f(3 * SCREEN_WIDTH / 4 - (dialogueBoxSprite.GetGlobalBounds().Width / 2), SCREEN_HEIGHT / 5);

                name = new Text(speaker.ToUpper(), speechFont, nameFontSize);
                name.Position = new Vector2f(dialogueBoxSprite.GetGlobalBounds().Left + ((118 * scale.X) - name.GetGlobalBounds().Width / 2), dialogueBoxSprite.GetGlobalBounds().Top + ((22 * scale.Y) - name.GetGlobalBounds().Height));
                dialogue = new Text(content, speechFont, dialogueFontSize);
                dialogue.Position = new Vector2f(dialogueBoxSprite.GetGlobalBounds().Left + (uint)(SCREEN_WIDTH * 0.004), dialogueBoxSprite.GetGlobalBounds().Top + (uint)(SCREEN_HEIGHT * 0.046));

            }
            else if (speaker == "player")
            {
                dialogueBoxSprite = spriteDict["player"];
                dialogueBoxSprite.Position = new Vector2f(0, (float)(SCREEN_HEIGHT * 0.74));

                name = new Text("YOU", speechFont, nameFontSize);
                name.Position = new Vector2f(dialogueBoxSprite.GetGlobalBounds().Left + ((354 * scale.X) - name.GetGlobalBounds().Width / 2), dialogueBoxSprite.GetGlobalBounds().Top + ((32 * scale.Y) - name.GetGlobalBounds().Height));
                dialogue = new Text(content, speechFont, dialogueFontSize);
                dialogue.Position = new Vector2f(dialogueBoxSprite.GetGlobalBounds().Left + (uint)(SCREEN_WIDTH * 0.004), dialogueBoxSprite.GetGlobalBounds().Top + (uint)(SCREEN_HEIGHT * 0.062));

            }

            // Cursor initializations
            if (tag == "AI")
            {
                OGcursorX = dialogueBoxSprite.GetGlobalBounds().Left + dialogueBoxSprite.GetGlobalBounds().Width - (float)(dialogueBoxSprite.GetGlobalBounds().Width * 0.05);
                OGcursorY = dialogueBoxSprite.GetGlobalBounds().Top + dialogueBoxSprite.GetGlobalBounds().Height - (float)(dialogueBoxSprite.GetGlobalBounds().Height * .3);
            }
            else
            {
                OGcursorX = (float)(SCREEN_WIDTH * 0.95);
                OGcursorY = (float)(SCREEN_HEIGHT * 0.95);
            }
            cursor.Position = new Vector2f(OGcursorX, OGcursorY);

            cursor.Origin = new Vector2f(cursor.GetGlobalBounds().Width / 2, cursor.GetGlobalBounds().Height / 2);
            FcursorX = OGcursorX + 3;
            FcursorY = OGcursorY + 3;
            // ----------------------

            name.Color = Color.White;
            dialogue.Color = Color.White;

            awaitInput = false;
            active = true;
            elementIndex = 0;

            renderDialogue(content);

        }

        public void renderDialogue(String s)
        {
            dialoguePanes.Clear();
            if (cts != null)
            {
                cts.Cancel();
            }
            cts = new CancellationTokenSource();

            dialoguePanes = createStrings();
            currentTask = Task.Run(async () =>
            { //Task.Run puts on separate thread
                printTime = 30;
                await animateText(cts.Token); //await pauses thread until animateText() is completed

            }, cts.Token);
        }

        public List<Text> createStrings()
        {
            
            float maxw;
            float maxh;
            if (tag == "AI")
            {
                maxw = dialogueBoxSprite.GetGlobalBounds().Width - cursor.GetGlobalBounds().Width;
                maxh = (float)(dialogueBoxSprite.GetGlobalBounds().Height * 0.8);
            }
            else
            {
                maxw = dialogueBoxSprite.GetGlobalBounds().Width - cursor.GetGlobalBounds().Width;
                maxh = SCREEN_HEIGHT - dialogue.GetGlobalBounds().Top;
            }

            Text line = dialogue;
            List<Text> list = new List<Text>();
            
            // split dialogue into words
            string[] s = line.DisplayedString.Split(' ');

            Text newline = new Text("", speechFont, dialogueFontSize);
            
            float currentLineWidth = 0;
            foreach (string word in s)
            {
                Text t = new Text(word + " ", speechFont, dialogueFontSize);
                float wordSizeWithSpace = t.GetGlobalBounds().Width;
                if (currentLineWidth + wordSizeWithSpace > maxw)
                {

                    newline.DisplayedString += "\n";
                    currentLineWidth = 0;
                    if (newline.GetGlobalBounds().Height * 1.5 > maxh)
                    {
                        list.Add(newline);
                        newline = new Text("", speechFont, dialogueFontSize);
                    }
                }

                newline.DisplayedString += (t.DisplayedString);
                currentLineWidth += wordSizeWithSpace;
            }

            // Add the last one
            if (newline.DisplayedString != "")
            {
                list.Add(newline);
            }

            return list;

        }

        public void AlertSoundMan()
        {
            //send signal to sound man
        }

        //async means this function can run separate from main app.
        //operate in own time and thread
        public async Task animateText(CancellationToken ct)
        {
            Text line = dialoguePanes[elementIndex];

            //Console.WriteLine("ANIMATE TEXT: " + line.DisplayedString);

            animationStart = true;
            awaitInput = false;

            state.resetTimer("game");
            dialogue.DisplayedString = "";

            int i = 0;
            while (i < line.DisplayedString.Length)
            {
                if (ct.IsCancellationRequested)
                {
                    ct.ThrowIfCancellationRequested();
                }
                if (state.GetState() != "pause")
                {
                    if (printTime != 0)
                    {
                        if(i == line.DisplayedString.Length - 2)
                        {
                            printTime = 0;
                        }
                        else if (".!?".Contains(line.DisplayedString[i]))
                        {
                            if (!(".!?".Contains(line.DisplayedString[i - 1])))
                            {
                                printTime *= 14;
                            }
                        }
                        else if (",".Contains(line.DisplayedString[i]))
                        {
                            printTime *= 10;
                        }
                        else
                        {
                            printTime = 30;
                        }
                    }
                    dialogue.DisplayedString = (string.Concat(dialogue.DisplayedString, line.DisplayedString[i++]));
                    
                    await Task.Delay(printTime); //equivalent of putting thread to sleep
                }
            }
            // Do asynchronous work.
            if (elementIndex < dialoguePanes.Count - 1)
            {
                cursor.Rotation = 180;
            }
            else
            {
                cursor.Rotation = 90;
            }

            animationStart = false; //done animating
            awaitInput = true;
            elementIndex++;

        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            if (init)
            {
                target.Draw(dialogueBoxSprite);
                target.Draw(name);
                target.Draw(dialogue);

                if (awaitInput)
                {
                    if (cursor.Rotation == 90)
                    {
                        if (cursor.Position.X > FcursorX)
                        {
                            iterator *= -1;
                        }


                        if (cursor.Position.X < OGcursorX)
                        {
                            iterator *= -1;
                        }
                        cursor.Position = new Vector2f(cursor.Position.X + iterator, cursor.Position.Y);
                    }
                    else
                    {
                        if (cursor.Position.Y > FcursorY)
                        {
                            iterator *= -1;
                        }


                        if (cursor.Position.Y < OGcursorY)
                        {
                            iterator *= -1;
                        }
                        cursor.Position = new Vector2f(cursor.Position.X, cursor.Position.Y + iterator);
                    }


                    target.Draw(cursor);
                }
            }
        }

        private uint getFontSize()
        {
            return (uint)((SCREEN_WIDTH / 1920) * 27);
        }


        public DialogueBox(GameState state, string tag)
        {
            this.state = state;
            this.tag = tag;
            if (tag == "AI")
            {
                cursor = new CircleShape((SCREEN_WIDTH / 1920) * 10, 3);
                dialogueFontSize = getFontSize();
                nameFontSize = getFontSize() + 20;
            }
            else
            {
                cursor = new CircleShape((SCREEN_WIDTH / 1920) * 20, 3);
                dialogueFontSize = getFontSize() + 20;
                nameFontSize = getFontSize() + 30;
            }

            cursor.Rotation = 180;

            if (!spriteDict.ContainsKey("left"))
            {
                spriteDict.Add("left", new Sprite(new Texture("../../Art/UI_Art/buttons n boxes/speechbubbleleft.png")));
                spriteDict["left"].Scale = scale;
            }
            if (!spriteDict.ContainsKey("right"))
            {
                spriteDict.Add("right", new Sprite(new Texture("../../Art/UI_Art/buttons n boxes/speechbubbleright.png")));
                spriteDict["right"].Scale = scale;
            }
            if (!spriteDict.ContainsKey("player"))
            {
                spriteDict.Add("player", new Sprite(new Texture("../../Art/UI_Art/buttons n boxes/psb.png")));
                spriteDict["player"].Scale = scale;
            }



        }
    }
}