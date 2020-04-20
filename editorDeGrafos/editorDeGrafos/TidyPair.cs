using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace editorDeGrafos
{
    public class TidyPair : Coordenate
    {
        public TidyPair(int x, int y) : base(x, y)
        {

        }
        public override String ToString()
        {
            // return " x = " + this.X + " y = " + this.Y;
            return this.Y + ":" + this.X;
        }
    }
}
