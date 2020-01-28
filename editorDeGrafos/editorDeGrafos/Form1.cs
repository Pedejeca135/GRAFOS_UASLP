using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace editorDeGrafos
{
    public partial class Form1 : Form
    {
        List<Node> nodeList;
        List<Edge> edgeList;

        Node selectedNode;
        Node selected = null;

        public int generalRadius;
        Boolean anyNodeSelected;
        int indexCount;
        AdjacencyList aListGraph;
        Boolean mousePressed;
        Boolean nodeMoved = false;

        public Form1()
        {
            InitializeComponent();
            generalRadius = 30; //Here you decide the size of the nodes
            nodeList = new List<Node>();
            edgeList = new List<Edge>();
            selectedNode = new Node();
            anyNodeSelected = false;
            indexCount = 0;
            mousePressed = false;
            aListGraph = new AdjacencyList();
            //TERMINAL
            terminal.Text = "0";
            
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            mousePressed = true;

            if (e.Button == System.Windows.Forms.MouseButtons.Left)//if mouse button pressed is left.
            {
                Node oneNode = null;
                //Node oneNode = new Node();

                foreach (Node onNode in nodeList)
                {
                    if (e.X > onNode.Position.X - onNode.Radius //for conditions in order to determine wheter or not , a click hit the specific node
                       && e.X < onNode.Position.X + onNode.Radius
                       && e.Y < onNode.Position.Y + onNode.Radius
                       && e.Y > onNode.Position.Y - onNode.Radius)
                    {
                        oneNode = onNode;
                    }//one node clicked.
                }//check all the node list.
                if (oneNode != null)//one Node was clicked
                {
                    if (oneNode.SelectedBool == true)//if the node selected was selected already in any state.
                    //if(oneNode == selected)
                    {
                        if (oneNode.Status == 3)// in the third state
                        {
                            oneNode.Status = 0;//change to the original state.
                            oneNode.COLOR = Color.Black;//change to black color(original state).
                            oneNode.SelectedBool = anyNodeSelected = false;
                            selected = null;
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

                            }
                        }

                    }
                    else // is tryng to do a link between nodes or select for first time
                    {
                       // if (anyNodeSelected == true)//want to do a link between nodes.
                       if (selected != null)
                        {
                            if (selected.Status == 2)//undirected link
                            {
                                //here i have to ask the weight of the link.
                                //int weight = AskForAWeight();
                                int weight = 0;
                                edgeList.Add(new Edge(selected, oneNode));
                                aListGraph.addUndirectedEdge(selected, oneNode, weight);
                            }
                            if (selected.Status == 3)//directed link
                            {
                                //here i have to ask the weight of the link.
                                //int weight = AskForAWeight();
                                int weight = 0;
                                edgeList.Add(new Edge(selected, oneNode, true));
                                aListGraph.addDirectedEdge(selected, oneNode, weight);
                            }
                        }
                        else // select for the first time.                            
                        {
                            // is not selected, (Status == 0).
                            oneNode.Status = 1;//change to the first selected state.
                            oneNode.COLOR = Color.Green;//change to green color to indicate the status(can move).
                            oneNode.SelectedBool = true;
                            anyNodeSelected = true;
                            selected = oneNode;
                        }
                    }
                }
                else // want to make a new node 
                {
                    Coordenate newNodePosition = new Coordenate(e.X, e.Y);
                    Node newNode = new Node(newNodePosition, generalRadius, indexCount++);
                    nodeList.Add(newNode);
                    aListGraph.addNode(newNode);
                }

            }//left mouse button presed.           
            else
            {
                //if (e.Button == System.Windows.Forms.MouseButtons.Right)//right mouse button clicked.
               // {
                    //if (anyNodeSelected == true)
                    if(selected != null)
                    {
                        if (e.X > selected.Position.X - selected.Radius //for conditions in order to determine wheter or not , a click hit the specific node
                       && e.X < selected.Position.X + selected.Radius
                       && e.Y < selected.Position.Y + selected.Radius
                       && e.Y > selected.Position.Y - selected.Radius)
                        {
                        if (selected.Status == 1 && nodeMoved == true)
                            {                             
                            /*selected.Status = 0;//change to the original state.
                            selected.COLOR = Color.Black;//change to black color(original state).
                            selected.SelectedBool = anyNodeSelected = false;
                            selected = null;*/
                        }
                            else
                            {
                                if(selected.Status == 2)//make a own link
                                {
                                    //int weight = AskForAWeight();
                                    int weight = 0;
                                    aListGraph.addDirectedEdge(selected, selected, weight);
                                }
                                else//eliminate the node
                                {
                                    aListGraph.removeANode(selected);
                                    nodeList.Remove(selected);
                                    selected = null;
                                    anyNodeSelected = false;
                                    indexCount--;
                                }
                            }
                            
                        }
                    }
                //}
            } 

            Invalidate();
        }//Form_MouseDown().


        private void Save_Click(object sender, EventArgs e)
        {

        }

        private void Load_Click(object sender, EventArgs e)
        {

        }

        private void New_Click(object sender, EventArgs e)
        {

        }
        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mousePressed = false;
            }
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if (mousePressed == true && e.Button == MouseButtons.Right && selected != null && selected.Status == 1)
            {
                selected.Position.X = e.X;
                selected.Position.Y = e.Y;
                Invalidate();
            }

            nodeMoved = true;
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape && selected != null)
            {
                selected.Status = 0;//change to the original state.
                selected.COLOR = Color.Black;//change to black color(original state).
                selected.SelectedBool = anyNodeSelected = false;
                selected = null;
                terminal.Text += "esc";
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            Pen pen = new Pen(Color.Black, 5);
            Brush brush = new SolidBrush(BackColor);
            Rectangle rectangle;
            foreach (Edge edge in edgeList)
            {
                graphics.DrawLine(pen, edge.A.X, edge.A.Y, edge.B.X, edge.B.Y);
            }

            foreach (Node node in nodeList)
            {
                rectangle = new Rectangle(node.Position.X - node.Radius, node.Position.Y - node.Radius, node.Radius * 2, node.Radius * 2);
                graphics.FillEllipse(brush, rectangle);
                pen = new Pen(node.COLOR, 5);
                graphics.DrawEllipse(pen, node.Position.X - node.Radius, node.Position.Y - node.Radius, node.Radius * 2, node.Radius * 2);

            }

        }

        private int AskForAWeight()
        {
            int weight = 0;

            return weight;
        }
        public void eliminateNexetEdges(Node node)
        {
            List<Edge> newEdges = new List<Edge>();

            foreach (Edge edge in edgeList)
            {
                if (edge.Client != node && edge.Server != node)
                {
                    newEdges.Add(edge);
                }
            }
            edgeList = newEdges;
        }

        /*************************************************************************************************************************
         * 
         * |||||||||||||||||||||||||||||||||||||||||||||||||||||   CLASSES   |||||||||||||||||||||||||||||||||||||||||||||||||||
         * 
         * ***********************************************************************************************************************/


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
                color = Color.Black;
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

            public int Index
            {
                get { return this.index; }
            }
            /*******************************************************
             *                Geters and seters(End)               *
             *******************************************************/

            /*******************************************************
             *                Methods(Begin)                       *
             *******************************************************/
           

        }//Node class.

        public class Edge
        {
            Node client = null;
            Node server = null;
            Boolean directed;

            Coordenate a;
            Coordenate b;
            int weight;
            int direction;

            public Edge(Node client,Node server)
            {
                this.client = client;
                this.server = server;
                directed = false;
            }

            public Edge(Node client, Node server, Boolean directedBool )
            {
                this.client = client;
                this.server = server;
                directed = directedBool;
            }

            public Coordenate A
            {
                get { return this.client.Position; }
                //set { this.a = value; }
            }

            public Coordenate B
            {
                get { return this.server.Position; }
                //set { this.b = value; }
            }

            public Node Client
            {
                get { return this.client; }
            }
            public Node Server
            {
                get { return this.server; }
            }

        }//Edge.

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
                set { this.y = value; }
            }
        }//Coordenate.

        public class WeightMatrixGraph
        {

        }

        public class NodeRef
        {
            int weight;
            Node nodo;

            public NodeRef(int weight, Node nodo)
                {
                    this.nodo = nodo;
                    this.weight = weight;
                }
            public Node NODO
            {
                get { return this.nodo; }
            }
            
            public int W
            {
                get { return this.weight; }
                set { this.weight = value; }
            }
        }//NodeRef.

        public class AdjacencyList
        {
            List<List<NodeRef>> graph;

            public AdjacencyList()
            {
                graph = new List<List<NodeRef>>();
            }

            public void addNode(Node nodo)
            {
               NodeRef nodoRef = new NodeRef(-1,nodo);            //the new Node that is going to be added have no conection, so it have a -1 value.
               List<NodeRef> newNodeRefList = new List<NodeRef>();//the new list for the new node conections.             
                                                                  //for controling the new list adding Node indexes.

                if (graph.Count() <= 0)//ther's no elements
                {
                    newNodeRefList.Add(nodoRef);
                    graph.Add(newNodeRefList);
                   
                }
                else//At least one element.
                {
                    int indexOfNode = 0;
                    foreach (List<NodeRef> row in graph)
                    {
                        if (row.Count > indexOfNode)
                        {
                            newNodeRefList.Add(new NodeRef(-1, row[indexOfNode].NODO));//making the new list at the end of the "array".
                            row.Add(nodoRef);//adding to each row the new Node.
                            indexOfNode++;
                        }                        
                    }
                    graph.Add(newNodeRefList);//adding the list made.
                }            
            }

            public void removeANode(Node nodo)//the same process ass addNode() but vice versa.
            {
                graph.RemoveAt(nodo.Index);//remove the list of adjacenci of the node.
                foreach(List<NodeRef> row in graph)
                {
                    row.RemoveAt(nodo.Index);//removing the node of all the list of nodes.
                }
            }

            public void addUndirectedEdge(Node client, Node server, int weight)
            {
                /*
                foreach (List<NodeRef> row in graph)
                {
                    if (row.NODO == server){}
                    if (row.NODO == client){}                  
                }
                */
                graph[client.Index][server.Index].W = weight;
                graph[server.Index][client.Index].W = weight;
            }

            public void addDirectedEdge(Node client, Node server, int weight)
            {
                graph[client.Index][server.Index].W = weight;
            }
        }//AdjacencyList.

     
    }//Form.
}//namespace.
