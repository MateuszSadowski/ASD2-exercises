
using System.Collections.Generic;
using ASD.Graphs;

namespace lab10
{

public struct AlmostMatchingSolution
    {
        public AlmostMatchingSolution(int edgesCount, List<Edge> solution)
            {
            this.edgesCount=edgesCount;
            this.solution=solution;
            }

        public readonly int edgesCount;
        public readonly List<Edge> solution;
    }


public class AlmostMatching
    {

        /// <summary>
        /// Zwraca najliczniejszy możliwy zbiór krawędzi, którego poziom
        /// ryzyka nie przekracza limitu. W ostatnim etapie zwracać
        /// zbiór o najmniejszej sumie wag ze wszystkich najliczniejszych.
        /// </summary>
        /// <returns>Liczba i lista linek (krawędzi)</returns>
        /// <param name="g">Graf linek</param>
        /// <param name="allowedCollisions">Limit ryzyka</param>
        /// 

        private static int maxLinesSum;
        private static double minWeightSum;

        private static int tmpCollisions;
        private static int tmpLinesSum;
        private static double tmpWeightSum;
        private static int[] vertexOutDegree;
        private static int[] vertexTouched;

        private static List<Edge> bestSolution;
        private static List<Edge> tmpSolution;
        private static List<Edge> edges;

        private static int edgesCount;
        private static int maxAllowedCollisions;

        public static AlmostMatchingSolution LargestS(Graph g, int allowedCollisions)
        {
            maxLinesSum = 0;
            minWeightSum = int.MaxValue;
            bestSolution = new List<Edge>();
            tmpSolution = new List<Edge>();
            vertexOutDegree = new int[g.VerticesCount];
            edges = new List<Edge>();
            edgesCount = g.EdgesCount;
            maxAllowedCollisions = allowedCollisions;
            tmpCollisions = 0;

            for (int v = 0; v < g.VerticesCount; v++)
            {
                foreach (var e in g.OutEdges(v))
                {
                    if (e.From > e.To)
                        continue;
                    edges.Add(e);   
                }
            }

            LargestSUtil(0);

            return new AlmostMatchingSolution(maxLinesSum, bestSolution);
        }

        private static void LargestSUtil(int edgesProcessedCount)
        {
                if(tmpLinesSum > maxLinesSum)
                {
                    maxLinesSum = tmpLinesSum;
                    minWeightSum = tmpWeightSum;
                    bestSolution = new List<Edge>(tmpSolution);
                }
                else if (tmpLinesSum == maxLinesSum && tmpWeightSum < minWeightSum)
                {
                    minWeightSum = tmpWeightSum;
                    bestSolution = new List<Edge>(tmpSolution);
                }

            if (tmpLinesSum + (edgesCount - edgesProcessedCount) < maxLinesSum)
                return;

            for (int i = edgesProcessedCount; i < edgesCount; i++)
            {
                Edge e = edges[i];

                //add edge
                tmpSolution.Add(e);
                tmpLinesSum += 1;
                tmpWeightSum += e.Weight;
                vertexOutDegree[e.From] += 1;
                vertexOutDegree[e.To] += 1;
                if (vertexOutDegree[e.From] > 1)
                    tmpCollisions += 1;
                if (vertexOutDegree[e.To] > 1)
                    tmpCollisions += 1;

                if(tmpCollisions <= maxAllowedCollisions)
                {
                    LargestSUtil(i + 1);
                }

                //remove edge
                tmpWeightSum -= e.Weight;
                if (vertexOutDegree[e.From] > 1)
                    tmpCollisions -= 1;
                if (vertexOutDegree[e.To] > 1)
                    tmpCollisions -= 1;
                vertexOutDegree[e.From] -= 1;
                vertexOutDegree[e.To] -= 1;
                tmpLinesSum -= 1;
                tmpSolution.Remove(e);
            }
        }
    }

}


