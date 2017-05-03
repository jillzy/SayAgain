using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.System;
using SFML.Window;
using SFML.Graphics;

namespace Test {
    class UIButton : UIElement {
        //constructor
        public UIButton(float x, float y, tone content) {

            this.buttonTone = content;

            buttonSprite = new Sprite(new Texture(buttonSpritePaths[content.ToString()][0]));
            buttonSpriteHighlight = new Sprite(new Texture(buttonSpritePaths[content.ToString()][1]));

            buttonSprite.Scale = new Vector2f(SCREEN_WIDTH / 1920, SCREEN_HEIGHT / 1080);
            buttonSpriteHighlight.Scale = new Vector2f(SCREEN_WIDTH / 1920, SCREEN_HEIGHT / 1080);

            this.x = x - buttonSprite.GetGlobalBounds().Width / 2;
            this.y = y;

            buttonSprite.Position = new Vector2f(this.x, this.y);
            buttonSpriteHighlight.Position = new Vector2f(this.x, this.y);

            tonalColor = buttonTonalColors[content.ToString()];
            //rect.FillColor = tonalColor;
        }

        //fields
        static UInt32 SCREEN_WIDTH = VideoMode.DesktopMode.Width;
        static UInt32 SCREEN_HEIGHT = VideoMode.DesktopMode.Height;

        Sprite buttonSprite;
        Sprite buttonSpriteHighlight;

        bool selected = false;
        bool hover = false;
        bool disabled = false;
        int mouseOffsetX = 0;
        int mouseOffsetY = 0;
        Color tonalColor;
        tone buttonTone;

        //methods
        //String eventHandler;

        public Vector2f getRectSize() {
            return new Vector2f(buttonSprite.GetGlobalBounds().Width, buttonSprite.GetGlobalBounds().Height);
        }

        public float getX() {
            return x;
        }

        public float getY() {
            return y;
        }

        public void setX(float newX) {
            x = newX;
        }

        public void setY(float newY) {
            y = newY;
        }

        public tone getTone() {
            return buttonTone;
        }

        public FloatRect getRectBounds() {
            return buttonSprite.GetGlobalBounds();
        }

        public void SetMouseOffset(int x, int y) {
            mouseOffsetX = x;
            mouseOffsetY = y;
        }

        public void SetSelected(bool val) {
            selected = val;
        }

        public void setDisabled(bool val) {
            disabled = val;
        }

        public bool getDisabled() {
            return disabled;
        }

        public bool GetSelected() {
            return selected;
        }

        public bool Contains(int mouseX, int mouseY) {
            FloatRect bounds = getRectBounds();
            if (mouseX >= bounds.Left && mouseX <= bounds.Left + bounds.Width && mouseY >= bounds.Top && mouseY <= bounds.Top + bounds.Height) {
                return true;
            }
            return false;
        }

        public void snapBack() {
            buttonSprite.Position = new Vector2f(x, y);
            buttonSpriteHighlight.Position = new Vector2f(x, y);
            //buttonText.Position = new Vector2f(x, y);
        }

        public void setHover(int mouseX, int mouseY) {
            hover = Contains(mouseX, mouseY);
        }

        public void translate(int x, int y, double winx, double winy) {
            var temp = screenHelper(winx, winy);
            var bounds = getRectBounds();
            double newXPos = x - (mouseOffsetX) * temp.Item1;
            double newYPos = y - (mouseOffsetY) * temp.Item2;

            if (x - mouseOffsetX < 0) {
                newXPos = 0;
            } else if (x - mouseOffsetX + bounds.Width > winx) {
                newXPos = winx - bounds.Width;
            }

            if (y - mouseOffsetY < 0) {
                newYPos = 0;
            } else if (y - mouseOffsetY + bounds.Height > winy) {
                newYPos = (float)winy - bounds.Height;
            }

            buttonSprite.Position = new Vector2f((float)newXPos, (float)newYPos);
            buttonSpriteHighlight.Position = new Vector2f((float)newXPos, (float)newYPos);
            //buttonText.Position = new SFML.System.Vector2f((float)newXPos, (float)newYPos);
        }

        #region screen helper
        public Tuple<double, double> screenHelper(double winx, double winy) {
            var DesktopX = (double)VideoMode.DesktopMode.Width;
            var DesktopY = (double)VideoMode.DesktopMode.Height;
            return new Tuple<double, double>(DesktopX / winx, DesktopY / winy);
        }
        #endregion


        public Color getTonalColor() {
            return tonalColor;
        }

        public override void Draw(RenderTarget target, RenderStates states) {
            //target.Draw(rect);
            if (hover && !disabled) {
                // Make button transperent when dragging
                if (selected) {
                    buttonSpriteHighlight.Color = new Color(255, 255, 255, 128);
                } else {
                    buttonSpriteHighlight.Color = new Color(255, 255, 255, 255);
                }
                target.Draw(buttonSpriteHighlight);
            } else {
                if (disabled) {
                    buttonSprite.Color = new Color(64, 64, 64);
                } else {
                    buttonSprite.Color = new Color(255, 255, 255);
                }
                target.Draw(buttonSprite);
            }
            //target.Draw(buttonText);
        }

    }
}
