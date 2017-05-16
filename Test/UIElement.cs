using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Window;
using SFML.Graphics;

//
namespace Test {
    class UIElement : Drawable {
        //constructor
        public UIElement() {
            this.size = 0;
            this.x = 0;
            this.y = 0;
        }

        //fields
        protected float size;
        protected float x;
        protected float y;
        protected string type;
        protected Dictionary<string, Color> buttonTonalColors = new Dictionary<string, Color>() {
            {"Default", new Color(214, 214, 214)}, // Red
            {"Blunt",  new Color(207, 162, 252) }, //Purple
            {"Indifferent", new Color(116, 123, 247)}, // Blue
            {"Compassionate", new Color(158, 222, 89)}, // Green
            {"Hesitant", new Color(255, 198, 76)} // Orange you glad I didnt say yellow
        };

        protected Dictionary<string, List<string>> buttonSpritePaths = new Dictionary<string, List<string>> {
            {"Default", new List<string>() {"", ""}}, // Red
            {"Blunt",   new List<string>() {"../../Art/UI_Art/buttons n boxes/purplebuttontext.png", "../../Art/UI_Art/buttons n boxes/purplebuttontexthighlight.png"}}, //Purple
            {"Indifferent", new List<string>() {"../../Art/UI_Art/buttons n boxes/bluebuttontext.png", "../../Art/UI_Art/buttons n boxes/bluebuttontexthighlight.png"}}, // Blue
            {"Compassionate", new List<string>() {"../../Art/UI_Art/buttons n boxes/greenbuttontext.png", "../../Art/UI_Art/buttons n boxes/greenbuttontexthighlight.png"}}, // Green
            {"Hesitant", new List<string>() {"../../Art/UI_Art/buttons n boxes/orangebuttontext.png", "../../Art/UI_Art/buttons n boxes/orangebuttontexthighlight.png"}}, // Yellow
            {"Start", new List<string>() { "../../Art/UI_Art/buttons n boxes/startmenu.png", "../../Art/UI_Art/buttons n boxes/startmenuhighlight.png"}}, //Start Menu Button
            {"Settings", new List<string>() { "../../Art/UI_Art/buttons n boxes/settingsmenu.png", "../../Art/UI_Art/buttons n boxes/settingsmenuhighlight.png"}}, //Settings Menu Button
            {"Back", new List<string>() { "../../Art/UI_Art/buttons n boxes/backmenu.png", "../../Art/UI_Art/buttons n boxes/backmenuhighlight.png"}}, //Back Menu Button
            {"Quit", new List<string>() { "../../Art/UI_Art/buttons n boxes/quitmenu.png", "../../Art/UI_Art/buttons n boxes/quitmenuhighlight.png"}}, //Quit Menu Button
            {"Sound", new List<string>() { "../../Art/UI_Art/buttons n boxes/soundmenuc.png", "../../Art/UI_Art/buttons n boxes/soundmenuchighlight.png", "../../Art/UI_Art/buttons n boxes/soundmenuuc.png", "../../Art/UI_Art/buttons n boxes/soundmenuuchighlight.png"}}, //Sound Menu Button
        };

        //methods
        public virtual void Draw(RenderTarget target, RenderStates states) {
            //target.Draw();
        }

        public bool inRange(int value, float min, float max) {
            return value >= min && value <= max;
        }

    }
}
