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
namespace Test {
    class Dad : Character, Drawable {

        Sprite happyrest, angryrest;

        int currentMouthIndex = 0; //variable to keep track of mouths for drawing
        float framerate = 4f;
        int prevIndex = -1;

        Texture t = new Texture("../../Art/dadMasterwoArm.png");

        Texture t2 = new Texture("../../Art/dadMasterwoArm2.png");

        Texture mouth = new Texture("../../Art/DadMouth.png");

        Dictionary<string, List<Sprite>> sprites = new Dictionary<string, List<Sprite>>() { { "angry", new List<Sprite>() },
                                                                                            { "happy", new List<Sprite>() },
                                                                                            { "neutral", new List<Sprite>() }
                                                                                           };
        Dictionary<string, List<Sprite>> noMouthSprites = new Dictionary<string, List<Sprite>>() { { "angry", new List<Sprite>() },
                                                                                            { "happy", new List<Sprite>() },
                                                                                            { "neutral", new List<Sprite>() }
                                                                                           };


        Sprite mouthSprite;

        List<Sprite> mouths = new List<Sprite>();


        string expr;


        public override void checkFNC() {
            throw new NotImplementedException();
        }

        public override void setArmPosition(Vector2f position) {
            throw new NotImplementedException();
        }

        public override Vector2f getArmPosition() {
            return new Vector2f(sprites["neutral"][0].Position.X, sprites["neutral"][0].Position.Y + (sprites["neutral"][0].GetGlobalBounds().Height * 0.49f));
        }

        public override void setSpriteEmotion(spriteEmotion e) {
            index = 0;


            if (e.ToString() != "sad") expr = e.ToString();
        }

        void returnToRestMouth() {
        }

        void hideMouth(int i)
        {
            mouths[i].Position = new Vector2f(-100, -100);
        }




        public override void Draw(RenderTarget target, RenderStates states) {
            rnd = r.Next(4, 14);
            rnd2 = r.Next(2, 4);


            if (!hide) {
                if (isTalking) {
                    //Console.WriteLine("inside dad.cs: expr ; index " + expr + " , " + index);
                    target.Draw(noMouthSprites[expr][index]);
                    //cycle between open mouth and rest mouth
                    //hide previous mouth
                    

                    if (currentMouthIndex == 0) //rest mouth
                    {

                        if (expr == "happy") {
                            happyrest.Position = (new Vector2f(xpos - 29, ypos + 141));
                            happyrest.Scale = (new Vector2f(0.62f, 0.62f));
                            target.Draw(happyrest);
                        } else if (expr == "angry") {
                            angryrest.Position = (new Vector2f(xpos - 25, ypos + 150));
                            angryrest.Scale = (new Vector2f(0.55f, 0.62f));
                            target.Draw(angryrest);
                        }
                    } else if (currentMouthIndex >= 1 && currentMouthIndex < 4)//open mouth
                      {
                        framerate = 15;


                        if (currentMouthIndex >= 0)
                        {
                            //hide previous mouth
                            //hideMouth(currentMouthIndex - 1);
                        }
                        mouths[currentMouthIndex].Scale = new Vector2f(1.2f, 1.2f);
                        mouths[currentMouthIndex].Position = new Vector2f(xpos - 45, ypos + 119);
                        target.Draw(mouths[currentMouthIndex]);


                    }

                    if ((DateTime.Now - time).TotalMilliseconds > (1400f / framerate)) {
                        time = DateTime.Now;
                        currentMouthIndex = r.Next(0, 4);
                    }


                }
                if (!isTalking) {
                    target.Draw(sprites[expr][index]);
                }
                if (index == 0 && prevIndex != 0) {
                    framerate = framerate / (float)rnd;
                    prevIndex = 0;
                } else if (index != 0) {
                    prevIndex = index - 1;
                    framerate = 4f;
                }

                if ((DateTime.Now - time).TotalMilliseconds > (1400f / framerate)) {
                    time = DateTime.Now;

                    if (++index > sprites[expr].Count-1) {
                        index = 0;
                    }
                }
            }

        }

