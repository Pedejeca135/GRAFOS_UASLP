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
    public partial class SaveChangesWindow : Form
    {

        int operation = 0;

        public SaveChangesWindow()
        {
            InitializeComponent();
        }

        public int Operation
        {
            get { return operation; }
            set { operation = value; }
        }


        private void Save_Click(object sender, EventArgs e)
        {
            operation = 1;
            this.Close();
        }

        private void notSave_Click(object sender, EventArgs e)
        {
            operation = 2;
            this.Close();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            operation = 0;
            this.Close();
        }
    }
}
