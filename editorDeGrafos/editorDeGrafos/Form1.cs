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
            public Node(Coordenate position, int radius, int index, int identifier, Color color)
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
            public TidyPair(int x, int y) : base(x, y)
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

            public NodeRef(int weight, Node nodo, TidyPair tidyPair, Boolean active)
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

            public String ToString(bool paramBool)
            {
                String resString = "";
                int i = 0;
                foreach (List<NodeRef> row in graph)
                {
                    resString += "@" + i;
                    foreach (NodeRef nodoR in row)
                    {
                        if (paramBool == false)
                        {
                            resString += "\t" + "(" + i + ":" + nodoR.NODO.Index + ")= " + nodoR.W;
                        }
                        else
                        {
                            if(nodoR.W > -1)
                            resString += "\t" + 1;
                            else
                            resString += "\t" + 0;
                        }
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
                    if (graph[i][i].W > -1)
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
                res = new DirectedGrade(input, output);
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

            public Boolean UndirectedCicled()
            {
                HashSet<int> visited = new HashSet<int>();
                for (int vertex = 0; vertex < graph.Count(); vertex++)
                {
                    if (visited.Contains(vertex))
                    {
                        continue;
                    }
                    Boolean flag = dfsU(vertex, visited, -1);
                    if (flag)
                    {
                        return true;
                    }
                }
                return false;
            }

            public Boolean dfsU( int vertex, HashSet<int> visited, int parent)
            {
                visited.Add(vertex);
                foreach(NodeRef nodeR in graph[vertex])
                {
                    if (nodeR.W > -1)
                    {
                        if (nodeR.NODO.Index.Equals(parent))
                        {
                            continue;
                        }
                        if (visited.Contains(nodeR.NODO.Index))
                        {
                            return true;
                        }
                        Boolean hasCycle = dfsU(nodeR.NODO.Index, visited, vertex);
                        if (hasCycle)
                        {
                            return true;
                        }
                    }
                }
                return false;
            }

            public Boolean Cicled()
            {
                if (this.Directed())
                    return this.directedCicled();
                else
                    return this.UndirectedCicled();
            }         


            //Determine if a graph is cicled with BFS algorithm.
            //watch this video: https://www.youtube.com/watch?v=rKQaZuoUR4M 
            public Boolean directedCicled()
            {
                Boolean res = false;

                HashSet<int> whiteSet = new HashSet<int>();
                HashSet<int> graySet = new HashSet<int>();
                HashSet<int> blackSet = new HashSet<int>();

                for (int i = 0; i < graph.Count(); i++)
                {
                    whiteSet.Add(i);
                }

                while (whiteSet.Count() > 0)
                {
                   // int current = whiteSet.First();
                   int current = whiteSet.Min();
                    if (dfs(current, whiteSet, graySet, blackSet))
                    {
                        return true;
                    }
                }
                return res;
            }

            private Boolean dfs(int currentIndex, HashSet<int> whiteS, HashSet<int> grayS, HashSet<int> blackS)
            {
                //move current to gray set from white set and then explore it.
                moveVertex(currentIndex, whiteS, grayS);
                foreach (NodeRef nodeR in graph[currentIndex])
                {
                    if (nodeR.W > -1)
                    {
                        //if in black set means already explored so continue.
                        if (blackS.Contains(nodeR.NODO.Index))
                        {
                            continue;
                        }
                        //if in gray set then cycle found.
                        if (grayS.Contains(nodeR.NODO.Index))
                        {
                            return true;
                        }
                        if (dfs(nodeR.NODO.Index, whiteS, grayS, blackS))
                        {
                            return true;
                        }
                    }
                }
                //move vertex from gray set to black set when done exploring.
                moveVertex(currentIndex, grayS, blackS);
                return false;
            }

            private void moveVertex(int v_Index, HashSet<int> sourceSet, HashSet<int> destinationSet)
            {
                sourceSet.Remove(v_Index);
                destinationSet.Add(v_Index);
            }

            public Boolean Bipartita()
            {
                HashSet<int> whiteSet = new HashSet<int>();
                HashSet<int> blueSet = new HashSet<int>();
                HashSet<int> redSet = new HashSet<int>();
                HashSet<int> visited = new HashSet<int>();

                for(int i = 0; i < graph.Count(); i++)
                {
                    whiteSet.Add(i);
                }
                moveVertex(0, whiteSet, blueSet);
                visited.Add(0);
                return Bipartita2(0,visited,blueSet,redSet,whiteSet);
            }

        public Boolean Bipartita2( int origin, HashSet<int> visited, HashSet<int> originColorSet , HashSet<int> destinationColorSet, HashSet<int> whiteSet)
        {            
            foreach(NodeRef nodeR in graph[origin])
            {
                    if (nodeR.W > -1)
                    {
                       if(!visited.Contains(nodeR.NODO.Index))
                        {
                            // mark present vertic as visited 
                            visited.Add(nodeR.NODO.Index);

                            // mark its color opposite to its parent 
                            this.moveVertex(nodeR.NODO.Index,whiteSet,destinationColorSet);

                            // if the subtree rooted at vertex v is not bipartite 
                            if (Bipartita2(nodeR.NODO.Index,  visited,destinationColorSet, originColorSet, whiteSet))
                                return false;
                        }
                        else 
                        if (originColorSet.Contains(nodeR.NODO.Index) && originColorSet.Contains(origin))
                            return false;
                    }
            }
            return true;
        }
            public Boolean Bip()
            {
                if (graph.Count() > 0)
                {
                    // Create a color array to store  
                    // colors assigned to all veritces. 
                    // Vertex number is used as index  
                    // in this array. The value '-1' 
                    // of colorArr[i] is used to indicate  
                    // that no color is assigned 
                    // to vertex 'i'. The value 1 is  
                    // used to indicate first color 
                    // is assigned and value 0 indicates  
                    // second color is assigned. 
                    int[] colorArr = new int[graph.Count()];
                    for (int i = 0; i < graph.Count(); ++i)
                        colorArr[i] = -1;

                    // Assign first color to source 
                    colorArr[0] = 1;

                    // Create a queue (FIFO) of vertex numbers  
                    // and enqueue source vertex for BFS traversal 
                    List<int> q = new List<int>();
                    q.Add(0);

                    // Run while there are vertices 
                    // in queue (Similar to BFS) 
                    while (q.Count != 0)
                    {
                        // Dequeue a vertex from queue 
                        int u = q[0];
                        q.RemoveAt(0);

                        // Return false if there is a self-loop  
                        if (graph[u][u].W > -1)
                            return false;

                        // Find all non-colored adjacent vertices 
                        for (int v = 0; v < graph.Count(); ++v)
                        {
                            // An edge from u to v exists  
                            // and destination v is not colored 
                            if (graph[u][v].W > -1 && colorArr[v] == -1)
                            {
                                // Assign alternate color  
                                // to this adjacent v of u 
                                colorArr[v] = 1 - colorArr[u];
                                q.Add(v);
                            }

                            // An edge from u to v exists and  
                            // destination v is colored with 
                            // same color as u 
                            else if (graph[u][v].W > -1 &&
                                     colorArr[v] == colorArr[u])
                                return false;
                        }
                    }

                    for (int i = 0; i < graph.Count(); i++)
                    {
                        for (int j = 0; j < graph.Count(); j++)
                        {
                            if (this.Directed() == true)
                            {
                                if (this.GradeOfDirectedNode(graph[i][j].NODO).Total == 0)
                                {
                                    return false;
                                }
                            }
                            else
                            {
                                if (this.GradeOfNode(graph[i][j].NODO) == 0)
                                {
                                    return false;
                                }
                            }
                            if (graph[i][i].W > -1)
                            {
                                return false;
                            }
                        }
                    }
                    // If we reach here, then all adjacent vertices 
                    // can be colored with alternate color 
                    return true;
                }
                else
                {
                    return false;
                }
            }
         /******************************************************************************************************************
          * 
          * STARTSTARTSTARTSTARTSTARTSTARTSTARTSTARTSTARTSTARTSTARTSTARTSTARTSTARTSTARTSTARTSTARTSTARTSTARTSTARTSTARTSTARTSTART
          * 
          *                         ----- START OF ALGORITHM OF ISOMORFISM -----
          *                                    
          * STARTSTARTSTARTSTARTSTARTSTARTSTARTSTARTSTARTSTARTSTARTSTARTSTARTSTARTSTARTSTARTSTARTSTARTSTARTSTARTSTARTSTARTSTART
          * 
          ********************************************************************************************************************/    


            /********************************************************************************************
             * 
             * 
             *      ISOMORPHISM :algorithm of tranpose matrix.
             * 
             * 
             * *******************************************************************************************/
            public Boolean Isom_Traspuesta(AdjacencyList other)
            {
                //Boolean res = false;              

                if (heuristicIsom(other))
                {
                    PermutationSetStruct gradePairs;
                    gradePairs = heuristicIsom_SEC_FASE(other);

                    if (other.GRAPH.Count() < 1 && this.GRAPH.Count() < 1)//
                    {
                        return true;
                    }
                    else
                    {
                        if (this.GRAPH.Equals(other.GRAPH))//si los grafos son iguales retorna true.
                        {
                            return true;
                        }
                        else
                        {
                            if (gradePairs.validateSet())
                            {
                                return Isom_Traspuesta_Algorithm(other, gradePairs);
                            }
                        }
                    }
                }
                return false;
            }
            public Boolean Isom_Traspuesta_Algorithm(AdjacencyList other, PermutationSetStruct gradePairs)//Algorithm
            {
                int limitOfPermutations = 40000;
                Matrix thisMatrix = this.toMatrix();
                Matrix otherMatrix = other.toMatrix();

                Matrix permutationMatrix = new Matrix();
                Matrix permutationMatrixTrans = new Matrix();

                Matrix resMatrixOperation = new Matrix();
                ListOfPerPairLists listOfPerPairLists = new ListOfPerPairLists(gradePairs);
                PermutationPairList aux;

                while (limitOfPermutations > 0 && listOfPerPairLists.PER_ALFA_LIST.Count() > 0)
                {
                    //get the next permutation:
                    aux = listOfPerPairLists.PER_ALFA_LIST.ElementAt(0);
                    listOfPerPairLists.PER_ALFA_LIST.RemoveAt(0);

                    aux.toMatrixOfPermutationB(ref permutationMatrix, ref permutationMatrixTrans);//make the permutation matrix and the transpose.

                    resMatrixOperation = permutationMatrix.MatrixProduct(otherMatrix);
                    resMatrixOperation = resMatrixOperation.MatrixProduct(permutationMatrixTrans);

                    if (thisMatrix.Equals(resMatrixOperation))
                    {
                        return true;
                    }
                    limitOfPermutations--;
                }
                return false;
            }

            /********************************************************************************************
            * 
            * 
            *      ISOMORPHISM : First algorithm of isimorphism. Implemented by me... ñ_ñ 
            * 
            * 
            * *******************************************************************************************/

            public Boolean Isom_Fuerza_Bruta(AdjacencyList other)
            {
                //Boolean res = false;              
                if(heuristicIsom(other))
                {
                    PermutationSetStruct gradePairs;
                    gradePairs = heuristicIsom_SEC_FASE(other);

                    if (other.GRAPH.Count() < 1 && this.GRAPH.Count() < 1)//
                    {
                        return true;
                    }
                    else
                    {
                        if (this.GRAPH.Equals(other.GRAPH))//si los grafos son iguales retorna true.
                        {
                            return true;
                        }
                        else
                        {
                            if (gradePairs.validateSet())
                            {
                                return Isom_Fuerza_Bruta_Algorithm(other, gradePairs);
                            }
                        }
                    }
                }//END of the heuristic.
                return false;
            }

            public Boolean Isom_Fuerza_Bruta_Algorithm(AdjacencyList other, PermutationSetStruct gradePairs)
            {

                int limitOfPermutations = 40000;
                Matrix thisMatrix = this.toMatrix();
                Matrix otherMatrix = other.toMatrix();

                bool band = false;

                List<int[]> listOfPermutations = gradePairs.makeAllPermutationsPure();
                int[] permutationArray;

                while (limitOfPermutations > 0 && listOfPermutations.Count() > 0 && !band)
                {
                    //get the next permutation:
                    permutationArray = listOfPermutations.ElementAt(0);
                    listOfPermutations.RemoveAt(0);

                    band = true;
                    for (int j = 0; j < permutationArray.Length; j++)                       
                    {                       
                        for (int i = 0; i < permutationArray.Length; i++)
                        {
                            if (otherMatrix.MATRIX[j, i] != thisMatrix.MATRIX[permutationArray[j], permutationArray[i]])
                            {
                                band = false;
                                break;
                            }
                        }
                        if (!band)
                        {
                            break;
                        }
                    }
                    limitOfPermutations--;
                }//END(while).     

                return band;
            }

            /********************************************************************************************
            * 
            * 
            *      ISOMORPHISM : In the manual.
            * 
            * 
            * *******************************************************************************************/
            public Boolean Isom_Inter(AdjacencyList other)
            {
                if (heuristicIsom(other))
                {
                    PermutationSetStruct gradePairs;
                    gradePairs = heuristicIsom_SEC_FASE(other);

                    if (other.GRAPH.Count() < 1 && this.GRAPH.Count() < 1)//
                    {
                        return true;
                    }
                    else
                    {
                        if (this.GRAPH.Equals(other.GRAPH))//si los grafos son iguales retorna true.
                        {
                            return true;
                        }
                        else
                        {
                            if (gradePairs.validateSet())
                            {
                                return Isom_Inter_Algorithm();
                            }
                        }
                    }
                }//END of the heuristic.
                return false;
            }

            public Boolean Isom_Inter_Algorithm()
            {
                return false;
            }

            /******************************************************************************************************************
            * 
            * ENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDEND
            * 
            *                              ----- END OF ALGORITHM OF ISOMORFISM -----
            *                                    
            * ENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDEND
            * 
            ********************************************************************************************************************/
            private Boolean heuristicIsom(AdjacencyList other)
            {               
                if(this.GRAPH.Count() == other.GRAPH.Count())
                {                       
                    if (this.Grade() == other.Grade())
                        if (this.Directed() == other.Directed())
                            if (this.Complete() == other.Complete())
                                if (this.Pseudo() == other.Pseudo())
                                    if (this.Cicled() == other.Cicled())
                                        if (this.Bip() == other.Bip())
                                            return true;
                }
                return false;
            }

            private PermutationSetStruct heuristicIsom_SEC_FASE(AdjacencyList other)
            {
                PermutationSetStruct res = new PermutationSetStruct(other.GRAPH.Count());
                int grade_T;
                int grade_O;

                for (int i = 0; i < other.GRAPH.Count(); i++)
                {
                    grade_T = this.GradeOfNode(this.GRAPH[i][i].NODO);
                    res.addIndex_T(grade_T, this.GRAPH[i][i].NODO.Index);

                    grade_O = other.GradeOfNode(other.GRAPH[i][i].NODO);
                    res.addIndex_O(grade_O, other.GRAPH[i][i].NODO.Index);
                }
                return res;
            }

            public List<int> neighborList(List<NodeRef> row)
            {
                List<int> res = null;          
                for(int i = 0; i< row.Count(); i++)
                { 
                    if(row[i].W > -1)
                    {
                        res.Add(row[i].NODO.Index);
                    }
                }
                return res;
            }

            public List<NodeRef> neighborListRef(List<NodeRef> row)
            {
                List<NodeRef> res = new List<NodeRef>();
                for (int i = 0; i < row.Count(); i++)
                {
                    if (row[i].W > -1)
                    {
                        res.Add(row[i]);
                    }
                }
                return res;
            }

            public Matrix toMatrix()
            {
                Matrix res;
                int[,] toDoMatrix = new int[graph.Count(), graph.Count()];

                for (int j = 0; j < graph.Count(); j++)
                    for (int i = 0; i < graph.Count(); i++)                   
                    {
                        if (graph[j][i].W > -1)
                            toDoMatrix[j,i] = 1;
                        else
                            toDoMatrix[j, i] = 0;
                    }       
                res = new Matrix(toDoMatrix);
                return res;
            }
        }//AdjacencyList(END).      


        public class SpecificGradeSets
        {
            public int grade;
            public List<int> thisIndices;
            public List<int> otherIndices;

            public SpecificGradeSets(int grade)
            {
                this.grade = grade;
                thisIndices = new List<int>();
                otherIndices = new List<int>();
            }

            public void addThis(int index)
            {
                thisIndices.Add(index);
            }

            public void addOther(int index)
            {
                otherIndices.Add(index);
            }

            public int numberOf_O()
            {
                return thisIndices.Count();
            }
            public int numberOf_T()
            {
                return otherIndices.Count();
            }

            public Boolean validation()
            {
                if (numberOf_O() == numberOf_T())
                {
                    return true;
                }
                return false;
            }

            public Boolean matchPair(int T, int O)
            {
                if (thisIndices.Contains(T) && otherIndices.Contains(O))
                    return true;
                else
                    return false;
            }

            public List<int> ThisIndicesList
            {
                get { return thisIndices; }
            }
            public List<int> OtherIndicesList
            {
                get { return otherIndices; }
            }

        }//END.SpecificGradeSets

        public class PermutationSetStruct
        {
                    List<int> gradeInts;
                    List<SpecificGradeSets> grades;
                    int tamOfGraph = 1;

                    int[] innerAray; 

                    public PermutationSetStruct()
                    {
                        gradeInts = new List<int>();
                        grades = new List<SpecificGradeSets>();
                        innerAray = makeInnerArray(1);
                    }
                    public PermutationSetStruct(int tamOfGraph)
                    {
                        gradeInts = new List<int>();
                        grades = new List<SpecificGradeSets>();
                        this.tamOfGraph = tamOfGraph;
                        innerAray = makeInnerArray(tamOfGraph);
                    }
                    private int [] makeInnerArray(int tam)
                    {
                        int[] res = new int[tam];
                        for(int i = 0; i < tam; i++)
                        {
                            res[i] = i;
                        }
                        return res;
                    }

                    public void addGrade(int grade)
                    {
                        if (!gradeInts.Contains(grade))
                        {
                            gradeInts.Add(grade);
                            grades.Add(new SpecificGradeSets(grade));
                        }
                    }
                    public Boolean matchPairGrades(int T, int O)
                    {
                        Boolean res = false;
                        foreach (SpecificGradeSets sgs in grades)
                        {
                            if (sgs.matchPair(T, O))
                                return true;
                        }
                        return res;
                    }

                    public void addIndex(int grade, int index, int this_other)
                    {
                        int indexInSet;
                        if (!gradeInts.Contains(grade))
                        {
                            addGrade(grade);
                        }
                        indexInSet = gradeInts.IndexOf(grade);//the index of the grade whereyou want to add
                        if (this_other <= 0)
                        {
                            grades[indexInSet].addThis(index);
                        }
                        else
                        {
                            grades[indexInSet].addOther(index);
                        }
                    }

                    public void addIndex_T(int grade, int index)
                    {
                        int indexInSet;
                        if (!gradeInts.Contains(grade))
                        {
                            addGrade(grade);
                        }
                        indexInSet = gradeInts.IndexOf(grade);//the index of the grade whereyou want to add

                        grades[indexInSet].addThis(index);
                    }

                    public void addIndex_O(int grade, int index)
                    {
                        int indexInSet;
                        if (!gradeInts.Contains(grade))
                        {
                            addGrade(grade);
                        }
                        indexInSet = gradeInts.IndexOf(grade);//the index of the grade whereyou want to add

                        grades[indexInSet].addOther(index);
                    }

                    public Boolean validateSet()
                    {
                        Boolean res = true;
                        foreach (SpecificGradeSets spG in grades)
                        {
                            if (spG.validation() == false)
                            {
                                return false;
                            }
                        }
                        return res;
                    }

                    public List<SpecificGradeSets> GRADES
                    {
                        get { return this.grades; }
                    }

                    public int calculatePer()
                    {
                        int res = 1;

                        for (int i = 0; i < GRADES.Count(); i++)
                        {
                            int nP = GRADES[i].OtherIndicesList.Count();
                            if (nP < 3)
                            {
                                nP = nP * nP;
                            }
                            else
                            {
                                nP = factorial(nP);
                            }
                            res *= nP;
                        }
                        return res;
                    }

                    //factorial method
                    public int factorial(int integer)
                    {
                        int res = 1;
                        for (int i = 1; i <= integer; i++)
                        {
                            res += res * i;
                        }
                        return res;
                    }                

                public List<PermutationPairList> LP_PairList;
                public PermutationPairList P_PairList;
                public PermutationPair auxPerPair;

                public List<PermutationPairList> makeAllPermutations()
                {
                    LP_PairList = new List<PermutationPairList>();
                    heapPermutation(innerAray,innerAray.Length);
                    return LP_PairList;
                }

                public void storePermutation(int[] a)//store only if all the permutations match.
                {
                    P_PairList = new PermutationPairList();
                    for (int i = 0; i < a.Length; i++)
                    {
                        if (matchPairGrades(i, a[i]))
                        {
                            auxPerPair = new PermutationPair(i, a[i]);
                            P_PairList.Add(auxPerPair);
                        }
                        else
                        {
                            return;
                        }    
                    }
                    LP_PairList.Add(P_PairList);
                }

                public void heapPermutation(int[] a, int size)
                {
                    // if size becomes 1 then prints the obtained 
                    // permutation 
                    if (size == 1)
                    {
                        storePermutation(a);//store and validate the permutation made.
                    }

                    for (int i = 0; i < size; i++)
                    {
                        heapPermutation(a, size - 1);

                        // if size is odd, swap first and last 
                        // element 
                        if (size % 2 == 1)
                        {
                            int temp = a[0];
                            a[0] = a[size - 1];
                            a[size - 1] = temp;
                        }

                        // If size is even, swap ith and last 
                        // element 
                        else
                        {
                            int temp = a[i];
                            a[i] = a[size - 1];
                            a[size - 1] = temp;
                        }
                    }
                }


            //permutations but like an array.
            public List<int []> makeAllPermutationsPure()
            {
                List<int[]> res = new List<int[]>();
                heapPermutationPure(innerAray, innerAray.Length,ref res);
                return res;
            }

            public void storePermutationPure(int[] a, ref List<int[]> res)//store only if all the permutations match.
            {
                for (int i = 0; i < a.Length; i++)
                {
                    if (!matchPairGrades(i, a[i]))
                    {
                        return;
                    }                    
                }
                res.Add(a);
            }

            public void heapPermutationPure(int[] a, int size, ref List<int[]> res)
            {
                // if size becomes 1 then prints the obtained 
                // permutation 
                if (size == 1)
                {
                    storePermutationPure(a, ref res);//store and validate the permutation made.
                }

                for (int i = 0; i < size; i++)
                {
                    heapPermutationPure(a, size - 1, ref res);

                    // if size is odd, swap first and last 
                    // element 
                    if (size % 2 == 1)
                    {
                        int temp = a[0];
                        a[0] = a[size - 1];
                        a[size - 1] = temp;
                    }

                    // If size is even, swap ith and last 
                    // element 
                    else
                    {
                        int temp = a[i];
                        a[i] = a[size - 1];
                        a[size - 1] = temp;
                    }
                }
            }



        }//END. PermutationSetStruct.

        public class PermutationPair//...........................................................
        {
            public int thisInt;
            public int otherInt;
            public PermutationPair(int T, int O)
            {
                thisInt = T;
                otherInt = O;
            }
        }
        public class PermutationPairList//.......................................................
        {
                /*
                 * this is the way to do permutations.
                 * (12345)
                 *        |----------------------> very important.
                 * (54321)
                 * the above numbers are an example of permutation. we can have n! permutation if the graphs have n nodes.
                 * the way we do it here make this less than n! permutations, due to the clasifications by grade,
                 * comparing each graph.
                 * 
                 * */
                List<PermutationPair> permutationList = null;
                //PermutationSetStruct workingPairs;

                public PermutationPairList()
                {
                    permutationList = new List<PermutationPair>();
                }
                public void Add(PermutationPair pp)
                {
                    permutationList.Add(pp);
                }
                public Matrix toMatrixOfPermutation()//to convert the permutation into a matrix of permutations.
                {
                    Matrix res = null;
                    if (permutationList.Count > 0)
                    {
                        int n = permutationList.Count();
                        int[,] toDoMatrix = new int[n, n];

                        for (int i = 0; i < n; i++)
                        {
                            toDoMatrix[permutationList[i].otherInt, permutationList[i].thisInt] = 1;
                        }
                        res = new Matrix(toDoMatrix);
                    }
                    return res;
                }

                public void toMatrixOfPermutationB(ref Matrix mNormal, ref Matrix mTrans)//to convert the permutation into a matrix of permutations.
                {               
                    if (permutationList.Count > 0)
                    {
                        int n = permutationList.Count();
                        int[,] toDoMatrix = new int[n, n];
                        int[,] toDoMatrixTrans = new int[n, n];

                        for (int i = 0; i < n; i++)
                        {
                            toDoMatrix[permutationList[i].otherInt, permutationList[i].thisInt] = 1;
                            toDoMatrixTrans[permutationList[i].thisInt , permutationList[i].otherInt] = 1;
                        }
                        mNormal = new Matrix(toDoMatrix);
                        mTrans = new Matrix(toDoMatrixTrans);
                    }
                }
        }

        public class ListOfPerPairLists//............................................................
        {
            int numOfPermutationPosibilities = 0;
            List<PermutationPairList> listOfPermutationAlfa;

            public ListOfPerPairLists( PermutationSetStruct permutSetStruct)
            {
                numOfPermutationPosibilities = permutSetStruct.calculatePer();
                listOfPermutationAlfa = permutSetStruct.makeAllPermutations();
            }
            
            public List<PermutationPairList> PER_ALFA_LIST
            {
                get { return listOfPermutationAlfa; }
            }
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
            if(formaIsomorfismo == null || formaIsomorfismo.Visible == false)
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

        
    }//Form.
}//namespace.
