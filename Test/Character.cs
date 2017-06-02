using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using SFML.Graphics;
using SFML.Window;
using SFML.System;


//todo: share random
//multiple interesting frames
namespace SayAgain {

    abstract class Character : Drawable {
        protected double[] FNCRange = new double[10]; //HF-MF-LF-LN-MN-HN-LC-MC-HC
        protected double currentFNC;



        public int index = 0;

        protected Random r = new Random();
        protected int rnd, rnd2;


        private List<Sprite> lipsprites = new List<Sprite>();
        private List<Sprite> sprites = new List<Sprite>();

        public DateTime time = DateTime.Now;

        protected float xpos, ypos, xscale, yscale, mouthPosX, mouthPosY;
        protected bool canTalk = false;
        public CharacterState state;
        public enum spriteEmotion { happy, angry, neutral, sad };
        public abstract void setSpriteEmotion(spriteEmotion e);
        public abstract void checkFNC();

        protected uint SCREEN_WIDTH = 1920;
        protected uint SCREEN_HEIGHT = 1080;


        public bool isTalking = false;

        protected bool hide = false;

        public bool getHide() {
            return hide;
        }

        public void setHide(bool v) {
            hide = v;
        }












        public void active(bool b) {
            if (b) {
                canTalk = true;
                //undim();
            } else if (!b) {
                canTalk = false;
               // dim();
            }
        }

        public void setTalking(bool b) {
            if (b) {
                isTalking = true;
            } else if (!b) {
                isTalking = false;
            }
        }


        public string fncState() {
            if (currentFNC < FNCRange[3]) return "frust";
            else if (currentFNC > FNCRange[7]) return "coop";
            else return "neut";
        }

        public void setSprite(List<Sprite> s) {
            sprites = s;
        }


        public virtual void Draw(RenderTarget target, RenderStates states) {

        }


        public abstract void dim();
        public abstract void undim();


        public abstract Vector2f getArmPosition();

        public abstract void setArmPosition(Vector2f position);


        public double[] getFNCRange() {
            return FNCRange;
        }

        public double getCurrentFNC() {
            return currentFNC;
        }

        public void setCurrentFNC(double d) {
            currentFNC = d;
        }

        public void changeFNC(double i) {
            currentFNC += i;
        }
        public void click() {
            if (sprites != null) {
                /////.http://stackoverflow.com/questions/23530360/how-do-you-make-a-clickable-sprite-in-sfml
                if (sprites[0].GetGlobalBounds().Contains
                    (Mouse.GetPosition().X, Mouse.GetPosition().Y)) {
                }

            }
        }

        public Character() {

            state = new CharacterState();

        }
    }
}
