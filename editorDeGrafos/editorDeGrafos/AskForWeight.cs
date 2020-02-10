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

        public AskForWeight()
        {
            InitializeComponent();
            this.typeLabel.Text = "No type applied";
        }
        public AskForWeight(String typeS)
        {
            InitializeComponent();
            this.typeLabel.Text = typeS;
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
    }
}
