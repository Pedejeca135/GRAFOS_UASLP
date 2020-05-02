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
    public partial class GraphForm : Form
    {
        #region GraphFormVariables
        /********************* Selected node control ***********************/
        Node selected = null;
        Node selectedJustFor_Moving = null;//for moving.
        Node selectedJustFor_Linking = null;

        /*********************  Inner flags ********************/
        Boolean mousePressed;
        Boolean justSaved = true;// -> storage saveStateAuxiliar.

        /***************** windows and Forms ******************************/
        GraphFormIsomorphic IsomorfismForm;//-> form for isomofism comparison.
        SaveChangesWindow gdc;// -> changes window.

        /************************ other variables ********************/
        public int generalRadius;
        String fileName = "";//   -> fileName.
        public Graph graph;// -> graph of the form.

        /****************** for view Operations ****************************/
        Boolean matIn = false;

        /****************************** for operations Do ******************
         * 
         * sample: Button_key_type. 
         * 
         * ************************************************************/
        Boolean Move_M_Do = false;
        Boolean MoveAll_A_Do = false;
        Boolean Remove_R_Do = false;
        Boolean MoRe_F_Do = false;
        Boolean Link_Do = false;
        Boolean Link_D_Do = false;
        Boolean Link_U_Do = false;

        /********** for linking operations ************************/
        Boolean D_linkingAnimation = false;
        Boolean U_LinkingAnimation = false;
        Edge linkingEdge = null;
        Boolean left_Linkind = false;
        Boolean right_Linking = false;


        /******************************** for ALGORITMOS EVENTS  **********************************************/
        //Dos...............................
        Boolean Isomorphism_FB_Do = false;
        Boolean Isomorphism_TS_Do = false;
        Boolean Isomorphism_IN_Do = false;
        Boolean path_Euler_Do = false;
        Boolean path_Hamilton_Do = false;
        Boolean dijkstra_Do = false;
        Boolean floyd_Do = false;
        Boolean warshall_Do = false;
        Boolean prim_Do = false;
        Boolean kruskal_Do = false;
        //Dos--------------------------------
        /****************** for Isomorphism *************************/
        Boolean isoForm;
        /****************** for paths and cicles ********************/
        List<Edge> pathToAnimate;
        Node initialNodePath = null;
        Node finalNodePath = null;
        Boolean nodePathsReady = false;
        Timer timerColor = new System.Windows.Forms.Timer();
        int timerColorOption = 0;
        int tmpCount = 0;

        /****************** for Floyd    *****************************/
        /****************** for Warshall *****************************/
        /****************** for Prim     *****************************/
        /****************** for Kruskal  *****************************/

        #endregion

        #region GraphFormConstructors

        /****************************************************************************************
         * 
         * 
         *              GraphForm constructors
         * 
         * 
         * *************************************************************************************/
        public GraphForm()
        {
            InitializeComponent();
            commonCostructor();
            isoForm = false;
            fuerzaBrutaToolStripMenuItem.Visible = false;
            traspuestaToolStripMenuItem.Visible = false;
            intercambioToolStripMenuItem.Visible = false;
            IsomtextBox.Visible = false;
        }

        public GraphForm(int equis)
        {
            InitializeComponent();
            commonCostructor();
            isoForm = true;
            fuerzaBrutaToolStripMenuItem.Visible = true;
            traspuestaToolStripMenuItem.Visible = true;
            intercambioToolStripMenuItem.Visible = true;
            IsomtextBox.Visible = true;
        }
        private void commonCostructor()// for all common variables.
        {
            generalRadius = 30;
            mousePressed = false;
            graph = new Graph();
            statusTB.Text = "Nombre :" + fileName;
            terminal.Text = "Node selected : ";
        }

        #endregion

        #region MouseEvents
        /************* tha mouse , tha f()#/&g boss*****************/
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            /*
            if (f3.Operation == 1)
            {
                graph.allBlack();
                Invalidate();
                f3.Operation = 0;
            }
            */

            mousePressed = true;

            if ((path_Euler_Do || path_Hamilton_Do) && graph.GRAPH.Count() > 1)//######## Do paths ##########
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
                        if (graph.GRAPH.Count() == 1)
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

                        if (path_Euler_Do)//le toca a euler.
                        {
                            path_Euler_Do = false;
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
                            path_Hamilton_Do = false;
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
            else if ( Move_M_Do ||  MoveAll_A_Do || Remove_R_Do || MoRe_F_Do || Link_Do || Link_D_Do || Link_U_Do)//######## Do operations ##########
            {
                selectedJustFor_Moving = findNodeClicked(new Coordenate(e.X, e.Y));
                selected = selectedJustFor_Moving;

                if (Remove_R_Do)
                {
                    eliminate();
                }
                else if (MoRe_F_Do)
                {
                    selected = selectedJustFor_Moving;
                    if (e.Button == System.Windows.Forms.MouseButtons.Right)
                    {
                        eliminate();
                    }
                    else
                    {
                        if (selected == null)
                        {
                            graph.create(new Coordenate(e.X, e.Y),generalRadius);
                            justSaved = false;
                        }
                    }
                }
                else if (Link_Do || Link_D_Do || Link_U_Do) // if doing a link and mousedown
                {
                    if(selectedJustFor_Linking == null)
                    {
                        if (selected == null)
                        {
                            Color colorToCreateNode = Color.Black;
                            if(Link_Do)
                            {
                                colorToCreateNode = Color.Purple;
                            }
                            else if(Link_D_Do)
                            {
                                colorToCreateNode = Color.Orange;
                            }
                            else if(Link_U_Do)
                            {
                                colorToCreateNode = Color.RoyalBlue;
                            }
                                    
                            graph.create(new Coordenate(e.X, e.Y),generalRadius, colorToCreateNode);
                            justSaved = false;
                        }
                        else
                        {
                            selectedJustFor_Linking = selected;
                        }
                    }
                }                
                InvalidatePlus();
            }
            else//######### Do other operations ##############
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

                                        graph.addUndirectedEdge(edge, weight);
                                    }
                                }
                                if (selected.Status == 3)//directed link
                                {
                                    //here i have to ask the weight of the link.
                                    int weight = AFWeight("Dirijido");
                                    //int weight = 0;
                                    if (weight >= 0)
                                    {
                                        graph.DIEDGE_LIST.Add(new Edge(selected, oneNode, true));
                                        graph.addDirectedEdge(selected, oneNode, weight);
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
                                        graph.addCicledEdge(selected, weight);
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
        }//Form_MouseDown(). BYE FOR THE MDF KING!!!! 

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mousePressed = false;
            }
            if (Link_Do || Link_D_Do || Link_U_Do)//Linking
            {
                Node auxMouseUperNode = findNodeClicked(new Coordenate(e.X, e.Y));
                if (auxMouseUperNode != null && selectedJustFor_Linking != null)
                {
                    if (Link_Do)
                    {
                        if (left_Linkind)//undirected
                        {
                            graph.addUndirectedEdge(selectedJustFor_Linking, auxMouseUperNode, 0);
                        }
                        else if (right_Linking)//directed
                        {
                            graph.addDirectedEdge(selectedJustFor_Linking, auxMouseUperNode, 0);
                        }
                    }
                    else if (Link_D_Do)
                    {
                        graph.addDirectedEdge(selectedJustFor_Linking, auxMouseUperNode, 0);
                    }
                    else if (Link_U_Do)
                    {
                        graph.addUndirectedEdge(selectedJustFor_Linking, auxMouseUperNode, 0);
                    }
                }
                selectedJustFor_Linking = null;
                InvalidatePlus(1);                
            }
        }


        Coordenate mouseLastPosition = null;//for last position in all moving.

        public void Form1_MouseMove(object sender, MouseEventArgs e)//for the mouse moving.
        {
            if (mousePressed == true && e.Button == MouseButtons.Right && selected != null && selected.Status == 1)//Selected: mouse moving 
            {
                selected.Position.X = e.X;
                selected.Position.Y = e.Y;
                InvalidatePlus(1);
            }
            if (mousePressed == true && Move_M_Do == true && selectedJustFor_Moving != null)//Move
            {
                selectedJustFor_Moving.Position.X = e.X;
                selectedJustFor_Moving.Position.Y = e.Y;
                InvalidatePlus(1);
            }
            if (mousePressed == true && MoveAll_A_Do == true && selectedJustFor_Moving != null)//MoveAll
            {
                if (mouseLastPosition != null)
                {
                    //Calculate delta of the mouse moving
                    int deltaX = selectedJustFor_Moving.Position.X - e.X;
                    int deltaY = selectedJustFor_Moving.Position.Y - e.Y;
                    Coordenate deltaOfCoordenate = new Coordenate(deltaX, deltaY);                

                    foreach (Node node in graph.NODE_LIST)
                    {
                            node.Position.X -= deltaOfCoordenate.X;
                            node.Position.Y -= deltaOfCoordenate.Y;
                    }                    
                }
                    mouseLastPosition = new Coordenate(e.X, e.Y);
                InvalidatePlus(1);
            }
            if (mousePressed == true && e.Button == MouseButtons.Left && MoRe_F_Do == true && selectedJustFor_Moving != null)//MoRe
            {
                selectedJustFor_Moving.Position.X = e.X;
                selectedJustFor_Moving.Position.Y = e.Y;
                InvalidatePlus(1);
            }
            if (mousePressed == true && (e.Button == MouseButtons.Left || e.Button == MouseButtons.Right ) && Link_Do == true && selectedJustFor_Linking != null)//Linking
            {
                Node auxMouseUperNode = findNodeClicked(new Coordenate(e.X,e.Y));
                Coordenate corToDraw;

                if (auxMouseUperNode != null)
                {
                    corToDraw = auxMouseUperNode.Position;
                }
                else
                {
                    corToDraw = new Coordenate(e.X, e.Y);
                }

                linkingEdge = new Edge(selectedJustFor_Linking, corToDraw);

                if (e.Button == MouseButtons.Left )//for undirected Edges.
                {
                    U_LinkingAnimation = true;
                    left_Linkind = true;
                    right_Linking = false;
                }
                else if(e.Button == MouseButtons.Right)//directed edges
                {
                    D_linkingAnimation = true;
                    left_Linkind = false;
                    right_Linking = true;
                }
                
                Invalidate();

            }
            if (mousePressed == true && (e.Button == MouseButtons.Left || e.Button == MouseButtons.Right) && Link_D_Do == true && selectedJustFor_Linking != null)//Linking D
            {
                Node auxMouseUperNode = findNodeClicked(new Coordenate(e.X, e.Y));
                Coordenate corToDraw;

                if (auxMouseUperNode != null)
                {
                    corToDraw = auxMouseUperNode.Position;
                }
                else
                {
                    corToDraw = new Coordenate(e.X, e.Y);
                }               
                    D_linkingAnimation = true;
                    linkingEdge = new Edge(selectedJustFor_Linking, corToDraw);
                    Invalidate();
            }
            if (mousePressed == true && (e.Button == MouseButtons.Left || e.Button == MouseButtons.Right) && Link_U_Do == true && selectedJustFor_Linking != null)//Linking D
            {
                Node auxMouseUperNode = findNodeClicked(new Coordenate(e.X, e.Y));
                Coordenate corToDraw;

                if (auxMouseUperNode != null)
                {
                    corToDraw = auxMouseUperNode.Position;
                }
                else
                {
                    corToDraw = new Coordenate(e.X, e.Y);
                }
                U_LinkingAnimation = true;
                linkingEdge = new Edge(selectedJustFor_Linking, corToDraw);
                Invalidate();
            }

        }

        #endregion



        /*************************************************************************************************************************
        * 
        * |||||||||||||||||||||||||||||||||||||||||||||||||||||  General EVENTS   |||||||||||||||||||||||||||||||||||||||||||||||||||
        * 
        * ***********************************************************************************************************************/
        public void closeIsoFormClicked(object sender, EventArgs e)
        {
            InvalidatePlus();
        }

        #region operation
        /********************** OPERATIONS *****************/
        #region operationEvents
        /************************ clicking an operation *****************/

        private void Move_Click(object sender, EventArgs e)
        {
            keyM_OR_MoveClick();
        }

        private void MoveAll_Click(object sender, EventArgs e)
        {
            keyA_OR_MoveAllClick();
        }

        private void Remove_Click(object sender, EventArgs e)
        {
            keyR_OR_RemoveClick();
        }

        private void MoRe_Click(object sender, EventArgs e)
        {
            keyF_OR_MoReClick();
        }

        private void linking_Click(object sender, EventArgs e)
        {
            keyL_OR_LinkingClick();
        }

        private void linking_D_Click(object sender, EventArgs e)
        {
            keyD_OR_D_LinkingClick();
        }

        private void linking_U_Click(object sender, EventArgs e)
        {
            keyU_OR_U_LinkingClick();
        }

        /************************ clicking operation keys *****************/
        private void Form1_KeyDown(object sender, KeyEventArgs e)// keys down.
        {
            if ((e.KeyCode == Keys.Escape || e.KeyCode == Keys.S) && selected != null)
            {
                deselect();
            }
            if (e.KeyCode == Keys.M)//Move (M).
            {
                keyM_OR_MoveClick();
            } 
            if (e.KeyCode == Keys.A)//Move All (A).
            {
                keyA_OR_MoveAllClick();
            }
            if (e.KeyCode == Keys.R)//Remove (R).
            {
                keyR_OR_RemoveClick();
            }
            if (e.KeyCode == Keys.F)//MoRe (F). 
            {
                keyF_OR_MoReClick();
            }
            if (e.KeyCode == Keys.L)//Linking (L).
            {
                keyL_OR_LinkingClick();
            }
            if (e.KeyCode == Keys.D)//Linking (D).
            {
                keyD_OR_D_LinkingClick();
            }
            if (e.KeyCode == Keys.U)//Linking (U)
            {
                keyU_OR_U_LinkingClick();
            }
            if (e.KeyCode == Keys.X && selected != null)
            {
                eliminate();
                InvalidatePlus();
            }

        }

        #endregion
        #region commonKeyOperations
        /********************* common key-operations (Begin) ****************************/
        private void keyM_OR_MoveClick()//
            {
               /* if (f3.Operation == 1)
                {
                    graph.allBlack();
                    Invalidate();
                    f3.Operation = 0;
                }
                */
                
                deselect();               

                if (Move_M_Do)
                {
                    foreach (Node node in graph.NODE_LIST)
                    {
                        node.COLOR = Color.Black;
                    }
                    Move_M_Do = (!Move_M_Do);
                }
                else
                {
                      allOperationOff();                
                      foreach (Node node in graph.NODE_LIST)
                      {
                        node.COLOR = Color.Green; 
                      }
                        Move_M_Do = (!Move_M_Do);                    
                }
                InvalidatePlus(1);
            }

            private void keyA_OR_MoveAllClick()
            {
                    deselect();                

                if (MoveAll_A_Do)
                {
                    foreach (Node node in graph.NODE_LIST)
                    {
                        node.COLOR = Color.Black;
                    }
                    MoveAll_A_Do = (!MoveAll_A_Do);
                }
                else
                {
                        allOperationOff();
                        foreach (Node node in graph.NODE_LIST)
                        {
                            node.COLOR = Color.LimeGreen;
                        }
                        MoveAll_A_Do = (!MoveAll_A_Do);                    
                }
                InvalidatePlus(1);
            }

            private void keyR_OR_RemoveClick()
            {

               deselect();              

                if (Remove_R_Do)
                {
                    foreach (Node node in graph.NODE_LIST)
                    {
                        node.COLOR = Color.Black;
                    }
                    Remove_R_Do = (!Remove_R_Do);
                }
                else
                {
                        allOperationOff();
                        foreach (Node node in graph.NODE_LIST)
                        {
                            node.COLOR = Color.Red;
                        }
                        Remove_R_Do = (!Remove_R_Do);                    
                }

                InvalidatePlus(1);
            }
            private void keyF_OR_MoReClick()
            {
                deselect();                

                if (MoRe_F_Do)
                {
                    foreach (Node node in graph.NODE_LIST)
                    {
                        node.COLOR = Color.Black;
                    }
                    MoRe_F_Do = (!MoRe_F_Do);
                }
                else
                {
                        allOperationOff();
                        foreach (Node node in graph.NODE_LIST)
                        {
                            node.COLOR = Color.Indigo;
                        }
                        MoRe_F_Do = (!MoRe_F_Do);                    
                }

                InvalidatePlus(1);
            }
            private void keyL_OR_LinkingClick()
            {
                deselect();

                if (Link_Do)
                {
                    foreach (Node node in graph.NODE_LIST)
                    {
                        node.COLOR = Color.Black;
                    }
                    Link_Do = (!Link_Do);
                }
                else
                {
                    allOperationOff();
                    foreach (Node node in graph.NODE_LIST)
                    {
                        node.COLOR = Color.Purple;
                    }
                    Link_Do = (!Link_Do);
                }
                InvalidatePlus(1);
            }

            private void keyD_OR_D_LinkingClick()
            {
                deselect();

                if (Link_D_Do)
                {
                    foreach (Node node in graph.NODE_LIST)
                    {
                        node.COLOR = Color.Black;
                    }
                    Link_D_Do = (!Link_D_Do);
                }
                else
                {
                    allOperationOff();
                    foreach (Node node in graph.NODE_LIST)
                    {
                        node.COLOR = Color.Orange;
                    }
                    Link_D_Do = (!Link_D_Do);
                }

                InvalidatePlus(1);
            }

            private void keyU_OR_U_LinkingClick()
            {
                deselect();

                if (Link_U_Do)
                {
                    foreach (Node node in graph.NODE_LIST)
                    {
                        node.COLOR = Color.Black;
                    }
                    Link_U_Do = (!Link_U_Do);
                }
                else
                {
                    allOperationOff();
                    foreach (Node node in graph.NODE_LIST)
                    {
                        node.COLOR = Color.RoyalBlue;
                    }
                    Link_U_Do = (!Link_U_Do);
                }

                InvalidatePlus(1);
            }


            /**************** deselect Operations ***************/
            private void allOperationOff()
            {
                Move_M_Do = false;
                MoveAll_A_Do = false;
                Remove_R_Do = false;
                MoRe_F_Do = false;
                Link_Do = false;
                Link_D_Do = false;
                Link_U_Do = false;
                mousePressed = false;
            }

        /********************* common key-operations (END) ****************************/
        #endregion
        #endregion

        #region algorithmsEvents
        /*************************************************************************************************
        * 
        * 
        *||||||||||||||||||||||||||||||||  ALGORITMOS EVENTS (Begin)|||||||||||||||||||||||||||||||||||
        *          
        * 
        * ************************************************************************************************/


        //ISOMORFISMO:
        protected virtual void fuerzaBrutaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsomorfismForm != null && IsomorfismForm.Visible)
            {
                changeIsomtextBox(this.graph.Isom_Fuerza_Bruta(IsomorfismForm.graph).ToString());
            }
        }

        protected virtual void traspuestaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsomorfismForm != null && IsomorfismForm.Visible)
            {
                changeIsomtextBox(this.graph.Isom_Traspuesta(IsomorfismForm.graph).ToString());
            }
        }

        protected virtual void intercambioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsomorfismForm != null && IsomorfismForm.Visible)
            {
                changeIsomtextBox(this.graph.Isom_Inter(IsomorfismForm.graph).ToString());
            }
        }


        //CAMINOS:
        private void eulerToolStripMenuItem_Click(object sender, EventArgs e)//make happend 
        {
            deselect();
            path_Euler_Do = true;
            path_Hamilton_Do = false;
            nodePathsReady = false;
        }

        private void hamiltonToolStripMenuItem_Click(object sender, EventArgs e)//make happend
        {
            deselect();
            path_Hamilton_Do = false;
            path_Euler_Do = true;
            nodePathsReady = false;
        }


        //DIJKSTRA:
        private void dijkstraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dijkstra_Do = !dijkstra_Do;
        }

        //PRIM:
        private void primToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        //KRUSKAL:
        private void kruskalToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        /***************||||||||||||||  ALGORITMOS EVENTS (END) |||||||||||||||||||||||*******************/
        #endregion

        #region viewEvents
        /*********************  View (Begin) **********************/
        private void maIn_Click(object sender, EventArgs e)
        {
            if (f3.Operation == 1)
            {
                graph.allBlack();
                Invalidate();
                f3.Operation = 0;
            }

            if (matIn)
                matIn = false;
            else
                matIn = true;
            InvalidatePlus();
        }
        #endregion

        #region fileOperations

        /*********************** file Operations (Begin) ********************************/

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
                    foreach (Node node in graph.NODE_LIST)
                    {
                        graph.eliminateNexetEdges(node);
                    }
                    //graph.NODE_LIST = new List<Node>();
                    graph = new Graph();
                    justSaved = true;
                }
            }
            Invalidate();
        }

        // when load a graph we need to regenerate all the parts of the graph, 
        //if it is no possible to load, the values are retored.
        private void loadCommonActions()
        {
            Graph graph_BU = graph;
            List<Node> nodeList_BU = graph.NODE_LIST;
            List<Edge> edgeList_BU = graph.EDGE_LIST;
            List<Edge> diEdgeList_BU = graph.DIEDGE_LIST;
            List<Edge> cicleEdgeList_BU = graph.CIEDGE_LIST;

            graph = new Graph();
            graph.NODE_LIST = new List<Node>();
            graph.EDGE_LIST = new List<Edge>();
            graph.DIEDGE_LIST = new List<Edge>();
            graph.CIEDGE_LIST = new List<Edge>();

            if (openFile() == 0)//couldn't open
            {
                graph = graph_BU;
                graph.NODE_LIST = nodeList_BU;
                graph.EDGE_LIST = edgeList_BU;
                graph.DIEDGE_LIST = diEdgeList_BU;
                graph.CIEDGE_LIST = cicleEdgeList_BU;
            }
            else//it was opened succesfully
            {
                InvalidatePlus();
                justSaved = true;
            }
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
            atributes of a node that can be unique
            Coordenate position; 
            int radiusLenght; 
            int index;
            int uniqueID;
            */
            foreach (Node node in graph.NODE_LIST)//all about the node
            {
                sw.WriteLine(node.ID + "," + node.Index + "," + node.Position.X + "," + node.Position.Y + "," + node.Radius);
            }
            sw.WriteLine("Matrix");
            foreach (List<NodeRef> row in graph.GRAPH)
            {
                foreach (NodeRef nodeR in row)
                {
                    sw.Write(nodeR.W + ",");
                }
                sw.WriteLine();
            }
            sw.WriteLine("Edges");
            foreach (Edge edge in graph.EDGE_LIST)
            {
                sw.WriteLine(edge.Client.Index + "," + edge.Server.Index);
            }
            sw.WriteLine("D_Edges");
            foreach (Edge edge in graph.DIEDGE_LIST)
            {
                sw.WriteLine(edge.Client.Index + "," + edge.Server.Index);
            }
            sw.WriteLine("C_Edges");
            foreach (Edge edge in graph.CIEDGE_LIST)
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
                    graph.addNode(node);
                    //nodeList.Add(node);
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
                        graph.GRAPH[i][j].W = Peso;
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

                    for (int j = 0; j < graph.GRAPH.Count; j++)
                    {
                        if (graph.GRAPH[j][j].NODO.Index == nodo_C)
                        {
                            client = graph.GRAPH[j][j].NODO;
                        }
                        if (graph.GRAPH[j][j].NODO.Index == nodo_S)
                        {
                            server = graph.GRAPH[j][j].NODO;
                        }
                    }

                    Edge edge = new Edge(server, client);
                    //edgeList.Add(edge);
                    graph.addUndirectedEdge(edge);
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

                    for (int j = 0; j < graph.GRAPH.Count; j++)
                    {
                        if (graph.GRAPH[j][j].NODO.Index == nodo_C)
                        {
                            client = graph.GRAPH[j][j].NODO;
                        }
                        if (graph.GRAPH[j][j].NODO.Index == nodo_S)
                        {
                            server = graph.GRAPH[j][j].NODO;
                        }
                    }

                    Edge edge = new Edge(server, client);
                    graph.DIEDGE_LIST.Add(edge);
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

                    for (int j = 0; j < graph.GRAPH.Count; j++)
                    {
                        if (graph.GRAPH[j][j].NODO.Index == nodo_S)
                        {
                            server = graph.GRAPH[j][j].NODO;
                        }
                    }

                    //Edge edge = new Edge(server, server);
                    //cicleEdgeList.Add(edge);
                    graph.addCicledEdge(server);
                    Input = sr.ReadLine().Split(Delimiters, StringSplitOptions.RemoveEmptyEntries);
                }
                sr.Close();
                statusRes = 1;
            }

            return statusRes;
        }
        /**************************** file operations(END) **********************/
        #endregion

        #region Paint
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


            foreach (Edge edge in graph.EDGE_LIST)//undirected edges.
            {
                drawEdge(graphics, edge);
            }

            foreach (Edge edge in graph.CIEDGE_LIST)//cicled edge.
            {
                drawCicledEdge(graphics, edge, e);
            }

            foreach (Edge edge in graph.DIEDGE_LIST)//directed edges.
            {
                drawDirectedEdge(graphics, edge);
            }

            for (int i = 0; i < graph.GRAPH.Count; i++)//Nodes.
            {
                NodeRef nod = graph.GRAPH[i][i];
                rectangle = new Rectangle(nod.NODO.Position.X - nod.NODO.Radius, nod.NODO.Position.Y - nod.NODO.Radius, nod.NODO.Radius * 2, nod.NODO.Radius * 2);
                graphics.FillEllipse(brush, rectangle);
                pen = new Pen(nod.NODO.COLOR, 5);
                graphics.DrawEllipse(pen, nod.NODO.Position.X - nod.NODO.Radius, nod.NODO.Position.Y - nod.NODO.Radius, nod.NODO.Radius * 2, nod.NODO.Radius * 2);

                //draw inside the node a index.
                String index_S = "" + nod.NODO.Index;
                int fontSize = generalRadius - 10;
                graphics.DrawString(index_S, new Font(FontFamily.GenericSansSerif, fontSize), new SolidBrush(Color.Black), nod.NODO.Position.X - (fontSize / 2), nod.NODO.Position.Y - (fontSize / 2));
            }

            if (D_linkingAnimation || U_LinkingAnimation)
            { 
                if (D_linkingAnimation)//for undirected 
                {
                    drawDirectedEdge(graphics, linkingEdge);                    
                }
                else if (U_LinkingAnimation)//for directed 
                {
                    drawEdge(graphics, linkingEdge);
                }

                D_linkingAnimation = false;
                U_LinkingAnimation = false;
            }

        }

               

        private void drawEdge(Graphics graphics, Edge edge)
        {
            Pen pen2 = new Pen(edge.COLOR, 5);
            graphics.DrawLine(pen2, edge.A.X, edge.A.Y, edge.B.X, edge.B.Y);
        }

        private void drawCicledEdge(Graphics graphics, Edge edge, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.Black, 5);
            Point StartPoint = new Point(edge.A.X, edge.A.Y);
            Point unoP = new Point(edge.A.X - generalRadius * 4, edge.A.Y - generalRadius * 4);
            Point dosP = new Point(edge.A.X - generalRadius * 4, edge.A.Y + generalRadius * 4);
            GraphicsPath gPath = new GraphicsPath();
            gPath.AddBezier(StartPoint, unoP, dosP, StartPoint);
            e.Graphics.DrawPath(pen, gPath);
        }

        private void drawDirectedEdge(Graphics graphics, Edge edge)
        {
            Pen penDirect = new Pen(Color.DimGray, 8);
            penDirect.StartCap = System.Drawing.Drawing2D.LineCap.RoundAnchor;
            penDirect.EndCap = System.Drawing.Drawing2D.LineCap.ArrowAnchor;
            Double equis_X;
            Double ye_Y;
            Double rate = edge.Distancia / generalRadius;
            equis_X = (edge.A.X + rate * edge.B.X) / (1 + rate);
            ye_Y = (edge.A.Y + rate * edge.B.Y) / (1 + rate);
            graphics.DrawLine(penDirect, edge.A.X, edge.A.Y, (float)equis_X, (float)ye_Y);
        }

        #endregion

        #region InvalidatePlus
        /****************************************************************************
         * 
         *  ||||||||||||||||||||| MENTADO INVALIDATE() (Begin) ||||||||||||||||||||
         * 
         * *****************************************************************************/

        /****************** invalidate Plus (Begin) ***************************/
        public void InvalidatePlus()
        {
            commonInvalidateActions();
            Invalidate();
        }
        public void InvalidatePlus(int code)// just save change to false because of an operation.
        {
            justSaved = false;// almost all that requires invalidate also should change the jusSaved state.           
            commonInvalidateActions();
            Invalidate();
        }
        /****************** invalidate Plus (END) ***************************/
        /****************** invalidate common (Begin) ***************************/
        public void commonInvalidateActions()
        {
            matrixTB.Text = graph.ToString(matIn);

            if (selected != null)
            {
                terminal.Text = "Node selected : " + System.Environment.NewLine + "ID = " + selected.ID + System.Environment.NewLine + "Index = " + selected.Index + "\t" + System.Environment.NewLine;
                if (graph.Directed() == true)
                {
                    DirectedGrade dG;
                    dG = graph.GradeOfDirectedNode(selected);
                    terminal.Text += "Grado(Nodo): " + dG.Total;
                    terminal.Text += System.Environment.NewLine;
                    terminal.Text += "  GradoEntrada ( [<-] ): " + dG.Input;
                    terminal.Text += System.Environment.NewLine;
                    terminal.Text += "  GradoSalida    ( [->] ): " + dG.Output;
                }
                else
                {
                    terminal.Text += "Grado(Nodo): " + graph.GradeOfNode(selected);
                }
            }
            else
            {
                terminal.Text = "Node selected : ";
            }
            statusTB.Text = "Nombre :" + fileName + System.Environment.NewLine;
            statusTB.Text += "Grado(Grafo) : " + graph.Grade();
            statusTB.Text += System.Environment.NewLine;
            statusTB.Text += "Dirigido : " + graph.Directed();
            statusTB.Text += System.Environment.NewLine;
            statusTB.Text += "Completo : " + graph.Complete();
            statusTB.Text += System.Environment.NewLine;
            statusTB.Text += "Pseudo: " + graph.Pseudo();
            statusTB.Text += System.Environment.NewLine;
            statusTB.Text += "Cíclico : " + graph.Cicled();
            statusTB.Text += System.Environment.NewLine;
            statusTB.Text += "Bipartita : " + graph.Bip();
            statusTB.Text += System.Environment.NewLine;

            if ((IsomorfismForm == null || (IsomorfismForm != null && IsomorfismForm.Visible == false)) && isoForm == false)
            {
                IsomtextBox.Visible = false;
            }
        }
        /****************** invalidate common (END) ***************************/
