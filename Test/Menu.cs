using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Window;
using SFML.Graphics;
using SFML.System;

namespace Test
{
    class Menu : UIElement
    {
        public Menu(string type)
        {
            this.type = type;
            if (type == "start")
            {
                //Console.WriteLine("MENU START S_W: " + SCREEN_WIDTH + ", S_H: " + SCREEN_HEIGHT);
                MenuButtons.Add(new MenuButton(SCREEN_WIDTH / 2, SCREEN_HEIGHT / 3, "Start"));
                MenuButtons.Add(new MenuButton(SCREEN_WIDTH / 2, SCREEN_HEIGHT / 3 + (float)(SCREEN_HEIGHT*.15), "Settings"));
            }
            else if (type == "settings")
            {
                MenuButtons.Add(new MenuButton(SCREEN_WIDTH / 2, SCREEN_HEIGHT / 3, "Sound"));
                MenuButtons.Add(new MenuButton(SCREEN_WIDTH / 2, SCREEN_HEIGHT / 3 + (float)(SCREEN_HEIGHT * .15), "Back"));
            }
            else if (type == "pause")
            {
                MenuButtons.Add(new MenuButton(SCREEN_WIDTH / 2, SCREEN_HEIGHT / 2 - (float)(SCREEN_HEIGHT * .15), "Back"));
                MenuButtons.Add(new MenuButton(SCREEN_WIDTH / 2, SCREEN_HEIGHT / 2, "Settings"));
                MenuButtons.Add(new MenuButton(SCREEN_WIDTH / 2, SCREEN_HEIGHT / 2 + (float)(SCREEN_HEIGHT * .15), "Quit"));
                pauseBG = new Sprite(new Texture("../../Art/UI_Art/buttons n boxes/pausemenu.png"));
                pauseBG.Scale = new Vector2f(SCREEN_WIDTH / 1920, SCREEN_HEIGHT / 1080);
                pauseBG.Position = new Vector2f(SCREEN_WIDTH / 2 - pauseBG.GetGlobalBounds().Width / 2, SCREEN_HEIGHT / 2 - pauseBG.GetGlobalBounds().Height / 2);
            }
        }

        UInt32 SCREEN_WIDTH = VideoMode.DesktopMode.Width;
        UInt32 SCREEN_HEIGHT = VideoMode.DesktopMode.Height;
        //string type;
        List<MenuButton> MenuButtons = new List<MenuButton>();
        Sprite pauseBG;

        public void DrawPauseBG(RenderTarget target)
        {
            target.Draw(pauseBG);
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            foreach (var butt in MenuButtons)
            {
                target.Draw(butt);
            }

        }

        public List<MenuButton> getMenuButtons()
        {
            return MenuButtons;
        }

        public bool getSoundToggle()
        {
            return getMenuButtons()[0].toggleon;
        }

        public void SweepButtons(int x, int y, double scalex, double scaley)
        {
            //Console.WriteLine("MENU SWEEP BUTTONS: " + scalex + ", " + scaley);
            var buttons = getMenuButtons();
            for (var i = 0; i < buttons.Count; i++)
            {
                buttons[i].setHover((int)(x * scalex), (int)(y * scaley));
            }
        }
    }
}
