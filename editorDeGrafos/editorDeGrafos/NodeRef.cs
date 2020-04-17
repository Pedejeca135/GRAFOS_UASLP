using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace editorDeGrafos
{
    public class NodeRef
    {
        int weight;
        Node nodo;
        TidyPair tidy;
        Boolean activeNode;


        public NodeRef(int weight, Node nodo, TidyPair tidyPair)
        {
            this.tidy = tidyPair;
            this.nodo = nodo;
            this.weight = weight;
            activeNode = false;
        }

        public NodeRef(int weight, Node nodo, TidyPair tidyPair, Boolean active)
        {
            this.tidy = tidyPair;
            this.nodo = nodo;
            this.weight = weight;
            activeNode = active;
        }

        public Node NODO
        {
            get { return this.nodo; }
        }

        public int W
        {
            get { return this.weight; }
            set { this.weight = value; }
        }

        public TidyPair TidyPair
        {
            get { return this.tidy; }
            set { this.tidy = value; }
        }

        public Boolean ACTIVATION
        {
            get { return activeNode; }
        }

    }//NodeRef.
}
