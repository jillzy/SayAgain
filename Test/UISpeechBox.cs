using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;
using SFML.System;
using SFML.Window;
//
namespace Test
{
    class UISpeechBox
    {
        public UISpeechBox(float width, float height, float x, float y, string dialogue, string person) {
            UISpeechBoxFont = new Font("Content/ARCADECLASSIC.ttf");
            UISpeechBoxText = new Text(dialogue, UISpeechBoxFont);
            UINameBoxText = new Text(person, UISpeechBoxFont);

            UISpeechBoxText.Position = new Vector2f(x, y);
            UINameBoxText.Position = new Vector2f(x + 40, y - 35);

            SpeechBox = new RectangleShape(new Vector2f(width, height));
            NameBox = new RectangleShape(new Vector2f(150, 50));

            SpeechBox.Position = new Vector2f(x, y);
            NameBox.Position = new Vector2f(x + 40, y - 35);

            SpeechBox.FillColor = Color.Black;
            UISpeechBoxText.Color = Color.White;
            NameBox.FillColor = Color.Blue;
            UINameBoxText.Color = Color.White;
        }

        public RectangleShape getSpeechBox()
        {
            return SpeechBox;
        }

        public Text getSpeechBoxText()
        {
            return UISpeechBoxText;
        }

        public RectangleShape getNameBox()
        {
            return NameBox;
        }

        public Text getNameBoxText()
        {
            return UINameBoxText;
        }



        Font UISpeechBoxFont;
        Text UISpeechBoxText;
        RectangleShape SpeechBox;
        RectangleShape NameBox;
        Text UINameBoxText;
        //bool started = false;
        //int printTime = 0;
    }
}
