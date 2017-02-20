using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Window;
using SFML.Graphics;

namespace Test
{
    class MenuButton : UIElement
    {
        public MenuButton(float x, float y, string content)
        {
            this.x = x;
            this.y = y;
            buttonText = new Text(content, buttonTextFont);

            buttonText.Position = new SFML.System.Vector2f(x - buttonText.GetGlobalBounds().Width/2, y);

            rect = new RectangleShape(new SFML.System.Vector2f(buttonText.GetGlobalBounds().Width + 7, buttonText.GetGlobalBounds().Height + 10));
            rect.Position = new SFML.System.Vector2f(this.x - buttonText.GetGlobalBounds().Width/2, this.y);
            rect.FillColor = Color.Black;
            Color myColor = new Color(177, 177, 177);
            rect.FillColor = myColor;
        }

        static UInt32 SCREEN_WIDTH = VideoMode.DesktopMode.Width;
        static UInt32 SCREEN_HEIGHT = VideoMode.DesktopMode.Height;

        Font buttonTextFont = new Font("../../Fonts/Adore64.ttf");
        Text buttonText;
        RectangleShape rect;
        bool selected = false;

        public RectangleShape getMenuButtonRect()
        {
            return rect;
        }

        public Text getMenuButtonText()
        {
            return buttonText;
        }

        public FloatRect getRectBounds()
        {
            return rect.GetGlobalBounds();
        }

        public bool Contains(int mouseX, int mouseY)
        {
            FloatRect bounds = getRectBounds();
            if (mouseX >= bounds.Left && mouseX <= bounds.Left + bounds.Width && mouseY >= bounds.Top && mouseY <= bounds.Top + bounds.Height)
            {
                return true;
            }
            return false;
        }
    }
}
