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
    class Arm : Character {


        Texture arm = new Texture("../../Art/armMaster.png");

        Dictionary<string, List<Sprite>> sprites = new Dictionary<string, List<Sprite>>() { { "angry", new List<Sprite>() },
                                                                                            { "neutral", new List<Sprite>() }
                                                                                           };
        string expr;

        public override void checkFNC() {
            throw new NotImplementedException();
        }

        public override void setSpriteEmotion(spriteEmotion e) {


            if (e.ToString() != "sad" && e.ToString() != "happy") expr = e.ToString();
        }

        public override void Draw(RenderTarget target, RenderStates states) {

            if (!hide) {
                if (expr == "angry") target.Draw(sprites["angry"][0]);
                if (expr == "neutral") target.Draw(sprites["neutral"][0]);
            }
        }


        public override void dim()
        {
            throw new NotImplementedException();
        }
        public override void undim()
        {
            throw new NotImplementedException();
        }

        public override void setArmPosition(Vector2f position) {
            foreach (var arm in sprites) {
                foreach (var sprite in arm.Value) {
                    sprite.Position = position;
                }
            }
        }


        public override Vector2f getArmPosition() {
            throw new NotImplementedException();
        }

        public Arm() {

            hide = true;

            //determine size and position
            xpos = (float)(SCREEN_WIDTH * .21);
            ypos = (float)(SCREEN_HEIGHT * 0.28);

            xscale = SCREEN_WIDTH / 1920;
            yscale = SCREEN_HEIGHT / 1080;


            sprites["neutral"].Add(new Sprite(arm, new IntRect(0, 0, 320, 229)));
            sprites["angry"].Add(new Sprite(arm, new IntRect(0, 229, 320, 229)));


            sprites["neutral"][0].Scale = new Vector2f(xscale, yscale);
            sprites["angry"][0].Scale = new Vector2f(xscale, yscale);
        }
    }
}