        public Dad() {
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
            currentFNC = 0;

            hide = true;

            //determine size and position
            xpos = (float)(SCREEN_WIDTH * .21);
            ypos = (float)(SCREEN_HEIGHT * 0.31);

            xscale = SCREEN_WIDTH / 1920;
            yscale = SCREEN_HEIGHT / 1080;

            for (int i = 0; i < (343 * 4); i += 343) {

                sprites["angry"].Add(new Sprite(t, new IntRect(i, 0, 343, 454))); //btw might get extra sprite if sizes no precise
                sprites["angry"][sprites["angry"].Count - 1].Scale = new Vector2f(xscale, yscale);
                sprites["angry"][sprites["angry"].Count - 1].Position = new Vector2f(xpos - sprites["angry"][0].GetGlobalBounds().Width / 2, ypos);

                noMouthSprites["angry"].Add(new Sprite(t2, new IntRect(i, 0, 343, 454))); //btw might get extra sprite if sizes no precise
                noMouthSprites["angry"][noMouthSprites["angry"].Count - 1].Scale = new Vector2f(xscale, yscale);
                noMouthSprites["angry"][noMouthSprites["angry"].Count - 1].Position = new Vector2f(xpos - sprites["angry"][0].GetGlobalBounds().Width / 2, ypos);


            }
            for (int i = 0; i < (343 * 4); i += 343) {
                sprites["happy"].Add(new Sprite(t, new IntRect(i, 454, 343, 454))); //second row of sprites; happy epression
                sprites["happy"][sprites["happy"].Count - 1].Scale = new Vector2f(xscale, yscale);
                sprites["happy"][sprites["happy"].Count - 1].Position = new Vector2f(xpos - sprites["happy"][0].GetGlobalBounds().Width / 2, ypos);

                noMouthSprites["happy"].Add(new Sprite(t2, new IntRect(i, 454, 343, 454))); //second row of sprites; happy epression
                noMouthSprites["happy"][noMouthSprites["happy"].Count - 1].Scale = new Vector2f(xscale, yscale);
                noMouthSprites["happy"][noMouthSprites["happy"].Count - 1].Position = new Vector2f(xpos - sprites["happy"][0].GetGlobalBounds().Width / 2, ypos);

            }
            for (int i = 0; i < (343 * 4); i += 343) {

                sprites["neutral"].Add(new Sprite(t, new IntRect(i, 454 * 2, 343, 454)));
                sprites["neutral"][sprites["neutral"].Count - 1].Scale = new Vector2f(xscale, yscale);
                sprites["neutral"][sprites["neutral"].Count - 1].Position = new Vector2f(xpos - sprites["neutral"][0].GetGlobalBounds().Width / 2, ypos);

                noMouthSprites["neutral"].Add(new Sprite(t2, new IntRect(i, 454 * 2, 343, 454)));
                noMouthSprites["neutral"][noMouthSprites["neutral"].Count - 1].Scale = new Vector2f(xscale, yscale);
                noMouthSprites["neutral"][noMouthSprites["neutral"].Count - 1].Position = new Vector2f(xpos - sprites["neutral"][0].GetGlobalBounds().Width / 2, ypos);

            }

            mouthSprite = new Sprite(mouth);
            mouthSprite.Scale = new Vector2f(1.2f, 1.2f);




            mouths.Add(new Sprite(new Texture("../../Art/DadMouth.png")));
            mouths.Add(new Sprite(new Texture("../../Art/DadMouth2.png")));
            mouths.Add(new Sprite(new Texture("../../Art/DadMouth3.png")));
            mouths.Add(new Sprite(new Texture("../../Art/DadMouth4.png")));


            happyrest = new Sprite(new Texture("../../Art/DadHappyRest.png"));
            angryrest = new Sprite(new Texture("../../Art/DadAngryRest.png"));
        }
    }
}