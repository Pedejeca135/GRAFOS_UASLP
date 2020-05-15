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

        public Boolean containsGrade(int grade)
        {
            foreach (NodeListGrade nlg in listOfList)
            {
                if(nlg.GRADE == grade)
                {
                    return true;
                }
            }
            return false;
        }

        public NodeListGrade containsGrade(NodeListGrade otherNlg)
        {
            foreach (NodeListGrade nlg in listOfList)
            {
                if (nlg.GRADE == otherNlg.GRADE)
                {
                    return nlg;
                }
            }
            return null;
        }


        public Boolean equals(listOfNodeListsGrade other)
        {
            if(this.LIST_OF_LISTS.Count() != other.LIST_OF_LISTS.Count())
            {
                return false; 
            }
            else
            {
                foreach (NodeListGrade nlg in  this.listOfList )
                {
                    NodeListGrade auxNlg = other.containsGrade(nlg);
                    if (auxNlg != null)
                    {
                        if(auxNlg.GRADE_NODE_LIST.Count() != nlg.GRADE_NODE_LIST.Count())
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
                return true;
            }            
        }

        public void Rotate()
        {
            Boolean lastRotate = true;
            foreach(NodeListGrade nlg in listOfList)   
            {
                if(lastRotate)
                {
                    nlg.Rotate();
                    if(nlg.ROTATIONS >= nlg.GRADE_NODE_LIST.Count())
                    {
                        lastRotate = true;
                        nlg.rotationNumberReset();
                    }
                    else
                    {
                        return;
                    }
                }
            }
        }

        public Coordenate cor_Of_Index(int index)
        {
            int x, y;

            for (int i = 0; i< listOfList.Count(); i++)
            {
                if (listOfList[i].contains(index))
                {
                    x = i;
                    y = listOfList[i].indexOfNodeIndex(index);
                    return new Coordenate(x,y);
                }
            }
            return null;
        }

        public int Index_Of_cor(Coordenate cor)
        {
            //int res;
            if(cor.X < this.listOfList.Count())
            {
                if(listOfList[cor.X].GRADE_NODE_LIST.Count() > cor.Y)
                {
                    return listOfList[cor.X].GRADE_NODE_LIST[cor.Y].NODE.Index;
                }
            }
            return 999999999;
        }

        //public class NodeListGrade
        

       //public  class NodeGrade
        

    }
}
