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
            return new MuseumRoutes( -1, null);
            }
    }
}

