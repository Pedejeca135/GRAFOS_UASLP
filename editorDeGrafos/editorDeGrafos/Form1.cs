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
using System.Drawing.Drawing2D;

namespace editorDeGrafos
{
    public partial class Form1 : Form
    {
        Boolean isoForm;

        List<Node> nodeList;
        List<Edge> edgeList;
        List<Edge> diEdgeList;
        List<Edge> cicleEdgeList;

        //for paths and cicles:
        List<Edge> eulerPathEdges;
        List<Node> eulerPathNodes;
        Boolean eulerBoolDo = false;
        List<Edge> pathToAnimate;

        Boolean nodePathsReady = false;
        Node initialNodePath = null;
        Node finalNodePath = null;
        Timer timerColor = new System.Windows.Forms.Timer();
        int timerColorOption = 0;
        int tmpCount = 0;

        //Boolean iniBoolClick_H_E = false;
        //Boolean finBoolClick_H_E = false;

        List<Edge> hamiltonPathEdges;
        List<Node> hamiltonPathNodes;
        Boolean hamiltonBoolDo = false;



        Node selectedNode;
        Node selected = null;
        Node selectedJustFor = null;

        public int generalRadius;
        Boolean anyNodeSelected;
        int indexCount;
        public AdjacencyList aListGraph;
        Boolean mousePressed;
        List<int> IDList;

        Boolean allMoving = false;
        Boolean allDeleting = false;
        Boolean allMoRe = false;
        Boolean justSaved = true;
        String nombreArchivo = "";

        Boolean matIn = false;
        Form2 formaIsomorfismo;// = null;
        SaveChangesWindow gdc;

        //Boolean nodeMoved = false;

        public Form1()
        {
            InitializeComponent();
            commonCostructor();
            isoForm = false;
            fuerzaBrutaToolStripMenuItem.Visible = false;
            traspuestaToolStripMenuItem.Visible = false;
            intercambioToolStripMenuItem.Visible = false;
            IsomtextBox.Visible = false;
        }

        public Form1(int equis)
        {
            InitializeComponent();
            commonCostructor();
            isoForm = true;
            fuerzaBrutaToolStripMenuItem.Visible = true;
            traspuestaToolStripMenuItem.Visible = true;
            intercambioToolStripMenuItem.Visible = true;
            IsomtextBox.Visible = true;
        }

        private void commonCostructor()
        {
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


            statusTB.Text = "Nombre :" + nombreArchivo;
            terminal.Text = "Node selected : ";

        }


        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {

            if (f3.Operation == 1)
            {
                aListGraph.allBlack();
                Invalidate();
                f3.Operation = 0;
             }

            
            mousePressed = true;
            if ((eulerBoolDo || hamiltonBoolDo) && aListGraph.GRAPH.Count() > 1)
            {

                if (initialNodePath == null || finalNodePath == null)//if any node does not exist.
                {
                    if (initialNodePath == null)
                    {
                        //one node clicked.//check all the node list.
                        initialNodePath = findNodeClicked(new Coordenate(e.X, e.Y));
                        if (initialNodePath != null)
                        {
                            initialNodePath.COLOR = Color.Blue;//change the color of the initial node
                            Invalidate();
                        }


                        if (aListGraph.GRAPH.Count() == 1)
                        {
                            finalNodePath = initialNodePath;
                        }
                    }
                    else
                    {
                        finalNodePath = findNodeClicked(new Coordenate(e.X, e.Y));
                        if (finalNodePath != null)
                        {
                            finalNodePath.COLOR = Color.Red;//change the color of the final node
                            Invalidate();
                        }


                        nodePathsReady = true;

                        if (eulerBoolDo)//le toca a euler.
                        {
                            eulerBoolDo = false;
                            if (initialNodePath == finalNodePath)//cycle
                            {
                                finalNodePath.COLOR = Color.Beige;//
                                Invalidate();
                                cycleOfEuler();
                            }
                            else//path
                            {
                                pathOfEuler();
                            }
                        }
                        else//le toca a hamilton.
                        {
                            hamiltonBoolDo = false;
                            if (initialNodePath == finalNodePath)//cycle
                            {
                                finalNodePath.COLOR = Color.Beige;//
                                cycleOfHamilton();
                            }
                            else//path
                            {
                                pathOfHamilton();
                            }

                        }

                        initialNodePath = null;
                        finalNodePath = null;
                    }

                }
            }
            else if (allMoving || allDeleting || allMoRe)
            {
                selectedJustFor = findNodeClicked(new Coordenate(e.X, e.Y));
                selected = selectedJustFor;

                if (allDeleting)
                {
                    //selected = selectedJustFor;
                    eliminate();
                }
                if (allMoRe)
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
                            justSaved = false;
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
                                    int weight = AFWeight("Bidireccional");
                                    //int weight = 0;
                                    if (weight >= 0)
                                    {
                                        Edge edge = new Edge(selected, oneNode);
                                        edgeList.Add(edge);
                                        aListGraph.addUndirectedEdge(edge, weight);
                                        justSaved = false;
                                    }
                                }
                                if (selected.Status == 3)//directed link
                                {
                                    //here i have to ask the weight of the link.
                                    int weight = AFWeight("Dirijido");
                                    //int weight = 0;
                                    if (weight >= 0)
                                    {
                                        diEdgeList.Add(new Edge(selected, oneNode, true));
                                        aListGraph.addDirectedEdge(selected, oneNode, weight);
                                    }
                                }
                                InvalidatePlus(1);
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
                            if (selected.Status > 1)
                            {
                                if (selected.Status == 2)//make a own link
                                {
                                    int weight = AFWeight("Ciclo");
                                    //int weight = 0;
                                    if (weight >= 0)
                                    {
                                        aListGraph.addDirectedEdge(selected, selected, weight);
                                        cicleEdgeList.Add(new Edge(selected));
                                    }
                                    InvalidatePlus(1);
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
            InvalidatePlus();
        }//Form_MouseDown().


        /*************************************************************************************************************************
        * 
        * |||||||||||||||||||||||||||||||||||||||||||||||||||||   EVENTS   |||||||||||||||||||||||||||||||||||||||||||||||||||
        * 
        * ***********************************************************************************************************************/

        public void closeIsoFormClicked(object sender, EventArgs e)
        {
            InvalidatePlus();
        }


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
                 gdc = new SaveChangesWindow();
                gdc.ShowDialog();
                if (gdc.Operation == 1 || gdc.Operation == 2)
                {
                    if (gdc.Operation == 1)
                    {
                        saveFile();
                    }
                    loadCommonActions();
                }
            }
            else
            {
                loadCommonActions();
            }
        }

