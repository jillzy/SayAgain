﻿using System;
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
        int currentMouthIndex = 0; //variable to keep track of mouths for drawing


        Texture t = new Texture("../../Art/momsprites.png");
        Texture t2 = new Texture("../../Art/momsprites2.png");
        string expr;
        Dictionary<string, List<Sprite>> sprites = new Dictionary<string, List<Sprite>>() { { "angry", new List<Sprite>() },
                                                                                            { "happy", new List<Sprite>() },
                                                                                            { "neutral", new List<Sprite>() },
                                                                                            { "sad", new List<Sprite>() }
                                                                                           };

        Dictionary<string, List<Sprite>> noMouthSprites = new Dictionary<string, List<Sprite>>() { { "angry", new List<Sprite>() },
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
                    //Console.WriteLine("1");
                }
                else if (rnd == 1)
                {
                    longerframe = 6;
                    //Console.WriteLine("2");

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

        public override void setArmPosition(Vector2f position) {
            throw new NotImplementedException();
        }

        public override Vector2f getArmPosition() {
            throw new NotImplementedException();
        }


        void returnToRestMouth()
        {
            //mouthSprite.Position = new Vector2f(-100, -100);
            //hide the sprite off screen, so don't have to destroy
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            rnd = r.Next(4, 14);
            





            if (isTalking)
            {
                target.Draw(noMouthSprites[expr][index]);
                //cycle between open mouth and rest mouth

                if (currentMouthIndex == 0) //rest mouth
                {

                    Console.WriteLine("C L O S E");


                }

                else if (currentMouthIndex == 1)//open mouth
                {
                    Console.WriteLine("open the mouth");
                    

                }

                if ((DateTime.Now - time).TotalMilliseconds > (1400f / framerate))
                {
                    time = DateTime.Now;
                    if (currentMouthIndex >= 1)
                    {
                        currentMouthIndex = 0;
                    }
                    else if (currentMouthIndex == 0)
                    {
                        currentMouthIndex = 1;
                    }
                }



            }
            if (!isTalking)
            {
                target.Draw(sprites[expr][index]);
                //stay at the default mouth
                returnToRestMouth();
            }


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

                if (isTalking && ++index >= noMouthSprites[expr].Count)
                {
                    pickSpecialFrame();
                    index = 0;
                }

                if (!isTalking && ++index >= sprites[expr].Count)
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
            ypos = (float)(SCREEN_HEIGHT*0.29);
            xscale = (float)((SCREEN_WIDTH / 1920) * 0.9);
            yscale = (float)((SCREEN_HEIGHT / 1080) * 0.9);


            for (int i = 0; i < (361 * 7); i += 361)
            {
                sprites["angry"].Add(new Sprite(t, new IntRect(i, 0, 361, 465))); //btw might get extra sprite if sizes no precise
                sprites["angry"][sprites["angry"].Count - 1].Scale = new Vector2f(xscale, yscale);
                sprites["angry"][sprites["angry"].Count - 1].Position = new Vector2f(xpos - sprites["angry"][0].GetGlobalBounds().Width/2, ypos);

                noMouthSprites["angry"].Add(new Sprite(t2, new IntRect(i, 0, 361, 465))); //btw might get extra sprite if sizes no precise
                noMouthSprites["angry"][noMouthSprites["angry"].Count - 1].Scale = new Vector2f(xscale, yscale);
                noMouthSprites["angry"][noMouthSprites["angry"].Count - 1].Position = new Vector2f(xpos - sprites["angry"][0].GetGlobalBounds().Width / 2, ypos);


            }
            for (int i = 0; i < (361 * 9); i += 361)
            {
                sprites["happy"].Add(new Sprite(t, new IntRect(i, 465, 361, 465))); //second row of sprites; happy epression
                sprites["happy"][sprites["happy"].Count - 1].Scale = new Vector2f(xscale, yscale);
                sprites["happy"][sprites["happy"].Count - 1].Position = new Vector2f(xpos - sprites["happy"][0].GetGlobalBounds().Width / 2, ypos);

                noMouthSprites["happy"].Add(new Sprite(t2, new IntRect(i, 465, 361, 465))); //second row of sprites; happy epression
                noMouthSprites["happy"][noMouthSprites["happy"].Count - 1].Scale = new Vector2f(xscale, yscale);
                noMouthSprites["happy"][noMouthSprites["happy"].Count - 1].Position = new Vector2f(xpos - sprites["happy"][0].GetGlobalBounds().Width / 2, ypos);

            }
            for (int i = 0; i < (361 * 4); i += 361)
            {
                sprites["neutral"].Add(new Sprite(t, new IntRect(i, 465 * 2, 361, 465)));
                sprites["neutral"][sprites["neutral"].Count - 1].Scale = new Vector2f(xscale, yscale);
                sprites["neutral"][sprites["neutral"].Count - 1].Position = new Vector2f(xpos - sprites["neutral"][0].GetGlobalBounds().Width / 2, ypos);

                noMouthSprites["neutral"].Add(new Sprite(t2, new IntRect(i, 465 * 2, 361, 465)));
                noMouthSprites["neutral"][noMouthSprites["neutral"].Count - 1].Scale = new Vector2f(xscale, yscale);
                noMouthSprites["neutral"][noMouthSprites["neutral"].Count - 1].Position = new Vector2f(xpos - sprites["neutral"][0].GetGlobalBounds().Width / 2, ypos);
            }
            for (int i = 0; i < (361 * 4); i += 361)
            {
                sprites["sad"].Add(new Sprite(t, new IntRect(i, 465 * 3, 361, 465)));
                sprites["sad"][sprites["sad"].Count - 1].Scale = new Vector2f(xscale, yscale);
                sprites["sad"][sprites["sad"].Count - 1].Position = new Vector2f(xpos - sprites["sad"][0].GetGlobalBounds().Width / 2, ypos);

                noMouthSprites["sad"].Add(new Sprite(t2, new IntRect(i, 465 * 3, 361, 465)));
                noMouthSprites["sad"][noMouthSprites["sad"].Count - 1].Scale = new Vector2f(xscale, yscale);
                noMouthSprites["sad"][noMouthSprites["sad"].Count - 1].Position = new Vector2f(xpos - sprites["sad"][0].GetGlobalBounds().Width / 2, ypos);

            }

        }
    }
}