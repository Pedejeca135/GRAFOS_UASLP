using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace editorDeGrafos
{
    public partial class Form1 : Form
    {
        List<Node> nodeList;
        List<Edge> edgeList;
        Node selectedNode;
        public int generalRadius;

        public Form1()
        {
            InitializeComponent();
            generalRadius = 5; //Here you decide the size of the nodes
            nodeList = new List<Node>();
            edgeList = new List<Edge>();
            selectedNode = new Node();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Left)//if mouse button pressed is left.
            {
                foreach (Node oneNode in nodeList)
                { 

                    if(e.X > oneNode.Position.X - oneNode.Radius //for conditions in oreder to determine wheter or not , a click hit the specific node
                        && e.X < oneNode.Position.X + oneNode.Radius
                        && e.Y < oneNode.Position.Y + oneNode.Radius
                        && e.Y > oneNode.Position.Y - oneNode.Radius)
                    {
                        if(oneNode.SelectedBool)
                        {
                            if(oneNode.Status == 3)// in the third state
                            {
                               oneNode.Status = 0;//change to the original state.
                               oneNode.COLOR = Color.Black;//change to black color(original state).

                            }
                            else
                            {
                                if (oneNode.Status == 2)//selected more than one time(second State).
                                {
                                    oneNode.Status = 3;//change to thid state for directed links.
                                    oneNode.COLOR = Color.Red;//change to red color(third state).
                                }
                                else
                                {
                                    if (oneNode.Status == 1)//selected one time.
                                    {
                                        oneNode.Status = 2;//change to the second selected State.
                                        oneNode.COLOR = Color.Blue;//change to blue color to indicate the status(can do undirected Edges).
                                    }
                                    else // is not selected, (Status == 0).
                                    {
                                        oneNode.Status = 1;//change to the first selected state.
                                        oneNode.COLOR = Color.Green;//change to green color to indicate the status(can move).
                                    }
                                }
                            }
                           
                        }
                        else // is tryng to do a link between nodes
                        {

                        }
                      
                    }
                }
            }
            else
            {

            }
        }

        private void Save_Click(object sender, EventArgs e)
        {

        }

        private void Load_Click(object sender, EventArgs e)
        {

        }

        private void New_Click(object sender, EventArgs e)
        {

        }

        public class Node
        {
            Coordenate position;
            Color color;
            int radiusLenght;
            Boolean justSelected;
            int selected;
            int index;

            public Node(Coordenate position, int radius, int index)
            {
                this.position = position;
                this.radiusLenght = radius;
                justSelected = false;
                selected = 0;
                this.index = index;//ID of the node
            }
            public Node() //default constructor of the class Node
            {

            }

            /*******************************************************
             *               Geters and seters(Begin)              *
             *******************************************************/

            public Coordenate Position
            {
                get { return this.position; }
                set { this.position = value; }
            }

            public int Radius
            {
                get { return this.radiusLenght; }
                set { this.radiusLenght = value; }
            }

            public int Status
            {
                get { return selected; }
                set { selected = value; }
            }

            public Color COLOR
            {
                get { return this.color; }
                set { this.color = value; }
            }

            public Boolean SelectedBool
            {
                get { return this.justSelected; }
                set { this.justSelected = value; }
            }
            /*******************************************************
             *                Geters and seters(End)               *
             *******************************************************/

            /*******************************************************
             *                Methods(Begin)                       *
             *******************************************************/
            public void Draw()
            {

            }

        }//Node class.




        public class Edge
        {
            Coordenate a;
            Coordenate b;
            int weight;
            int direction;

            public Edge()
            {

            }


        }

        public class Coordenate
        {
            int x;
            int y;

            public Coordenate(int x, int y)
            {
                this.x = x;
                this.y = y;
            }

            public int X
            {
                get { return this.x; }
                set { this.x = value; }
            }

            public int Y
            {
                get { return this.y; }
                set { this.x = value; }
            }
        }

        public class WeightMatrixGraph
        {

        }

        public class adjacencyList
        {

        }


    }//Form.
}//namespace.
