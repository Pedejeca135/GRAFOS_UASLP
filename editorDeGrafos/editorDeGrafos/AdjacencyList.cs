﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace editorDeGrafos
{
    public class AdjacencyList
    {
        List<List<NodeRef>> graph;
        public List<Node> listOfNodes_IG;
        public List<Edge> listOfEdges_IG;



        public AdjacencyList()
        {
            graph = new List<List<NodeRef>>();
            listOfNodes_IG = new List<Node>();
            listOfEdges_IG = new List<Edge>();
        }

        AdjacencyList(List<List<NodeRef>> graph, List<Node> listOfNodes_IG, List<Edge> listOfEdges_IG)
        {
            this.graph = graph;
            this.listOfNodes_IG = listOfNodes_IG;
            this.listOfEdges_IG = listOfEdges_IG;
        }

        public int NumberOfLists
        {
            get { return this.graph.Count(); }
        }

        public int NumberOfNestedLists
        {
            get { return this.graph[0].Count(); }
        }

        public List<List<NodeRef>> GRAPH
        {
            get { return this.graph; }
        }

        public List<Node> LIST_NODES
        {
            get { return this.listOfNodes_IG; }
        }


        public Boolean allVisitedExept(Edge workinEdge)
        {
            foreach (Edge edge in this.listOfEdges_IG)
            {
                if (edge != workinEdge && edge.visitada == false)
                {
                    return false;
                }
            }

            return true;
        }

        public Edge thisEdge(Node client, Node server)
        {
            Edge thisEdge = new Edge(client, server);

            foreach (Edge edge in this.listOfEdges_IG)
            {
                if (edge.EqualsU(thisEdge))
                {
                    return edge;
                }
            }
            return null;
        }

        public Boolean allVisitedExept(Node workingNode)
        {
            foreach (Edge edge in listOfEdges_IG)
            {
                if (edge.server != workingNode)
                {
                    if (edge.server.Visitado == false)
                    {
                        return false;
                    }
                }
                if (edge.client != workingNode)
                {
                    if (edge.client.Visitado)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public Boolean allVisitedExept(List<Node> listNodes, Node workingNode)
        {
            foreach (Node node in listNodes)
            {
                if (node != workingNode)
                {
                    if (node.Visitado == false)
                        return false;
                }
            }
            return true;
        }

        public void addNode(Node nodo)
        {
            List<NodeRef> newNodeRefList = new List<NodeRef>();//the new list for the new node conections.           
            listOfNodes_IG.Add(nodo);

            if (graph.Count() == 0)//ther's no elements
            {
                TidyPair tidyP = new TidyPair(0, 0); ;
                NodeRef nodoRef = new NodeRef(-1, nodo, tidyP, true);            //the new Node that is going to be added have no conection, so it have a -1 value.
                newNodeRefList.Add(nodoRef);
                graph.Add(newNodeRefList);
            }
            else//At least one element.
            {
                //int j = 0;
                for (int i = 0; i < graph.Count; i++)
                {
                    if (i == graph.Count)
                    {
                        newNodeRefList.Add(new NodeRef(-1, graph[i][i].NODO, new TidyPair(i, graph.Count()), true)); //making the new list for the end of the "array".
                    }
                    else
                    {
                        newNodeRefList.Add(new NodeRef(-1, graph[i][i].NODO, new TidyPair(i, graph.Count()))); //making the new list for the end of the "array".
                    }

                }

                graph.Add(newNodeRefList);//adding the list made.
                                          // terminal = "hay elementos" + graph.Count() + graph[0][0].NODO.ToString();
                int iF = 0;
                foreach (List<NodeRef> row in graph)
                {
                    row.Add(new NodeRef(-1, nodo, new TidyPair(nodo.Index, iF))); //adding to each row the new Node.
                    iF++;
                }
            }
        }

        public void removeNode(Node nodo)//almost the same process as addNode() but vice versa.
        {
            int nodeIndexToEiminate = nodo.Index;
            listOfNodes_IG.Remove(nodo);

            foreach (List<NodeRef> row in graph)
            {
                //  if (row.Count > nodeIndexToEiminate)// ESTO NO DEBERIA PORQUE ESTAR AQUI(por eso esta en español) :(.
                //{
                row.RemoveAt(nodeIndexToEiminate);//removing the NodeRef of all the list of nodes.
                                                  //}
            }

            //if (nodeIndexToEiminate < graph.Count())
            //{
            graph.RemoveAt(nodeIndexToEiminate);//remove the list of adjacency of the node.
                                                //}
            /*

            foreach (List<NodeRef> row in graph)// for changing the inner index of each node;
            {
                foreach (NodeRef node in row)
                {
                    if (node.ACTIVATION == true && node.NODO.Index > nodeIndexToEiminate)
                    {
                        node.NODO.Index --;
                    }
                }
            }
            */

            for (int j = 0; j < graph.Count(); j++)
            {
                List<NodeRef> noRe = graph[j];
                for (int i = 0; i < noRe.Count(); i++)
                {
                    if (i == j && noRe[i].NODO.Index > nodeIndexToEiminate)
                    {
                        noRe[i].NODO.Index--;
                    }
                }
            }

        }//remove a node.

        public void addUndirectedEdge(Node client, Node server, int weight)
        {
            if (graph.Count > client.Index && graph.Count > server.Index)
            {
                if (graph[client.Index].Count > server.Index && graph[server.Index].Count > client.Index)
                {
                    graph[client.Index][server.Index].W = weight;
                    graph[server.Index][client.Index].W = weight;
                }
            }

            //cadena = " index de cliente " + client.Index + " index de servidor " + server.Index;
        }

        public void addUndirectedEdge(Edge edge)
        {
            listOfEdges_IG.Add(edge);

            //cadena = " index de cliente " + client.Index + " index de servidor " + server.Index;
        }

        public void addUndirectedEdge(Edge edge, int weight)
        {
            listOfEdges_IG.Add(edge);
            if (graph.Count > edge.client.Index && graph.Count > edge.server.Index)
            {
                if (graph[edge.client.Index].Count > edge.server.Index && graph[edge.server.Index].Count > edge.client.Index)
                {
                    graph[edge.client.Index][edge.server.Index].W = weight;
                    graph[edge.server.Index][edge.client.Index].W = weight;
                }
            }

            //cadena = " index de cliente " + client.Index + " index de servidor " + server.Index;
        }

        public void addDirectedEdge(Node client, Node server, int weight)
        {
            //if (graph.Count > client.Index)
            //{
            //if (graph[client.Index].Count > server.Index)
            graph[client.Index][server.Index].W = weight;

            //}
        }

        public String ToString(bool paramBool)
        {
            String resString = "";
            int i = 0;
            foreach (List<NodeRef> row in graph)
            {
                resString += "@" + i;
                foreach (NodeRef nodoR in row)
                {
                    if (paramBool == false)
                    {
                        resString += "\t" + "(" + i + ":" + nodoR.NODO.Index + ")= " + nodoR.W;
                    }
                    else
                    {
                        if (nodoR.W > -1)
                            resString += "\t" + 1;
                        else
                            resString += "\t" + 0;
                    }
                }
                resString += System.Environment.NewLine;
                i++;
            }
            return resString;
        }

        public int Grade()
        {
            int res = 0;
            for (int i = 0; i < graph.Count(); i++)
            {
                res += GradeOfNode(graph[i][i].NODO);
            }
            return res;
        }

        public Boolean Pseudo()
        {
            Boolean res = false;
            for (int i = 0; i < graph.Count(); i++)
            {
                if (graph[i][i].W > -1)
                {
                    res = true;
                }
            }
            return res;
        }

        public int GradeOfNode(Node nodo)
        {
            int res = 0;
            int i = 0;
            int nodeIndex = nodo.Index;
            foreach (NodeRef nodeR in graph[nodeIndex])
            {
                if (nodeR.W > -1)
                {
                    if (nodeIndex == i)
                    {
                        res += 2;
                    }
                    else
                    {
                        res++;
                    }
                }
                i++;
            }
            return res;
        }

        public DirectedGrade GradeOfDirectedNode(Node nodo)
        {
            DirectedGrade res;
            int input = 0;
            int output = 0;
            foreach (List<NodeRef> row in graph)
            {
                foreach (NodeRef nodeR in row)
                {
                    if (graph.IndexOf(row) == row.IndexOf(nodeR))
                    {
                        if (nodeR.W > -1)
                        {
                            input++;
                            output++;
                        }
                    }
                    else
                    {
                        if (graph.IndexOf(row) == nodo.Index)
                        {
                            if (nodeR.W > -1)
                            {
                                output++;
                            }
                        }
                        if (row.IndexOf(nodeR) == nodo.Index)
                        {
                            if (nodeR.W > -1)
                            {
                                input++;
                            }
                        }

                    }
                }
            }
            res = new DirectedGrade(input, output);
            return res;
        }

        public Boolean Directed()
        {
            for (int i = 0; i < graph.Count(); i++)
            {
                for (int j = 0; j < graph.Count(); j++)
                {
                    if (graph[i][j].W != graph[j][i].W)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public Boolean Complete()
        {
            Boolean res = true;
            for (int i = 0; i < graph.Count(); i++)
            {
                for (int j = 0; j < graph[i].Count(); j++)
                {
                    if (graph[i][j].W < 0 && i != j)
                    {
                        res = false;
                    }
                }

            }
            return res;
        }

        public Boolean UndirectedCicled()
        {
            HashSet<int> visited = new HashSet<int>();
            for (int vertex = 0; vertex < graph.Count(); vertex++)
            {
                if (visited.Contains(vertex))
                {
                    continue;
                }
                Boolean flag = dfsU(vertex, visited, -1);
                if (flag)
                {
                    return true;
                }
            }
            return false;
        }

        public Boolean dfsU(int vertex, HashSet<int> visited, int parent)
        {
            visited.Add(vertex);
            foreach (NodeRef nodeR in graph[vertex])
            {
                if (nodeR.W > -1)
                {
                    if (nodeR.NODO.Index.Equals(parent))
                    {
                        continue;
                    }
                    if (visited.Contains(nodeR.NODO.Index))
                    {
                        return true;
                    }
                    Boolean hasCycle = dfsU(nodeR.NODO.Index, visited, vertex);
                    if (hasCycle)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public Boolean Cicled()
        {
            if (this.Directed())
                return this.directedCicled();
            else
                return this.UndirectedCicled();
        }


        //Determine if a graph is cicled with BFS algorithm.
        //watch this video: https://www.youtube.com/watch?v=rKQaZuoUR4M 
        public Boolean directedCicled()
        {
            Boolean res = false;

            HashSet<int> whiteSet = new HashSet<int>();
            HashSet<int> graySet = new HashSet<int>();
            HashSet<int> blackSet = new HashSet<int>();

            for (int i = 0; i < graph.Count(); i++)
            {
                whiteSet.Add(i);
            }

            while (whiteSet.Count() > 0)
            {
                // int current = whiteSet.First();
                int current = whiteSet.Min();
                if (dfs(current, whiteSet, graySet, blackSet))
                {
                    return true;
                }
            }
            return res;
        }

        private Boolean dfs(int currentIndex, HashSet<int> whiteS, HashSet<int> grayS, HashSet<int> blackS)
        {
            //move current to gray set from white set and then explore it.
            moveVertex(currentIndex, whiteS, grayS);
            foreach (NodeRef nodeR in graph[currentIndex])
            {
                if (nodeR.W > -1)
                {
                    //if in black set means already explored so continue.
                    if (blackS.Contains(nodeR.NODO.Index))
                    {
                        continue;
                    }
                    //if in gray set then cycle found.
                    if (grayS.Contains(nodeR.NODO.Index))
                    {
                        return true;
                    }
                    if (dfs(nodeR.NODO.Index, whiteS, grayS, blackS))
                    {
                        return true;
                    }
                }
            }
            //move vertex from gray set to black set when done exploring.
            moveVertex(currentIndex, grayS, blackS);
            return false;
        }

        private void moveVertex(int v_Index, HashSet<int> sourceSet, HashSet<int> destinationSet)
        {
            sourceSet.Remove(v_Index);
            destinationSet.Add(v_Index);
        }

        public Boolean Bipartita()
        {
            HashSet<int> whiteSet = new HashSet<int>();
            HashSet<int> blueSet = new HashSet<int>();
            HashSet<int> redSet = new HashSet<int>();
            HashSet<int> visited = new HashSet<int>();

            for (int i = 0; i < graph.Count(); i++)
            {
                whiteSet.Add(i);
            }
            moveVertex(0, whiteSet, blueSet);
            visited.Add(0);
            return Bipartita2(0, visited, blueSet, redSet, whiteSet);
        }

        public Boolean Bipartita2(int origin, HashSet<int> visited, HashSet<int> originColorSet, HashSet<int> destinationColorSet, HashSet<int> whiteSet)
        {
            foreach (NodeRef nodeR in graph[origin])
            {
                if (nodeR.W > -1)
                {
                    if (!visited.Contains(nodeR.NODO.Index))
                    {
                        // mark present vertic as visited 
                        visited.Add(nodeR.NODO.Index);

                        // mark its color opposite to its parent 
                        this.moveVertex(nodeR.NODO.Index, whiteSet, destinationColorSet);

                        // if the subtree rooted at vertex v is not bipartite 
                        if (Bipartita2(nodeR.NODO.Index, visited, destinationColorSet, originColorSet, whiteSet))
                            return false;
                    }
                    else
                     if (originColorSet.Contains(nodeR.NODO.Index) && originColorSet.Contains(origin))
                        return false;
                }
            }
            return true;
        }
        public Boolean Bip()
        {
            if (graph.Count() > 0)
            {
                // Create a color array to store  
                // colors assigned to all veritces. 
                // Vertex number is used as index  
                // in this array. The value '-1' 
                // of colorArr[i] is used to indicate  
                // that no color is assigned 
                // to vertex 'i'. The value 1 is  
                // used to indicate first color 
                // is assigned and value 0 indicates  
                // second color is assigned. 
                int[] colorArr = new int[graph.Count()];
                for (int i = 0; i < graph.Count(); ++i)
                    colorArr[i] = -1;

                // Assign first color to source 
                colorArr[0] = 1;

                // Create a queue (FIFO) of vertex numbers  
                // and enqueue source vertex for BFS traversal 
                List<int> q = new List<int>();
                q.Add(0);

                // Run while there are vertices 
                // in queue (Similar to BFS) 
                while (q.Count != 0)
                {
                    // Dequeue a vertex from queue 
                    int u = q[0];
                    q.RemoveAt(0);

                    // Return false if there is a self-loop  
                    if (graph[u][u].W > -1)
                        return false;

                    // Find all non-colored adjacent vertices 
                    for (int v = 0; v < graph.Count(); ++v)
                    {
                        // An edge from u to v exists  
                        // and destination v is not colored 
                        if (graph[u][v].W > -1 && colorArr[v] == -1)
                        {
                            // Assign alternate color  
                            // to this adjacent v of u 
                            colorArr[v] = 1 - colorArr[u];
                            q.Add(v);
                        }

                        // An edge from u to v exists and  
                        // destination v is colored with 
                        // same color as u 
                        else if (graph[u][v].W > -1 &&
                                 colorArr[v] == colorArr[u])
                            return false;
                    }
                }

                for (int i = 0; i < graph.Count(); i++)
                {
                    for (int j = 0; j < graph.Count(); j++)
                    {
                        if (this.Directed() == true)
                        {
                            if (this.GradeOfDirectedNode(graph[i][j].NODO).Total == 0)
                            {
                                return false;
                            }
                        }
                        else
                        {
                            if (this.GradeOfNode(graph[i][j].NODO) == 0)
                            {
                                return false;
                            }
                        }
                        if (graph[i][i].W > -1)
                        {
                            return false;
                        }
                    }
                }
                // If we reach here, then all adjacent vertices 
                // can be colored with alternate color 
                return true;
            }
            else
            {
                return false;
            }
        }
        /******************************************************************************************************************
         * 
         * STARTSTARTSTARTSTARTSTARTSTARTSTARTSTARTSTARTSTARTSTARTSTARTSTARTSTARTSTARTSTARTSTARTSTARTSTARTSTARTSTARTSTARTSTART
         * 
         *                         ----- START OF ALGORITHM OF ISOMORFISM -----
         *                                    
         * STARTSTARTSTARTSTARTSTARTSTARTSTARTSTARTSTARTSTARTSTARTSTARTSTARTSTARTSTARTSTARTSTARTSTARTSTARTSTARTSTARTSTARTSTART
         * 
         ********************************************************************************************************************/


        /********************************************************************************************
         * 
         * 
         *      ISOMORPHISM :algorithm of tranpose matrix.
         * 
         * 
         * *******************************************************************************************/
        public Boolean Isom_Traspuesta(AdjacencyList other)
        {
            //Boolean res = false;              

            if (heuristicIsom(other))
            {
                PermutationSetStruct gradePairs;
                gradePairs = heuristicIsom_SEC_FASE(other);

                if (other.GRAPH.Count() < 1 && this.GRAPH.Count() < 1)//
                {
                    return true;
                }
                else
                {
                    if (this.GRAPH.Equals(other.GRAPH))//si los grafos son iguales retorna true.
                    {
                        return true;
                    }
                    else
                    {
                        if (gradePairs.validateSet())
                        {
                            return Isom_Traspuesta_Algorithm(other, gradePairs);
                        }
                    }
                }
            }
            return false;
        }
        public Boolean Isom_Traspuesta_Algorithm(AdjacencyList other, PermutationSetStruct gradePairs)//Algorithm
        {
            int limitOfPermutations = 40000;
            Matrix thisMatrix = this.toMatrix();
            Matrix otherMatrix = other.toMatrix();

            Matrix permutationMatrix = new Matrix();
            Matrix permutationMatrixTrans = new Matrix();

            Matrix resMatrixOperation = new Matrix();
            ListOfPerPairLists listOfPerPairLists = new ListOfPerPairLists(gradePairs);
            PermutationPairList aux;

            while (limitOfPermutations > 0 && listOfPerPairLists.PER_ALFA_LIST.Count() > 0)
            {
                //get the next permutation:
                aux = listOfPerPairLists.PER_ALFA_LIST.ElementAt(0);
                listOfPerPairLists.PER_ALFA_LIST.RemoveAt(0);

                aux.toMatrixOfPermutationB(ref permutationMatrix, ref permutationMatrixTrans);//make the permutation matrix and the transpose.

                resMatrixOperation = permutationMatrix.MatrixProduct(otherMatrix);
                resMatrixOperation = resMatrixOperation.MatrixProduct(permutationMatrixTrans);

                if (thisMatrix.Equals(resMatrixOperation))
                {
                    return true;
                }
                limitOfPermutations--;
            }
            return false;
        }

        /********************************************************************************************
        * 
        * 
        *      ISOMORPHISM : First algorithm of isimorphism. Implemented by me... ñ_ñ 
        * 
        * 
        * *******************************************************************************************/

        public Boolean Isom_Fuerza_Bruta(AdjacencyList other)
        {
            //Boolean res = false;              
            if (heuristicIsom(other))
            {
                PermutationSetStruct gradePairs;
                gradePairs = heuristicIsom_SEC_FASE(other);

                if (other.GRAPH.Count() < 1 && this.GRAPH.Count() < 1)//
                {
                    return true;
                }
                else
                {
                    if (this.GRAPH.Equals(other.GRAPH))//si los grafos son iguales retorna true.
                    {
                        return true;
                    }
                    else
                    {
                        if (gradePairs.validateSet())
                        {
                            return Isom_Fuerza_Bruta_Algorithm(other, gradePairs);
                        }
                    }
                }
            }//END of the heuristic.
            return false;
        }

        public Boolean Isom_Fuerza_Bruta_Algorithm(AdjacencyList other, PermutationSetStruct gradePairs)
        {

            int limitOfPermutations = 40000;
            Matrix thisMatrix = this.toMatrix();
            Matrix otherMatrix = other.toMatrix();

            bool band = false;

            List<int[]> listOfPermutations = gradePairs.makeAllPermutationsPure();
            int[] permutationArray;

            while (limitOfPermutations > 0 && listOfPermutations.Count() > 0 && !band)
            {
                //get the next permutation:
                permutationArray = listOfPermutations.ElementAt(0);
                listOfPermutations.RemoveAt(0);

                band = true;
                int j;
                for (j = 0; j < permutationArray.Length; j++)
                {
                    for (int i = 0; i < permutationArray.Length; i++)
                    {
                        if (otherMatrix.MATRIX[j, i] != thisMatrix.MATRIX[permutationArray[j], permutationArray[i]])
                        {
                            band = false;
                            continue;
                        }
                    }
                    if (!band)
                    {
                        continue;
                    }
                }
                limitOfPermutations--;
            }//END(while).     

            return band;
        }

        /********************************************************************************************
        * 
        * 
        *      ISOMORPHISM : In the manual.
        * 
        * 
        * *******************************************************************************************/
        public Boolean Isom_Inter(AdjacencyList other)
        {
            if (heuristicIsom(other))
            {
                PermutationSetStruct gradePairs;
                gradePairs = heuristicIsom_SEC_FASE(other);

                if (other.GRAPH.Count() < 1 && this.GRAPH.Count() < 1)//
                {
                    return true;
                }
                else
                {
                    if (this.GRAPH.Equals(other.GRAPH))//si los grafos son iguales retorna true.
                    {
                        return true;
                    }
                    else
                    {
                        if (gradePairs.validateSet())
                        {
                            return Isom_Inter_Algorithm(other);
                        }
                    }
                }
            }//END of the heuristic.
            return false;
        }

        public Boolean Isom_Inter_Algorithm(AdjacencyList other)
        {
            Matrix thisMatrix = this.toMatrix();
            Matrix otherMatrix = other.toMatrix();

            List<int> colIndexOne;
            List<int> sumEachRow = new List<int>();
            int sumCol = 0;

            List<int> colIndex_Two = new List<int>();
            int sumaCol_Two = 0;
            List<int> sumEachRow_Two = new List<int>();

            int sumaRen;

            for (int iteratorInt = 0; iteratorInt < this.GRAPH.Count() - 1; iteratorInt++)
            {
                colIndexOne = new List<int>();
                sumCol = 0;
                sumEachRow = new List<int>();

                for (int j = 0; j < this.GRAPH.Count(); j++)
                {
                    if (thisMatrix.MATRIX[j, iteratorInt] == 1)
                    {
                        colIndexOne.Add(j);
                        sumCol++;
                        sumEachRow.Add(0);
                    }
                }

                int inte = 0;
                foreach (int index in colIndexOne)
                {
                    for (int j = 0; j < this.GRAPH.Count(); j++)
                    {
                        sumEachRow[inte] += thisMatrix.MATRIX[index, j];
                    }
                    inte++;
                }

                //EN EL SEGUNDO GRAFO:
                int iteratorColSec = iteratorInt + 1;

                for (int i = iteratorColSec; i < other.GRAPH.Count(); i++)
                {
                    colIndex_Two = new List<int>();
                    sumaCol_Two = 0;
                    sumEachRow_Two = new List<int>();

                    for (int j = 0; j < other.GRAPH.Count(); j++)
                    {
                        if (otherMatrix.MATRIX[j, iteratorColSec] == 1)
                        {
                            colIndex_Two.Add(j);
                            sumaCol_Two++;
                            sumEachRow_Two.Add(0);
                        }
                    }

                    inte = 0;
                    foreach (int index in colIndex_Two)
                    {
                        for (int j = 0; j < this.GRAPH.Count(); j++)
                        {
                            if (index < this.GRAPH.Count() && inte < sumEachRow.Count())
                                sumEachRow[inte] += thisMatrix.MATRIX[index, j];
                        }
                        inte++;
                    }

                    if (sumaCol_Two == sumCol && colIndexOne.Count() == colIndex_Two.Count())
                    {
                        sumEachRow.Sort();
                        sumEachRow_Two.Sort();
                        if (sumEachRow.Count() == sumEachRow_Two.Count())
                        {
                            int k;
                            for (k = 0; k < colIndexOne.Count(); k++)
                            {
                                if (sumEachRow[k] != sumEachRow_Two[k])
                                {
                                    continue;
                                }
                            }
                            if (k < colIndexOne.Count())
                            {
                                continue;
                            }

                            otherMatrix.interchangeRC(ref otherMatrix, otherMatrix.MATRIX.GetLength(0), iteratorInt, i);
                            if (otherMatrix.Equals(thisMatrix))
                            {
                                return true;
                            }
                        }
                        else
                        {
                            continue;
                        }

                    }
                    else
                    {
                        continue;
                    }
                }
            }



            return false;
        }

        /******************************************************************************************************************
        * 
        * ENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDEND
        * 
        *                              ----- END OF ALGORITHM OF ISOMORFISM -----
        *                                    
        * ENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDEND
        * 
        ********************************************************************************************************************/
        private Boolean heuristicIsom(AdjacencyList other)
        {
            if (this.GRAPH.Count() == other.GRAPH.Count())
            {
                if (this.Grade() == other.Grade())
                    if (this.Directed() == other.Directed())
                        if (this.Complete() == other.Complete())
                            if (this.Pseudo() == other.Pseudo())
                                if (this.Cicled() == other.Cicled())
                                    if (this.Bip() == other.Bip())
                                        return true;
            }
            return false;
        }

        private PermutationSetStruct heuristicIsom_SEC_FASE(AdjacencyList other)
        {
            PermutationSetStruct res = new PermutationSetStruct(other.GRAPH.Count());
            int grade_T;
            int grade_O;

            for (int i = 0; i < other.GRAPH.Count(); i++)
            {
                grade_T = this.GradeOfNode(this.GRAPH[i][i].NODO);
                res.addIndex_T(grade_T, this.GRAPH[i][i].NODO.Index);

                grade_O = other.GradeOfNode(other.GRAPH[i][i].NODO);
                res.addIndex_O(grade_O, other.GRAPH[i][i].NODO.Index);
            }
            return res;
        }

        public List<int> neighborList(List<NodeRef> row)
        {
            List<int> res = null;
            for (int i = 0; i < row.Count(); i++)
            {
                if (row[i].W > -1)
                {
                    res.Add(row[i].NODO.Index);
                }
            }
            return res;
        }

        public List<NodeRef> neighborListRef(List<NodeRef> row)
        {
            List<NodeRef> res = new List<NodeRef>();
            for (int i = 0; i < row.Count(); i++)
            {
                if (row[i].W > -1)
                {
                    res.Add(row[i]);
                }
            }
            return res;
        }

        public List<Node> neighborListNode(Node workingNode)
        {
            List<Node> res = new List<Node>();
            for (int i = 0; i < graph[workingNode.Index].Count(); i++)
            {
                if (graph[workingNode.Index][i].W > -1)
                {
                    res.Add(graph[workingNode.Index][i].NODO);

                }
            }
            return res;

        }

        public List<Node> neighborListNodeNoVisited(Node workingNode)
        {
            List<Node> res = new List<Node>();
            for (int i = 0; i < graph[workingNode.Index].Count(); i++)
            {
                if (graph[workingNode.Index][i].W > -1 && graph[workingNode.Index][i].NODO.Visitado == false)
                {
                    res.Add(graph[workingNode.Index][i].NODO);

                }
            }
            return res;

        }

        public List<Node> notVisitedList()
        {
            List<Node> res = new List<Node>();
            foreach(Node node in listOfNodes_IG)
            {
                if(node.Visitado == false)
                {
                    res.Add(node);
                }
            }
            return res;
          }

        public void restoreNotVisited(List<Node> notVisitedYet)
        {
            foreach(Node node in listOfNodes_IG)
            {
                if(notVisitedYet.Contains(node))
                {
                    node.Visitado = false;
                }
                else
                {
                    node.Visitado = true;
                }
            }
        }

        public List<Node> listOfconectedNodes()//nodes that have at least one conection.
            {
            List<Node> resList = new List<Node>();
            for(int i = 0; i < graph.Count(); i++)
            {
                if(graph[i][i].W > -1)
                {
                    resList.Add(graph[i][i].NODO);
                }
            }
            return resList;

            }

        public Boolean isACutNodeBool(Node node)
        {
            node.Visitado = true;             
            isConected();
            if (allVisitedExept(node))
            {
                return true;
            }
            return false;
        }

        public Boolean redeiPAthUtil(Node workingNode)
        {
            foreach(Node node in this.listOfNodes_IG)
            {
                if(!this.neighborListNode(workingNode).Contains(node))
                {
                    if(this.neighborListNode(workingNode).Count() 
                        + this.neighborListNode(node).Count() 
                        < this.listOfNodes_IG.Count()-1)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public Boolean redeiCycleUtil(Node workingNode)
        {
            foreach (Node node in this.listOfNodes_IG)
            {
                if (!this.neighborListNode(workingNode).Contains(node))
                {
                    if (this.neighborListNode(workingNode).Count()
                        + this.neighborListNode(node).Count()
                        < this.listOfNodes_IG.Count())
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public Boolean redeiPAth()
        {
            foreach (Node node in this.listOfNodes_IG)
            {
                if (!redeiPAthUtil(node))//if any does not do the redei
                {
                    return false;
                }
            }
            return true;
        }
        public Boolean redeiCycle(Node workingNode)
        {
            foreach (Node node in this.listOfNodes_IG)
            {
                if (!this.neighborListNode(workingNode).Contains(node))
                {
                    if (this.neighborListNode(workingNode).Count()
                        + this.neighborListNode(node).Count()
                        < this.listOfNodes_IG.Count())
                    {
                        return false;
                    }
                }
            }
            return true;
        }



        public Boolean isConected()
        {
            // Mark all the vertices as not visited 
            markAllLikeNotVisited();

            // Start DFS traversal from a vertex with non-zero degree 
            DFSUtilAllConected(this.LIST_NODES[0]);

            // Check if all non-zero degree vertices are visited 
            foreach (Node node in this.LIST_NODES)
            {
                if (node.Visitado == false)
                {
                    return false;
                }
            }

            return true;
        }


        void DFSUtilAllConected(Node workingNode/*int v, bool visited[]*/)
        {
            // Mark the current node as visited
            workingNode.Visitado = true;


            // Recur for all the vertices adjacent to this vertex
            foreach (Node node in this.neighborListNode(workingNode))
            {
                if (node.Visitado == false)
                {
                    DFSUtilAllConected(node);
                }
            }
        }



        public void markIsolateNodesAsVisited()
        {
            List < Node > conectedNodes = listOfconectedNodes();
            foreach(Node node in listOfNodes_IG)//each node 
            { 
              if(!conectedNodes.Contains(node))//if the node is out of the list of conectedNodes
                {
                    node.Visitado = true;
                }
            }
        }







        public void allBlack()
        {
            foreach(Edge edge in listOfEdges_IG)
            {
                edge.COLOR = Color.Black;
            }
            foreach(Node node in listOfNodes_IG)
            {
                node.COLOR = Color.Black;
            }
        }


        public void eliminateNexetEdges(Node node)
        {
            List<Edge> newEdges = new List<Edge>();

            foreach (Edge edge in this.listOfEdges_IG)
            {
                if (edge.Client != node && edge.Server != node)
                {
                    newEdges.Add(edge);
                }
            }
           this.listOfEdges_IG =  newEdges;
        }








        public void markAllLikeVisited()
        {
            for (int j = 0; j < graph.Count(); j++)
                for (int i = 0; i < graph.Count(); i++)
                {
                    graph[j][i].NODO.Visitado = true;
                }
        }

        public void markAllNodesLikeNotVisited()
        {
            foreach(Node node in this.listOfNodes_IG)
            {
                node.Visitado = false;
            }
        }

        public Boolean allNodesVisitedBool()
        {
            foreach(Node node in listOfNodes_IG)
            {
                if (node.Visitado == false)
                    return false;
            }
            return true;
        }

        public void markAllEdgesLikeNotVisited()
        {
            foreach (Edge edge in listOfEdges_IG)
            {
                edge.visitada = false;
            }
        }

        public void markAllNodeAndEdgesNotVisited()
        {
            markAllNodesLikeNotVisited();
            markAllEdgesLikeNotVisited();

        }

        

        public void markAllLikeNotVisited(int code)
        {
            markAllLikeNotVisited();
            foreach(Edge edge in listOfEdges_IG)
            {
                edge.visitada = false;
            }
        }

        public void markAllLikeNotBridge()
        {
            foreach(Edge edge in this.listOfEdges_IG)
            {
                edge.Bridge = false;
            }
        }

        public AdjacencyList clone()
        {
            List<List<NodeRef>> graphEno = new List<List<NodeRef>>();
            List<Node> listOfNodes = new List<Node>();
            List<Edge> listOfEdges = new List<Edge>();

        foreach(Edge edge in listOfEdges_IG)
            {
                listOfEdges.Add(edge);
            }
        foreach(Node node in listOfNodes_IG)
            {
                listOfNodes.Add(node);
            }
            graphEno = this.graph;
            return (new AdjacencyList(graphEno, listOfNodes, listOfEdges));
    }

        public void markAllLikeNotVisited()
        {
            for (int j = 0; j < graph.Count(); j++)
                for (int i = 0; i < graph.Count(); i++)
                {
                    graph[j][i].NODO.Visitado = false;
                    graph[j][i].NODO.COLOR = Color.Black;
                }
        }

        public void markAsVisited_T_F(int index, Boolean mark)
        {
            graph[index][index].NODO.Visitado = mark;
        }


        public Matrix toMatrix()
        {
            Matrix res;
            int[,] toDoMatrix = new int[graph.Count(), graph.Count()];

            for (int j = 0; j < graph.Count(); j++)
                for (int i = 0; i < graph.Count(); i++)
                {
                    if (graph[j][i].W > -1)
                        toDoMatrix[j, i] = 1;
                    else
                        toDoMatrix[j, i] = 0;
                }
            res = new Matrix(toDoMatrix);
            return res;
        }

        /******************************************************************************************************************
        * 
        * STARTSTARTSTARTSTARTSTARTSTARTSTARTSTARTSTARTSTARTSTARTSTARTSTARTSTARTSTARTSTARTSTARTSTARTSTARTSTARTSTARTSTARTSTART
        * 
        *                         ----- START OF PATHS AND CIRCUITS -----
        *                                    
        * STARTSTARTSTARTSTARTSTARTSTARTSTARTSTARTSTARTSTARTSTARTSTARTSTARTSTARTSTARTSTARTSTARTSTARTSTARTSTARTSTARTSTARTSTART
        * 
        ********************************************************************************************************************/


        /******************************************************************************************************************
       * 
       * ENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDEND
       * 
       *                              ----- END OF PATHS AND CIRCUITS -----
       *                                    
       * ENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDENDEND
       * 
       ********************************************************************************************************************/


    }//AdjacencyList(END).    
}
