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
    class Mom : Character
    {
        float framerate = 4f;
        int prevIndex = -1;
        int longerframe;

        Texture t = new Texture("../../Art/momsprites.png");
        string expr;
        Dictionary<string, List<Sprite>> sprites = new Dictionary<string, List<Sprite>>() { { "angry", new List<Sprite>() },
                                                                                            { "happy", new List<Sprite>() },
                                                                                            { "neutral", new List<Sprite>() },
                                                                                            { "sad", new List<Sprite>() }
                                                                                           };
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
                int rnd = r.Next(0, 3);

                if (rnd == 0)
                {
                    longerframe = 1;
                    Console.WriteLine("1");
                }
                else if (rnd == 1)
                {
                    longerframe = 6;
                    Console.WriteLine("2");

                }
            }

            if (expr == "angry")
            {
                int rnd = r.Next(0, 3);

                if (rnd == 0)
                {
                    longerframe = 0;
                }
                else if (rnd == 1)
                {
                    longerframe = 3;

                }
            }
            if (expr == "sad")
            {
               
                longerframe = 0;
               
               
            }
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            rnd = r.Next(4, 14);
            


            target.Draw(sprites[expr][index]);

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


        public Mom()
        {
            FNCRange[0] = -10;
            FNCRange[1] = -8.66;
            FNCRange[2] = -7.33;
            FNCRange[3] = -6;
            FNCRange[4] = -2;
            FNCRange[5] = 2;
            FNCRange[6] = 6;
            FNCRange[7] = 7.33;
            FNCRange[8] = 8.66;
            FNCRange[9] = 10;

            currentFNC = -1;

            //determine size and position
            xpos = (float)(SCREEN_WIDTH*.79);
            ypos = (float)(SCREEN_HEIGHT*0.28);
            xscale = SCREEN_WIDTH / 1920;
            yscale = SCREEN_HEIGHT / 1080;

            for (int i = 0; i < (361 * 7); i += 361)
            {
                sprites["angry"].Add(new Sprite(t, new IntRect(i, 0, 361, 465))); //btw might get extra sprite if sizes no precise
                sprites["angry"][sprites["angry"].Count - 1].Scale = new Vector2f(xscale, yscale);
                sprites["angry"][sprites["angry"].Count - 1].Position = new Vector2f(xpos - sprites["angry"][0].GetGlobalBounds().Width/2, ypos);
            }
            for (int i = 0; i < (361 * 9); i += 361)
            {
                sprites["happy"].Add(new Sprite(t, new IntRect(i, 465, 361, 465))); //second row of sprites; happy epression
                sprites["happy"][sprites["happy"].Count - 1].Scale = new Vector2f(xscale, yscale);
                sprites["happy"][sprites["happy"].Count - 1].Position = new Vector2f(xpos - sprites["happy"][0].GetGlobalBounds().Width / 2, ypos);
            }
            for (int i = 0; i < (361 * 4); i += 361)
            {
                sprites["neutral"].Add(new Sprite(t, new IntRect(i, 465 * 2, 361, 465)));
                sprites["neutral"][sprites["neutral"].Count - 1].Scale = new Vector2f(xscale, yscale);
                sprites["neutral"][sprites["neutral"].Count - 1].Position = new Vector2f(xpos - sprites["neutral"][0].GetGlobalBounds().Width / 2, ypos);
            }
            for (int i = 0; i < (361 * 4); i += 361)
            {
                sprites["sad"].Add(new Sprite(t, new IntRect(i, 465 * 3, 361, 465)));
                sprites["sad"][sprites["sad"].Count - 1].Scale = new Vector2f(xscale, yscale);
                sprites["sad"][sprites["sad"].Count - 1].Position = new Vector2f(xpos - sprites["sad"][0].GetGlobalBounds().Width / 2, ypos);
            }
            
        }
    }
}