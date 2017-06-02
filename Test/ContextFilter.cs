using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//D MULTIPLIER
namespace SayAgain {
    class ContextFilter : GameMatrix {
        string tag; // identifier
                    //double[] seriousness; //a range that decides whether it is okay to joke about this context or not.


        public ContextFilter(string tag, double[] nums) {
            this.tag = tag;

            this.matrix = new double[3, 4]; //where 3 is the # of characters and 4 is the # of tones

            int k = 0;
            for (int i = 0; i < 3; i++) {
                for (int j = 0; j < 4; j++) {
                    this.matrix[i, j] = nums[k++];
                }
            }
        }

        public override double GetValue(int x, int y) {
            return matrix[x, y];
        }

        public override void SetValue(int x, int y, double value) {
            matrix[x, y] = value;
        }




    }

    public class RootObjectContext {
        public int[] school { get; set; }
        public int[] alextalksplayer { get; set; }
        public int[] momtalksmom { get; set; }
    }

}
