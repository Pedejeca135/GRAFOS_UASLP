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
    public partial class AskForWeight : Form
    {
        int x = 0;
        Boolean outSide = true;

        public AskForWeight()
        {
            InitializeComponent();
            this.typeLabel.Text = "No type applied";
            this.textBox.Text = "" + x;
        }
        public AskForWeight(String typeS)
        {
            InitializeComponent();
            this.typeLabel.Text = typeS;
            this.textBox.Text = "" + x;
        }

        private void Aceptar_Click(object sender, EventArgs e)
        {
            int.TryParse(textBox.Text,out x);
            this.Close();
        }

        private void Cancelar_Click(object sender, EventArgs e)
        {
            x = -1;
            this.Close();
        }

        public int getX
        {
            get { return x; }
        }

        private void AskForWeight_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Space)
            {
                Aceptar_Click(sender,e);
            }
        }

        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Space)
            {
                Aceptar_Click(sender, e);
            }
        }

        private void AskForWeight_Load(object sender, EventArgs e)
        {
            
        }

        private void AskForWeight_Click(object sender, EventArgs e)
        {
            Aceptar_Click(sender, e);
        }

        private void Aceptar_MouseLeave(object sender, EventArgs e)
        {
            outSide = true;
        }

        private void Aceptar_MouseEnter(object sender, EventArgs e)
        {
            outSide = false;
        }

        private void Aceptar_MouseClick(object sender, MouseEventArgs e)
        {
            if(outSide == true)
            {
                Aceptar_Click(sender, e);
            }
        }
    }
}
