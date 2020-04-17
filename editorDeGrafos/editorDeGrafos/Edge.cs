using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace editorDeGrafos
{
    public class Edge
    {
        Node client = null;
        Node server = null;
        Boolean directed;

        //Coordenate a;
        //Coordenate b;
        //int weight;
        //int direction;

        //for undirected edges.
        public Edge(Node client, Node server)
        {
            this.client = client;
            this.server = server;
            directed = false;
        }

        //for directed edges.
        public Edge(Node client, Node server, Boolean directedBool)
        {
            this.client = client;
            this.server = server;
            directed = directedBool;
        }

        //for cicle edge
        public Edge(Node unique)
        {
            this.client = unique;
            this.server = unique;
            directed = false;
        }

        public Coordenate A
        {
            get { return this.client.Position; }
            //set { this.a = value; }
        }

        public Coordenate B
        {
            get { return this.server.Position; }
            //set { this.b = value; }
        }

        public Node Client
        {
            get { return this.client; }
        }
        public Node Server
        {
            get { return this.server; }
        }

        public Double Distancia => Math.Pow(Math.Pow(B.X - A.X, 2.0) + Math.Pow(B.Y - A.Y, 2.0), 0.5);

    }//Edge.

}
