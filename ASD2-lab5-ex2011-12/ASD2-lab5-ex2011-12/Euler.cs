using System;

namespace ASD.Graphs
{
using System.Linq;  // potrzebne dla metody First rozszerzającej interfejs IEnumerable

/// <summary>
/// Rozszerzenie interfejsu <see cref="IGraph"/> o wyszukiwanie ścieżki Eulera
/// </summary>
public static class EulerPathGraphExtender
    {

    /// <summary>
    /// Znajduje scieżkę Eulera w grafie
    /// </summary>
    /// <param name="g">Badany graf</param>
    /// <param name="ec">Znaleziona ścieżka (parametr wyjściowy)</param>
    /// <returns>Informacja czy ścieżka Eulera istnieje</returns>
    /// <remarks>
    /// Jeśli w badanym grafie nie istnieje ścieżka Eulera metoda zwraca <b>false</b>, parametr <i>ec</i> ma wówczas wartość <b>null</b>.<br/>
    /// <br/>
    /// Metoda nie modyfikuje badanego grafu.<br/>
    /// <br/>
    /// Metoda implementuje algorytm Fleury'ego.
    /// </remarks>
    public static bool Lab04_Euler(this Graph g, out Edge[] ec)
        {
            // tylko cykl     - 2 pkt
            // cykl i sciezka - 3 pkt

            //check if graph has euler cycle or path
            int numOfOddVertices = 0, cc = 0;
            int oddVertex = -1;

            Predicate<int> preVertex = delegate(int vertex)
            {
                if(g.OutDegree(vertex) % 2 != 0)
                {
                    numOfOddVertices++;
                    oddVertex = vertex;
                    if (numOfOddVertices > 2)
                        return false;
                }

                return true;
            };

            GeneralSearchGraphExtender.GeneralSearchAll<EdgesStack>(g, preVertex, null, null, out cc);

            if(cc != 1 || numOfOddVertices > 2)
            {
                ec = null;
                return false;
            }

            Graph h = g.Clone();

            //find euler cycle
            //if(0 == numOfOddVertices)
            //{
                EdgesStack euler = new EdgesStack();
                EdgesStack tmp = new EdgesStack();

                int v = -1 == oddVertex ? 0 : oddVertex;  //h.OutDegree(0) > 0, if start with oddVertex algorithm finds euler path
                tmp.Put(new Edge(v, v));    //dummy edge to remove later
                while(!tmp.Empty)
                {
                    v = tmp.Peek().To;
                    if(h.OutDegree(v) > 0)
                    {
                        foreach (var e in h.OutEdges(v))
                        {
                            tmp.Put(e);
                            h.DelEdge(e);
                            break;
                        }
                    }
                    else
                    {
                        euler.Put(tmp.Get());
                    }
                }

                euler.Get();    //dummy edge

                ec = new Edge[euler.Count];
                for (int i = 0; i < ec.Length; i++)
                {
                    ec[i] = euler.Get();
                }
                return true;
            //}

        /*
        Algorytm Fleury'ego

        utworz pusty stos krawedzi Euler
        utworz pusty stos krawedzi pom
        w = dowolny wierzcholek grafu
        umiesc na stosie pom sztuczna krawedz <w,w>
        dopoki pom jest niepusty powtarzaj
            w = wierzch. koncowy krawedzi ze szczytu stosu pom (bez pobierania krawedzi ze stosu)
            jesli stopien wychodzacy w > 0 
                e = dowolna krawedz wychodzaca z w
                umiesc krawedz e na stosie pom      
                usun krawedz e z grafu
            w przeciwnym przypadku
                pobiez szczytowy element ze stosu pom i umiesc go na stosie Euler
        usun ze stosu Euler sztuczna krawedz (petle) startowa (jest na szczycie)

        wynik: krawedzie tworzace cykl sa na stosie Euler
        
        Uwaga: powyzszy algorytm znajduje cykl Eulera (jesli istnieje),
               aby znalezc sciezke nalezy najpierw wyznaczyc wierzcholek startowy
               (nie mozna wystartowac z dowolnego)
        */
        }

    }  // class EulerGraphExtender

}  // namespace ASD.Graph
