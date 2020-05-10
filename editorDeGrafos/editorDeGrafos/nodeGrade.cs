using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace editorDeGrafos
{
    public class NodeGrade
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
            get { return grade; }
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
