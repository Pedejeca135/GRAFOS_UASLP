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
    public partial class pruebas : Form
    {

        List<superPepe> lista4 = new List<superPepe>();
        public class pepe
        {
            public int i = 0;
            public int j = 5;
            public pepe(int i)
            {
                this.i = i;
            }
        }

        public class superPepe
        {
            public pepe pepe;
            public superPepe(pepe pe)
            {
                this.pepe = pe;
            }
        }
    
        public pruebas()
        {
            InitializeComponent();
           
            String mensaje = "";
            List<pepe> lista1 = new List<pepe>();
            lista1.Add(new pepe(0));
            lista1.Add(new pepe(1));
            lista1.Add(new pepe(2));
            lista1.Add(new pepe(3));
            List<pepe> lista2 = new List<pepe>();
            List<pepe> lista3;
            //lista3.
            //lista3 = lista1;
            pepe[] arr3 = new pepe[4] ;
            lista1.CopyTo(arr3);
            lista3 = arr3.ToList();
            lista3.RemoveAt(0);
            lista3[0].i = 500;
            add(new superPepe(lista3[0]));

            lista2 = lista1;

            foreach (pepe en in lista1)
            {
                mensaje += "(" + en.i + ")";
            }
            mensaje += " # ";
            foreach (pepe en in lista2)
            {
                mensaje += "(" + en.i + ")";
            }
            mensaje += " # ";
            foreach (pepe en in lista3)
            {
                mensaje += "(" + en.i + ")";
            }
            this.richTextBox1.Text = mensaje;
        }

        public void add(superPepe pep)
        {
            lista4.Add(pep);
            pep.pepe.i = 444;
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

    }
}
