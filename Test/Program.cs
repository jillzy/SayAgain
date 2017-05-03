using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test {
    public enum tone {
        Blunt = 0,
        Indifferent = 1,
        Compassionate = 2,
        Hesitant = 4,
        Root = 8,
    }


    class Program {

        protected static SA myGame;

        public static SA getGame() {
            return myGame;
        }



        static void Main(string[] args) {

            Program.myGame = new SA();
            myGame.Run();

        }
    }
}
