using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test {
    class DramaManager {
        public DramaManager() {
            Alex = new CharacterState("alex");
            Mom = new CharacterState("mom");
            Dad = new CharacterState("dad");
        }

        private CharacterState Alex;
        private CharacterState Mom;
        private CharacterState Dad;

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
    }
}
