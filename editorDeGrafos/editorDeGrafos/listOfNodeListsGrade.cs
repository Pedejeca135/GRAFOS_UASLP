using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace editorDeGrafos
{    
    public class listOfNodeListsGrade
    {
        private List<NodeListGrade> listOfList;
        public listOfNodeListsGrade()
        {
            listOfList = new List<NodeListGrade>();
        }

        public List<NodeListGrade> LIST_OF_LISTS
        {
            get { return listOfList; }
        }

        public void init(Graph graph)
        {
            foreach(Node node in graph.NODE_LIST)
            {
                this.addNode(node, graph.GradeOfNode(node));
            }
            
            listOfList.Sort(delegate(NodeListGrade list_1, NodeListGrade list_2)
                {
                    return list_1.GRADE.CompareTo(list_2.GRADE);
            });
        }

        public void addNode(Node node , int grade)
        {
            foreach(NodeListGrade nodeListGrade in listOfList)
            {
                if(nodeListGrade.GRADE == grade)
                {
                    nodeListGrade.addNode(node,grade);
                    return;
                }
            }
            NodeListGrade listOfNodeGrade = new NodeListGrade(grade);
            this.listOfList.Add(listOfNodeGrade);
            this.addNode(node,grade);
        }


        public class NodeListGrade
        {
            int grade;
            List<NodeGrade> nodeList;

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

            public void addNode(Node node, int grade)
            {
                NodeGrade nodeGrade = new NodeGrade(node,grade);
                nodeList.Add(nodeGrade);
            }
        }

       public  class NodeGrade
        {
            int grade;
            Node node = null;
            Boolean treated = false;
            public NodeGrade(Node node, int grade)
            {
                this.grade = grade;
                this.node = node;
            }

            public int GRADE
            {
                get {return grade; }
            }

            public Node NODE
            {
                get { return node; }
            }
            public Boolean TREATED
            {
                get { return TREATED; }
                set { this.treated = value; }
            }
        }

    }
}