        private void loadCommonActions()
        {
            
            
            List<Node> nodeList_BU = nodeList;
            AdjacencyList aListGraph_BU = aListGraph;
            List<Edge> diEdgeList_BU = diEdgeList;
            List<Edge> edgeList_BU = edgeList;
            List<Edge> cicleEdgeList_BU = cicleEdgeList;

            List<Node> listOfNodes = aListGraph.listOfNodes_IG ;
            List<Edge> listOfEdges  = aListGraph.listOfEdges_IG;

            nodeList = new List<Node>();
            aListGraph = new AdjacencyList();
            diEdgeList = new List<Edge>();
            edgeList = new List<Edge>();
            cicleEdgeList = new List<Edge>();

            aListGraph.listOfNodes_IG = new List<Node>();
            aListGraph.listOfEdges_IG = new List<Edge>();

            if (openFile() == 0)
            {
                nodeList = nodeList_BU;
                aListGraph = aListGraph_BU;
                diEdgeList = diEdgeList_BU;
                edgeList = edgeList_BU;
                cicleEdgeList = cicleEdgeList_BU;

                aListGraph.listOfNodes_IG = listOfNodes;
                aListGraph.listOfEdges_IG = listOfEdges;
            }
            else
            {
                InvalidatePlus();
                justSaved = true;
            }
            


         
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
                        aListGraph.eliminateNexetEdges(node);
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
                InvalidatePlus(1);
            }
            if (mousePressed == true && allMoving == true && selectedJustFor != null)
            {
                selectedJustFor.Position.X = e.X;
                selectedJustFor.Position.Y = e.Y;
                InvalidatePlus(1);
            }
            if (mousePressed == true && e.Button == MouseButtons.Left && allMoRe == true && selectedJustFor != null)
            {
                selectedJustFor.Position.X = e.X;
                selectedJustFor.Position.Y = e.Y;
                InvalidatePlus(1);

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
            commonInvalidateActions();
            Invalidate();
        }

        public void InvalidatePlus(int code)
        {
            justSaved = false;// all that requires invalidate also should change the jusSaved state.           
            commonInvalidateActions();
            Invalidate();
        }

