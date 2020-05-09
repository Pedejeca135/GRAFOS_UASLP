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


        public int Weight
        {
            get { return weight; }
            set { this.weight = value; }
        }


        //for undirected edges.
        public Edge(Node client, Node server)
        {
            this.client = client;
            this.server = server;
            directed = false;
        }

        //for undirected edges.
        public Edge(Node client, Node server,int weight)
        {
            this.client = client;
            this.server = server;
            directed = false;
            this.weight = weight;
        }


        //for directed edges.
        public Edge(Node client, Node server, Boolean directedBool)
        {
            this.client = client;
            this.server = server;
            directed = directedBool;
        }

        //for directed edges.
        public Edge(Node client, Node server,int weight, Boolean directedBool)
        {
            this.client = client;
            this.server = server;
            directed = directedBool;
            this.weight = weight;
        }

        //for cicle edge
        public Edge(Node unique,int weight)
        {
            this.client = unique;
            this.server = unique;
            directed = false;
            this.weight = weight;
        }

        // for not used edges
        public Edge(Node client , Coordenate cor)
        {
            this.client = client;
            this.server = new Node(cor.X,cor.Y);
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
