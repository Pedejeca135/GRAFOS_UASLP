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
    public class ListOfPerPairLists//............................................................
    {
        int numOfPermutationPosibilities = 0;
        List<PermutationPairList> listOfPermutationAlfa;

        public ListOfPerPairLists(PermutationSetStruct permutSetStruct)
        {
            numOfPermutationPosibilities = permutSetStruct.calculatePer();
            listOfPermutationAlfa = permutSetStruct.makeAllPermutations();
        }

        public List<PermutationPairList> PER_ALFA_LIST
        {
            get { return listOfPermutationAlfa; }
        }
    }

    public class PermutationPair//...........................................................
    {
        public int thisInt;
        public int otherInt;
        public PermutationPair(int T, int O)
        {
            thisInt = T;
            otherInt = O;
        }
    }



    public class SpecificGradeSets
    {
        public int grade;
        public List<int> thisIndices;
        public List<int> otherIndices;

        public SpecificGradeSets(int grade)
        {
            this.grade = grade;
            thisIndices = new List<int>();
            otherIndices = new List<int>();
        }

        public void addThis(int index)
        {
            thisIndices.Add(index);
        }

        public void addOther(int index)
        {
            otherIndices.Add(index);
        }

        public int numberOf_O()
        {
            return thisIndices.Count();
        }
        public int numberOf_T()
        {
            return otherIndices.Count();
        }

        public Boolean validation()
        {
            if (numberOf_O() == numberOf_T())
            {
                return true;
            }
            return false;
        }

        public Boolean matchPair(int T, int O)
        {
            if (thisIndices.Contains(T) && otherIndices.Contains(O))
                return true;
            else
                return false;
        }

        public List<int> ThisIndicesList
        {
            get { return thisIndices; }
        }
        public List<int> OtherIndicesList
        {
            get { return otherIndices; }
        }

    }//END.SpecificGradeSets
}
