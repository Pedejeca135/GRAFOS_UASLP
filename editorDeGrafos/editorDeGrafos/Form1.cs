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



        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }//Form.

    public class Node
    {
        Coordenate position;
        Color color;


        public Node(Coordenate position)
        {
            this.position = position;
        }

        public void Draw()
        {

        }

    }

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
    }

    public class WeightMatrixGraph
    {

    }

    public class adjacencyList
    {

    }








}//namespace.
