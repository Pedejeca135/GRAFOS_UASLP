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
        List<Edge> diEdgeList;
        List<Edge> cicleEdgeList;

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
        Boolean allMoRe = false;
        Boolean justSaved = true;
        String nombreArchivo = "";

        //Boolean nodeMoved = false;

        public Form1()
        {
            InitializeComponent();
            generalRadius = 30; //Here you decide the size of the nodes
            nodeList = new List<Node>();
            edgeList = new List<Edge>();
            diEdgeList = new List<Edge>();
            cicleEdgeList = new List<Edge>();

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
            if (allMoving || allDeleting || allMoRe)
            {
                selectedJustFor = findNodeClicked(new Coordenate(e.X, e.Y));
                selected = selectedJustFor;

                if (allDeleting)
                {
                    //selected = selectedJustFor;
                    eliminate();
                }
                if(allMoRe)
                {
                    //selected = selectedJustFor;
                    if (e.Button == System.Windows.Forms.MouseButtons.Right)
                    {                                       
                        eliminate();                        
                    }
                    else
                    {
                        if (selected == null)
                        {
                            create(new Coordenate(e.X, e.Y));
                        }
                    }
                }
            }
            else
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Left)//if mouse button pressed is left.
                {
                    Node oneNode = null;
                    //one node clicked.//check all the node list.
                    oneNode = findNodeClicked(new Coordenate(e.X, e.Y));

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
                            if (selected != null)//want to do a link between nodes.
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
                                    diEdgeList.Add(new Edge(selected, oneNode, true));
                                    aListGraph.addDirectedEdge(selected, oneNode, weight);
                                }
                                InvalidatePlus();
                            }
                            else // select for the first time.                            
                            {
                                // is not selected, (Status == 0).
                                oneNode.Status = 1;//change to the first selected state.
                                oneNode.COLOR = Color.ForestGreen;//change to green color to indicate the status(can move).
                                oneNode.SelectedBool = true;
                                anyNodeSelected = true;
                                selected = oneNode;
                            }
                        }
                    }
                    else // want to make and add a new node 
                    {
                        create(new Coordenate(e.X, e.Y));                       
                    }

                }//left mouse button presed.           
                else//right mouse button pressed.
                {
                    if (selected != null)
                    {
                        if (selected == findNodeClicked(new Coordenate(e.X, e.Y)))
                        {
                            if (selected.Status > 1)                                {
                                if (selected.Status == 2)//make a own link
                                {
                                    //int weight = AskForAWeight();
                                    int weight = 0;
                                    aListGraph.addDirectedEdge(selected, selected, weight);
                                    cicleEdgeList.Add(new Edge(selected));
                                    InvalidatePlus();
                                    }
                                    else//eliminate the node
                                    {
                                        eliminate();
                                    }
                                }
                        }                        
                    }                   
                }
            }
            InvalidatePlus(1);
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

        private void MoRe_Click(object sender, EventArgs e)
        {
            keyF_OR_MoRe();
        }

        private void Save_Click(object sender, EventArgs e)
        {
            saveFile();
        }

        private void Load_Click(object sender, EventArgs e)
        {
            if (justSaved == false)
            {
                SaveChangesWindow gdc = new SaveChangesWindow();
                gdc.ShowDialog();
                if (gdc.Operation == 1 || gdc.Operation == 2)
                {
                    if (gdc.Operation == 1)
                    {
                        saveFile();
                    }

                    foreach (Node node in nodeList)
                    {
                        eliminateNexetEdges(node);
                        //
                    }
                    nodeList = new List<Node>();
                    justSaved = true;
                    openFile();

                }
            }
            InvalidatePlus();
        }

        private void New_Click(object sender, EventArgs e)
        {
            if (justSaved == false)
            {
                SaveChangesWindow gdc = new SaveChangesWindow();
                gdc.ShowDialog();
                if (gdc.Operation == 1 || gdc.Operation == 2)
                {
                    if (gdc.Operation == 1)
                    {
                        saveFile();
                    }
                    foreach (Node node in nodeList)
                    {
                        eliminateNexetEdges(node);
                        //eliminateNexetDirectedEdges(node);
                    }
                    nodeList = new List<Node>();
                    justSaved = true;
                }
            }
            Invalidate();
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
                InvalidatePlus();
            }
            if (mousePressed == true && allMoving == true && selectedJustFor != null)
            {
                selectedJustFor.Position.X = e.X;
                selectedJustFor.Position.Y = e.Y;
                InvalidatePlus();
            }
            if(mousePressed == true && e.Button == MouseButtons.Left && allMoRe == true && selectedJustFor != null)
            {
                selectedJustFor.Position.X = e.X;
                selectedJustFor.Position.Y = e.Y;             
                InvalidatePlus();
                
            }
            //nodeMoved = true;
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Escape || e.KeyCode == Keys.S) && selected != null)
            {
                deselect();
            }

            if (e.KeyCode == Keys.D && selected != null)
            {
                eliminate();
            }

            if (e.KeyCode == Keys.A)
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
            justSaved = false;// all that requires invalidate also should change the jusSaved state.
            commonInvalidateActions();
            Invalidate();
        }

        public void InvalidatePlus(int code)
        {
            commonInvalidateActions();
            Invalidate();
        }

        public void commonInvalidateActions()
        {
            
            if (selected != null)
            {
                terminal.Text = "Node selected : " + System.Environment.NewLine + "ID = " + selected.ID + System.Environment.NewLine + "Index = " + selected.Index + "\t" + System.Environment.NewLine;
               if (aListGraph.Directed() == true)
                {
                    DirectedGrade dG;
                    dG = aListGraph.GradeOfDirectedNode(selected);
                    terminal.Text += "Grado(Nodo): " + dG.Total;
                    terminal.Text += System.Environment.NewLine;
                    terminal.Text += "  GradoEntrada ( [<-] ): " + dG.Input;
                    terminal.Text += System.Environment.NewLine;
                    terminal.Text += "  GradoSalida    ( [->] ): " + dG.Output;                 
               }
                else
                {

                    terminal.Text += "Grado(Nodo): " + aListGraph.GradeOfNode(selected);
                }
            }
            else
            {
                terminal.Text = "Node selected : ";
            }
            statusTB.Text = "Nombre :" + nombreArchivo;
            statusTB.Text += "Grado(Grafo) : " + aListGraph.Grade();
            statusTB.Text += System.Environment.NewLine;
            statusTB.Text += "Dirigido : " + aListGraph.Directed();            
            statusTB.Text += System.Environment.NewLine;
            statusTB.Text += "Completo : " + aListGraph.Complete();
            statusTB.Text += System.Environment.NewLine;
            statusTB.Text += "Pseudo: "+ aListGraph.Pseudo();
            statusTB.Text += System.Environment.NewLine;
            statusTB.Text += "Cíclico : ";// + aListGraph.Ciclo();
            statusTB.Text += System.Environment.NewLine;
            statusTB.Text += "Bipartita : ";// + aListGraph.Bip();
            statusTB.Text += System.Environment.NewLine;

            matrixTB.Text = aListGraph.ToString();
        }

        /*****************************
         * 
         *      method for painting.
         * 
         * ****************************/
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = e.Graphics;
            Pen pen = new Pen(Color.Black, 5);
            Brush brush = new SolidBrush(BackColor);
            Rectangle rectangle;

            Pen penDirect = new Pen(Color.Black, 8);
            penDirect.StartCap = System.Drawing.Drawing2D.LineCap.RoundAnchor; 
            penDirect.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;


            foreach (Edge edge in edgeList)//undirected edges.
            {
                graphics.DrawLine(pen, edge.A.X, edge.A.Y, edge.B.X, edge.B.Y);
            }
            foreach (Edge edge in cicleEdgeList)//cicled edge.
            {
                Point StartPoint = new Point(edge.A.X, edge.A.Y);
                Point unoP = new Point(edge.A.X - generalRadius*4 , edge.A.Y - generalRadius*4);
                Point dosP = new Point(edge.A.X - generalRadius*4 , edge.A.Y + generalRadius*4);
                graphics.DrawBezier(pen, StartPoint,unoP,dosP,StartPoint);
            }

            Double equis_X;
            Double ye_Y;
            foreach (Edge edge in diEdgeList)//directed edges.
            {
                Double rate = edge.Distancia/generalRadius;
                equis_X = (edge.A.X + rate * edge.B.X)/(1+rate);
                ye_Y = (edge.A.Y + rate * edge.B.Y)/(1 + rate);
                //graphics.DrawLine(penDirect, edge.A.X, edge.A.Y, edge.B.X, edge.B.Y);
                graphics.DrawLine(penDirect, edge.A.X, edge.A.Y, (float)equis_X, (float)ye_Y);
                //int difFromCenterToPin = (int)Math.Pow((generalRadius / 2.0), 0.5);
                //Coordenate pinOfEdge = new Coordenate(edge.B.X-difFromCenterToPin, edge.B.Y-difFromCenterToPin);              


                //graphics.DrawLine(penDirect, pinOfEdge.X, pinOfEdge.Y, pinOfEdge.X-generalRadius/2 , pinOfEdge.Y);
                //graphics.DrawLine(penDirect, pinOfEdge.X, pinOfEdge.Y, pinOfEdge.X, pinOfEdge.Y - generalRadius / 2);               
            }
            for (int i = 0; i < aListGraph.GRAPH.Count; i++)//Nodes.
            {
                NodeRef nod = aListGraph.GRAPH[i][i];
                rectangle = new Rectangle(nod.NODO.Position.X - nod.NODO.Radius, nod.NODO.Position.Y - nod.NODO.Radius, nod.NODO.Radius * 2, nod.NODO.Radius * 2);
                graphics.FillEllipse(brush, rectangle);
                pen = new Pen(nod.NODO.COLOR, 5);
                graphics.DrawEllipse(pen, nod.NODO.Position.X - nod.NODO.Radius, nod.NODO.Position.Y - nod.NODO.Radius, nod.NODO.Radius * 2, nod.NODO.Radius * 2);

                //draw inside the node a index.
                String index_S = "" + nod.NODO.Index;
                int fontSize = generalRadius - 10;
                graphics.DrawString(index_S, new Font(FontFamily.GenericSansSerif, fontSize), new SolidBrush(Color.Black), nod.NODO.Position.X - (fontSize / 2), nod.NODO.Position.Y - (fontSize / 2));
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
        }

        /*************************************************************************************************************************
         * 
         * |||||||||||||||||||||||||||||||||||||||||||||||||||||   METHODS ()  |||||||||||||||||||||||||||||||||||||||||||||||||||
         * 
         * ***********************************************************************************************************************/

        public void saveFile()
        {
            
            TextWriter sw = null;
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Save text Files";
            saveFileDialog.DefaultExt = "grp";
            saveFileDialog.Filter = "Text files (*.grp)|*.grp|All files (*.*)|*.*";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                sw = new StreamWriter(saveFileDialog.FileName);
                MessageBox.Show(saveFileDialog.FileName);
            }
            /*
             * atributes of a node that can be unique
            Coordenate position; 
            int radiusLenght; 
            int index;
            int uniqueID;
            */
            if (justSaved == false)
            {
                foreach (Node node in nodeList)//all about the node
                {
                    sw.WriteLine(node.ID + "," + node.Index + "," + node.Position.X + "," + node.Position.Y + "," + node.Radius);
                }
                sw.WriteLine("Marix");
                foreach (List<NodeRef> row in aListGraph.GRAPH)
                {
                    foreach (NodeRef nodeR in row)
                    {
                        sw.Write("|" + nodeR.W);
                    }
                    sw.WriteLine();
                }
                sw.WriteLine("Edges");
                foreach (Edge edge in edgeList)
                {
                    sw.WriteLine(edge.A.X + "," + edge.A.Y + "," + edge.B.X + "," + edge.B.Y);
                }
                sw.WriteLine("D_Edges");
                foreach (Edge edge in diEdgeList)
                {
                    sw.WriteLine(edge.A.X + "," + edge.A.Y + "," + edge.B.X + "," + edge.B.Y);
                }
                sw.Close();
                justSaved = true;
            }
        }

        public void openFile()
        {
            string[] auxiliar;
            StreamReader sr = null;
            OpenFileDialog openFileDialog = new OpenFileDialog();


            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                sr = new StreamReader(openFileDialog.FileName);
                auxiliar = sr.ReadLine().Split(',');
                while (sr != null && !sr.EndOfStream && auxiliar[0] != "Edges")
                {
                    int x = int.Parse(auxiliar[0]);
                    int y = int.Parse(auxiliar[1]);
                    Node nodo = new Node(x, y);
                    nodeList.Add(nodo);
                    auxiliar = sr.ReadLine().Split(',');
                }
                while (sr != null && !sr.EndOfStream && auxiliar[0] != "D_Edges")
                {
                    Node server = new Node();
                    Node client = new Node();
                    auxiliar = sr.ReadLine().Split(',');
                    int nodo1X = int.Parse(auxiliar[0]);
                    int nodo1Y = int.Parse(auxiliar[1]);
                    int nodo2X = int.Parse(auxiliar[2]);
                    int nodo2Y = int.Parse(auxiliar[3]);

                    foreach (Node node in nodeList)
                    {
                        if (node.Position.X == nodo1X && node.Position.Y == nodo1Y)//it just can be one of all
                        {
                            server = node;
                        }
                        else
                        {
                            if (node.Position.X == nodo2X && node.Position.Y == nodo2Y)//it also can be just one of all
                            {
                                client = node;
                            }
                        }
                    }
                    Edge edge = new Edge(server, client);
                    edgeList.Add(edge);
                }
                while (sr != null && !sr.EndOfStream )
                {
                    Node server = new Node();
                    Node client = new Node();
                    auxiliar = sr.ReadLine().Split(',');
                    int nodo1X = int.Parse(auxiliar[0]);
                    int nodo1Y = int.Parse(auxiliar[1]);
                    int nodo2X = int.Parse(auxiliar[2]);
                    int nodo2Y = int.Parse(auxiliar[3]);

                    foreach (Node node in nodeList)
                    {
                        if (node.Position.X == nodo1X && node.Position.Y == nodo1Y)//it just can be one of all
                        {
                            server = node;
                        }
                        else
                        {
                            if (node.Position.X == nodo2X && node.Position.Y == nodo2Y)//it also can be just one of all
                            {
                                client = node;
                            }
                        }
                    }
                    Edge edge = new Edge(server, client);
                    diEdgeList.Add(edge);
                }
                sr.Close();
            }

        }

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
                if (allDeleting == false && allMoRe == false)
                {
                    foreach (Node node in nodeList)
                    {
                        node.COLOR = Color.Green;
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
                if (allMoving == false && allMoRe == false)
                {
                    foreach (Node node in nodeList)
                    {
                        node.COLOR = Color.Red;
                    }
                    allDeleting = (!allDeleting);
                }
            }
            deselect();
            InvalidatePlus();
        }

        public void keyF_OR_MoRe()
        {
            if (selected != null)
            {
                deselect();
            }

            if (allMoRe)
            {
                foreach (Node node in nodeList)
                {
                    node.COLOR = Color.Black;
                }
                allMoRe = (!allMoRe);
            }
            else
            {
                if (allDeleting == false && allMoving == false)
                {
                    foreach (Node node in nodeList)
                    {
                        node.COLOR = Color.Indigo;
                    }
                    allMoRe = (!allMoRe);
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
            if (aListGraph.GRAPH.Count <= 1)
                justSaved = true;

            if (selected != null)
            {
                eliminateNexetEdges(selected);
                eliminateNexetDirectedEdges(selected);
                eliminateCicledEdges(selected);

                aListGraph.removeNode(selected);
                nodeList.Remove(selected);
                selected = null;
                anyNodeSelected = false;
                indexCount--;
            }
            InvalidatePlus();
        }
        
        public void create(Coordenate cor)
        {
            Coordenate newNodePosition = new Coordenate(cor.X, cor.Y);
            Node newNode;
            if (allMoRe)
            {
               newNode = new Node(newNodePosition, generalRadius, indexCount, this.uniqueID(),Color.Indigo);
            }
            else
            {
               newNode = new Node(newNodePosition, generalRadius, indexCount, this.uniqueID());
            }           
            indexCount++;
            nodeList.Add(newNode);
            aListGraph.addNode(newNode);
            InvalidatePlus();
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

        public void eliminateNexetDirectedEdges(Node node)
        {
            List<Edge> newEdges = new List<Edge>();

            foreach (Edge edge in diEdgeList)
            {
                if (edge.Client != node && edge.Server != node)
                {
                    newEdges.Add(edge);
                }
            }
            diEdgeList = newEdges;
        }

        public void eliminateCicledEdges(Node node)
        {
            List<Edge> newEdges = new List<Edge>();

            foreach (Edge edge in cicleEdgeList)
            {
                if (edge.Client != node && edge.Server != node)
                {
                    newEdges.Add(edge);
                }
            }
            cicleEdgeList = newEdges;
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

            public Node() //default constructor of the class Node
            {

            }

            public Node(int x, int y)
            {
                this.Position = new Coordenate(x, y);
            }

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
            public Node(Coordenate position, int radius, int index, int identifier,Color color)
            {
                this.position = position;
                this.radiusLenght = radius;
                justSelected = false;
                selected = 0;
                this.index = index;//ID of the node
                this.color = color;
                uniqueID = identifier;
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

                //for undirected edges.
            public Edge(Node client, Node server)
            {
                this.client = client;
                this.server = server;
                directed = false;
            }

            //for directed edges.
            public Edge(Node client, Node server, Boolean directedBool)
            {
                this.client = client;
                this.server = server;
                directed = directedBool;
            }

            //for cicle edge
            public Edge(Node unique)
            {
                this.client = unique;
                this.server = unique;
                directed = false;
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

            public Double Distancia => Math.Pow(Math.Pow(B.X - A.X, 2.0) + Math.Pow(B.Y - A.Y, 2.0), 0.5); 
            
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

        public class DirectedGrade
        {
            int input;
            int output;

            public DirectedGrade()
            {

            }
            public DirectedGrade(int input, int output)
            {
                this.input = input;
                this.output = output;
            }
        
            public int Input
            {
                get { return this.input; }
            }
            public int Output
            {
                get { return this.output; }
            }

           public int Total
            {
                get { return input + output; }
            }
        }//DirectedGrade.

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

            public void addNode(Node nodo)
            {
                List<NodeRef> newNodeRefList = new List<NodeRef>();//the new list for the new node conections.           

                if (graph.Count() == 0)//ther's no elements
                {
                    TidyPair tidyP = new TidyPair(0, 0); ;
                    NodeRef nodoRef = new NodeRef(-1, nodo, tidyP, true);            //the new Node that is going to be added have no conection, so it have a -1 value.
                    newNodeRefList.Add(nodoRef);
                    graph.Add(newNodeRefList);
                }
                else//At least one element.
                {
                    //int j = 0;
                    for (int i = 0; i < graph.Count; i++)
                    {
                        if (i == graph.Count)
                        {
                            newNodeRefList.Add(new NodeRef(-1, graph[i][i].NODO, new TidyPair(i, graph.Count()), true)); //making the new list for the end of the "array".
                        }
                        else
                        {
                            newNodeRefList.Add(new NodeRef(-1, graph[i][i].NODO, new TidyPair(i, graph.Count()))); //making the new list for the end of the "array".
                        }

                    }

                    graph.Add(newNodeRefList);//adding the list made.
                                              // terminal = "hay elementos" + graph.Count() + graph[0][0].NODO.ToString();
                    int iF = 0;
                    foreach (List<NodeRef> row in graph)
                    {
                        row.Add(new NodeRef(-1, nodo, new TidyPair(nodo.Index, iF))); //adding to each row the new Node.
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
                    for (int i = 0; i < noRe.Count(); i++)
                    {
                        if (i == j && noRe[i].NODO.Index > nodeIndexToEiminate)
                        {
                            noRe[i].NODO.Index--;
                        }
                    }
                }

            }//remove a node.

            public void addUndirectedEdge(Node client, Node server, int weight)
            {
                if (graph.Count > client.Index && graph.Count > server.Index)
                {
                    if (graph[client.Index].Count > server.Index && graph[server.Index].Count > client.Index)
                    {
                        graph[client.Index][server.Index].W = weight;
                        graph[server.Index][client.Index].W = weight;
                    }
                }

                //cadena = " index de cliente " + client.Index + " index de servidor " + server.Index;
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
                        resString += "\t"  + "("+ i + ":" + nodoR.NODO.Index + ")= " + nodoR.W;
                    }
                    resString += System.Environment.NewLine;
                    i++;
                }
                return resString;
            }

            public int Grade()
            {
                int res = 0;
                for (int i = 0; i < graph.Count(); i++)
                {
                  res += GradeOfNode(graph[i][i].NODO);                 
                }
                return res;
            }

            public Boolean Pseudo()
            {
                Boolean res = false;
                for (int i = 0; i < graph.Count(); i++)
                {
                    if(graph[i][i].W > -1)
                    {
                        res = true;
                    }
                }
                return res;
            }

            public int GradeOfNode(Node nodo)
            {
                int res = 0;
                int i = 0;
                int nodeIndex = nodo.Index;
                foreach (NodeRef nodeR in graph[nodeIndex])
                {
                    if (nodeR.W > -1)
                    {
                        if (nodeIndex == i)
                        {
                            res += 2;
                        }
                        else
                        {
                            res++;
                        }
                    }
                    i++;
                }
                return res;
            }

    
           public DirectedGrade GradeOfDirectedNode(Node nodo)
            {
                DirectedGrade res;
                int input = 0;
                int output = 0;
                foreach (List<NodeRef> row in graph)
                {
                    foreach (NodeRef nodeR in row)
                    {
                        if (graph.IndexOf(row) == row.IndexOf(nodeR))
                        {
                            if (nodeR.W > -1)
                            {
                               input++;
                               output++;
                            }
                        }
                        else
                        {
                            if (graph.IndexOf(row) == nodo.Index)
                            {
                                if (nodeR.W > -1)
                                {
                                    output++;
                                }
                            }
                            if (row.IndexOf(nodeR) == nodo.Index)
                            {
                                if (nodeR.W > -1)
                                {
                                    input++;
                                }
                            }

                        }
                    }
                }
                res = new DirectedGrade(input,output);
                return res;
            }

            public Boolean Directed()
            {
                for (int i = 0; i < graph.Count(); i++)
                {
                    for (int j = 0; j < graph.Count(); j++)
                    {
                        if (graph[i][j].W != graph[j][i].W)
                        {
                            return true;
                        }
                    }
                }
                return false;
            }

            public Boolean Complete()
            {
                Boolean res = true;
                for (int i = 0; i < graph.Count(); i++)
                {
                    for (int j = 0; j < graph[i].Count(); j++)
                    {
                        if (graph[i][j].W < 0 && i != j)
                        {
                            res = false;
                        }
                    }
                   
                }
                return res;
            }


        }//AdjacencyList.      

        private void terminal_TextChanged(object sender, EventArgs e)
        {

        }
    }//Form.
}//namespace.
