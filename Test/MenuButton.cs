using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Window;
using SFML.Graphics;
using SFML.System;

namespace SayAgain {
    class MenuButton : UIElement {
        public MenuButton(float x, float y, string content) {
            this.x = x;
            this.y = y;
            this.content = content;
            menuButtonSprite = new Sprite(new Texture(buttonSpritePaths[content][0]));
            menuButtonSpriteHighlight = new Sprite(new Texture(buttonSpritePaths[content][1]));
            menuButtonSprite.Scale = scale;
            menuButtonSpriteHighlight.Scale = scale;

            menuButtonSprite.Position = new Vector2f(x - menuButtonSprite.GetGlobalBounds().Width / 2, y - menuButtonSprite.GetGlobalBounds().Height / 2);
            menuButtonSpriteHighlight.Position = new Vector2f(x - menuButtonSprite.GetGlobalBounds().Width / 2, y - menuButtonSprite.GetGlobalBounds().Height / 2);




            if (content == "Sound") {
                menuSoundUntoggleSprite = new Sprite(new Texture(buttonSpritePaths[content][2]));
                menuSoundUntoggleSpriteHighlight = new Sprite(new Texture(buttonSpritePaths[content][3]));
                menuSoundUntoggleSprite.Scale = scale;
                menuSoundUntoggleSpriteHighlight.Scale = scale;

                menuSoundUntoggleSprite.Position = new Vector2f(x - menuSoundUntoggleSprite.GetGlobalBounds().Width / 2, y - menuSoundUntoggleSprite.GetGlobalBounds().Height / 2);
                menuSoundUntoggleSpriteHighlight.Position = new Vector2f(x - menuSoundUntoggleSprite.GetGlobalBounds().Width / 2, y - menuSoundUntoggleSprite.GetGlobalBounds().Height / 2);
            }

        }

        static UInt32 SCREEN_WIDTH = 1920;
        static UInt32 SCREEN_HEIGHT = 1080;
        Vector2f scale = new Vector2f(SCREEN_WIDTH / 1920, SCREEN_HEIGHT / 1080);
        Sprite menuButtonSprite;
        Sprite menuButtonSpriteHighlight;
        Sprite menuSoundUntoggleSprite;
        Sprite menuSoundUntoggleSpriteHighlight;
        string content;
        bool hover = false;
        public bool toggleon = true;

        public void setHover(int mouseX, int mouseY) {
            hover = Contains(mouseX, mouseY);
        }

        public Sprite getMenuButtonSprite() {
            return menuButtonSprite;
        }

        public string getMenuButtonContent() {
            return content;
        }

        public FloatRect getRectBounds() {
            return menuButtonSprite.GetGlobalBounds();
        }

        public bool Contains(int mouseX, int mouseY) {
            FloatRect bounds = getRectBounds();
            if (mouseX >= bounds.Left && mouseX <= bounds.Left + (bounds.Width - 4) && mouseY >= bounds.Top && mouseY <= bounds.Top + (bounds.Height - 4)) {
                return true;
            }
            return false;
        }

        public override void Draw(RenderTarget target, RenderStates states) {
            //target.Draw(rect);
            if (content == "Sound") {
                if (hover) {
                    if (toggleon) {
                        target.Draw(menuButtonSpriteHighlight);
                    } else {
                        target.Draw(menuSoundUntoggleSpriteHighlight);
                    }
                } else {
                    if (!toggleon) {
                        target.Draw(menuSoundUntoggleSprite);
                    } else {
                        target.Draw(menuButtonSprite);
                    }


                }
            } else {
                if (hover) {
                    target.Draw(menuButtonSpriteHighlight);
                } else {

                    target.Draw(menuButtonSprite);

                }
            }

        }
    }
}
