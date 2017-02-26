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
        public float w, h, x, y;//, x2, y2, OffsetX, OffsetY, OffsetX2, OffsetY2;
        private Text name, dialogue;
        //public Dialogue line;
        public RectangleShape box;
        public RectangleShape nameBox;
        Task currentTask;
        CancellationTokenSource cts;
        Text[] arr = {};
        public int printTime;
        public bool active = false;
        int elementIndex = 0;

        public View view { get; private set; }

        Font speechFont = new Font("../../Fonts/Ticketing.ttf");

        public void forward()
        {
            if (currentTask == null || currentTask.IsCompleted) {
                getNext();
                checkEnd();
            }
        }
        public void setPrintTime(int i)
        {
            printTime = i;
        }
        public int getElementIndex()
        {
            return elementIndex;
        }

        public int getArrLength()
        {
            return arr.Length;
        }


        public void checkEnd()
        {
            if (getElementIndex() >= getArrLength())
            {
                active = false;
            }
        }

        public void getNext()
        {
            elementIndex += 1;
            if (elementIndex < arr.Length)
            {
                if (cts != null)
                {
                    cts.Cancel();
                }
                cts = new CancellationTokenSource();
                currentTask = Task.Run(async () =>
                { //Task.Run puts on separate thread
                    printTime = 60;
                    await animateText(arr[elementIndex], cts.Token); //await pauses thread until animateText() is completed
                }, cts.Token);
            }
        }
        public void renderDialogue(String s, String speaker)
        {
            active = true;
            elementIndex = 0;
            if (cts != null)
            {
                cts.Cancel();
            }
            cts = new CancellationTokenSource();
            name = BufferName(speaker);
            dialogue = BufferDialogue("");
            Text tmp = new Text(s, speechFont, 24);

            arr = createStrings(tmp);
            currentTask = Task.Run(async () =>
            { //Task.Run puts on separate thread
                printTime = 60;
                await animateText(arr[elementIndex], cts.Token); //await pauses thread until animateText() is completed
            }, cts.Token);
        }

        public Text[] createStrings(Text line)
        {
            float maxw = w - 26;
            float maxh = GetMaxTextHeight();
            List<Text> list = new List<Text>();

            // split dialogue into words
            String[] s = line.DisplayedString.Split(' ');
            line.DisplayedString = "";
            float currentLineWidth = 0;
            foreach (String word in s)
            {
                Text t = new Text(word + " ", speechFont, 24);
                float wordSizeWithSpace = t.GetGlobalBounds().Width;
                if (currentLineWidth + wordSizeWithSpace > maxw)
                {

                    line.DisplayedString += "\n";
                    currentLineWidth = 0;
                    if (line.GetGlobalBounds().Height > maxh)
                    {
                        list.Add(line);
                        line = new Text("", speechFont, 24);
                    }
                }

                line.DisplayedString += (t.DisplayedString);
                currentLineWidth += wordSizeWithSpace;
            }

            // Add the last one
            if (line.DisplayedString != "")
            {
                list.Add(line);

            }
            for (int i = 0; i < list.Count; i++)
            {
                Console.WriteLine(list[i]);
            }

            return list.ToArray();

        }

        //async means this function can run separate from main app.
        //operate in own time and thread
        public async Task animateText(Text line, CancellationToken ct)
        {
            int i = 0;
            dialogue.DisplayedString = "";
            while (i < line.DisplayedString.Length)
            {
                if (ct.IsCancellationRequested)
                {
                    ct.ThrowIfCancellationRequested();
                }
                dialogue.DisplayedString = (string.Concat(dialogue.DisplayedString, line.DisplayedString[i++]));
                await Task.Delay(printTime); //equivalent of putting thread to sleep
            }

            // Do asynchronous work.

        }

        public void Draw(RenderTarget target, RenderStates states)
        {
            View resetView = target.GetView();
            target.SetView(view);

            target.Draw(box);
            target.Draw(nameBox);
            target.Draw(name);
            target.Draw(dialogue);

            target.SetView(resetView);
        }
        public FloatRect GetBounds()
        {
            FloatRect f = new FloatRect(box.GetGlobalBounds().Left - 20,
                nameBox.GetGlobalBounds().Top - 20,
                box.GetGlobalBounds().Width + 20,
                (box.GetGlobalBounds().Top + box.GetGlobalBounds().Height + 20) - nameBox.GetGlobalBounds().Top);
            return f;
        }
        public FloatRect GetGlobalBounds()
        {
            return box.GetGlobalBounds();
        }

        public float GetMaxTextHeight()
        {
            return box.GetGlobalBounds().Height - 20;
        }

        public Text BufferName(String speaker)
        {
            Text n = new Text(speaker.ToUpper(), speechFont, 24);
            n.Position = new Vector2f(nameBox.Position.X + 17, nameBox.Position.Y + 12);
            n.Color = Color.Black;
            return n;
        }

        public Text BufferDialogue(String s)
        {
            Text d = new Text(s, speechFont, 24);
            d.Position = new Vector2f(box.Position.X + 13, box.Position.Y + 20);
            d.Color = Color.Black;
            return d;
        }


        public DialogueBox(float x, float y, float width, float height)
        {
            this.x = x;
            this.y = y;
            this.w = width;
            this.h = height;


            box = new RectangleShape(new Vector2f(this.w, this.h));
            box.Position = new Vector2f(this.x - 40, this.y + 35);
            box.OutlineThickness = 3;
            box.OutlineColor = Color.Black;

            nameBox = new RectangleShape(new Vector2f(this.w - 575, this.h - 100));
            nameBox.Position = new Vector2f(this.x, this.y);
            nameBox.OutlineThickness = 3;
            nameBox.OutlineColor = Color.Black;

            view = new View(GetBounds());
        }




    }

}