        public void commonInvalidateActions()
        {
            matrixTB.Text = aListGraph.ToString(matIn);

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
            statusTB.Text = "Nombre :" + nombreArchivo + System.Environment.NewLine;
            statusTB.Text += "Grado(Grafo) : " + aListGraph.Grade();
            statusTB.Text += System.Environment.NewLine;
            statusTB.Text += "Dirigido : " + aListGraph.Directed();
            statusTB.Text += System.Environment.NewLine;
            statusTB.Text += "Completo : " + aListGraph.Complete();
            statusTB.Text += System.Environment.NewLine;
            statusTB.Text += "Pseudo: " + aListGraph.Pseudo();
            statusTB.Text += System.Environment.NewLine;
            statusTB.Text += "Cíclico : " + aListGraph.Cicled();
            statusTB.Text += System.Environment.NewLine;
            statusTB.Text += "Bipartita : " + aListGraph.Bip();
            statusTB.Text += System.Environment.NewLine;

            if ((formaIsomorfismo == null || (formaIsomorfismo != null && formaIsomorfismo.Visible == false)) && isoForm == false)
            {
                IsomtextBox.Visible = false;
            }
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
            Pen pen2;

            Pen penDirect = new Pen(Color.DimGray, 8);
            penDirect.StartCap = System.Drawing.Drawing2D.LineCap.RoundAnchor;
            penDirect.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;


            /*foreach (Edge edge in edgeList)//undirected edges.
             {
                 pen2 = new Pen(edge.COLOR, 5);
                 graphics.DrawLine(pen2, edge.A.X, edge.A.Y, edge.B.X, edge.B.Y);
             }*/
            foreach (Edge edge in aListGraph.listOfEdges_IG)//undirected edges.
            {
                pen2 = new Pen(edge.COLOR, 5);
                graphics.DrawLine(pen2, edge.A.X, edge.A.Y, edge.B.X, edge.B.Y);
            }

            foreach (Edge edge in cicleEdgeList)//cicled edge.
            {
                Point StartPoint = new Point(edge.A.X, edge.A.Y);
                Point unoP = new Point(edge.A.X - generalRadius * 4, edge.A.Y - generalRadius * 4);
                Point dosP = new Point(edge.A.X - generalRadius * 4, edge.A.Y + generalRadius * 4);
                //graphics.DrawBezier(pen, StartPoint,unoP,dosP,StartPoint);
                GraphicsPath gPath = new GraphicsPath();
                gPath.AddBezier(StartPoint, unoP, dosP, StartPoint);
                e.Graphics.DrawPath(pen, gPath);
            }

            Double equis_X;
            Double ye_Y;
            foreach (Edge edge in diEdgeList)//directed edges.
            {
                Double rate = edge.Distancia / generalRadius;
                equis_X = (edge.A.X + rate * edge.B.X) / (1 + rate);
                ye_Y = (edge.A.Y + rate * edge.B.Y) / (1 + rate);
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


        private void marAllEdgesAsNotVisited(List<Edge> listEdge)
        {
            foreach (Edge edge in listEdge)
            {
                edge.visitada = false;
                edge.COLOR = Color.Black;
            }
        }
        public int AFWeight(String type)
        {
            int res = 0;
            AskForWeight afaw = new AskForWeight(type);
            afaw.ShowDialog();
            res = afaw.getX;
            return res;
        }

        public void saveFile()
        {
            TextWriter sw = null;
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = "Save text Files";
            saveFileDialog.DefaultExt = "txt";
            saveFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
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
            foreach (Node node in aListGraph.LIST_NODES)//all about the node
            {
                sw.WriteLine(node.ID + "," + node.Index + "," + node.Position.X + "," + node.Position.Y + "," + node.Radius);
            }
            sw.WriteLine("Matrix");
            foreach (List<NodeRef> row in aListGraph.GRAPH)
            {
                foreach (NodeRef nodeR in row)
                {
                    sw.Write(nodeR.W + ",");
                }
                sw.WriteLine();
            }
            sw.WriteLine("Edges");
            foreach (Edge edge in aListGraph.listOfEdges_IG)
            {
                sw.WriteLine(edge.Client.Index + "," + edge.Server.Index);
            }
            sw.WriteLine("D_Edges");
            foreach (Edge edge in diEdgeList)
            {
                sw.WriteLine(edge.Client.Index + "," + edge.Server.Index);
            }
            sw.WriteLine("C_Edges");
            foreach (Edge edge in cicleEdgeList)
            {
                sw.WriteLine(edge.Client.Index);
            }
            sw.Close();
            justSaved = true;

        }

        public int openFile()
        {
            int statusRes = 0;

            StreamReader sr = null;
            OpenFileDialog openFileDialog = new OpenFileDialog();


            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                sr = new StreamReader(openFileDialog.FileName);
                char[] Delimiters = new char[] { ',' };
                string[] Input = sr.ReadLine().Split(Delimiters, StringSplitOptions.RemoveEmptyEntries);

                while (sr != null && !sr.EndOfStream && Input[0] != "Matrix")
                {
                    int idON;
                    int indexON;
                    int x;
                    int y;
                    int radiusON;


                    int.TryParse(Input[0], out idON);
                    int.TryParse(Input[1], out indexON);
                    int.TryParse(Input[2], out x);
                    int.TryParse(Input[3], out y);
                    int.TryParse(Input[4], out radiusON);

                    Coordenate cor = new Coordenate(x, y);
                    Node node = new Node(cor, radiusON, indexON, idON);
                    aListGraph.addNode(node);
                    nodeList.Add(node);
                    Input = sr.ReadLine().Split(Delimiters, StringSplitOptions.RemoveEmptyEntries);
                }
                if (Input[0] == "Matrix")
                {
                    Input = sr.ReadLine().Split(Delimiters, StringSplitOptions.RemoveEmptyEntries);
                }

                int i = 0;
                while (sr != null && !sr.EndOfStream && Input[0] != "Edges")
                {
                    int Peso;

                    for (int j = 0; j < Input.Length; j++)
                    {
                        int.TryParse(Input[j], out Peso);
                        aListGraph.GRAPH[i][j].W = Peso;
                    }
                    Input = sr.ReadLine().Split(Delimiters, StringSplitOptions.RemoveEmptyEntries);
                    i++;
                }
                if (Input[0] == "Edges")
                {
                    Input = sr.ReadLine().Split(Delimiters, StringSplitOptions.RemoveEmptyEntries);
                }
                while (sr != null && !sr.EndOfStream && Input[0] != "D_Edges")
                {
                    Node server = new Node();
                    Node client = new Node();

                    int nodo_C;
                    int.TryParse(Input[1], out nodo_C);
                    int nodo_S;
                    int.TryParse(Input[0], out nodo_S);

                    for (int j = 0; j < aListGraph.GRAPH.Count; j++)
                    {
                        if (aListGraph.GRAPH[j][j].NODO.Index == nodo_C)
                        {
                            client = aListGraph.GRAPH[j][j].NODO;
                        }
                        if (aListGraph.GRAPH[j][j].NODO.Index == nodo_S)
                        {
                            server = aListGraph.GRAPH[j][j].NODO;
                        }
                    }

                    Edge edge = new Edge(server, client);
                    edgeList.Add(edge);
                    aListGraph.addUndirectedEdge(edge);
                    Input = sr.ReadLine().Split(Delimiters, StringSplitOptions.RemoveEmptyEntries);
                }
                if (Input[0] == "D_Edges" && !sr.EndOfStream)
                {
                    Input = sr.ReadLine().Split(Delimiters, StringSplitOptions.RemoveEmptyEntries);
                }
                while (sr != null && !sr.EndOfStream && Input[0] != "C_Edges")
                {
                    Node server = new Node();
                    Node client = new Node();

                    int nodo_C;
                    int.TryParse(Input[1], out nodo_C);
                    int nodo_S;
                    int.TryParse(Input[0], out nodo_S);

                    for (int j = 0; j < aListGraph.GRAPH.Count; j++)
                    {
                        if (aListGraph.GRAPH[j][j].NODO.Index == nodo_C)
                        {
                            client = aListGraph.GRAPH[j][j].NODO;
                        }
                        if (aListGraph.GRAPH[j][j].NODO.Index == nodo_S)
                        {
                            server = aListGraph.GRAPH[j][j].NODO;
                        }
                    }

                    Edge edge = new Edge(server, client);
                    diEdgeList.Add(edge);
                    Input = sr.ReadLine().Split(Delimiters, StringSplitOptions.RemoveEmptyEntries);
                }
                if (Input[0] == "C_Edges" && !sr.EndOfStream)
                {
                    Input = sr.ReadLine().Split(Delimiters, StringSplitOptions.RemoveEmptyEntries);
                }
                while (sr != null && !sr.EndOfStream)
                {
                    Node server = new Node();

                    int nodo_S;
                    int.TryParse(Input[0], out nodo_S);

                    for (int j = 0; j < aListGraph.GRAPH.Count; j++)
                    {
                        if (aListGraph.GRAPH[j][j].NODO.Index == nodo_S)
                        {
                            server = aListGraph.GRAPH[j][j].NODO;
                        }
                    }

                    Edge edge = new Edge(server, server);
                    cicleEdgeList.Add(edge);
                    Input = sr.ReadLine().Split(Delimiters, StringSplitOptions.RemoveEmptyEntries);
                }
                sr.Close();
                statusRes = 1;
            }

            /*
            StreamReader sr = null;
            OpenFileDialog openFileDialog = new OpenFileDialog();

           

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                sr = new StreamReader(openFileDialog.FileName);
                char[] Delimiters = new char[] { ',' };
                string[] Input = sr.ReadLine().Split(Delimiters, StringSplitOptions.RemoveEmptyEntries);
                while ( !sr.EndOfStream )
                {
                    foreach (string pal in Input)
                    {
                        matrixTB.Text += pal + System.Environment.NewLine;
                    }
                    Input = sr.ReadLine().Split(Delimiters, StringSplitOptions.RemoveEmptyEntries);
                }

            }

            
            */
            return statusRes;
        }

        public void keyA_OR_MoveClick()
        {
            if (f3.Operation == 1)
            {
                aListGraph.allBlack();
                Invalidate();
                f3.Operation = 0;
            }

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
            InvalidatePlus(1);
        }


        public void keyX_OR_RemoveClick()
        {
            if (f3.Operation == 1)
            {
                aListGraph.allBlack();
                Invalidate();
                f3.Operation = 0;
            }
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
            InvalidatePlus(1);
        }

        public void keyF_OR_MoRe()
        {
            if (f3.Operation == 1)
            {
                aListGraph.allBlack();
                Invalidate();
                f3.Operation = 0;
            }
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
            InvalidatePlus(1);
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
                aListGraph.eliminateNexetEdges(selected);
                eliminateNexetDirectedEdges(selected);
                eliminateCicledEdges(selected);

                aListGraph.removeNode(selected);
                nodeList.Remove(selected);
                selected = null;
                anyNodeSelected = false;
                indexCount--;
            }
            InvalidatePlus(1);
        }

