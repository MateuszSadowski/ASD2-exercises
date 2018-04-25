
namespace ASD.Graphs
{

/// <summary>
/// Rozszerzenie interfejsu <see cref="IGraph"/> o wyznaczanie minimalnego drzewa rozpinającego algorytmem Kruskala
/// </summary>
public static class KruskalGraphExtender
    {

    /// <summary>
    /// Wyznacza minimalne drzewo rozpinające grafu algorytmem Kruskala
    /// </summary>
    /// <param name="g">Badany graf</param>
    /// <param name="mst">Wyznaczone drzewo rozpinające (parametr wyjściowy)</param>
    /// <returns>Waga minimalnego drzewa rozpinającego</returns>
    /// <remarks>
    /// Dla grafu skierowanego metoda zgłasza wyjątek <see cref="System.ArgumentException"/>.<br/>
    /// Wyznaczone drzewo reprezentowane jast jako graf bez cykli, to umożliwia jednolitą obsługę sytuacji
    /// gdy analizowany graf jest niespójny, wyzmnaczany jest wówczas las rozpinający.
    /// </remarks>
    public static int Lab04_Kruskal(this Graph g, out Graph mst)
        {
            // 1 pkt

            // wykorzystac klase UnionFind z biblioteki Graph
            if (g.Directed) throw new System.ArgumentException("Directed graphs are not allowed");
            Graph t = g.IsolatedVerticesGraph();
            UnionFind uf = new UnionFind(g.VerticesCount);
            EdgesMinPriorityQueue q = new EdgesMinPriorityQueue();
            int TotalWeight = 0;

            //KRUSKAL
            //for (int v = 0; v < g.VerticesCount; v++)
            //{
            //    foreach (var e in g.OutEdges(v))
            //    {
            //        q.Put(e);
            //    }
            //}

            //while (!q.Empty && t.EdgesCount < t.VerticesCount - 1)
            //{
            //    Edge e = q.Get();
            //    if (uf.Find(e.From) != uf.Find(e.To))
            //    {
            //        t.AddEdge(e);
            //        uf.Union(e.From, e.To);
            //        TotalWeight += (int)e.Weight;
            //    }
            //}

            //BORUVKA
            bool change;
            do
            {
                change = false;
                for (int i = 0; i < g.VerticesCount; i++)
                {
                    Edge MinEdge = new Edge(i, int.MaxValue, int.MaxValue);
                    bool find = false;

                    foreach (Edge e in g.OutEdges(i))
                        if ((e.Weight < MinEdge.Weight || (e.Weight == MinEdge.Weight && e.To < MinEdge.To)) && uf.Find(e.To) != uf.Find(e.From))
                        {
                            MinEdge = e;
                            find = true;
                        }

                    if (find) q.Put(MinEdge);
                }

                while (!q.Empty)
                {
                    Edge e = q.Get();
                    if (uf.Find(e.To) != uf.Find(e.From))
                    {
                        uf.Union(e.To, e.From);
                        t.AddEdge(e);
                        TotalWeight += (int)e.Weight;
                        change = true;
                    }
                }
            } while (change);

            mst = t;
        return TotalWeight;
        }

    }  // class KruskalGraphExtender

}  // namespace ASD.Graph
