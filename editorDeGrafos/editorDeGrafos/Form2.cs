﻿using System;
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
    public partial class Form2 : Form1
    {

        Form1 f1;
        public Form2(Form1 father) : base(1)
        {
            InitializeComponent();
            f1 = father;
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        protected override void isoForm_Click(object sender, EventArgs e)
        {
        }

        protected override void fuerzaBrutaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(this.aListGraph != null)
            {
                changeIsomtextBox( this.aListGraph.Isom_Fuerza_Bruta(f1.aListGraph).ToString());
            }
        }

        protected override void traspuestaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.aListGraph != null)
            {
                changeIsomtextBox(this.aListGraph.Isom_Traspuesta(f1.aListGraph).ToString());
            }
        }

        protected override void intercambioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.aListGraph != null)
            {
                changeIsomtextBox(this.aListGraph.Isom_Inter(f1.aListGraph).ToString());
            }
        }

        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            forClosing(sender,e);
        }
        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            forClosing(sender, e);
        }

        private void forClosing(object sender, EventArgs e)
        {
            f1.closeIsoFormClicked(sender, e);
        }

       
    }//Form.
}//Namespace.