        public void create(Coordenate cor)
        {
            Coordenate newNodePosition = new Coordenate(cor.X, cor.Y);
            Node newNode;
            if (allMoRe)
            {
                newNode = new Node(newNodePosition, generalRadius, aListGraph.GRAPH.Count(), this.uniqueID(), Color.Indigo);
            }
            else
            {
                newNode = new Node(newNodePosition, generalRadius, aListGraph.GRAPH.Count(), this.uniqueID());
            }
            indexCount++;
            nodeList.Add(newNode);
            aListGraph.addNode(newNode);
            InvalidatePlus(1);
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
                foreach (int num in IDList)//ID list should be a tree so the time-complexity to compruebe the exixtence of the random number generated could decresse
                {
                    if (res == num)
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
        public void changeIsomtextBox(String str)
        {
            IsomtextBox.Text = "Isomorfismo : ";
            IsomtextBox.Text += System.Environment.NewLine;
            IsomtextBox.Text += str;
        }

        /*******************************************
         * 
         * 
         *  ////////////////   paths and cycles.(caminos y circuitos).
         *      
         * 
         * **********************************************/

        AdjacencyList aux;
        List<Edge> cutEdges;

        List<Node> estimadedIniFinNodes;

        List<Node> pathOfNodes;
        List<Node> workingNodes;
        pathsOK f3 = new pathsOK();

        #region Euler

        #region cycleEuler

        public void cycleOfEuler()//next to the event.
        {
            if (!cycleOfEulerBool())
            {
                //deploy a OK form to finish.
                MessageBox.Show("no hay circuito de Euler");
                aListGraph.allBlack();
                Invalidate();
            }
            else//a trabajar
            {
                // do the animation of the path or cycle
                    cycleOfEuler_Algorithm();
                if (pathToAnimate == null)
                {
                    //deploy a OK form to finish.
                    MessageBox.Show("no hay circuito de Euler");
                    aListGraph.allBlack();
                    Invalidate();
                }
                else
                {
                    String forma3Mensaje = "";

                    foreach (Node node in pathOfNodes)
                    {
                        forma3Mensaje = forma3Mensaje + node.Index + "->";
                    }
                    tmpCount = 0;
                    f3.changeMesagge(forma3Mensaje);
                    timerColor.Start();
                    f3.ShowDialog();
                    //MessageBox.Show(forma3Mensaje);
                }

            }
        }
        public Boolean cycleOfEulerBool()//to determine if the graph has a Eulerian cycle.
        {
            // An undirected graph has Eulerian cycle if following two conditions are true.
            //….a) All vertices with non-zero degree are connected.We don’t care about 
            //vertices with zero degree because they don’t belong to Eulerian Cycle or Path(we only consider all edges).
            //….b) All vertices have even degree.
            Boolean res = true;
           // aux = new AdjacencyList();
            workingNodes = new List<Node>();

            foreach (Node node in nodeList)
            {
                int degreeByN = aListGraph.neighborListNode(node).Count();

                if (degreeByN > 0)//atleast one neightboor
                {
                    if (degreeByN % 2 != 0)// one node have not an even number of neirhtbors
                    {
                        return false;
                    }
                    //aux.addNode(node);
                    workingNodes.Add(node);
                }
            }

            //if (aux.LIST_NODES.Count() > 0)
            if (workingNodes.Count > 0)
            {
                if (!allConected(workingNodes))// if not all are connected
                {
                    return false;
                }
            }
            return res;
        }

       
        public List<Edge> cycleOfEuler_Algorithm()//algoritmos
        {
            List<Edge> res = new List<Edge>();
            List<Edge> edgeListInside = new List<Edge>();
            edgeListInside = edgeList;
            pathOfNodes = new List<Node>();
            pathToAnimate = new List<Edge>();

            cutEdges = new List<Edge>();

            aListGraph.markAllLikeNotBridge();
            aListGraph.markAllLikeNotVisited(1);
           

            foreach (Edge edge in aListGraph.listOfEdges_IG)
            {
                if (isABridgeBool(edge))
                {
                    cutEdges.Add(edge);
                }
            }

            // Mark all the vertices as not visited 
            aListGraph.markAllLikeNotVisited();

            // Start DFS traversal from a vertex with non-zero degree 

            pathOfNodes.Add(initialNodePath);
            DFSEulerCycle(initialNodePath);
            //pathOfNodes.Add(finalNodePath);
            return res;
        }
        void DFSEulerCycle(Node workingNode/*int v, bool visited[]*/)
        {
            // Mark the current node as visited
           // pathOfNodes.Add(workingNode);
            //workingNode.Visitado = true;

            List<Node> dejaAlFinal = new List<Node>();

            foreach (Node node in aListGraph.neighborListNode(workingNode))
            {
                //if(node.Visitado == false)
                //{
               Edge edge= aListGraph.thisEdge(workingNode,node);
                   // foreach (Edge edge in aListGraph.listOfEdges_IG)// edgeList)
                   // {
                    if (edge.visitada == false )
                        {
                            if (isABridgeVisitedsBool(edge,aListGraph))//cutEdges.Contains(edge))//||edge.Bridge == true)
                            {
                                dejaAlFinal.Add(node);
                            }
                            else
                            {
                                if (node != finalNodePath)
                                {
                                    edge.visitada = true;
                                    pathToAnimate.Add(edge);
                                    pathOfNodes.Add(node);
                                    DFSEulerCycle(node);
                                }
                            }

                        }
                   // }
                //}
            }

            foreach (Node node in dejaAlFinal)
            {
               // if (node.Visitado == false)
                //{
                    foreach (Edge edge in aListGraph.listOfEdges_IG)
                    {
                        if (edge.isThisUndirected(workingNode, node) && edge.visitada == false)
                        {
                            edge.visitada = true;
                            pathToAnimate.Add(edge);
                            pathOfNodes.Add(node);
                            break;

                        }
                    }
                    DFSEulerCycle(node);
                //}
            }

            if (aListGraph.neighborListNode(workingNode).Contains(finalNodePath))
            {
                Edge edgeFinal = aListGraph.thisEdge(workingNode,finalNodePath);

                if (aListGraph.allVisitedExept(edgeFinal) && edgeFinal.visitada == false)
                {
                    edgeFinal.visitada = true;
                    pathToAnimate.Add(edgeFinal);
                    pathOfNodes.Add(finalNodePath);
                }
            }

        }

        #endregion

        #region pathEuler
        public void pathOfEuler()//next to the event
        {
            if (!pathOfEulerBool())
            {
                //deploy a OK form to finish.
                MessageBox.Show("no hay camino de Euler");
                aListGraph.allBlack();
                Invalidate();
            }
            else//a trabajar
            {
                if (aListGraph.neighborListNode(initialNodePath).Count() % 2 == 0 
                 || aListGraph.neighborListNode(finalNodePath).Count() % 2 == 0)
                {
                    if (estimadedIniFinNodes.Count()>1)
                    {
                        MessageBox.Show("Existe un camino de Euler pero no el sugerido, intenta con " + estimadedIniFinNodes[0].Index + ","+  estimadedIniFinNodes[1].Index);
                        aListGraph.allBlack();
                        Invalidate();
                    }
                    else
                    {
                        MessageBox.Show("No existe el camino de Euler");
                        aListGraph.allBlack();
                        Invalidate();
                    }
                }
                else
                {                
                    pathOfEuler_Algorithm();
                    String forma3Mensaje = "";

                    foreach (Node node in pathOfNodes)
                    {
                        forma3Mensaje = forma3Mensaje + node.Index + "->";
                    }
                    tmpCount = 0;
                    f3.changeMesagge(forma3Mensaje);
                    timerColor.Start();
                    f3.ShowDialog();
                }
            }
        }
        public List<Edge> pathOfEuler_Algorithm()//algorithm
        {
            List<Edge> res = new List<Edge>();
            List<Edge> edgeListInside = new List<Edge>();
            edgeListInside = edgeList;
            pathOfNodes = new List<Node>();
            pathToAnimate = new List<Edge>();

            cutEdges = new List<Edge>();

            aListGraph.markAllLikeNotBridge();
            aListGraph.markAllLikeNotVisited(1);


            foreach (Edge edge in aListGraph.listOfEdges_IG)
            {
                if (isABridgeBool(edge))
                {
                    cutEdges.Add(edge);
                }
            }

            // Mark all the vertices as not visited 
            aListGraph.markAllLikeNotVisited();

            // Start DFS traversal from a vertex with non-zero degree 

            pathOfNodes.Add(initialNodePath);
            DFSEulerCycle(initialNodePath);
            //pathOfNodes.Add(finalNodePath);
            return res;

        }

        public Boolean pathOfEulerBool()
        {
            bool res = true;
            aux = new AdjacencyList();
            estimadedIniFinNodes = new List<Node>();
            int oddDegreeCont = 0;

            foreach (Node node in nodeList)
            {
                int degreeByN = aListGraph.neighborListNode(node).Count();

                if (degreeByN > 0)//atleast one neightboor
                {
                    if (degreeByN % 2 != 0)// the node have not an even number of neightbors
                    {
                        oddDegreeCont++;
                        estimadedIniFinNodes.Add(node);
                    }
                    aux.addNode(node);
                }
            }

            if (aux.LIST_NODES.Count() > 0)
            {
                if (oddDegreeCont != 2)
                {
                    return false;
                }

                /*if (!allConected())// if not all are connected
                {
                    return false;
                }
                */
            }
            return res;
        }
        #endregion
        #endregion

        #region Hamilton
        //-------------------------------- Hamilton--------------------
        public void cycleOfHamilton()
        {
            if (!cycleOfHamiltonBool())
            {
                MessageBox.Show("no hay ciclos de hamilton");
                aListGraph.allBlack();
                Invalidate();
            }
            else//a trabajar
            {
                if (cycleOfHamilton_Algorithm())
                {
                    String forma3Mensaje = "";

                    foreach (Node node in pathOfNodes)
                    {
                        forma3Mensaje = forma3Mensaje + node.Index + "->";
                    }
                    tmpCount = 0;
                    f3.changeMesagge(forma3Mensaje);
                    timerColor.Start();
                    f3.ShowDialog();
                }
                else
                {
                    MessageBox.Show("no existe el ciclo de hamilton especificado");
                    aListGraph.allBlack();
                    Invalidate();
                }
                   
            }
        }

        public Boolean cycleOfHamiltonBool()
        {

            Boolean res = true;

            //can not have a disconnected node
            if (!allConected(aListGraph))
            {
                return false;
            }
            //not cut vertices
            foreach (Edge edge in aListGraph.listOfEdges_IG)
            {
                if (isABridgeBool(edge))//if any edge is a bridge it return false to hamilton cycle.
                {
                    return false;
                }
            }

            foreach (Node node in aListGraph.LIST_NODES)
            {
                if (aListGraph.isACutNodeBool(node))//if any node is a cut node return false to hamilton cycle.
                {
                    return false;
                }
                if (aListGraph.neighborListNode(node).Count() < 2)
                {
                    return false;
                }
            }

            return res;
        }

        public Boolean cycleOfHamilton_Algorithm()
        {  
           
            pathOfNodes = new List<Node>();
            pathToAnimate = new List<Edge>();
            cutEdges = new List<Edge>();

            aListGraph.markAllLikeNotBridge();
            aListGraph.markAllLikeNotVisited(1);


            foreach (Edge edge in aListGraph.listOfEdges_IG)
            {
                if (isABridgeBool(edge))
                {
                    cutEdges.Add(edge);
                }
            }

            // Mark all the vertices as not visited 

            // Start DFS traversal from a vertex with non-zero degree 
            //return DFSHamiltonCycle(initialNodePath);
 
            aListGraph.markAllNodeAndEdgesNotVisited();//marcar todos los nodos y aristas como no visitados.
           
            return DFS_Any_HamiltonCycle(initialNodePath);
        }

        List<Node> nodesPath = new List<Node>();
Boolean DFS_Any_HamiltonCycle(Node workingNode)//recursive function.
        {
            workingNode.Visitado= true;//marcar el nodo actual como visitado.
            List<Node> notVisitedYet = aListGraph.notVisitedList();//nodos sin visitar para restauraciones.
            List<Node> neightboors = aListGraph.neighborListNode(workingNode);//vecinos del nodo actual.

            /*********************
             *       Caso Base. 
             * *********************/
            if (notVisitedYet.Count() < 1 && neightboors.Contains(initialNodePath))//todos los nodos visitados && el nodo actual tiene de vecino al nodo inicial
            {
                Edge edge = aListGraph.thisEdge(workingNode, initialNodePath);
                pathToAnimate.Add(edge);//agrega la arista( actual->inicial) al camino para animar
                pathOfNodes.Add(initialNodePath);//se agrega por primera vez el nodoInicial(mismo que nodoFinal) al camino de nodos;
                pathOfNodes.Add(workingNode);//agrega el nodo actual al camino de nodos 
                return true;
            }

            //acomodar los vecinos de menor a mayor en cuestion de grado.
            neightboors.Sort(delegate (Node x, Node y)
            {
                return aListGraph.neighborListNodeNoVisited(x).Count().CompareTo(aListGraph.neighborListNodeNoVisited(y).Count());
            });


            /*********************
             *       Caso General. 
             * *********************/
            foreach (Node node in neightboors)
            {
                if (node.Visitado == false)
                {
                    if (DFS_Any_HamiltonCycle(node))//si el nodo vecino retorna un ciclo
                    {

                        // nodesPath.Add(workingNode);
                        Edge edge = aListGraph.thisEdge(workingNode, node);
                        pathOfNodes.Add(workingNode);
                        pathToAnimate.Add(edge);
                        return true;
                    }
                    else// si se retorna false se restauran los nodos de la lista de restaturacion(notVisitedYet)
                        aListGraph.restoreNotVisited(notVisitedYet);//restaturacion.
                }

            }

            //no se encontro nigun ciclo.
            return false;
        }//DFS_Any_HamiltonCycle(END).







       


 Boolean DFSHamiltonCycle(Node workingNode/*int v, bool visited[]*/)
        {
            // Mark the current node as visited
            // pathOfNodes.Add(workingNode);
            workingNode.Visitado = true;

            List<Node> dejaAlFinal = new List<Node>();
            List<Node> dejarAlMedio = new List<Node>();

            List<Node> neightboors = aListGraph.neighborListNode(workingNode);
            neightboors.Sort(delegate(Node x, Node y)
            {
               return aListGraph.neighborListNodeNoVisited(x).Count().CompareTo(aListGraph.neighborListNodeNoVisited(y).Count());
            });

            Boolean inmovilizado = true ;//when all eas visited already
            

            foreach (Node node in neightboors)
            {
                if (node.Visitado == false)
                {
                    inmovilizado = false;
                    Edge edge = aListGraph.thisEdge(workingNode, node);
                    if (edge.visitada == false)
                    {
                        if (isABridgeVisitedsBool(edge, aListGraph))//cutEdges.Contains(edge))//||edge.Bridge == true)
                        {
                            
                            dejaAlFinal.Add(node);
                        }
                        else
                        {

                            if (node != finalNodePath)
                            {
                                edge.visitada = true;
                                pathToAnimate.Add(edge);
                                pathOfNodes.Add(node);
                                return DFSHamiltonCycle(node);
                            }

                        }
                    }
                
                }
            }

            foreach (Node node in dejaAlFinal)
            {
                 if (node.Visitado == false)
                {
                        Edge edge = aListGraph.thisEdge(workingNode, node);
                    if (edge.visitada == false)                    
                    {
                            edge.visitada = true;
                            pathToAnimate.Add(edge);
                            pathOfNodes.Add(node);
                            return DFSHamiltonCycle(node);                                             
                    }
                
                }
            }

            if (neightboors.Contains(finalNodePath))
            {
                Edge edgeFinal = aListGraph.thisEdge(workingNode, finalNodePath);

                if (aListGraph.allNodesVisitedBool() )//&& edgeFinal.visitada == false)
                {
                    edgeFinal.visitada = true;                    
                    pathToAnimate.Add(edgeFinal);
                    pathOfNodes.Add(finalNodePath);
                    return true;
                }
            }
            return false;

        }

        public void pathOfHamilton()
        {
            if (!pathOfHamiltonBool())
            {
                //deploy a OK form to finish.
                MessageBox.Show("no hay path of hamilton");
                aListGraph.allBlack();
                Invalidate();
            }
            else//a trabajar
            {
                if (pathOfHamilton_Algorithm())
                {
                    String forma3Mensaje = "";

                    foreach (Node node in pathOfNodes)
                    {
                        forma3Mensaje = forma3Mensaje + node.Index + "->";
                    }
                    tmpCount = 0;
                    f3.changeMesagge(forma3Mensaje);
                    timerColor.Start();
                    f3.ShowDialog();
                }
                else
                {
                    MessageBox.Show("no existe el camino de hamilton especificado");
                }
            }
        }



        int DFS_Any_HamiltonCycleOrPath(Node workingNode)//recursive function.
        {
            workingNode.Visitado = true;
            List<Node> notVisitedYet = aListGraph.notVisitedList();
            List<Node> neightboors = aListGraph.neighborListNode(workingNode);
            if (notVisitedYet.Count() < 1)// si todos los nodos han sido visitados
            {
                if (neightboors.Contains(initialNodePath) && initialNodePath == finalNodePath)// si cumple el ciclo y se busca el ciclo.
                {
                    Edge edge = aListGraph.thisEdge(workingNode, initialNodePath);
                    pathToAnimate.Add(edge);
                    pathOfNodes.Add(initialNodePath);
                    pathOfNodes.Add(workingNode);
                    return 2;
                }
                else
                {
                    pathOfNodes.Add(workingNode);
                    return 1;
                }

            }
            else if (notVisitedYet.Count() == 1 && neightboors.Contains(finalNodePath) && notVisitedYet.Contains(finalNodePath))
            {
                int res = DFS_Any_HamiltonCycleOrPath(finalNodePath);
                if (res > 0)
                {
                    Edge edge = aListGraph.thisEdge(workingNode, finalNodePath);
                    pathOfNodes.Add(workingNode);
                    pathToAnimate.Add(edge);
                    return res;
                }
            }


            neightboors.Sort(delegate (Node x, Node y)
            {
                return aListGraph.neighborListNodeNoVisited(x).Count().CompareTo(aListGraph.neighborListNodeNoVisited(y).Count());
            });

            foreach (Node node in neightboors)
            {
                if (node.Visitado == false && node != finalNodePath)
                {
                    int res = DFS_Any_HamiltonCycleOrPath(finalNodePath);
                    if (res > 0)
                    {

                        // nodesPath.Add(workingNode);
                        Edge edge = aListGraph.thisEdge(workingNode, node);
                        pathOfNodes.Add(workingNode);
                        pathToAnimate.Add(edge);
                        return res;
                    }
                    else
                        aListGraph.restoreNotVisited(notVisitedYet);
                }

            }

            return 0;
        }//DFS_Any_HamiltonCycle(END).




        public Boolean pathOfHamiltonBool()
        {
            Boolean res = false;
            //can not have a disconnected node
            if (!allConected(aListGraph))
            {
                return false;
            }

            return true;
        }


        public Boolean pathOfHamilton_Algorithm()
        {
            pathOfNodes = new List<Node>();
            pathToAnimate = new List<Edge>();
            cutEdges = new List<Edge>();

            aListGraph.markAllLikeNotBridge();
            aListGraph.markAllLikeNotVisited(1);


            foreach (Edge edge in aListGraph.listOfEdges_IG)
            {
                if (isABridgeBool(edge))
                {
                    cutEdges.Add(edge);
                }
            }

            // Mark all the vertices as not visited 

            // Start DFS traversal from a vertex with non-zero degree 

            pathOfNodes.Add(initialNodePath);
            aListGraph.markAllNodeAndEdgesNotVisited();
            //return DFSHamiltonPath(initialNodePath);
            if(DFS_Any_HamiltonCycleOrPath(initialNodePath)>0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        Boolean DFSHamiltonPath(Node workingNode)
        {
            // Mark the current node as visited
            // pathOfNodes.Add(workingNode);
            workingNode.Visitado = true;

            List<Node> dejaAlFinal = new List<Node>();
            List<Node> dejarAlMedio = new List<Node>();

            List<Node> neightboors = aListGraph.neighborListNode(workingNode);
            neightboors.Sort(delegate(Node x, Node y)
            {
               return aListGraph.neighborListNode(x).Count().CompareTo(aListGraph.neighborListNode(y).Count());
            });

            Boolean inmovilizado = true;//when all eas visited already


            if (workingNode == finalNodePath && aListGraph.allNodesVisitedBool())//&& edgeFinal.visitada == false)
            {               
                //pathOfNodes.Add(finalNodePath);
                return true;
            }
            else
            {

                foreach (Node node in neightboors)
                {
                    if (node.Visitado == false)
                    {
                        inmovilizado = false;
                        Edge edge = aListGraph.thisEdge(workingNode, node);
                        if (edge.visitada == false)
                        {
                            if (isABridgeVisitedsBool(edge, aListGraph))//cutEdges.Contains(edge))//||edge.Bridge == true)
                            {

                                dejaAlFinal.Add(node);
                            }
                            else
                            {

                                if (node != finalNodePath)
                                {
                                    edge.visitada = true;
                                    pathToAnimate.Add(edge);
                                    pathOfNodes.Add(node);
                                    return DFSHamiltonPath(node);
                                }

                            }
                        }

                    }
                }

                foreach (Node node in dejaAlFinal)
                {
                    if (node.Visitado == false)
                    {
                        Edge edge = aListGraph.thisEdge(workingNode, node);
                        if (edge.visitada == false)
                        {
                            edge.visitada = true;
                            pathToAnimate.Add(edge);
                            pathOfNodes.Add(node);
                            return DFSHamiltonPath(node);
                        }

                    }
                }
            }

            return false;
        }

        #endregion

        /**************************************
         * 
         * 
         * Bool to comproove .
         * 
         * **********************************/

        public Boolean allConected(List<Node> nodeList)
        {
            // Mark all the vertices as not visited 
            aListGraph.markAllLikeNotVisited();

            // Start DFS traversal from a vertex with non-zero degree 
            DFSUtilAllConected(nodeList[0]);

            // Check if all non-zero degree vertices are visited 
            foreach (Node node in nodeList)
            {
                if (node.Visitado == false)
                {
                    return false;
                }
            }

            return true;
        }

        public Boolean allConected(AdjacencyList graph)
        {
            // Mark all the vertices as not visited 
            graph.markAllLikeNotVisited();

            // Start DFS traversal from a vertex with non-zero degree 
            DFSUtilAllConected(graph.LIST_NODES[0]);

            // Check if all non-zero degree vertices are visited 
            foreach (Node node in graph.LIST_NODES)
            {
                if (node.Visitado == false)
                {
                    return false;
                }
            }

            return true;
        }

        void DFSUtilAllConected(Node workingNode/*int v, bool visited[]*/)
        {
            // Mark the current node as visited
            workingNode.Visitado = true;


            // Recur for all the vertices adjacent to this vertex
            foreach (Node node in aListGraph.neighborListNode(workingNode))
            {
                if (node.Visitado == false)
                {
                    DFSUtilAllConected(node);
                }
            }
        }

        public Boolean isABridgeBool(Edge posibleBridge)
        {
            // Mark all the vertices as not visited 
            aListGraph.markAllLikeNotVisited();

            // Start DFS traversal from a vertex with non-zero degree 
            //DFSUtilAllConectedBridge(aux.LIST_NODES[0], posibleBridge);
            DFSUtilAllConectedBridge(aListGraph.LIST_NODES[0], posibleBridge);

            // Check if all non-zero degree vertices are visited 
            //foreach (Node node in aux.LIST_NODES)
            foreach (Node node in aListGraph.LIST_NODES)
            {
                if (node.Visitado == false)
                {
                    //posibleBridge.COLOR = Color.Gold;
                    posibleBridge.Bridge = true;                    
                    return true;
                }
            }

            posibleBridge.Bridge = false;
            return false;//if all vertices was visited evenif the edge was cutted.
        }

        void DFSUtilAllConectedBridge(Node workingNode, Edge posibleBridge/*int v, bool visited[]*/)
        {
            // Mark the current node as visited
            workingNode.Visitado = true;


            // Recur for all the vertices adjacent to this vertex
            foreach (Node node in aListGraph.neighborListNode(workingNode))
            {
                if (workingNode == posibleBridge.Client && node == posibleBridge.Server
                 || workingNode == posibleBridge.Server && node == posibleBridge.Client)
                {

                }
                else if (node.Visitado == false)
                {
                    DFSUtilAllConectedBridge(node, posibleBridge);
                }
            }
        }


        public Boolean isABridgeVisitedsBool(Edge posibleBridge,AdjacencyList graph)
        {
            List<int> listOfNonVisited = new List<int>();
            foreach(Node node in graph.LIST_NODES)
            {
                if(node.Visitado == false)
                {
                    listOfNonVisited.Add(node.Index);
                }
            }

            // Mark all the vertices as not visited 
            graph.markAllLikeNotVisited();

            // Start DFS traversal from a vertex with non-zero degree 
            //DFSUtilAllConectedBridge(aux.LIST_NODES[0], posibleBridge);
            DFSUtilAllConectedVisitedsBridge(graph.LIST_NODES[0], posibleBridge,graph);

            // Check if all non-zero degree vertices are visited 
            //foreach (Node node in aux.LIST_NODES)
            foreach (Node node in aListGraph.LIST_NODES)
            {
                if (node.Visitado == false)
                {
                    //posibleBridge.COLOR = Color.Gold;
                    posibleBridge.Bridge = true;


                    foreach (Node nodeG in graph.LIST_NODES)
                    {
                        if (listOfNonVisited.Contains(nodeG.Index))
                        {
                            nodeG.Visitado = false;
                        }
                        else
                        {
                            nodeG.Visitado = true;
                        }
                    }

                    return true;
                }
            }

            posibleBridge.Bridge = false;

            foreach (Node node in graph.LIST_NODES)
            {
                if (listOfNonVisited.Contains(node.Index))
                {
                    node.Visitado = false;
                }
                else
                {
                    node.Visitado = true;
                }
            }



            return false;//if all vertices was visited evenif the edge was cutted.
        }

        void DFSUtilAllConectedVisitedsBridge(Node workingNode, Edge posibleBridge,AdjacencyList graph/*int v, bool visited[]*/)
        {
            // Mark the current node as visited
            workingNode.Visitado = true;


            // Recur for all the vertices adjacent to this vertex
            foreach (Node node in graph.neighborListNode(workingNode))
            {
                if (workingNode == posibleBridge.Client && node == posibleBridge.Server
                 || workingNode == posibleBridge.Server && node == posibleBridge.Client)
                {

                }
                else if (node.Visitado == false && graph.thisEdge(workingNode, node).visitada == false)
                {
                    DFSUtilAllConectedVisitedsBridge(node, posibleBridge,graph);
                }
            }
        }



        

       

        /*************************************************************************************************************************
         * 
         * |||||||||||||||||||||||||||||||||||||||||||||||||||||   CLASSES   |||||||||||||||||||||||||||||||||||||||||||||||||||
         * 
         * ***********************************************************************************************************************/

        private void terminal_TextChanged(object sender, EventArgs e)
        {

        }

        private void maIn_Click(object sender, EventArgs e)
        {
            if (f3.Operation == 1)
            {
                aListGraph.allBlack();
                Invalidate();
                f3.Operation = 0;
            }

            if (matIn)
                matIn = false;
            else
                matIn = true;
            InvalidatePlus();
        }

        protected virtual void isoForm_Click(object sender, EventArgs e)
        {
            if (f3.Operation == 1)
            {
                aListGraph.allBlack();
                Invalidate();
                f3.Operation = 0;
            }
            if (formaIsomorfismo == null || formaIsomorfismo.Visible == false)
            {
                formaIsomorfismo = new Form2(this);
                formaIsomorfismo.Show();
            }

            fuerzaBrutaToolStripMenuItem.Visible = true;
            traspuestaToolStripMenuItem.Visible = true;
            intercambioToolStripMenuItem.Visible = true;
            IsomtextBox.Visible = true;

            //formaIsomorfismo.Show();
        }

        Boolean switcher = false;
        
        public void GraphTimerColor(object sender, EventArgs e)
        {
            if (pathOfNodes != null && tmpCount >= pathOfNodes.Count())
            { 
                timerColor.Stop();
                if(f3.Operation == 1)
                {
                    aListGraph.allBlack();
                    Invalidate();
                    f3.Operation = 0;
                }
                
            }
            else
            {
                if (pathOfNodes != null)
                {
                    if (switcher == false)
                    {
                        pathOfNodes[tmpCount].COLOR = Color.Aquamarine;
                        switcher = true;
                    }
                    else
                    {
                        int serverIndex = tmpCount + 1;
                        if (serverIndex == pathOfNodes.Count())
                        {
                            serverIndex = 0;
                        }

                        foreach (Edge edge in pathToAnimate)// edgeList)
                        {
                            if (edge.EqualsU(new Edge(pathOfNodes[tmpCount], pathOfNodes[serverIndex])))
                            {
                                edge.COLOR = Color.Red;
                                Invalidate();
                            }
                        }
                        switcher = false;
                        tmpCount++;
                    }

                }
                else
                {
                    timerColor.Stop();
                    if (f3.Operation == 1)
                    {
                        aListGraph.allBlack();
                        Invalidate();
                        f3.Operation = 0;
                    }
                }

            }
            Invalidate();
        }

        Boolean switcher2 = false;
        List<Edge> workingEdgesList;
       // List<NeightborsTreatet>

        public void GraphTimerColor2(object sender, EventArgs e)
        {
            aListGraph.markAllLikeNotVisited();
            

            
            Edge[] workingEdgesArray = new Edge[edgeList.Count()];
            edgeList.CopyTo(workingEdgesArray);
            workingEdgesList = workingEdgesArray.ToList();

            marAllEdgesAsNotVisited(workingEdgesList);

            do {
                BFSColored(initialNodePath);
            }
            while (allVisited(workingEdgesList) == true);
            Invalidate();
            timerColor.Stop();
        }

        public void BFSColored(Node node)
        {
            node.Visitado = true;
            node.COLOR = Color.Red;

            foreach(Node nodo in aListGraph.neighborListNode(node))
            {
                foreach (Edge edge in workingEdgesList)
                {
                    if (!edge.isThis(node, nodo))
                    {
                        if(edge.visitada == false)
                        {
                            edge.visitada = true;
                            edge.COLOR = Color.Green;
                            BFSColored(nodo);
                        }
                    }
                }                
                   
            }
            Invalidate();

        }

        

        public Boolean allVisited(List<Edge> listEdges)
        {
            foreach(Edge edge in listEdges)
            {
                if (edge.visitada == false)
                    return false;
            }
            return true;
        }

       

        private void Form1_Load(object sender, EventArgs e)
        {
            //pruebas pb = new pruebas();
            //pb.ShowDialog();
            timerColor = new System.Windows.Forms.Timer();
            timerColor.Interval = 500;
            timerColor.Tick += new EventHandler(GraphTimerColor/*GraphTimerColor*/);
            tmpCount = 0;
        }

        

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        //ISOMORFISMO:
        protected virtual void fuerzaBrutaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(formaIsomorfismo != null && formaIsomorfismo.Visible)
            {
                changeIsomtextBox(this.aListGraph.Isom_Fuerza_Bruta(formaIsomorfismo.aListGraph).ToString());
            }
        }

        protected virtual void traspuestaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (formaIsomorfismo != null && formaIsomorfismo.Visible)
            {
                changeIsomtextBox(this.aListGraph.Isom_Traspuesta(formaIsomorfismo.aListGraph).ToString());
            }
        }

        protected virtual void intercambioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (formaIsomorfismo != null && formaIsomorfismo.Visible)
            {
                changeIsomtextBox(this.aListGraph.Isom_Inter(formaIsomorfismo.aListGraph).ToString());
            }
        }

        String typeOfPath;
        private void eulerToolStripMenuItem_Click(object sender, EventArgs e)//make happend 
        {
            deselect();
            eulerBoolDo = true;
            hamiltonBoolDo = false;
            nodePathsReady = false;   
            
        }

        private void hamiltonToolStripMenuItem_Click(object sender, EventArgs e)//make happend
        {
            deselect();
            eulerBoolDo = false;
            hamiltonBoolDo = true;
            nodePathsReady = false;

        }

        private void caminosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (f3.Operation == 1)
            {
                aListGraph.allBlack();
                Invalidate();
                f3.Operation = 0;
            }
            deselect();
        }

        private void brToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach(Edge edge in edgeList)
            {
                isABridgeBool(edge);
            }
        }


        Boolean directLinking = false;
        Boolean undirectLinking = false;

        private void directToolStripMenuItem_Click(object sender, EventArgs e)
        {
            directLinking = !directLinking;
        }

        private void undirectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            undirectLinking = !undirectLinking;
        }
    }//Form.
}//namespace.