#endregion


        /*************************************************************************************************************************
         * 
         * |||||||||||||||||||||||||||||||||||||||||||||||||||||   OTHER METHODS ()  |||||||||||||||||||||||||||||||||||||||||||||||||||
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

        public Node findNodeClicked(Coordenate cor)
        {
            Node resNode = null;

            foreach (Node onNode in graph.NODE_LIST)
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
                selected.SelectedBool =AllowDrop = false;//
                selected = null;
            }
        }

        public void eliminate()
        {
            if (graph.NODE_LIST.Count() <= 1)
                justSaved = true;

            if (selected != null)
            {
                graph.eliminateNexetEdges(selected);
                graph.eliminateNexetDirectedEdges(selected);
                graph.eliminateCicledEdges(selected);
                graph.removeNode(selected);

                selected = null;
            }
            InvalidatePlus(1);
        }

        public void create(Coordenate cor)
        {
            Coordenate newNodePosition = new Coordenate(cor.X, cor.Y);
            Node newNode;
            if (MoRe_F_Do)
            {
                graph.create(newNodePosition,generalRadius, Color.Indigo);
            }
            else
            {
                graph.create(newNodePosition, generalRadius);
            }
            InvalidatePlus(1);
        }

        private int AskForAWeight()
        {
            int weight = 0;

            return weight;
        }

        public void changeIsomtextBox(String str)
        {
            IsomtextBox.Text = "Isomorfismo : ";
            IsomtextBox.Text += System.Environment.NewLine;
            IsomtextBox.Text += str;
        }

        /*************************************************************************************************************************
         * 
         * |||||||||||||||||||||||||||||||||||||||||||||||||||||   ALGORITHMS  |||||||||||||||||||||||||||||||||||||||||||||||||||
         * 
         * ***********************************************************************************************************************/

        /*******************************************************************************************
         * 
         * 
         *  ////////////////   paths and cycles.(caminos y circuitos).  //////////////////////
         *      
         * 
         * ******************************************************************************************/

        Graph aux;
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
                graph.allBlack();
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
                    graph.allBlack();
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

            foreach (Node node in graph.NODE_LIST)
            {
                int degreeByN = graph.neighborListNode(node).Count();

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
            edgeListInside = graph.EDGE_LIST;
            pathOfNodes = new List<Node>();
            pathToAnimate = new List<Edge>();

            cutEdges = new List<Edge>();

            graph.markAllLikeNotBridge();
            graph.markAllLikeNotVisited(1);
           

            foreach (Edge edge in graph.EDGE_LIST)
            {
                if (isABridgeBool(edge))
                {
                    cutEdges.Add(edge);
                }
            }

            // Mark all the vertices as not visited 
            graph.markAllLikeNotVisited();

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

            foreach (Node node in graph.neighborListNode(workingNode))
            {
                //if(node.Visitado == false)
                //{
               Edge edge= graph.thisEdge(workingNode,node);
                   // foreach (Edge edge in aListGraph.listOfEdges_IG)// edgeList)
                   // {
                    if (edge.visitada == false )
                        {
                            if (isABridgeVisitedsBool(edge,graph))//cutEdges.Contains(edge))//||edge.Bridge == true)
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
                    foreach (Edge edge in graph.EDGE_LIST)
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

            if (graph.neighborListNode(workingNode).Contains(finalNodePath))
            {
                Edge edgeFinal = graph.thisEdge(workingNode,finalNodePath);

                if (graph.allVisitedExept(edgeFinal) && edgeFinal.visitada == false)
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
                graph.allBlack();
                Invalidate();
            }
            else//a trabajar
            {
                if (graph.neighborListNode(initialNodePath).Count() % 2 == 0 
                 || graph.neighborListNode(finalNodePath).Count() % 2 == 0)
                {
                    if (estimadedIniFinNodes.Count()>1)
                    {
                        MessageBox.Show("Existe un camino de Euler pero no el sugerido, intenta con " + estimadedIniFinNodes[0].Index + ","+  estimadedIniFinNodes[1].Index);
                        graph.allBlack();
                        Invalidate();
                    }
                    else
                    {
                        MessageBox.Show("No existe el camino de Euler");
                        graph.allBlack();
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
            edgeListInside = graph.EDGE_LIST;
            pathOfNodes = new List<Node>();
            pathToAnimate = new List<Edge>();

            cutEdges = new List<Edge>();

            graph.markAllLikeNotBridge();
            graph.markAllLikeNotVisited(1);


            foreach (Edge edge in graph.EDGE_LIST)
            {
                if (isABridgeBool(edge))
                {
                    cutEdges.Add(edge);
                }
            }

            // Mark all the vertices as not visited 
            graph.markAllLikeNotVisited();

            // Start DFS traversal from a vertex with non-zero degree 

            pathOfNodes.Add(initialNodePath);
            DFSEulerCycle(initialNodePath);
            //pathOfNodes.Add(finalNodePath);
            return res;

        }

        public Boolean pathOfEulerBool()
        {
            bool res = true;
            aux = new Graph();
            estimadedIniFinNodes = new List<Node>();
            int oddDegreeCont = 0;

            foreach (Node node in graph.NODE_LIST)
            {
                int degreeByN = graph.neighborListNode(node).Count();

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

            if (aux.NODE_LIST.Count() > 0)
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
                graph.allBlack();
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
                    graph.allBlack();
                    Invalidate();
                }
                   
            }
        }

        public Boolean cycleOfHamiltonBool()
        {

            Boolean res = true;

            //can not have a disconnected node
            if (!allConected(graph))
            {
                return false;
            }
            //not cut vertices
            foreach (Edge edge in graph.EDGE_LIST)
            {
                if (isABridgeBool(edge))//if any edge is a bridge it return false to hamilton cycle.
                {
                    return false;
                }
            }

            foreach (Node node in graph.NODE_LIST)
            {
                if (graph.isACutNodeBool(node))//if any node is a cut node return false to hamilton cycle.
                {
                    return false;
                }
                if (graph.neighborListNode(node).Count() < 2)
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

            graph.markAllLikeNotBridge();
            graph.markAllLikeNotVisited(1);


            foreach (Edge edge in graph.EDGE_LIST)
            {
                if (isABridgeBool(edge))
                {
                    cutEdges.Add(edge);
                }
            }

            // Mark all the vertices as not visited 

            // Start DFS traversal from a vertex with non-zero degree 
            //return DFSHamiltonCycle(initialNodePath);
 
            graph.markAllNodeAndEdgesNotVisited();//marcar todos los nodos y aristas como no visitados.
           
            return DFS_Any_HamiltonCycle(initialNodePath);
        }

        List<Node> nodesPath = new List<Node>();
Boolean DFS_Any_HamiltonCycle(Node workingNode)//recursive function.
        {
            workingNode.Visitado= true;//marcar el nodo actual como visitado.
            List<Node> notVisitedYet = graph.notVisitedList();//nodos sin visitar para restauraciones.
            List<Node> neightboors = graph.neighborListNode(workingNode);//vecinos del nodo actual.

            /*********************
             *       Caso Base. 
             * *********************/
            if (notVisitedYet.Count() < 1 && neightboors.Contains(initialNodePath))//todos los nodos visitados && el nodo actual tiene de vecino al nodo inicial
            {
                Edge edge = graph.thisEdge(workingNode, initialNodePath);
                pathToAnimate.Add(edge);//agrega la arista( actual->inicial) al camino para animar
                pathOfNodes.Add(initialNodePath);//se agrega por primera vez el nodoInicial(mismo que nodoFinal) al camino de nodos;
                pathOfNodes.Add(workingNode);//agrega el nodo actual al camino de nodos 
                return true;
            }

            //acomodar los vecinos de menor a mayor en cuestion de grado.
            neightboors.Sort(delegate (Node x, Node y)
            {
                return graph.neighborListNodeNoVisited(x).Count().CompareTo(graph.neighborListNodeNoVisited(y).Count());
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
                        Edge edge = graph.thisEdge(workingNode, node);
                        pathOfNodes.Add(workingNode);
                        pathToAnimate.Add(edge);
                        return true;
                    }
                    else// si se retorna false se restauran los nodos de la lista de restaturacion(notVisitedYet)
                        graph.restoreNotVisited(notVisitedYet);//restaturacion.
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

            List<Node> neightboors = graph.neighborListNode(workingNode);
            neightboors.Sort(delegate(Node x, Node y)
            {
               return graph.neighborListNodeNoVisited(x).Count().CompareTo(graph.neighborListNodeNoVisited(y).Count());
            });

            Boolean inmovilizado = true ;//when all eas visited already
            

            foreach (Node node in neightboors)
            {
                if (node.Visitado == false)
                {
                    inmovilizado = false;
                    Edge edge = graph.thisEdge(workingNode, node);
                    if (edge.visitada == false)
                    {
                        if (isABridgeVisitedsBool(edge, graph))//cutEdges.Contains(edge))//||edge.Bridge == true)
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
                        Edge edge = graph.thisEdge(workingNode, node);
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
                Edge edgeFinal = graph.thisEdge(workingNode, finalNodePath);

                if (graph.allNodesVisitedBool() )//&& edgeFinal.visitada == false)
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
                graph.allBlack();
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
            List<Node> notVisitedYet = graph.notVisitedList();
            List<Node> neightboors = graph.neighborListNode(workingNode);
            if (notVisitedYet.Count() < 1)// si todos los nodos han sido visitados
            {
                if (neightboors.Contains(initialNodePath) && initialNodePath == finalNodePath)// si cumple el ciclo y se busca el ciclo.
                {
                    Edge edge = graph.thisEdge(workingNode, initialNodePath);
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
                    Edge edge = graph.thisEdge(workingNode, finalNodePath);
                    pathOfNodes.Add(workingNode);
                    pathToAnimate.Add(edge);
                    return res;
                }
            }


            neightboors.Sort(delegate (Node x, Node y)
            {
                return graph.neighborListNodeNoVisited(x).Count().CompareTo(graph.neighborListNodeNoVisited(y).Count());
            });

            foreach (Node node in neightboors)
            {
                if (node.Visitado == false && node != finalNodePath)
                {
                    int res = DFS_Any_HamiltonCycleOrPath(finalNodePath);
                    if (res > 0)
                    {

                        // nodesPath.Add(workingNode);
                        Edge edge = graph.thisEdge(workingNode, node);
                        pathOfNodes.Add(workingNode);
                        pathToAnimate.Add(edge);
                        return res;
                    }
                    else
                        graph.restoreNotVisited(notVisitedYet);
                }

            }

            return 0;
        }//DFS_Any_HamiltonCycle(END).




        public Boolean pathOfHamiltonBool()
        {
        // Boolean res = false;
            //can not have a disconnected node
            if (!allConected(graph))
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

            graph.markAllLikeNotBridge();
            graph.markAllLikeNotVisited(1);


            foreach (Edge edge in graph.EDGE_LIST)
            {
                if (isABridgeBool(edge))
                {
                    cutEdges.Add(edge);
                }
            }

            // Mark all the vertices as not visited 

            // Start DFS traversal from a vertex with non-zero degree 

            pathOfNodes.Add(initialNodePath);
            graph.markAllNodeAndEdgesNotVisited();
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

            List<Node> neightboors = graph.neighborListNode(workingNode);
            neightboors.Sort(delegate(Node x, Node y)
            {
               return graph.neighborListNode(x).Count().CompareTo(graph.neighborListNode(y).Count());
            });

            Boolean inmovilizado = true;//when all eas visited already


            if (workingNode == finalNodePath && graph.allNodesVisitedBool())//&& edgeFinal.visitada == false)
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
                        Edge edge = graph.thisEdge(workingNode, node);
                        if (edge.visitada == false)
                        {
                            if (isABridgeVisitedsBool(edge, graph))//cutEdges.Contains(edge))//||edge.Bridge == true)
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
                        Edge edge = graph.thisEdge(workingNode, node);
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
            graph.markAllLikeNotVisited();

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

        public Boolean allConected(Graph graph)
        {
            // Mark all the vertices as not visited 
            graph.markAllLikeNotVisited();

            // Start DFS traversal from a vertex with non-zero degree 
            DFSUtilAllConected(graph.NODE_LIST[0]);

            // Check if all non-zero degree vertices are visited 
            foreach (Node node in graph.NODE_LIST)
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
            foreach (Node node in graph.neighborListNode(workingNode))
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
            graph.markAllLikeNotVisited();

            // Start DFS traversal from a vertex with non-zero degree 
            //DFSUtilAllConectedBridge(aux.LIST_NODES[0], posibleBridge);
            DFSUtilAllConectedBridge(graph.NODE_LIST[0], posibleBridge);

            // Check if all non-zero degree vertices are visited 
            //foreach (Node node in aux.LIST_NODES)
            foreach (Node node in graph.NODE_LIST)
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
            foreach (Node node in graph.neighborListNode(workingNode))
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


        public Boolean isABridgeVisitedsBool(Edge posibleBridge, Graph graph)
        {
            List<int> listOfNonVisited = new List<int>();
            foreach(Node node in graph.NODE_LIST)
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
            DFSUtilAllConectedVisitedsBridge(graph.NODE_LIST[0], posibleBridge,graph);

            // Check if all non-zero degree vertices are visited 
            //foreach (Node node in aux.LIST_NODES)
            foreach (Node node in this.graph.NODE_LIST)
            {
                if (node.Visitado == false)
                {
                    //posibleBridge.COLOR = Color.Gold;
                    posibleBridge.Bridge = true;


                    foreach (Node nodeG in graph.NODE_LIST)
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

            foreach (Node node in graph.NODE_LIST)
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

        void DFSUtilAllConectedVisitedsBridge(Node workingNode, Edge posibleBridge, Graph graph/*int v, bool visited[]*/)
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


        private void terminal_TextChanged(object sender, EventArgs e)
        {

        }

        
        protected virtual void isoForm_Click(object sender, EventArgs e)
        {
            if (f3.Operation == 1)
            {
                graph.allBlack();
                Invalidate();
                f3.Operation = 0;
            }
            if (IsomorfismForm == null || IsomorfismForm.Visible == false)
            {
                IsomorfismForm = new GraphFormIsomorphic(this);
                IsomorfismForm.Show();
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
                    graph.allBlack();
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
                        graph.allBlack();
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
            graph.markAllLikeNotVisited();
            

            
            Edge[] workingEdgesArray = new Edge[graph.EDGE_LIST.Count()];
            graph.EDGE_LIST.CopyTo(workingEdgesArray);
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

            foreach(Node nodo in graph.neighborListNode(node))
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

       

        String typeOfPath;
        

        private void caminosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (f3.Operation == 1)
            {
                graph.allBlack();
                Invalidate();
                f3.Operation = 0;
            }
            deselect();
        }

        private void brToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach(Edge edge in graph.EDGE_LIST)
            {
                isABridgeBool(edge);
            }
        }


        #region Algorithms


            #region Isomorphism
            #endregion


            #region Paths and Cycles

            #region Euler
            #endregion

            #region Hamilton
            #endregion

            #endregion


            #region Dijkstra
            #endregion


            #region Floyd
            #endregion


            #region Warshall
            #endregion


            #region Prim
            #endregion


            #region Kruskal
            #endregion
        #endregion

    }//Form.
}//namespace.
