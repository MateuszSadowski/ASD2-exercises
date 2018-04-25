
namespace ASD.Graphs
{

public static class ColoringExtender
    {

    // koloruje graf algorytmem zachlannym (byc moze niepotymalnie)
    public static int GreedyColor(this Graph g, out int[] colors)
        {
            // kazdemu wierzcholkowi 
            // przydzielamy najmniejszy kolor nie kolidujacy z juz pokolorowanymi sasiadami
            // (wpisujemy go do tablicy colors)
            // zwracamy liczbe uzytych kolorow

            int[] tmpColors = new int[g.VerticesCount];
            int[] usedColors = new int[g.VerticesCount];
            int maxColor = 0;

            for (int v = 0; v < g.VerticesCount; v++)
            {
                usedColors = new int[g.VerticesCount];
                foreach (var e in g.OutEdges(v))
                {
                    usedColors[tmpColors[e.To]] = 1;
                }
                for (int i = 0; i < usedColors.Length; i++)
                {
                    if (usedColors[i] == 1)
                        continue;

                    tmpColors[v] = i;
                    if (i > maxColor)
                        maxColor = i;
                    break;
                }
            }

            colors = tmpColors;
        /* ZMIENIC */ return maxColor + 1;
        }

    // koloruje graf algorytmem z powrotami (optymalnie)
    public static int BacktrackingColor(this Graph g, out int[] colors)
        {
        var gc = new Coloring(g);
            int[] tmpColors = new int[g.VerticesCount];
            for (int i = 0; i < tmpColors.Length; i++)
            {
                tmpColors[i] = -1;
            }
        gc.Color(0,tmpColors,0);
        colors=gc.bestColors;
        return gc.bestColorsNumber;
        }

    // klasa pomocnicza dla algorytmu z powrotami
    private sealed class Coloring
        {
        
        // tablica pamietajaca najlepsze dotychczas znalezione pokolorowanie
        internal int[] bestColors=null;

        // zmienna pamietajaca liczbe kolorow w najlepszym dotychczas znalezionym pokolorowaniu
        internal int bestColorsNumber;

        // badany graf
        private Graph g;
        
        // konstruktor
        internal Coloring(Graph g)
            {
            this.g=g;
            bestColorsNumber=g.VerticesCount+1;
            }

        // rekurencyjna metoda znajdujaca najlepsze pokolorowanie
        // v - wierzcholek do pokolorowania
        // colors - tablica kolorow
        // n - liczba kolorow uzytych w pokolorowaniu zapisanym w colors
        internal void Color(int v ,int[] colors, int n)
            {
                // tu zaimplementowac algorytm z powrotami
                if (n >= bestColorsNumber)
                    return;

                if(v == g.VerticesCount)
                    {   //colored last vertex on previous level
                        bestColors = (int[])colors.Clone();
                        bestColorsNumber = n;

                    return;
                    }

                int[] usedColors = new int[g.VerticesCount];

                    foreach (var e in g.OutEdges(v))
                    {
                    if (colors[e.To] == -1)
                        continue;

                        usedColors[colors[e.To]] = 1;
                    }
                    for (int i = 0; i < usedColors.Length; i++)
                    {
                        if (usedColors[i] == 1)
                            continue;

                        colors[v] = i;
                        if (i + 1 > n)
                            n = i + 1;

                    Color(v + 1, colors, n);

                    if (bestColorsNumber == 1)
                        return;

                    }
            }

        }  // class Coloring

    }  // class ColoringExtender

}  // namespace ASD.Graph
