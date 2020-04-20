using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace editorDeGrafos
{
    public class Node
    {
        Coordenate position;
        Color color;
        int radiusLenght;
        Boolean justSelected;
        int selected;
        int index;
        int uniqueID;

        Boolean visitado = false;


        public Node() //default constructor of the class Node
        {

        }

        public Node(int x, int y)
        {
            this.Position = new Coordenate(x, y);
        }

        public Node(Coordenate position, int radius, int index, int identifier)
        {
            this.position = position;
            this.radiusLenght = radius;
            justSelected = false;
            selected = 0;
            this.index = index;//ID of the node
            color = Color.Black;
            uniqueID = identifier;
        }
        public Node(Coordenate position, int radius, int index, int identifier, Color color)
        {
            this.position = position;
            this.radiusLenght = radius;
            justSelected = false;
            selected = 0;
            this.index = index;//ID of the node
            this.color = color;
            uniqueID = identifier;
        }

        /*******************************************************
         *               Geters and seters(Begin)              *
         *******************************************************/
        public Coordenate Position
        {
            get { return this.position; }
            set { this.position = value; }
        }

        public int Radius
        {
            get { return this.radiusLenght; }
            set { this.radiusLenght = value; }
        }

        public int Status
        {
            get { return selected; }
            set { selected = value; }
        }

        public Color COLOR
        {
            get { return this.color; }
            set { this.color = value; }
        }

        public Boolean SelectedBool
        {
            get { return this.justSelected; }
            set { this.justSelected = value; }
        }

        public int Index
        {
            get { return this.index; }
            set { this.index = value; }
        }

        public int ID
        {
            get { return this.uniqueID; }
            set { this.uniqueID = value; }
        }

        public Boolean Visitado
        {
            get { return this.visitado; }
            set { this.visitado = value; }
        }
        /*******************************************************
         *                Geters and seters(End)               *
         *******************************************************/

        /*******************************************************
         *                Methods(Begin)                       *
         *******************************************************/

        public override String ToString()
        {
            return this.ID + this.position.ToString() + " -index = " + this.Index;
        }

    }//Node class.
}
