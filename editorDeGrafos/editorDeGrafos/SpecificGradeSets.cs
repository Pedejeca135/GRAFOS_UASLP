using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace editorDeGrafos
{
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
