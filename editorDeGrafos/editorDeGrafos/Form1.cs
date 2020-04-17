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
                                        edgeList.Add(new Edge(selected, oneNode));
                                        aListGraph.addUndirectedEdge(selected, oneNode, weight);
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
                SaveChangesWindow gdc = new SaveChangesWindow();
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

            nodeList = new List<Node>();
            aListGraph = new AdjacencyList();
            diEdgeList = new List<Edge>();
            edgeList = new List<Edge>();
            cicleEdgeList = new List<Edge>();

            if (openFile() == 0)
            {
                nodeList = nodeList_BU;
                aListGraph = aListGraph_BU;
                diEdgeList = diEdgeList_BU;
                edgeList = edgeList_BU;
                cicleEdgeList = cicleEdgeList_BU;
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

            Pen penDirect = new Pen(Color.DimGray, 8);
            penDirect.StartCap = System.Drawing.Drawing2D.LineCap.RoundAnchor;
            penDirect.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;


            foreach (Edge edge in edgeList)//undirected edges.
            {
                graphics.DrawLine(pen, edge.A.X, edge.A.Y, edge.B.X, edge.B.Y);
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
            foreach (Node node in nodeList)//all about the node
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
            foreach (Edge edge in edgeList)
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
                newNode = new Node(newNodePosition, generalRadius, indexCount, this.uniqueID(), Color.Indigo);
            }
            else
            {
                newNode = new Node(newNodePosition, generalRadius, indexCount, this.uniqueID());
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


        private void terminal_TextChanged(object sender, EventArgs e)
        {

        }

        private void maIn_Click(object sender, EventArgs e)
        {
            if (matIn)
                matIn = false;
            else
                matIn = true;
            InvalidatePlus();
        }

        protected virtual void isoForm_Click(object sender, EventArgs e)
        {
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        //ISOMORFISMO:
        protected virtual void fuerzaBrutaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (formaIsomorfismo != null && formaIsomorfismo.Visible)
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

        #region Caminos

        #region Euler
        private void eulerToolStripMenuItem_Click_1(object sender, EventArgs e)
        {        
            //Euler euler;
            grafo.setGrados();
            if (!grafo.aislado())//Si no tiene un nodo aislado puede que tenga circuito o camino
            {
                #region Circuito
                this.grafo.setGrados();
                if (grafo.gradosPares())//Si todos sus nodos son de grado par Existe circuito
                {
                    /**/
                    this.recorrido = grafo.circuitoEuleriano();//Regresa una lista de strings que ya esta secuenciada el dato
                    euler = new Euler(recorrido, "Existe el circuito", relojEuler);//Inicializa el dialogo
                    euler.borra_Recorrido += new Euler.Borra_Recorrido(this.redibujaGrafo);//Detalles
                    rec = 0;//Detalles
                    bandRecorrido = false;//Detalles
                    euler.Show();//Detalles
                    this.EditorDeGrafos_Paint(this, null);//Detalles
                }
                #endregion

                #region Camino
                else
                {
                    if (grafo.nodosImpares() == 2)//Si tiene 2 nodos de grado impar tiene camino
                    {
                        if (grafo.paresParejos())
                        {

                            this.recorrido = grafo.caminoEuleriano();
                            euler = new Euler(recorrido, "Existe el Camino", relojEuler);
                            euler.borra_Recorrido += new Euler.Borra_Recorrido(this.redibujaGrafo);
                            rec = 0;
                            bandRecorrido = false;
                            euler.Show();
                            this.EditorDeGrafos_Paint(this, null);
                        }
                        else
                        {
                            MessageBox.Show("Un nodo no cumple con las condiciones para poder hacer un ciclo", "No existe");
                        }
                    }
                    else
                    {
                        MessageBox.Show("El Grafo no tiene ni camino ni circuito", "No existe");
                    }
                }
            }
            else//Si tiene un nodo aislado tiene circuito y camino
            {
                MessageBox.Show("El grafo tiene un nodo aislado, por lo tanto no existe camino ni circuito", "No existe");
            }
            #endregion
        }

        //private void euler

        #endregion

        #region Hamilton
        private void hamiltonToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        #endregion

        #endregion

        /*************************************************************************************************************************
         * 
         * |||||||||||||||||||||||||||||||||||||||||||||||||||||   CLASSES   |||||||||||||||||||||||||||||||||||||||||||||||||||
         * 
         * ***********************************************************************************************************************/

      
           

      

    }//Form.
}//namespace.
