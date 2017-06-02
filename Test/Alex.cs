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

namespace SayAgain {
    class Alex : Character {
        //private View _view;
        Texture t = new Texture("../../Art/alexMaster.png");
        string expr;
        int longerframe;
        Dictionary<string, List<Sprite>> sprites = new Dictionary<string, List<Sprite>>() { { "angry", new List<Sprite>() },
                                                                                            { "happy", new List<Sprite>() },
                                                                                            { "neutral", new List<Sprite>() }
                                                                                           };


        List<Sprite> mouths = new List<Sprite>();

        int currentMouthIndex = -1; //variable to keep track of mouths for drawing

        float framerate = 4f;
        int prevIndex = -1;

        public override void checkFNC() {
            throw new NotImplementedException();
        }

        public override void setArmPosition(Vector2f position) {
            throw new NotImplementedException();
        }

        public override Vector2f getArmPosition() {
            throw new NotImplementedException();
        }

        public override void setSpriteEmotion(spriteEmotion e) {
            index = 0;
            expr = e.ToString();

        }

        void hideMouth(int i) {
            mouths[i].Position = new Vector2f(-100, -100);
        }




        public override void dim()
        {

            foreach (Sprite s in mouths)
            {
                s.Color = new Color(s.Color.R, s.Color.G, s.Color.B, 180);
            }
            foreach (Sprite s in sprites["happy"])
            {
                s.Color = new Color(s.Color.R, s.Color.G, s.Color.B, 180);

            }

            foreach (Sprite s in sprites["neutral"])
            {
                s.Color = new Color(s.Color.R, s.Color.G, s.Color.B, 180);

            }

            foreach (Sprite s in sprites["angry"])
            {
                s.Color = new Color(s.Color.R, s.Color.G, s.Color.B, 180);

            }
        }
        public override void undim()
        {
            foreach (Sprite s in mouths)
            {
                s.Color = new Color(s.Color.R, s.Color.G, s.Color.B, 255);
            }
            foreach (Sprite s in sprites["happy"])
            {
                s.Color = new Color(s.Color.R, s.Color.G, s.Color.B, 255);
            }
            foreach (Sprite s in sprites["neutral"])
            {
                s.Color = new Color(s.Color.R, s.Color.G, s.Color.B, 255);
            }
            foreach (Sprite s in sprites["angry"])
            {
                s.Color = new Color(s.Color.R, s.Color.G, s.Color.B, 255);
            }

        }






        public void pickSpecialFrame() {
            if (expr == "neutral") {
                longerframe = 0;
            }
            if (expr == "happy") {
                int rnd = r.Next(0, 4);

                if (rnd == 0) {
                    longerframe = 1;
                } else if (rnd == 1) {
                    longerframe = 4;

                } else if (rnd == 2) {
                    longerframe = 8;
                }
            }

            if (expr == "angry") {
                int rnd = r.Next(0, 4);

                if (rnd == 0) {
                    longerframe = 2;
                } else if (rnd == 1) {
                    longerframe = 8;

                }
            }
        }

        public override void Draw(RenderTarget target, RenderStates states) {
            rnd = r.Next(4, 14);
            //Console.WriteLine("inside alex.cs: expr ; index " + expr + " , " + index);
            target.Draw(sprites[expr][index]);
            if (!hide) {
                if (isTalking) {


                    if (currentMouthIndex == -1) //rest mouth
                    {

                        hideMouth(4);
                        framerate = 7;




                    } else if (currentMouthIndex >= 0 && currentMouthIndex < 5)//open mouth
                      {
                        framerate = 15;



                        if (currentMouthIndex >= 1) {
                            //hide previous mouth
                            //hideMouth(currentMouthIndex - 1);
                        }


                    }

                    if (currentMouthIndex != -1) {
                        //mouths[currentMouthIndex].Scale = new Vector2f(0.9f, 0.9f);
                        mouths[currentMouthIndex].Position = new Vector2f(xpos - 30, ypos + 136);
                        target.Draw(mouths[currentMouthIndex]);
                    }


                    if ((DateTime.Now - time).TotalMilliseconds > (1400f / framerate)) {
                        time = DateTime.Now;
                        /*if (currentMouthIndex >= -1 && currentMouthIndex <4)
                        {
                            currentMouthIndex += 1;
                        }
                        else if (currentMouthIndex == 4)
                        {
                            currentMouthIndex = -1;
                        }*/
                        if (currentMouthIndex == -1) {

                            currentMouthIndex = r.Next(0, 5);
                        } else {
                            currentMouthIndex = r.Next(-1, 5);
                        }


                    }


                }
                if (!isTalking) {
                    target.Draw(sprites[expr][index]);
                }



                // neutral expressoin
                //dont want draw to contain any logic about which expression,
                //so have the interested index defined somewhere else
                if (index == longerframe && prevIndex != longerframe) {
                    framerate = framerate / (float)rnd;
                    prevIndex = longerframe;
                } else if (index != longerframe) {
                    prevIndex = index - 1;
                    framerate = 4f;
                }

                if ((DateTime.Now - time).TotalMilliseconds > (1400f / framerate)) {
                    time = DateTime.Now;
                    if (++index > sprites[expr].Count - 1) {

                        pickSpecialFrame();
                        index = 0;
                    }
                }
            }
        }

        public Alex() {
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

            currentFNC = 0;

            hide = false;

            //determine size and position
            xpos = (float)(SCREEN_WIDTH * 0.5);
            ypos = (float)(SCREEN_HEIGHT * 0.4);
            xscale = (float)((SCREEN_WIDTH / 1920) * 0.78);
            yscale = (float)((SCREEN_HEIGHT / 1080) * 0.78);

            for (int i = 0; i < (361 * 4); i += 361) {
                sprites["neutral"].Add(new Sprite(t, new IntRect(i, 0, 361, 449)));
                sprites["neutral"][sprites["neutral"].Count - 1].Scale = new Vector2f(xscale, yscale);
                sprites["neutral"][sprites["neutral"].Count - 1].Position = new Vector2f(xpos - sprites["neutral"][0].GetGlobalBounds().Width / 2, ypos);
            }
            for (int i = 0; i < (361 * 9); i += 361) {
                sprites["happy"].Add(new Sprite(t, new IntRect(i, 449, 361, 449)));
                sprites["happy"][sprites["happy"].Count - 1].Scale = new Vector2f(xscale, yscale);
                sprites["happy"][sprites["happy"].Count - 1].Position = new Vector2f(xpos - sprites["happy"][0].GetGlobalBounds().Width / 2, ypos);
            }
            for (int i = 0; i < (337 * 9); i += 337) {

                sprites["angry"].Add(new Sprite(t, new IntRect(i, 899, 337, 449)));
                sprites["angry"][sprites["angry"].Count - 1].Scale = new Vector2f(xscale, yscale);
                sprites["angry"][sprites["angry"].Count - 1].Position = new Vector2f(xpos - sprites["angry"][0].GetGlobalBounds().Width / 2, ypos);
            }


            mouths.Add(new Sprite(new Texture("../../Art/AlexMouth1.png")));
            mouths.Add(new Sprite(new Texture("../../Art/AlexMouth2.png")));
            mouths.Add(new Sprite(new Texture("../../Art/AlexMouth3.png")));
            mouths.Add(new Sprite(new Texture("../../Art/AlexMouth4.png")));
            mouths.Add(new Sprite(new Texture("../../Art/AlexMouth5.png")));

            for (int x = 0; x < mouths.Count; x++) {
                mouths[x].Scale = new Vector2f(xscale, yscale);
            }

        }
    }
}
