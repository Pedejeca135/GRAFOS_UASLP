using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace editorDeGrafos
{
    public class DirectedGrade
    {
        int input;
        int output;

        public DirectedGrade()
        {

        }
        public DirectedGrade(int input, int output)
        {
            this.input = input;
            this.output = output;
        }

        public int Input
        {
            get { return this.input; }
        }
        public int Output
        {
            get { return this.output; }
        }

        public int Total
        {
            get { return input + output; }
        }
    }//DirectedGrade.
}
