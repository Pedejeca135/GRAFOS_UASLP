using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace editorDeGrafos
{
    public class PermutationPairList//.......................................................
    {
        /*
         * this is the way to do permutations.
         * (12345)
         *        |----------------------> very important.
         * (54321)
         * the above numbers are an example of permutation. we can have n! permutation if the graphs have n nodes.
         * the way we do it here make this less than n! permutations, due to the clasifications by grade,
         * comparing each graph.
         * 
         * */
        List<PermutationPair> permutationList = null;
        //PermutationSetStruct workingPairs;

        public PermutationPairList()
        {
            permutationList = new List<PermutationPair>();
        }
        public void Add(PermutationPair pp)
        {
            permutationList.Add(pp);
        }
        public Matrix toMatrixOfPermutation()//to convert the permutation into a matrix of permutations.
        {
            Matrix res = null;
            if (permutationList.Count > 0)
            {
                int n = permutationList.Count();
                int[,] toDoMatrix = new int[n, n];

                for (int i = 0; i < n; i++)
                {
                    toDoMatrix[permutationList[i].otherInt, permutationList[i].thisInt] = 1;
                }
                res = new Matrix(toDoMatrix);
            }
            return res;
        }

        public void toMatrixOfPermutationB(ref Matrix mNormal, ref Matrix mTrans)//to convert the permutation into a matrix of permutations.
        {
            if (permutationList.Count > 0)
            {
                int n = permutationList.Count();
                int[,] toDoMatrix = new int[n, n];
                int[,] toDoMatrixTrans = new int[n, n];

                for (int i = 0; i < n; i++)
                {
                    toDoMatrix[permutationList[i].otherInt, permutationList[i].thisInt] = 1;
                    toDoMatrixTrans[permutationList[i].thisInt, permutationList[i].otherInt] = 1;
                }
                mNormal = new Matrix(toDoMatrix);
                mTrans = new Matrix(toDoMatrixTrans);
            }
        }
    }
}
