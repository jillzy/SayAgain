using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class ToneEffects: GameMatrix
    {
        public ToneEffects() {
            this.matrix = new double[3, 4]; //where 3 is the # of characters and 4 is the # of tones
            double[] nums = { -1, 2, 3, 4,
                            1, 2, 3, 4,
                            1, 2, 3, 4
                            };
            int k = 0;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    this.matrix[i, j] = nums[k++];
                }
            }
            

        }

        public override double GetValue(int x, int y)
        {
            return matrix[x, y];
        }

        public override void SetValue(int x, int y, double value)
        {
            matrix[x, y] = value;
        }

    }
}
