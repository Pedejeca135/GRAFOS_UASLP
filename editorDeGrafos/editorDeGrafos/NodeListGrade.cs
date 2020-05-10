using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace editorDeGrafos
{
    public class NodeListGrade
    {
        int grade;
        List<NodeGrade> nodeList;
        int numberOfRotations = 0;

        public NodeListGrade(int grade)
        {
            this.grade = grade;
            nodeList = new List<NodeGrade>();
        }
        /********* geters and sters *************/

        public int GRADE
        {
            get { return grade; }
        }

        public List<NodeGrade> GRADE_NODE_LIST
        {
            get { return nodeList; }
        }

        public int ROTATIONS
        {
            get { return numberOfRotations; }
        }

        public void addNode(Node node, int grade)
        {
            NodeGrade nodeGrade = new NodeGrade(node, grade);
            nodeList.Add(nodeGrade);
        }

        public void rotationNumberReset()
        {
            numberOfRotations = 0;
        }

        public Boolean contains(int index)
        {
            for(int i = 0; i < this.nodeList.Count(); i++ )
            { 
                if(nodeList[i].NODE.Index == index)
                {
                    return true;
                }
            }
            return false;
        }

        public int indexOfNodeIndex(int index)
        {
            for (int i = 0; i < this.nodeList.Count(); i++)
            {
                if (nodeList[i].NODE.Index == index)
                {
                    return i;
                }
            }
            return nodeList.Count();
        }


        public void Rotate()
        {
            if (this.GRADE_NODE_LIST.Count() > 1)
            {
                NodeGrade first = this.GRADE_NODE_LIST[0];
                this.GRADE_NODE_LIST.RemoveAt(0);
                this.GRADE_NODE_LIST.Add(first);
            }
            numberOfRotations++;
        }
    }
}
