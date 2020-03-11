using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace editorDeGrafos
{
    public class Matrix
    {
        int[,] matrix;

        public Matrix()
        {
            this.matrix = new int[1, 1];
        }

        public Matrix(int row, int col)
        {
            this.matrix = new int[row, col];
        }

        public Matrix(int[,] matrix)
        {
            this.matrix = matrix;
        }

        public Matrix MatrixProduct(Matrix other)
        {
            Matrix res;
            res = new Matrix(product(this.MATRIX, other.MATRIX));
            return res;
        }

        public int[,] MatrixProductFree(int[,] other)
        {
            int[,] res;
            res = product(this.MATRIX, other);
            return res;
        }

        private int[,] product(int[,] g, int[,] h)
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
                            res[j, i] += g[j, k] * h[k, i];
                        }
            }
            return res;
        }

        public int[,] TransposeFree()
        {
            int[,] res = new int[this.MATRIX.GetLength(1), this.MATRIX.GetLength(0)];

            for (int j = 0; j < this.MATRIX.GetLength(0); j++)
                for (int i = 0; i < this.MATRIX.GetLength(1); i++)
                {
                    res[i, j] = this.matrix[j, i];
                }
            return res;
        }

        public Matrix TransposeMatrix()
        {
            Matrix res;
            int[,] pseudoRes = new int[this.MATRIX.GetLength(1), this.MATRIX.GetLength(0)];

            for (int j = 0; j < this.MATRIX.GetLength(0); j++)
                for (int i = 0; i < this.MATRIX.GetLength(1); i++)
                {
                    pseudoRes[i, j] = this.matrix[j, i];
                }
            res = new Matrix(pseudoRes);
            return res;
        }

        public Matrix interchangeRC(ref Matrix m, int tam, int index1, int index2)
        {
            Matrix res = m ;
            for(int j = 0; j < tam ; j++)
            {
                swapInt(ref res.MATRIX[j,index1] , ref res.MATRIX[j,index2]);
            }
            for (int j = 0; j < tam; j++)
            {
                swapInt(ref res.MATRIX[index1 , j], ref res.MATRIX[index2 , j]);
            }
            res = new Matrix(res.MATRIX);
            return res;
        }
        private void swapInt(ref int one, ref int two)
        {
            int aux;
            aux = one;
            one = two;
            two = aux;
        }

        public Boolean Equals(Matrix other)
        {
            if (this.MATRIX.GetLength(0) == other.MATRIX.GetLength(0) && this.MATRIX.GetLength(1) == other.MATRIX.GetLength(1))
            {
                for (int j = 0; j < this.MATRIX.GetLength(0); j++)
                    for (int i = 0; i < this.MATRIX.GetLength(1); i++)
                    { 
                        if(this.MATRIX[j,i] != other.MATRIX[j,i])                       
                        {
                            return false;
                        }
                    }
                return true;
            }
            return false;
        }


        public int[,] MATRIX
        {
            get { return this.matrix; }
        }
        
    }



}
