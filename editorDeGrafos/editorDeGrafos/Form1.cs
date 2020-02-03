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
using System.Runtime.InteropServices;

namespace editorDeGrafos
{
    public partial class Form1 : Form
    {

        List<Node> nodeList;
        List<Edge> edgeList;

        Node selectedNode;
        Node selected = null;
        Node selectedJustFor = null;

        public int generalRadius;
        Boolean anyNodeSelected;
        int indexCount;
        AdjacencyList aListGraph;
        Boolean mousePressed;
        List<int> IDList;
        Boolean allMoving = false;
        Boolean allDeleting = false;
        //Boolean nodeMoved = false;

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
            IDList = new List<int>();
            IDList.Add(1000);

            //TERMINAL
            terminal.Text = "0";

        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            mousePressed = true;
            if (allMoving || allDeleting)
            {
                selectedJustFor = findNodeClicked(new Coordenate(e.X, e.Y));
                if (allDeleting)
                {
                    selected = selectedJustFor;
                    eliminate();
                }
            }
            else
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Left)//if mouse button pressed is left.
                {
                    Node oneNode = null;
                    //Node oneNode = new Node();
                //one node clicked.//check all the node list.
                    
                    oneNode = findNodeClicked(new Coordenate(e.X,e.Y));

                    if (oneNode != null)//one Node was clicked
                    {
                        if (oneNode.SelectedBool == true)//if the node selected was selected already in any state.
                                                         //if(oneNode == selected)
                        {
                            if (oneNode.Status == 3)// in the third state
                            {
                                deselect();
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
                                    String estrink = "jeje";
                                    aListGraph.addUndirectedEdge(selected, oneNode, weight, ref estrink);
                                    terminal.Text += estrink;
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
                    else // want to make and add a new node 
                    {
                        Coordenate newNodePosition = new Coordenate(e.X, e.Y);
                        Node newNode = new Node(newNodePosition, generalRadius, indexCount, this.uniqueID());
                        indexCount++;
                        nodeList.Add(newNode);
                        String estrink = "jeje";
                        aListGraph.addNode(newNode, ref estrink);
                        //TERMINAL
                        //terminal.Text = estrink;
                        terminal.Text = "numero de listas normal = " + aListGraph.NumberOfLists + " numero de lista anidada = " + aListGraph.NumberOfNestedLists;
                    }

                }//left mouse button presed.           
                else
                {
                    if (selected != null)
                    {
                        if (e.X > selected.Position.X - selected.Radius //for conditions in order to determine wheter or not , a click hit the specific node
                       && e.X < selected.Position.X + selected.Radius
                       && e.Y < selected.Position.Y + selected.Radius
                       && e.Y > selected.Position.Y - selected.Radius)
                        {
                            if (selected.Status == 1)
                            {
                                /*selected.Status = 0;//change to the original state.
                                selected.COLOR = Color.Black;//change to black color(original state).
                                selected.SelectedBool = anyNodeSelected = false;
                                selected = null;*/
                            }
                            else
                            {
                                if (selected.Status == 2)//make a own link
                                {
                                    //int weight = AskForAWeight();
                                    int weight = 0;
                                    aListGraph.addDirectedEdge(selected, selected, weight);
                                }
                                else//eliminate the node
                                {
                                    eliminate();
                                }
                            }

                        }
                    }
                    //}
                }

            }
            InvalidatePlus();
        }//Form_MouseDown().
    

         /*************************************************************************************************************************
         * 
         * |||||||||||||||||||||||||||||||||||||||||||||||||||||   EVENTS   |||||||||||||||||||||||||||||||||||||||||||||||||||
         * 
         * ***********************************************************************************************************************/
    private void Move_Click(object sender, EventArgs e)
    {
            keyA_OR_MoveClick();
        
    }

    private void Remove_Click(object sender, EventArgs e)
    {
            keyX_OR_RemoveClick();
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
            if(mousePressed == true && allMoving == true && selectedJustFor != null )
            {
                selectedJustFor.Position.X = e.X;
                selectedJustFor.Position.Y = e.Y;
                Invalidate();
            }

            //nodeMoved = true;
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Escape || e.KeyCode == Keys.S) && selected != null)
            {
                deselect();               
            }

