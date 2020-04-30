using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace editorDeGrafos
{
    public class Edge
    {
        public Node client = null;
        public Node server = null;

        Boolean directed;
        Color color = Color.Black;
        Boolean visited = false;
        Boolean bridge = false;
        int weight;


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

        public Boolean visitada
        {
            get { return this.visited; }
            set { this.visited = value; }
        }

        public Boolean Bridge
        {
            get { return this.bridge; }
            set { this.bridge = value; }
        }

        public Color COLOR
        {
            get { return this.color; }
            set { this.color = value; }
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

        public Boolean isThis(Node client, Node server)
        {
            if (client == this.client && server == this.server)
            {
                return true;
            }
            return false;
        }

        public Boolean isThis(int client, int server)
        {
            if (this.client.Index == client && this.server.Index == server)
            {
                return true;
            }
            return false;
        }

        public Boolean EqualsU(Edge edge)
        {
            if (this.client == edge.client && this.server == edge.server
             || this.server == edge.client && this.client == edge.server)
            {
                return true;
            }
            return false;
        }

        public Boolean EqualsD(Edge edge)
        {
            if (this.client == edge.client && this.server == edge.server)
            {
                return true;
            }
            return false;
        }

        public Boolean isThisUndirected(Node client, Node server)
        {
            if (this.client == client && this.server == server
              || this.server == client && this.client == server)
            {
                return true;
            }
            return false;
        }

        public Boolean isThisUndirected(int client, int server)
        {
            if (this.client.Index == client && this.server.Index == server
              || this.server.Index == client && this.client.Index == server)
            {
                return true;
            }
            return false;

        }

        }//Edge.

    }
