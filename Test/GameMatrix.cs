using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class GameMatrix
    {

        protected double[,] matrix;

        public GameMatrix() { }

        public virtual double GetValue(int x, int y) {
            return matrix[x, y];
        }

        public virtual void SetValue(int x, int y, double value) {
            matrix[x, y] = value;
        }

        public double[,] MatrixMult(GameMatrix a, GameMatrix b) {

            double[,] final = new double[3,4];
            for (int i = 0; i < 3; i++) {
                for (int j = 0; j < 4; j++) {
                    final[i, j] = a.matrix[i, j] * b.matrix[i, j];
                }
            }
            return final; 
        }


    }
}