            if(e.KeyCode == Keys.D && selected != null)
            {
               eliminate();
            }

            if(e.KeyCode == Keys.A)
            {
                keyA_OR_MoveClick();  
            }

            if (e.KeyCode == Keys.X)
            {
                keyX_OR_RemoveClick();
            }
            InvalidatePlus();

        }

        public void InvalidatePlus()
        {
            if (selected != null)
            {
                matrixTB.Text = "node selected : " + "ID = " + selected.ID + " Index = " + selected.Index + "\t" + System.Environment.NewLine ;
                matrixTB.Text += System.Environment.NewLine;
            }
            else
            {
                matrixTB.Text = System.Environment.NewLine;
            }
            matrixTB.Text += aListGraph.ToString();
            Invalidate();
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

            /*
            foreach (Node node in nodeList)
            {
                rectangle = new Rectangle(node.Position.X - node.Radius, node.Position.Y - node.Radius, node.Radius * 2, node.Radius * 2);
                graphics.FillEllipse(brush, rectangle);
                pen = new Pen(node.COLOR, 5);
                graphics.DrawEllipse(pen, node.Position.X - node.Radius, node.Position.Y - node.Radius, node.Radius * 2, node.Radius * 2);

            }
            */
            
            for (int i = 0; i < aListGraph.GRAPH.Count; i++)
            {
                
                NodeRef nod = aListGraph.GRAPH[i][i];
                rectangle = new Rectangle(nod.NODO.Position.X - nod.NODO.Radius, nod.NODO.Position.Y - nod.NODO.Radius, nod.NODO.Radius * 2, nod.NODO.Radius * 2);
                graphics.FillEllipse(brush, rectangle);
                pen = new Pen(nod.NODO.COLOR, 5);
                graphics.DrawEllipse(pen, nod.NODO.Position.X - nod.NODO.Radius, nod.NODO.Position.Y - nod.NODO.Radius, nod.NODO.Radius * 2, nod.NODO.Radius * 2);
            }

        }

    /*************************************************************************************************************************
     * 
     * |||||||||||||||||||||||||||||||||||||||||||||||||||||   METHODS   |||||||||||||||||||||||||||||||||||||||||||||||||||
     * 
     * ***********************************************************************************************************************/



       public void keyA_OR_MoveClick()
        {
            if (selected != null)
            {
                deselect();
            }

            if (allMoving)
            {
                foreach (Node node in nodeList)
                {
                    node.COLOR = Color.Black;
                }
                allMoving = (!allMoving);
            }
            else
            {
                if (allDeleting == false)
                {
                    foreach (Node node in nodeList)
                    {
                        node.COLOR = Color.Indigo;
                    }
                    allMoving = (!allMoving);
                }
            }
            InvalidatePlus();
        }


        public void keyX_OR_RemoveClick()
        {
            if (selected != null)
            {
                deselect();
            }

            if (allDeleting)
            {
                foreach (Node node in nodeList)
                {
                    node.COLOR = Color.Black;
                }
                allDeleting = (!allDeleting);
            }
            else
            {
                if (allMoving == false)
                {

                    foreach (Node node in nodeList)
                    {
                        node.COLOR = Color.IndianRed;
                    }
                    allDeleting = (!allDeleting);
                }
            }
            deselect();
            InvalidatePlus();
        }



        public Node findNodeClicked(Coordenate cor)
        {
            Node resNode = null;

            foreach (Node onNode in nodeList)
            {
                if (cor.X > onNode.Position.X - onNode.Radius //for conditions in order to determine wheter or not , a click hit the specific node
                   && cor.X < onNode.Position.X + onNode.Radius
                   && cor.Y < onNode.Position.Y + onNode.Radius
                   && cor.Y > onNode.Position.Y - onNode.Radius)
                {
                    resNode = onNode;
                }//one node clicked.
            }//check all the node list.
            return resNode;
        }

        public void deselect()
        {
            if (selected != null)
            {
                selected.Status = 0;//change to the original state.
                selected.COLOR = Color.Black;//change to black color(original state).
                selected.SelectedBool = anyNodeSelected = false;
                selected = null;
            }
        }

        public void eliminate()
        {
            if (selected != null)
            {
                eliminateNexetEdges(selected);
                aListGraph.removeNode(selected);
                nodeList.Remove(selected);
                selected = null;
                anyNodeSelected = false;
                indexCount--;
            }
        }
        

        public int uniqueID()
        {

            Boolean different;
            int res;
            Random random = new Random();
        
            do
            {
                different = true;
                res = random.Next(1000, 9999);
                foreach(int num in IDList)//ID list should be a tree so the time-complexity to compruebe the exixtence of the random number generated could decresse
                {
                    if(res == num )
                    {
                        different = false;
                        break;//doesn't make sense continuing serching. Basic heuristic avrd.
                    }
                }                
            }
            while (different == false);

            return res;
            
        }

        private int AskForAWeight()
        {
            int weight = 0;

            return weight;
        }

       

        public void InvalidatePlus(String terminalText )
        {
            terminal.Text = terminalText;
            matrixTB.Text = aListGraph.ToString();
            Invalidate();
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
            int uniqueID;

            public Node(Coordenate position, int radius, int index, int identifier)
            {
                this.position = position;
                this.radiusLenght = radius;
                justSelected = false;
                selected = 0;
                this.index = index;//ID of the node
                color = Color.Black;
                uniqueID = identifier;
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
                set { this.index = value; }
            }

            public int ID
            {
                get { return this.uniqueID; }
                set { this.uniqueID = value; }
            }
            /*******************************************************
             *                Geters and seters(End)               *
             *******************************************************/

            /*******************************************************
             *                Methods(Begin)                       *
             *******************************************************/

            public override String ToString()
            {
                return this.ID + this.position.ToString() + " -index = " + this.Index;
            }

        }//Node class.

        public class Edge
        {
            Node client = null;
            Node server = null;
            Boolean directed;

            //Coordenate a;
            //Coordenate b;
            //int weight;
            //int direction;

            public Edge(Node client, Node server)
            {
                this.client = client;
                this.server = server;
                directed = false;
            }

            public Edge(Node client, Node server, Boolean directedBool)
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
            protected int x;
            protected int y;

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

                public override String ToString()
            {
                // return " x = " + this.X + " y = " + this.Y;
                return this.X + "," + this.Y;
            }
        

        }//Coordenate.

        public class TidyPair : Coordenate
        {
            public TidyPair(int x, int y) : base(x,y)
            {
                              
            }
            public override String ToString()
            {
                // return " x = " + this.X + " y = " + this.Y;
                return this.Y + ":" + this.X;
            }
        }

        public class WeightMatrixGraph
        {

        }

        public class NodeRef
        {
            int weight;
            Node nodo;
            TidyPair tidy;
            Boolean activeNode;

            public NodeRef(int weight, Node nodo, TidyPair tidyPair)
            {
                this.tidy = tidyPair;
                this.nodo = nodo;
                this.weight = weight;
                activeNode = false;
            }

            public NodeRef(int weight, Node nodo, TidyPair tidyPair,Boolean active)
            {
                this.tidy = tidyPair;
                this.nodo = nodo;
                this.weight = weight;
                activeNode = active;
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

            public TidyPair TidyPair
            {
                get { return this.tidy; }
                set { this.tidy = value; }
            }

            public Boolean ACTIVATION
            {
                get { return activeNode; }
            }

        }//NodeRef.

        public class AdjacencyList
        {
            List<List<NodeRef>> graph;

            public AdjacencyList()
            {
                graph = new List<List<NodeRef>>();
            }


            public int NumberOfLists
            {
                get { return this.graph.Count(); }
            }

            public int NumberOfNestedLists
            {
                get { return this.graph[0].Count(); }
            }

            public List<List<NodeRef>> GRAPH
            {
                get { return this.graph; }
            }

            public void addNode(Node nodo, ref String terminal)
            {                
                List<NodeRef> newNodeRefList = new List<NodeRef>();//the new list for the new node conections.             
                terminal = "no hay elementos" + graph.Count();                                         //for controling the new list adding Node indexes.
                if (graph.Count() == 0)//ther's no elements
                {
                    TidyPair tidyP = new TidyPair(0, 0); ;
                    NodeRef nodoRef = new NodeRef(-1, nodo, tidyP ,true);            //the new Node that is going to be added have no conection, so it have a -1 value.
                    newNodeRefList.Add(nodoRef);
                    graph.Add(newNodeRefList);                
                }
                else//At least one element.
                {
                    //int j = 0;
                    for(int i = 0; i < graph.Count; i++)
                    {
                        if(i == graph.Count)
                        {
                            newNodeRefList.Add(new NodeRef(-1, graph[i][i].NODO, new TidyPair(i, graph.Count()),true)); //making the new list for the end of the "array".
                        }
                        else
                        {
                           newNodeRefList.Add(new NodeRef(-1, graph[i][i].NODO, new TidyPair(i, graph.Count() ) ) ); //making the new list for the end of the "array".
                        }
                        
                    }

                    graph.Add(newNodeRefList);//adding the list made.
                                              // terminal = "hay elementos" + graph.Count() + graph[0][0].NODO.ToString();
                    int iF = 0;
                    foreach (List<NodeRef> row in graph)
                    {
                        row.Add(new NodeRef(-1, nodo,new TidyPair(nodo.Index,iF))); //adding to each row the new Node.
                        iF++;
                    }
                }
            }

            public void removeNode(Node nodo)//almost the same process as addNode() but vice versa.
            {
                int nodeIndexToEiminate = nodo.Index;         

                foreach (List<NodeRef> row in graph)
                {
                  //  if (row.Count > nodeIndexToEiminate)// ESTO NO DEBERIA PORQUE ESTAR AQUI(por eso esta en español) :(.
                    //{
                        row.RemoveAt(nodeIndexToEiminate);//removing the NodeRef of all the list of nodes.
                    //}
                }

                //if (nodeIndexToEiminate < graph.Count())
                //{
                    graph.RemoveAt(nodeIndexToEiminate);//remove the list of adjacency of the node.
                //}
                /*

                foreach (List<NodeRef> row in graph)// for changing the inner index of each node;
                {
                    foreach (NodeRef node in row)
                    {
                        if (node.ACTIVATION == true && node.NODO.Index > nodeIndexToEiminate)
                        {
                            node.NODO.Index --;
                        }
                    }
                }
                */

                for (int j = 0; j < graph.Count(); j++)
                {
                    List<NodeRef> noRe = graph[j];
                    for (int i = 0; i < noRe.Count();i++ )
                    {
                        if(i == j && noRe[i].NODO.Index > nodeIndexToEiminate)
                        {
                            noRe[i].NODO.Index--;
                        }
                    }
                }

            }//remove a node.

            public void addUndirectedEdge(Node client, Node server, int weight, ref String cadena)
            {
                if(graph.Count > client.Index && graph.Count > server.Index)
                {
                    if (graph[client.Index].Count > server.Index && graph[server.Index].Count > client.Index)
                    {
                        graph[client.Index][server.Index].W = weight;
                        graph[server.Index][client.Index].W = weight;
                    }                       
                }
               
                cadena = " index de cliente " + client.Index + " index de servidor " + server.Index;
            }

            public void addDirectedEdge(Node client, Node server, int weight)
            {
                //if (graph.Count > client.Index)
                //{
                    //if (graph[client.Index].Count > server.Index)
                        graph[client.Index][server.Index].W = weight;

                //}
            }


            public override String ToString()
            {
                String resString = "";
                int i = 0;
                foreach (List<NodeRef> row in graph)
                {
                    resString += "@" + i;
                    foreach (NodeRef nodoR in row)
                    {
                       resString += "\t" + "+" + nodoR.NODO.Index + "+" + "(" + nodoR.TidyPair.ToString() + ")=" + nodoR.W ;
                    }
                    resString += System.Environment.NewLine;
                    i++;
                }
                return resString;
            }
        }//AdjacencyList.

       
    }//Form.
}//namespace.
