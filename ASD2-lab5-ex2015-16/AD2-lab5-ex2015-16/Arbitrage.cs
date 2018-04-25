using System;
using System.Collections.Generic;

namespace AsdLab5
{
	public class InvalidExchangeException : Exception
	{
		public InvalidExchangeException ()
		{
		}

		public InvalidExchangeException (string msg) : base(msg)
		{
		}

		public InvalidExchangeException (string msg, Exception ex) : base(msg, ex)
		{
		}
	}

	public struct ExchangePair
	{
		public readonly int From;
		public readonly int To;
		public readonly double Price;

		public ExchangePair (int from, int to, double price)
		{
			if (to < 0 || from < 0 || price <= 0.0)
				throw new InvalidExchangeException ();
			From = from;
			To = to;
			Price = price;
		}
	}

	public class CurrencyGraph
	{
		private static double priceToWeight (double price)
		{
			return -Math.Log (price);
		}

		private static double weightToPrice (double weight)
		{
			return Math.Exp (-weight);
		}

		private double[,] weights;

		public CurrencyGraph (int n, ExchangePair[] exchanges)
		{
			weights = new double[n, n];
            //
            // uzupelnic
            //
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    weights[i, j] = Int32.MaxValue;
                }
            }

            foreach (var e in exchanges)
            {
                weights[e.From, e.To] = e.Price;
            }
		}

		// wynik: true jesli nie na cyklu ujemnego
		// currency: waluta "startowa"
		// bestPrices: najlepszy (najwyzszy) kurs wszystkich walut w stosunku do currency (byc mo¿e osiagalny za pomoca wielu wymian)
		//   jesli wynik == false to bestPrices = null
		public bool findBestPrice (int currency, out double[] bestPrices)
		{
            //
            // wywolac odpowiednio FordBellmanShortestPaths
            // i na tej podstawie obliczyc bestPrices
            //
            int[] arr;

            bool noNegativeCycle = FordBellmanShortestPaths(currency, out bestPrices, out arr);

            if(!noNegativeCycle)
            {
                bestPrices = null;
            }

            if(null != bestPrices)
            {
                bestPrices[currency] = 1;
            }

			return noNegativeCycle;
		}

		// wynik: true jesli jest mozliwosc arbitrazu, false jesli nie ma (nie rzucamy wyjatkow!)
		// currency: waluta "startowa"
		// exchangeCycle: a cycle of currencies starting from 'currency' and ending with 'currency'
		//  jesli wynik == false to exchangeCycle = null
		public bool findArbitrage (int currency, out int[]exchangeCycle)
		{
			//
			// Czêœæ 1: wywolac odpowiednio FordBellmanShortestPaths

			// Czêœæ 2: dodatkowo wywolac odpowiednio FindNegativeCostCycle
			//
			exchangeCycle = null;
			return true;
		}

		// wynik: true jesli nie na cyklu ujemnego
		// s: wierzcho³ek startowy
		// dist: obliczone odleglosci
		// prev: tablica "poprzednich"
		private bool FordBellmanShortestPaths (int s, out double[] dist, out int[] prev)
		{
            int verticesCount = weights.GetLength(0);
            int iterationCount = 0;

            dist = new double[verticesCount];
            prev = new int[verticesCount];
            //
            // implementacja algorytmu Forda-Bellmana
            //
            for (int i = 0; i < verticesCount; i++)
            {
                dist[i] = Int32.MaxValue;
                prev[i] = -1;
            }

            dist[s] = 0;

            bool changed = true;
            Queue<int> verticesToProcess = new Queue<int>();
            //bool[] verticessProcessed = new bool[verticesCount];

            verticesToProcess.Enqueue(s);
            while (changed && verticesToProcess.Count > 0)
            {
                changed = false;
                iterationCount++;
                int v = verticesToProcess.Dequeue();
                //verticessProcessed[v] = true;
                for (int i = 0; i < verticesCount; i++)
                {
                    if (weights[v, i] != Int32.MaxValue && dist[i] > dist[v] + weights[v, i])
                    {
                        dist[i] = dist[v] + weights[v, i];
                        prev[i] = v;
                        changed = true;
                    }

                    if (weights[v, i] != Int32.MaxValue)    //also when dist[i] <= dist[v] + weights[v, i]
                    {
                        verticesToProcess.Enqueue(i);
                    }
                }
                if (iterationCount > verticesCount - 1 && true == changed)
                {   //there was a negative cycle
                    return false;
                }
            }

            return true;
		}

		// wynik: true jesli JEST cykl ujemny
		// dist: tablica odleglosci
		// prev: tablica "poprzednich"
		// cycle: wyznaczony cykl (kolejne elementy to kolejne wierzcholki w cyklu, pierwszy i ostatni element musza byc takie same - zamkniêcie cyklu)
		private bool FindNegativeCostCycle (double[] dist, int[] prev, out int[] cycle)
		{
			cycle = null;
            //
            // wyznaczanie cyklu ujemnego
            // przykladowy pomysl na algorytm
            // 1) znajdowanie wierzcholka, którego odleglosc zostalaby poprawiona w kolejnej iteracji algorytmu Forda-Bellmana
            int s = -1;
            for (int v = 0; v < dist.Length; v++)
            {
                for (int i = 0; i < dist.Length; i++)
                {
                    if (weights[v, i] != Int32.MaxValue && dist[i] > dist[v] + weights[v, i])
                    {   //vertex is part of the negative cycle
                        s = v;
                    }
                }
            }
            // 2) cofanie sie po lancuchu poprzednich (prev) - gdy zaczna sie powtarzac to znaleŸlismy wierzcholek nale¿acy do cyklu o ujemnej dlugosci
            List<int> visitedVertices = new List<int>();
            visitedVertices.Add(s);
            int nextV = prev[s];
            while(!visitedVertices.Contains(s))
            {
                visitedVertices.Add(nextV);
                nextV = prev[nextV];
            }
            // 3) konstruowanie odpowiedzi zgodnie z wymogami zadania
            //
            int cycleLength = visitedVertices.Count;
            cycle = new int[cycleLength + 1];
            for (int i = 0; i < cycleLength; i++)
            {   //start and end with s
                cycle[i] = visitedVertices[i];
            }
            cycle[cycleLength] = s;

            return true;
		}
	}
}