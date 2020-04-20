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
    public partial class pathsOK : Form
    {
        public pathsOK(String message)
        {
            InitializeComponent();
            this.label1.Text = message;
        }
        public pathsOK()
        {
            InitializeComponent();
            this.label1.Text = "";
        }

        public void changeMesagge(String str)
        {
            this.label1.Text = str;
        }

        private void pathsOK_Load(object sender, EventArgs e)
        {

        }

        int operation = 0;

        public int Operation
        {
            get { return operation; }
            set { operation = value; }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            operation = 1;
            this.Close();
        }
    }


       

        
    

}
