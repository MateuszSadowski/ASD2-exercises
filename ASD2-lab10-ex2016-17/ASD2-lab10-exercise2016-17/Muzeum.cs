using System;
using System.Collections.Generic;
using System.Linq;
using ASD.Graphs;

namespace Lab9
{
public struct MuseumRoutes
    {
        public MuseumRoutes(int count, int[][] routes)
            {
            this.liczba = count;
            this.trasy = routes;
            }

        public readonly int liczba;
        public readonly int[][] trasy;
    }


static class Muzeum
    {
        /// <summary>
        /// Znajduje najliczniejszy multizbiór tras
        /// </summary>
        /// <returns>Znaleziony multizbiór</returns>
        /// <param name="g">Graf opisujący muzeum</param>
        /// <param name="cLevel">Tablica o długości równej liczbie wierzchołków w grafie -- poziomy ciekawości wystaw</param>
        /// <param name="entrances">Wejścia</param>
        /// <param name="exits">Wyjścia</param>
        public static MuseumRoutes FindRoutes(Graph g, int[] cLevel, int[] entrances, int[] exits)
            {
            Graph seekFlowGraph = BuildSeekFlowGraph(g, cLevel, entrances, exits);

            //var ge = new GraphExport();
            //ge.Export(g);
            //ge.Export(seekFlowGraph);

            var maxFlow = MaxFlowGraphExtender.FordFulkersonDinicMaxFlow(seekFlowGraph, 0, 1, MaxFlowGraphExtender.OriginalDinicBlockingFlow);

            return new MuseumRoutes((int)maxFlow.value, null);
            }

        internal static Graph BuildSeekFlowGraph(Graph g, int[] cLevel, int[] entrances, int[] exits)
        {
            int n = g.VerticesCount;
            Graph h = g.IsolatedVerticesGraph(true, 2 * n + 2);

            foreach(int entrance in entrances)  //Attach super-source to entrances
                h.AddEdge(0, 2 + entrance, Int32.MaxValue);

            foreach (int exit in exits) //Attach super-targer to exits
                h.AddEdge(2 + n + exit, 1, Int32.MaxValue);

            for (int v = 0; v < n; v++) //Attach room to curiosity
                h.AddEdge(2 + v, 2 + n + v, cLevel[v]);

            for (int v = 0; v < n; v++) //Attach room to room
                foreach (var edge in g.OutEdges(v))
                    h.AddEdge(2 + n + v, 2 + edge.To, Int32.MaxValue);

            return h;
        }
    }
}

