using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Window;
using SFML.Graphics;

//
namespace Test
{
    class UIElement : Drawable
    {
        //constructor
        public UIElement()
        {
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
            {"Default", new Color(255, 0, 0)}, // Red
            {"Blunt",  new Color(179, 97, 255) }, //Purple
            {"Indifferent", new Color(56, 120, 255)}, // Blue
            {"Compassionate", new Color(102, 255, 149)}, // Green
            {"Hesitant", new Color(255, 225, 50)} // Yellow
        };

        //methods
        public virtual void Draw(RenderTarget target, RenderStates states)
        {
            //target.Draw();
        }

    }
}
