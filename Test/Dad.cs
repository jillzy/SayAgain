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
    class Dad : Character, Drawable
    {

        int currentMouthIndex = 0; //variable to keep track of mouths for drawing
        float framerate = 4f;
        int prevIndex = -1;

        Texture t = new Texture("../../Art/dadMasterwoArm.png");
        Texture mouth = new Texture("../../Art/DadMouth.png");

        Dictionary<string, List<Sprite>> sprites = new Dictionary<string, List<Sprite>>() { { "angry", new List<Sprite>() },
                                                                                            { "happy", new List<Sprite>() },
                                                                                            { "neutral", new List<Sprite>() }
                                                                                           };
        Sprite mouthSprite;
        string expr;

        public override void checkFNC()
        {
            throw new NotImplementedException();
        }

        public override void setArmPosition(Vector2f position) {
            throw new NotImplementedException();
        }

        public override Vector2f getArmPosition() {
            return new Vector2f(sprites["neutral"][0].Position.X, sprites["neutral"][0].Position.Y + 224);
        }

        public override void setSpriteEmotion(spriteEmotion e)
        {

           
            if (e.ToString() != "sad") expr = e.ToString();
        }
        
        void returnToRestMouth()
        {
            mouthSprite.Position = new Vector2f(-100, -100);
                //hide the sprite off screen, so don't have to destroy
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            rnd = r.Next(4, 14);
            rnd2 = r.Next(2, 4);
            target.Draw(sprites[expr][index]);
            target.Draw(mouthSprite);


            if (isTalking)
            {
                
                //cycle between open mouth and rest mouth

                if (currentMouthIndex == 0) //rest mouth
                {

                    Console.WriteLine("C L O S E");
                    returnToRestMouth();
                    framerate = (float)rnd2;

                }

                else if (currentMouthIndex == 1)//open mouth
                {
                    Console.WriteLine("open the mouth");
                    mouthSprite.Position = new Vector2f(xpos - 35, ypos + 123);
                    framerate = (float) rnd;
                    
                }

                if ((DateTime.Now - time).TotalMilliseconds > (1400f / framerate))
                {
                    time = DateTime.Now;
                    if (currentMouthIndex >= 1)
                    {
                        currentMouthIndex = 0;
                    } else if (currentMouthIndex == 0)
                    {
                        currentMouthIndex = 1;
                    }
                }

                
                
            } if (!isTalking)
            {
                //stay at the default mouth
                returnToRestMouth();
            }

            

            if (index == 0 && prevIndex != 0)
            {
                framerate = framerate/(float)rnd;
                prevIndex = 0;
            }

            else if (index != 0)
            {
                prevIndex = index - 1;
                framerate = 4f;
            }

            if ((DateTime.Now - time).TotalMilliseconds > (1400f / framerate))
            {
                time = DateTime.Now;
                if (++index >= sprites[expr].Count)
                {
                    index = 0;
                }
            }


        }

        public Dad()
        {
            FNCRange[0] = -10;
            FNCRange[1] = -7.33;
            FNCRange[2] = -4.66;
            FNCRange[3] = -2;
            FNCRange[4] = -0.77;
            FNCRange[5] = 0.66;
            FNCRange[6] = 2;
            FNCRange[7] = 4.66;
            FNCRange[8] = 7.33;
            FNCRange[9] = 10;
            currentFNC = -1;

            //determine size and position
            xpos = (float)(SCREEN_WIDTH * .21);
            ypos = (float)(SCREEN_HEIGHT * 0.28);

            xscale = SCREEN_WIDTH / 1920;
            yscale = SCREEN_HEIGHT / 1080;

            for (int i = 0; i < (343 * 4); i += 343)
            {

                sprites["angry"].Add(new Sprite(t, new IntRect(i, 0, 343, 454))); //btw might get extra sprite if sizes no precise
                sprites["angry"][sprites["angry"].Count - 1].Scale = new Vector2f(xscale, yscale);
                sprites["angry"][sprites["angry"].Count - 1].Position = new Vector2f(xpos - sprites["angry"][0].GetGlobalBounds().Width / 2, ypos);

            }
            for (int i = 0; i < (343 * 4); i += 343)
            {
                sprites["happy"].Add(new Sprite(t, new IntRect(i, 454, 343, 454))); //second row of sprites; happy epression
                sprites["happy"][sprites["happy"].Count - 1].Scale = new Vector2f(xscale, yscale);
                sprites["happy"][sprites["happy"].Count - 1].Position = new Vector2f(xpos - sprites["happy"][0].GetGlobalBounds().Width / 2, ypos);
            }
            for (int i = 0; i < (343 * 4); i += 343)
            {

                sprites["neutral"].Add(new Sprite(t, new IntRect(i, 454 * 2, 343, 454)));
                sprites["neutral"][sprites["neutral"].Count - 1].Scale = new Vector2f(xscale, yscale);
                sprites["neutral"][sprites["neutral"].Count - 1].Position = new Vector2f(xpos - sprites["neutral"][0].GetGlobalBounds().Width / 2, ypos);

            }

            mouthSprite = new Sprite(mouth);

        }
    }
}