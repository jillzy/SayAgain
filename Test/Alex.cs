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

namespace Test
{
    class Alex : Character
    {
        //private View _view;
        Texture t = new Texture("../../Art/alexMaster.png");
        string expr;
        int longerframe;
        Dictionary<string, List<Sprite>> sprites = new Dictionary<string, List<Sprite>>() { { "angry", new List<Sprite>() },
                                                                                            { "happy", new List<Sprite>() },
                                                                                            { "neutral", new List<Sprite>() }
                                                                                           };
        float framerate = 4f;
        int prevIndex = -1;

        public override void checkFNC()
        {
            throw new NotImplementedException();
        }



        public override void setSpriteEmotion(spriteEmotion e)
        {
            expr = e.ToString();
            
        }

        public void pickSpecialFrame()
        {
            if (expr == "neutral")
            {
                longerframe = 0;
            }
            if (expr == "happy")
            {
                int rnd = r.Next(0, 4);

                if (rnd == 0)
                {
                    longerframe = 1;
                }
                else if (rnd == 1)
                {
                    longerframe = 4;

                }
                else if (rnd == 2)
                {
                    longerframe = 8;
                }
            }

            if (expr == "angry")
            {
                int rnd = r.Next(0, 4);

                if (rnd == 0)
                {
                    longerframe = 2;
                }
                else if (rnd == 1)
                {
                    longerframe = 8;

                }
            }
        }


        public override void Draw(RenderTarget target, RenderStates states)
        {
            rnd = r.Next(4, 14);
            
            // neutral expressoin
            //dont want draw to contain any logic about which expression,
            //so have the interested index defined somewhere else
            if (index == longerframe && prevIndex != longerframe)
            {
                framerate = framerate / (float)rnd;
                prevIndex = longerframe;
            }

            else if (index != longerframe)
            {
                prevIndex = index - 1;
                framerate = 4f;
            }

            target.Draw(sprites[expr][index]);
            if ((DateTime.Now - time).TotalMilliseconds > (1400f / framerate))
            {
                time = DateTime.Now;
                if (++index >= sprites[expr].Count)
                {
                    pickSpecialFrame();
                    index = 0;
                }
            }
        }

        public Alex()
        {
            FNCRange[0] = -10;
            FNCRange[1] = -8;
            FNCRange[2] = -6;
            FNCRange[3] = -4;
            FNCRange[4] = -0.33;
            FNCRange[5] = 3.33;
            FNCRange[6] = 7;
            FNCRange[7] = 8;
            FNCRange[8] = 9;
            FNCRange[9] = 10;

            currentFNC = -1;

            //determine size and position
            xpos = (float)(SCREEN_WIDTH*0.5);
            ypos = (float)(SCREEN_HEIGHT*0.37);
            xscale = SCREEN_WIDTH / 1920;
            yscale = SCREEN_HEIGHT / 1080;

            for (int i = 0; i < (361 * 4); i += 361)
            {
                sprites["neutral"].Add(new Sprite(t, new IntRect(i, 0, 361, 449)));
                sprites["neutral"][sprites["neutral"].Count - 1].Scale = new Vector2f(xscale, yscale);
                sprites["neutral"][sprites["neutral"].Count - 1].Position = new Vector2f(xpos - sprites["neutral"][0].GetGlobalBounds().Width/2, ypos);
            }
            for (int i = 0; i < (361 * 9); i += 361)
            {
                sprites["happy"].Add(new Sprite(t, new IntRect(i, 449, 361, 449)));
                sprites["happy"][sprites["happy"].Count - 1].Scale = new Vector2f(xscale, yscale);
                sprites["happy"][sprites["happy"].Count - 1].Position = new Vector2f(xpos - sprites["happy"][0].GetGlobalBounds().Width/2, ypos);
            }
            for (int i = 0; i < (337 * 9); i += 337)
            {
                
                sprites["angry"].Add(new Sprite(t, new IntRect(i, 449 * 2, 337, 449)));
                sprites["angry"][sprites["angry"].Count - 1].Scale = new Vector2f(xscale, yscale);
                sprites["angry"][sprites["angry"].Count - 1].Position = new Vector2f(xpos - sprites["angry"][0].GetGlobalBounds().Width/2, ypos);
            }
        }
    }
}
