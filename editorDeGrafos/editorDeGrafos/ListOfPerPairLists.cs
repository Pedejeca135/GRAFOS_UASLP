using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace editorDeGrafos
{
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
}
