﻿
using System;
using ASD.Graphs;

class Example
    {

    public static void Main ()
        {
        bool b;
        int mst;
        Graph g, t;
        Edge[] ep;
        var rgg = new RandomGraphGenerator();
        var ge = new GraphExport();
        
        // nieskierowany - cykl
        rgg.SetSeed(1111);
        g = rgg.EulerGraph(typeof(AdjacencyMatrixGraph),false,6,0.5,2,4);
        b=g.Lab04_Euler(out ep);
        if ( b )
            {
            Console.WriteLine("Znaleziono cykl Eulera w grafie 1 - ma {0} krawedzi", ep.Length);
            ge.Export(Construct(g.VerticesCount,ep),"nieskierowany-cykl");
            }
        else
            Console.WriteLine("Nie znaleziono sciezki Eulera w grafie 1");

        // skierowany - cykl
        rgg.SetSeed(2222);
        g = rgg.EulerGraph(typeof(AdjacencyMatrixGraph),true,5,0.2,1,3);
        b=g.Lab04_Euler(out ep);
        if ( b )
            {
            Console.WriteLine("Znaleziono cykl Eulera w grafie 2 - ma {0} krawedzi", ep.Length);
            ge.Export(Construct(g.VerticesCount,ep),"skierowany-cykl");
            }
        else
            Console.WriteLine("Nie znaleziono sciezki Eulera w grafie 2");

        // nieskierowany - sciezka
        rgg.SetSeed(3333);
        g = rgg.SemiEulerGraph(typeof(AdjacencyMatrixGraph),false,6,0.2,2,4);
        b=g.Lab04_Euler(out ep);
        if ( b )
            {
            Console.WriteLine("Znaleziono sciezke Eulera w grafie 3 - ma {0} krawedzi", ep.Length);
            ge.Export(Construct(g.VerticesCount,ep),"nieskierowany-sciezka");
            }
        else
            Console.WriteLine("Nie znaleziono sciezki Eulera w grafie 3");

        // skierowany - sciezka
        rgg.SetSeed(4444);
        g = rgg.SemiEulerGraph(typeof(AdjacencyMatrixGraph),true,5, 0.2, 1,3);
        b=g.Lab04_Euler(out ep);
        if ( b )
            {
            Console.WriteLine("Znaleziono sciezke Eulera w grafie 4 - ma {0} krawedzi", ep.Length);
            ge.Export(Construct(g.VerticesCount,ep),"skierowany-sciezka");
            }
        else
            Console.WriteLine("Nie znaleziono sciezki Eulera w grafie 4");

        // drzewo
        rgg.SetSeed(5555);
        g = rgg.UndirectedGraph(typeof(AdjacencyMatrixGraph),20,0.2,-10,50);
        mst = g.Lab04_Kruskal(out t);
        Console.WriteLine("Minimalne drzewo ma wage {0}", mst);
            ge.Export(t,"drzewo");

        }

    public static Graph Construct(int n, Edge[] ep)
        {
        Graph g = new AdjacencyMatrixGraph(true,n);
        for ( int i=0 ; i<ep.Length ; ++i )
            g.AddEdge(ep[i].From,ep[i].To,i+1);
        return g;
        }

    }
