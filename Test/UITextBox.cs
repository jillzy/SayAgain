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
            UITextBoxFont = new Font("../../Fonts/Adore64.ttf");
            UITextBoxText = new Text(dialogue, UITextBoxFont);
            UITextBoxText.Position = new Vector2f(x, y);
            box = new RectangleShape(new Vector2f(UITextBoxText.GetGlobalBounds().Width + 5, UITextBoxText.GetGlobalBounds().Height + 5));
            box.Position = new Vector2f(x, y);
            box.FillColor = buttonTonalColors["Default"];
            prevColor = buttonTonalColors["Default"];
            UITextBoxText.Color = new Color(227, 215, 207);
            this.affected = false;
            this.cluster = cluster;
        }

        Font UITextBoxFont;
        Text UITextBoxText;
        RectangleShape box;
        bool affected = false;
        Color prevColor;
        bool mouseWasIn = false;
        int cluster = -1;
        tone tone = tone.Root;

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

        public bool Contains(int mouseX, int mouseY)
        {
            FloatRect bounds = getBoxBounds();
            if (mouseX >= bounds.Left && mouseX <= bounds.Left + bounds.Width && mouseY >= bounds.Top && mouseY <= bounds.Top + bounds.Height)
            {
                return true;
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
