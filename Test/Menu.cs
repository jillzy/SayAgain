using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Window;
using SFML.Graphics;

namespace Test
{
    class Menu : UIElement
    {
        public Menu(string type)
        {
            this.type = type;
            if (type == "start")
            {
                MenuButtons.Add(new MenuButton(SCREEN_WIDTH / 2, SCREEN_HEIGHT / 3, "Game Start"));
                MenuButtons.Add(new MenuButton(SCREEN_WIDTH / 2, SCREEN_HEIGHT / 3 + 200, "Settings"));
            }
            else if (type == "settings")
            {
                MenuButtons.Add(new MenuButton(SCREEN_WIDTH / 2, SCREEN_HEIGHT / 3, "8K GAMING"));
                MenuButtons.Add(new MenuButton(SCREEN_WIDTH / 2, SCREEN_HEIGHT / 3 + 200, "<- Back"));
            }
            else if (type == "pause")
            {
                MenuButtons.Add(new MenuButton(SCREEN_WIDTH / 2, SCREEN_HEIGHT / 3 - 100, "Back to Game"));
                MenuButtons.Add(new MenuButton(SCREEN_WIDTH / 2, SCREEN_HEIGHT / 3 + 100, "Settings"));
                MenuButtons.Add(new MenuButton(SCREEN_WIDTH / 2, SCREEN_HEIGHT / 3 + 300, "Quit"));
            }
            bg = new RectangleShape(new SFML.System.Vector2f(SCREEN_WIDTH / 3, SCREEN_HEIGHT - 500));
            bg.Position = new SFML.System.Vector2f((SCREEN_WIDTH / 2) - bg.GetGlobalBounds().Width / 2, (SCREEN_HEIGHT / 2) - bg.GetGlobalBounds().Height / 2 - 100);
            bg.FillColor = new Color(200, 45, 17);
        }

        static UInt32 SCREEN_WIDTH = VideoMode.DesktopMode.Width;
        static UInt32 SCREEN_HEIGHT = VideoMode.DesktopMode.Height;

        List<MenuButton> MenuButtons = new List<MenuButton>();
        RectangleShape bg;

        public void DrawBG(RenderWindow target)
        {
            target.Draw(bg);
        }


        public override void Draw(RenderTarget target, RenderStates states)
        {

            foreach (var butt in MenuButtons)
            {
                target.Draw(butt.getMenuButtonRect());
                target.Draw(butt.getMenuButtonText());
            }

        }

        public List<MenuButton> getMenuButtons()
        {
            return MenuButtons;
        }
    }
}
