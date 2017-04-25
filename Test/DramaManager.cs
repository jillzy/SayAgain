using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SFML.Graphics;

namespace Test {
    class DramaManager : Drawable {
        public DramaManager() {
            Alex = new CharacterState();
            Mom = new CharacterState();
            Dad = new CharacterState();
        }

        private CharacterState Alex;
        private CharacterState Mom;
        private CharacterState Dad;
        static bool[] targets = { false, false, false }; // 0 = alex 1 = mom 2 = dad

        public void activateCharacterChoice(string c)
        {
            if(c == "Alex")
            {
                targets[0] = true;
            } else if(c == "Mom")
            {
                targets[1] = true;
            } else if(c == "Dad")
            {
                targets[2] = true;
            }
        }

        public void setTargets(string who)
        {
            //targets[0] = alex; targets[1] = mom; targets[2] = dad;
            // Old - Used for clicking will, revamp to newer system
            //if (who == "alex")
            //{
            //    targets[0] = !targets[0];
            //}

            //else if (who == "mom")
            //{
            //    targets[1] = !targets[1];
            //}

            //else if (who == "dad")
            //{
            //    targets[2] = !targets[2];
            //}

        }

        public bool[] getTargets()
        {
            return targets;
        }


        List<GameMatrix> matrices = new List<GameMatrix>();

        public CharacterState getAlex() {
            return Alex;
        }
        public CharacterState getMom() {
            return Mom;
        }
        public CharacterState getDad() {
            return Dad;
        }

        public void Draw(RenderTarget target, RenderStates states)
        {
        }
    }
}
