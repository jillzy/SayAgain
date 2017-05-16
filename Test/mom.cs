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
        int currentMouthIndex = -1; //variable to keep track of mouths for drawing
        Sprite happyrest, sadrest, angryrest, neutralrest;


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

        List<Sprite> mouths = new List<Sprite>();
        
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
                }
                else if (rnd == 1)
                {
                    longerframe = 6;
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


        void hideMouth(int i)
        {
            mouths[i].Position = new Vector2f(-100, -100);
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            rnd = r.Next(4, 14);
            rnd2 = r.Next(2, 4);


            if (!hide)
            {
                if (isTalking)
                {

                    target.Draw(noMouthSprites[expr][index]);


                    if (currentMouthIndex == -1) //rest mouth
                    {
                        hideMouth(4);
                        framerate = (float)rnd2;
                        if (expr == "happy")
                        {
                            happyrest.Position = (new Vector2f(xpos - 50, ypos + 155));
                            happyrest.Scale = (new Vector2f(0.62f, 0.62f));
                            target.Draw(happyrest);
                        }
                        else if (expr == "angry")
                        {
                            angryrest.Position = (new Vector2f(xpos - 48, ypos + 150));
                            angryrest.Scale = (new Vector2f(0.62f, 0.62f));
                            target.Draw(angryrest);
                        }
                        else if (expr == "sad")
                        {
                            sadrest.Position = (new Vector2f(xpos - 45, ypos + 150));
                            sadrest.Scale = (new Vector2f(0.62f, 0.62f));
                            target.Draw(sadrest);
                        }
                        else if (expr == "neutral")
                        {
                            neutralrest.Position = (new Vector2f(xpos - 45, ypos + 153));
                            neutralrest.Scale = (new Vector2f(0.62f, 0.62f));
                            target.Draw(neutralrest);
                        }




                    }

                    else if (currentMouthIndex >= 0 && currentMouthIndex < 5)//open mouth
                    {
                        framerate = 15;


                        if (currentMouthIndex >= 1)
                        {
                            //hide previous mouth
                            //hideMouth(currentMouthIndex - 1);
                        }
                        mouths[currentMouthIndex].Scale = new Vector2f(0.8f, 0.8f);
                        mouths[currentMouthIndex].Position = new Vector2f(xpos - 43, ypos + 152);
                        target.Draw(mouths[currentMouthIndex]);


                        /*
                         *   
                        if (currentMouthIndex == 4)
                        {
                            currentMouthIndex = -1;
                        } else
                        {
                            currentMouthIndex += 1;
                        }*/


                    }

                    if ((DateTime.Now - time).TotalMilliseconds > (1400f / framerate))
                    {
                        time = DateTime.Now;
                        /*if (currentMouthIndex >= -1 && currentMouthIndex <4)
                        {
                            currentMouthIndex += 1;
                        }
                        else if (currentMouthIndex == 4)
                        {
                            currentMouthIndex = -1;
                        }*/
                        currentMouthIndex = r.Next(-1, 5);
                    }



                }
                if (!isTalking)
                {
                    target.Draw(sprites[expr][index]);
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

            hide = true;

            //determine size and position
            xpos = (float)(SCREEN_WIDTH*.79);
            ypos = (float)(SCREEN_HEIGHT*0.32);
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

            mouths.Add(new Sprite(new Texture("../../Art/MomMouth1.png")));
            mouths.Add(new Sprite(new Texture("../../Art/MomMouth2.png")));
            mouths.Add(new Sprite(new Texture("../../Art/MomMouth3.png")));
            mouths.Add(new Sprite(new Texture("../../Art/MomMouth4.png")));
            mouths.Add(new Sprite(new Texture("../../Art/MomMouth5.png")));

            happyrest = new Sprite(new Texture("../../Art/MomHappyRest.png"));
            sadrest = new Sprite(new Texture("../../Art/MomSadRest.png"));
            neutralrest = new Sprite(new Texture("../../Art/MomNeutralRest.png"));
            angryrest = new Sprite(new Texture("../../Art/MomAngryRest.png"));


        }
    }
}
 
