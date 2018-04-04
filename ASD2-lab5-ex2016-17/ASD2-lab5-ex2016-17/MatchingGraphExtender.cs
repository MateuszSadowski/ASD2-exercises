using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ASD.Graphs;

namespace ASD
{
    public static class MatchingGraphExtender
    {
        /// <summary>
        /// Podział grafu na cykle. Zakładamy, że dostajemy graf nieskierowany i wszystkie wierzchołki grafu mają parzyste stopnie
        /// (nie trzeba sprawdzać poprawności danych).
        /// </summary>
        /// <param name="G">Badany graf</param>
        /// <returns>Tablica cykli; krawędzie każdego cyklu powinny być uporządkowane zgodnie z kolejnością na cyklu, zaczynając od dowolnej</returns>
        /// <remarks>
        /// Metoda powinna działać w czasie O(m)
        /// </remarks>
        public static Edge[][] cyclePartition(this Graph G)
        {
            List<Edge[]> cycles = new List<Edge[]>();
            Graph h = G.Clone();

            bool[] visited = new bool[h.VerticesCount];
            EdgesStack edges = new EdgesStack();
            int lastVertex = -1;

            while (h.EdgesCount > 0)
            {
                // Jesli zaczynamy szukac nowy cykl to:
                // - znajdz dowolny wierzcholek ktory ma jakies krawedzie
                // Wpp kontynuujemy z lastVertex
                int v = lastVertex;
                if (v == -1)
                {
                    visited = new bool[h.VerticesCount];
                    edges = new EdgesStack();

                    for (int i = 0; i < h.VerticesCount; i++)
                    {
                        if (h.OutDegree(i) > 0)
                        {
                            v = i;
                            break;
                        }
                    }
                }

                // v - wierzcholek, ktory ma co najmniej 1 krawedz
                // Wezmy pierwsza krawedz
                visited[v] = true;
                Edge edge;

                foreach (Edge e in h.OutEdges(v))   //TODO: WHY FOREACH?
                {
                    // Usuwamy z grafu pierwsza krawedz i wrzucamy na stos
                    h.DelEdge(e);
                    edges.Put(e);

                    // Napotkalismy cykl
                    if (visited[e.To])
                    {
                        Edge[] cycle = new Edge[edges.Count];
                        Edge firstEdge = edges.Get();
                        int i = 0;
                        cycle[i++] = firstEdge;

                        while (!edges.Empty)
                        {
                            Edge e2 = edges.Get();
                            cycle[i++] = e2;
                            visited[e2.From] = visited[e2.To] = false;

                            if (e2.From == firstEdge.To)
                                break;
                        }

                        Array.Resize(ref cycle, i);
                        Array.Reverse(cycle);
                        cycles.Add(cycle);

                        // Czy wierzcholek na ktorym skonczylismy ma jeszcze jakies krawedzie
                        if (h.OutDegree(e.To) > 0)
                            lastVertex = e.To;
                        else
                            lastVertex = -1;
                    }
                    else
                        lastVertex = e.To;

                    break;
                }
            }

            Edge[][] ret = new Edge[cycles.Count][];
            int count = cycles.Count;
            for (int i = 0; i < count; i++)
            {
                ret[i] = cycles.Last();
                cycles.RemoveAt(cycles.Count - 1);
            }
            return ret;

            //TODO: find the efficient method
            //Graph g = G.Clone();
            //Stack<Edge[]> cycles = new Stack<Edge[]>();
            //Edge[] cycle;

            //while(FindCycle(g, out cycle))
            //{
            //    cycles.Push(cycle);

            //    foreach (var e in cycle)
            //    {
            //        g.DelEdge(e);
            //    }
            //}

            //Edge[][] result = new Edge[cycles.Count][];

            //int i = 0;
            //while(cycles.Count > 0)
            //{
            //    result[i++] = cycles.Pop();
            //}

            //return result;
        }

        public static bool FindCycle(this Graph g, out Edge[] cycle)
        {
            int[] visited = new int[g.VerticesCount]; // 0 - nieodwiedzony, 1 - szary, 2 - czarny
            int[] from = new int[g.VerticesCount];
            EdgesStack edges = new EdgesStack();
            cycle = new Edge[g.VerticesCount];
            bool hasCycle = false;
            int cycleEnd = -1;
            int cc;

            Predicate<int> preVertex = delegate (int v)
            {
                visited[v] = 1; // szary
                return true;
            };

            Predicate<int> postVertex = delegate (int v)
            {
                visited[v] = 2; // czarny

                if (!edges.Empty)
                    edges.Get();

                return true;
            };

            Predicate<Edge> visitEdge = delegate (Edge e)
            {
                if (!g.Directed && from[e.From] == e.To)
                    return true;

                from[e.To] = e.From;
                edges.Put(e);

                if (visited[e.To] == 1)
                {
                    hasCycle = true;
                    cycleEnd = e.To;
                    return false;
                }

                return true;
            };

            g.GeneralSearchAll<EdgesStack>(preVertex, postVertex, visitEdge, out cc);

            if (hasCycle)
            {
                int i = 0;
                bool stop = false;

                while (!edges.Empty)
                {
                    Edge e = edges.Get();

                    if (!stop)
                        cycle[i++] = e;

                    if (!edges.Empty && edges.Peek().To == cycleEnd)
                    {
                        stop = true;
                        continue;
                    }
                }

                Array.Resize<Edge>(ref cycle, i);
                Array.Reverse(cycle);

                return true;
            }

            cycle = null;
            return false;
        }

        /// <summary>
        /// Szukanie skojarzenia doskonałego w grafie nieskierowanym o którym zakładamy, że jest dwudzielny i 2^r-regularny
        /// (nie trzeba sprawdzać poprawności danych)
        /// </summary>
        /// <param name="G">Badany graf</param>
        /// <returns>Skojarzenie doskonałe w G</returns>
        /// <remarks>
        /// Metoda powinna działać w czasie O(m), gdzie m jest liczbą krawędzi grafu G
        /// </remarks>
        public static Graph perfectMatching(this Graph G)
        {
            while(G.OutDegree(0) != 1)  //TODO: where did it come from??
            {
                Edge[][] cycles = cyclePartition(G);
                foreach (var cycle in cycles)
                {
                    for (int i = 0; i < cycle.Length; i += 2)
                    {
                        G.DelEdge(cycle[i]);
                    }
                }
            }

            return G;
        }
    }
}
