using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace editorDeGrafos
{
    public class PermutationSetStruct
    {
        List<int> gradeInts;
        List<SpecificGradeSets> grades;
        int tamOfGraph = 1;

        int[] innerAray;

        public PermutationSetStruct()
        {
            gradeInts = new List<int>();
            grades = new List<SpecificGradeSets>();
            innerAray = makeInnerArray(1);
        }
        public PermutationSetStruct(int tamOfGraph)
        {
            gradeInts = new List<int>();
            grades = new List<SpecificGradeSets>();
            this.tamOfGraph = tamOfGraph;
            innerAray = makeInnerArray(tamOfGraph);
        }
        private int[] makeInnerArray(int tam)
        {
            int[] res = new int[tam];
            for (int i = 0; i < tam; i++)
            {
                res[i] = i;
            }
            return res;
        }

        public void addGrade(int grade)
        {
            if (!gradeInts.Contains(grade))
            {
                gradeInts.Add(grade);
                grades.Add(new SpecificGradeSets(grade));
            }
        }
        public Boolean matchPairGrades(int T, int O)
        {
            Boolean res = false;
            foreach (SpecificGradeSets sgs in grades)
            {
                if (sgs.matchPair(T, O))
                    return true;
            }
            return res;
        }

        public void addIndex(int grade, int index, int this_other)
        {
            int indexInSet;
            if (!gradeInts.Contains(grade))
            {
                addGrade(grade);
            }
            indexInSet = gradeInts.IndexOf(grade);//the index of the grade whereyou want to add
            if (this_other <= 0)
            {
                grades[indexInSet].addThis(index);
            }
            else
            {
                grades[indexInSet].addOther(index);
            }
        }

        public void addIndex_T(int grade, int index)
        {
            int indexInSet;
            if (!gradeInts.Contains(grade))
            {
                addGrade(grade);
            }
            indexInSet = gradeInts.IndexOf(grade);//the index of the grade whereyou want to add

            grades[indexInSet].addThis(index);
        }

        public void addIndex_O(int grade, int index)
        {
            int indexInSet;
            if (!gradeInts.Contains(grade))
            {
                addGrade(grade);
            }
            indexInSet = gradeInts.IndexOf(grade);//the index of the grade whereyou want to add

            grades[indexInSet].addOther(index);
        }

        public Boolean validateSet()
        {
            Boolean res = true;
            foreach (SpecificGradeSets spG in grades)
            {
                if (spG.validation() == false)
                {
                    return false;
                }
            }
            return res;
        }

        public List<SpecificGradeSets> GRADES
        {
            get { return this.grades; }
        }

        public int calculatePer()
        {
            int res = 1;

            for (int i = 0; i < GRADES.Count(); i++)
            {
                int nP = GRADES[i].OtherIndicesList.Count();
                if (nP < 3)
                {
                    nP = nP * nP;
                }
                else
                {
                    nP = factorial(nP);
                }
                res *= nP;
            }
            return res;
        }

        //factorial method
        public int factorial(int integer)
        {
            int res = 1;
            for (int i = 1; i <= integer; i++)
            {
                res += res * i;
            }
            return res;
        }

        public List<PermutationPairList> LP_PairList;
        public PermutationPairList P_PairList;
        public PermutationPair auxPerPair;

        public List<PermutationPairList> makeAllPermutations()
        {
            LP_PairList = new List<PermutationPairList>();
            heapPermutation(innerAray, innerAray.Length);
            return LP_PairList;
        }

        public void storePermutation(int[] a)//store only if all the permutations match.
        {
            P_PairList = new PermutationPairList();
            for (int i = 0; i < a.Length; i++)
            {
                if (matchPairGrades(i, a[i]))
                {
                    auxPerPair = new PermutationPair(i, a[i]);
                    P_PairList.Add(auxPerPair);
                }
                else
                {
                    return;
                }
            }
            LP_PairList.Add(P_PairList);
        }

        public void heapPermutation(int[] a, int size)
        {
            // if size becomes 1 then prints the obtained 
            // permutation 
            if (size == 1)
            {
                storePermutation(a);//store and validate the permutation made.
            }

            for (int i = 0; i < size; i++)
            {
                heapPermutation(a, size - 1);

                // if size is odd, swap first and last 
                // element 
                if (size % 2 == 1)
                {
                    int temp = a[0];
                    a[0] = a[size - 1];
                    a[size - 1] = temp;
                }

                // If size is even, swap ith and last 
                // element 
                else
                {
                    int temp = a[i];
                    a[i] = a[size - 1];
                    a[size - 1] = temp;
                }
            }
        }


        //permutations but like an array.
        public List<int[]> makeAllPermutationsPure()
        {
            List<int[]> res = new List<int[]>();
            heapPermutationPure(innerAray, innerAray.Length, ref res);
            return res;
        }

        public void storePermutationPure(int[] a, ref List<int[]> res)//store only if all the permutations match.
        {
            for (int i = 0; i < a.Length; i++)
            {
                if (!matchPairGrades(i, a[i]))
                {
                    return;
                }
            }
            res.Add(a);
        }

        public void heapPermutationPure(int[] a, int size, ref List<int[]> res)
        {
            // if size becomes 1 then prints the obtained 
            // permutation 
            if (size == 1)
            {
                storePermutationPure(a, ref res);//store and validate the permutation made.
            }

            for (int i = 0; i < size; i++)
            {
                heapPermutationPure(a, size - 1, ref res);

                // if size is odd, swap first and last 
                // element 
                if (size % 2 == 1)
                {
                    int temp = a[0];
                    a[0] = a[size - 1];
                    a[size - 1] = temp;
                }

                // If size is even, swap ith and last 
                // element 
                else
                {
                    int temp = a[i];
                    a[i] = a[size - 1];
                    a[size - 1] = temp;
                }
            }
        }



    }//END. PermutationSetStruct.
}
