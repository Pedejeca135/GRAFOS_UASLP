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
    public partial class Form2 : Form1
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        protected override void isoForm_Click(object sender, EventArgs e)
        {
            Form2 forma = new Form2();
            SaveChangesWindow scw = new SaveChangesWindow();
            scw.ShowDialog();

        }


    }
}
