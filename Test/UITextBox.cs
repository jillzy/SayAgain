using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

namespace Test
{
    class UITextBox : UIElement//The box where the dialogue will appear, not clickable or draggable
    {
        public UITextBox(float x, float y, string dialogue, int cluster)
        {
            UITextBoxText = new Text(dialogue, UITextBoxFont, getFontSize());
            //Console.WriteLine("TEXT HEIGHT: " + (UITextBoxText.GetGlobalBounds().Height * 1.5) + 10);
            box = new RectangleShape(new Vector2f(UITextBoxText.GetGlobalBounds().Width + 30, (float)(UITextBoxText.GetGlobalBounds().Height*1.5) + 10));
            box.Position = new Vector2f(x, y + 17);
            UITextBoxText.Position = new Vector2f((box.GetGlobalBounds().Width/2 - UITextBoxText.GetGlobalBounds().Width/2) + box.GetGlobalBounds().Left, (box.GetGlobalBounds().Height / 2 - (float)(UITextBoxText.GetGlobalBounds().Height / 1.1)) + box.GetGlobalBounds().Top);
            box.FillColor = buttonTonalColors["Default"];
            prevColor = buttonTonalColors["Default"];
            UITextBoxText.Color = new Color(67, 65, 69);
            this.affected = false;
            this.cluster = cluster;
        }

        Font UITextBoxFont = new Font("../../Art/UI_Art/fonts/ticketing/TICKETING/ticketing.ttf");
        Text UITextBoxText;
        RectangleShape box;
        bool affected = false;
        Color prevColor;
        bool mouseWasIn = false;
        int cluster = -1;
        tone tone = tone.Root;
        UInt32 SCREEN_WIDTH = VideoMode.DesktopMode.Width;
        UInt32 SCREEN_HEIGHT = VideoMode.DesktopMode.Height;

        private uint getFontSize()
        {

            return (uint)((SCREEN_WIDTH / 1920) * 50);
        }

        public RectangleShape getBox()
        {
            return box;
        }

        public tone getTone() {
            return tone;
        }

        public void setTone(tone tonez) {
            tone = tonez;
        }

        public int getCluster()
        {
            return cluster;
        }

        public Text getBoxText()
        {
            return UITextBoxText;
        }

        public FloatRect getBoxBounds()
        {
            return box.GetGlobalBounds();
        }


        public void changeDialogue(string newDialogue)
        {
            UITextBoxText.DisplayedString = newDialogue;
        }

        public void setBoxColor(Color color)
        {
            box.FillColor = color;
        }

        public void setPrevColor(Color color)
        {
            prevColor = color;
        }

        public Color getBoxColor(string s)
        {
            if (s == "curr") return box.FillColor;
            return prevColor;
        }

        public bool getAffected()
        {
            return affected;
        }

        public void setAffected(bool b)
        {
            affected = b;
        }

        public void setMouseWasIn(bool w)
        {
            mouseWasIn = w;
        }

        public bool wasMouseIn()
        {
            return mouseWasIn;
        }

        public bool Contains(UIButton button)
        {
            //FloatRect bounds = getBoxBounds();
            //if (mouseX >= bounds.Left && mouseX <= bounds.Left + bounds.Width && mouseY >= bounds.Top && mouseY <= bounds.Top + bounds.Height)
            //{
            //    return true;
            //}
            //return false;
            FloatRect rootBounds = getBoxBounds();
            FloatRect toneButton = button.getRectBounds();

            if (rootBounds.Left < toneButton.Left + toneButton.Width &&
   rootBounds.Left + rootBounds.Width > toneButton.Left &&
   rootBounds.Top < toneButton.Top + toneButton.Height &&
   rootBounds.Height + rootBounds.Top > toneButton.Top)
            {
                return true;
                // collision detected!
            }
            return false;
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            target.Draw(box);
            target.Draw(UITextBoxText);
        }



        public string getDialogue()
        {
            return UITextBoxText.DisplayedString;
        }
    }
}
