using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace editorDeGrafos
{
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
}
