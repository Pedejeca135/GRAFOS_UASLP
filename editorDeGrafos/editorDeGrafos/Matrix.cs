using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace editorDeGrafos
{
    public class Matrix
    {
        int [,] matrix;

        public Matrix(int row, int col)
        {
            this.matrix = new int[row, col];
        }

        public Matrix(int [,] matrix)
        {
            this.matrix = matrix;
        }

        public Matrix MatrixProduct(Matrix other)
        {
            Matrix res;
            res = new Matrix( product(this.MATRIX, other.MATRIX) );
            return res;
        }

        public int[,] MatrixProductFree( int [,] other)
        {
            int[,] res;
            res = product(this.MATRIX, other);
            return res;
        }

        private int[,] product(int[,] g , int[,] h )
        {
            int[,] res = null;
            int commonLength = g.GetLength(1);
            if (commonLength == h.GetLength(0))
            {
                res = new int[g.GetLength(0), h.GetLength(1)];
                for (int j = 0; j < g.GetLength(0); j++)
                    for (int i = 0; i < h.GetLength(1); i++)
                        for (int k = 0; k < commonLength; k++)
                        {
                            res[j,i] += g[j, k] * h[k, i];
                        }
            }
            return res;
        }

        public int[,] Transpose ()
        {
            int[,] res = new int[this.MATRIX.GetLength(1), this.MATRIX.GetLength(0)];

            for(int j = 0; j< this.MATRIX.GetLength(0);j++ )
                for(int i = 0; i < this.MATRIX.GetLength(1); i++ )
                {
                    res[i, j] = this.matrix[j, i];
                }

            return res;
        }


        public int[,] MATRIX
        {
            get { return this.matrix; }
        }
        
    }



}
