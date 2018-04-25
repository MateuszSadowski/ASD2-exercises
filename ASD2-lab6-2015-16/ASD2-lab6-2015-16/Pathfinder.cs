using System;
using System.Linq;
using ASD.Graphs;

namespace lab06
{
    class Pathfinder
    {
        static int noEdgePenalty = 10000;

        Graph RoadsGraph;
        int[] CityCosts;
        GraphExport graphExport;

        public Pathfinder(Graph roads, int[] cityCosts)
        {
            RoadsGraph = roads;
            CityCosts = cityCosts;
            graphExport = new GraphExport();
        }

        //uwagi do wszystkich części (graf najkrótszych ścieżek)
        //   Jeżeli nie ma ścieżki pomiędzy miastami A i B to tworzymy sztuczną krawędź od A do B o karnym koszcie 10 000.

        // return: tablica kosztów organizacji ŚDM dla poszczególnym miast gdzie
        // za koszt organizacji ŚDM uznajemy sumę kosztów dostania się ze wszystkim miast do danego miasta, bez uwzględnienia kosztów przechodzenia przez miasta.
        // minCost: najmniejszy koszt
        // paths: graf skierowany zawierający drzewo najkrótyszch scieżek od wszyskich miast do miasta organizującego ŚDM (miasta z najmniejszym kosztem organizacji). 
        public int[] FindBestLocationWithoutCityCosts(out int minCost, out Graph paths)
        {
            //graphExport.Export(RoadsGraph);
            int tmpCost = 0;
            PathsInfo[][] pathsInfos = new PathsInfo[RoadsGraph.VerticesCount][];    //pathsInfos[v] -> PathsInfo from vertex v to every other vertex
            minCost = Int32.MaxValue;
            int[] forEachCityCosts = new int[RoadsGraph.VerticesCount];
            Graph shortestPaths = RoadsGraph.IsolatedVerticesGraph(true, RoadsGraph.VerticesCount);
            int minVertex = -1;

            //calculate minimal cost
            for (int v = 0; v < RoadsGraph.VerticesCount; v++)
            {
                RoadsGraph.DijkstraShortestPaths(v, out pathsInfos[v]);

                for (int i = 0; i < pathsInfos.Length; i++)
                {   //if there is no connection, add connection of cost noEdgePenalty
                    tmpCost += double.IsNaN(pathsInfos[v][i].Dist) ? noEdgePenalty : (int)pathsInfos[v][i].Dist;
                }

                forEachCityCosts[v] = tmpCost;

                if(minCost > tmpCost)
                {
                    minCost = tmpCost;
                    minVertex = v;
                    //minVertexPathsInfos = pathsInfos;
                }

                tmpCost = 0;
            }

            //construct paths from all cities to minimal cost city
            for (int v = 0; v < RoadsGraph.VerticesCount; v++)
            {
                if(v == minVertex)
                {
                    continue;
                }

                Edge[] path = PathsInfo.ConstructPath(v, minVertex, pathsInfos[v]);

                //if there is no connection, add connection of cost noEdgePenalty
                if(null == path)
                {
                    shortestPaths.AddEdge(v, minVertex, noEdgePenalty);
                }
                else
                {
                    foreach (var e in path)
                    {
                        shortestPaths.AddEdge(e);
                    }
                }
            }

            paths = shortestPaths;
            return forEachCityCosts;
        }

        // return: tak jak w punkcie poprzednim, ale tym razem
        // za koszt organizacji ŚDM uznajemy sumę kosztów dostania się ze wszystkim miast do wskazanego miasta z uwzględnieniem kosztów przechodzenia przez miasta (cityCosts[]).
        // Nie uwzględniamy kosztu przejścia przez miasto które organizuje ŚDM.
        // minCost: najlepszy koszt
        // paths: graf skierowany zawierający drzewo najkrótyszch scieżek od wszyskich miast do miasta organizującego ŚDM (miasta z najmniejszym kosztem organizacji). 
                public int[] FindBestLocation(out int minCost, out Graph paths)
        {
            minCost = -1;
            paths = null;
            return null;
        }

        // return: tak jak w punkcie poprzednim, ale tym razem uznajemy zarówno koszt przechodzenia przez miasta, jak i wielkość miasta startowego z którego wyruszają pielgrzymi.
        // Szczegółowo opisane jest to w treści zadania "Częśc 2". 
        // minCost: najlepszy koszt
        // paths: graf skierowany zawierający drzewo najkrótyszch scieżek od wszyskich miast do miasta organizującego ŚDM (miasta z najmniejszym kosztem organizacji). 
        public int[] FindBestLocationSecondMetric(out int minCost, out Graph paths)
        {
            minCost = -1;
            paths = null;
            return null;
        }

    }
}
