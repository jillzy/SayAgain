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
using System.Drawing;


//todo: share random
//multiple interesting frames
namespace Test
{
    
    abstract class Character: Drawable
    {

        protected double[] FNCRange = new double[10]; //HF-MF-LF-LN-MN-HN-LC-MC-HC
        protected double currentFNC;

        public int index = 0;

        protected Random r = new Random();
        protected int rnd;


        private List<Sprite> lipsprites = new List<Sprite>();
        private List<Sprite> sprites = new List<Sprite>();

        public DateTime time = DateTime.Now;
        protected float xpos, ypos, xscale, yscale;
        protected bool canTalk = false;
        public CharacterState state;
        public enum spriteEmotion { happy, angry, neutral, sad };
        public abstract void setSpriteEmotion(spriteEmotion e);  
        public abstract void checkFNC();

        protected uint SCREEN_WIDTH = VideoMode.DesktopMode.Width;
        protected uint SCREEN_HEIGHT = VideoMode.DesktopMode.Height;

        public void dim()
        {

            foreach (Sprite s in sprites)
            {
                s.Color = new Color(s.Color.R, s.Color.G, s.Color.B, 180);
            }
        }

        public double[] getFNCRange()
        {
            return FNCRange;
        }

        public void undim()
        {

            foreach (Sprite s in sprites)
            {
                s.Color = new Color(s.Color.R, s.Color.G, s.Color.B, 255);
            }
        }

        public void active(bool b)
        {
            if (b)
            {
                canTalk = true;
                undim();
            } else if (!b)
            {
                canTalk = false;
                dim();
            }
        }
  
        public void setSprite(List<Sprite> s)
        {
            sprites = s;
        }


        public virtual void Draw(RenderTarget target, RenderStates states)
        {
            
        }
        
        public double[] getSpectrum()
        {
            return FNCRange;
        }

        public double getCurrentFNC()
        {
            return currentFNC;
        }

        public void changeFNC()
        {

        }
        public void click()
        {
            if (sprites != null)
            {
                /*if (Mouse.GetPosition().X >= (x * SA.getW())
                    && Mouse.GetPosition().X <= x * (SA.getW()) + sprites[0].GetGlobalBounds().Width
                    && Mouse.GetPosition().Y >= (y * SA.getH())
                    && Mouse.GetPosition().Y <= (y * SA.getH()) + sprites[0].GetGlobalBounds().Height)
                */  

                    //http://stackoverflow.com/questions/23530360/how-do-you-make-a-clickable-sprite-in-sfml
                    if (sprites[0].GetGlobalBounds().Contains
                        (Mouse.GetPosition().X, Mouse.GetPosition().Y)) {
                            Console.WriteLine("clicked mom");
                        }

            }
}


        public Character() {

            state = new CharacterState();

        }
    }
}
