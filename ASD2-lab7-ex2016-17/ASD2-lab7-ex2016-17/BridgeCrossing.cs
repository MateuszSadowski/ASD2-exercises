using System;
using System.Collections.Generic;
using System.Linq;

namespace ASD
{
    public class BridgeCrossing
    {

        private static int minTimeToCross;
        private static int timeSpentCrossing;
        private static int touristCount;
        private static bool[] touristCrossed;
        private static List<List<int>> minStrategy;
        private static int[] crossTimes;

        /// <summary>
        /// Metoda rozwiązuje zadanie optymalnego przechodzenia przez most.
        /// </summary>
        /// <param name="_times">Tablica z czasami przejścia poszczególnych osób</param>
        /// <param name="strategy">Strategia przekraczania mostu: lista list identyfikatorów kolejnych osób,
        /// które przekraczają most (na miejscach parzystych przejścia par przez most,
        /// na miejscach nieparzystych powroty jednej osoby z latarką). Jeśli istnieje więcej niż jedna strategia
        /// realizująca przejście w optymalnym czasie wystarczy zwrócić dowolną z nich.</param>
        /// <returns>Minimalny czas, w jakim wszyscy turyści mogą pokonać most</returns>
        public static int CrossBridge(int[] times, out List<List<int>> strategy)
        {
            minTimeToCross = int.MaxValue;
            timeSpentCrossing = 0;
            minStrategy = new List<List<int>>();
            List<List<int>> tmpStrategy = new List<List<int>>();
            touristCount = times.Length;
            touristCrossed = new bool[touristCount];
            crossTimes = times;

            if (touristCount == 1)
            {
                tmpStrategy.Add(new List<int>() { 0 });
                strategy = tmpStrategy;
                return times[0];
            }

            CrossBridgeUtilPair(0, tmpStrategy);

            strategy = minStrategy;
            return minTimeToCross;
        }

        // MOŻESZ DOPISAĆ POTRZEBNE POLA I METODY POMOCNICZE
        // MOŻESZ NAWET DODAĆ CAŁE KLASY (ALE NIE MUSISZ)

        public static int CrossBridgeUtilPair(int k, List<List<int>> tmpStrategy)
        {   //handles situation when 2 tourists cross the bridge
            //choose 2 tourist to cross
            for (int i = 0; i < touristCount - 1; i++)
            {
                if (crossTimes[i] + timeSpentCrossing >= minTimeToCross)
                {
                    continue;
                }

                for (int j = i + 1; j < touristCount; j++)
                {
                    if (!touristCrossed[i] && !touristCrossed[j])
                    {
                        if (Math.Max(crossTimes[i], crossTimes[j]) + timeSpentCrossing >= minTimeToCross)
                        {
                            continue;
                        }

                        touristCrossed[i] = touristCrossed[j] = true;
                        timeSpentCrossing += Math.Max(crossTimes[i], crossTimes[j]);
                        tmpStrategy.Add(new List<int> { i, j });

                        CrossBridgeUtilBack(k + 2, tmpStrategy);

                        touristCrossed[i] = touristCrossed[j] = false;
                        timeSpentCrossing -= Math.Max(crossTimes[i], crossTimes[j]);
                        tmpStrategy.RemoveAt(tmpStrategy.Count - 1);
                    }
                }
            }

            return timeSpentCrossing;
        }

        public static int CrossBridgeUtilBack(int k, List<List<int>> tmpStrategy)
        {   
            if (k == touristCount)
            {   //every tourist crossed, check if update min time and strategy
                if (timeSpentCrossing < minTimeToCross)
                {
                    minTimeToCross = timeSpentCrossing;
                    minStrategy = new List<List<int>>(tmpStrategy);
                }
                return minTimeToCross;
            }

            //handles situatiin when tourist comes back with flashlight
            //choose fastest tourist
            int minCrossTime = Int32.MaxValue;
            int fastestTourist = -1;
            for (int tourist = 0; tourist < touristCount; tourist++)
            {
                if(touristCrossed[tourist])
                {
                    if(minCrossTime > crossTimes[tourist]
                       && timeSpentCrossing + crossTimes[tourist] < minTimeToCross)
                    {
                        minCrossTime = crossTimes[tourist];
                        fastestTourist = tourist;
                    }
                }
            }

            if(fastestTourist != -1)
            {
                touristCrossed[fastestTourist] = false;
                timeSpentCrossing += minCrossTime;
                tmpStrategy.Add(new List<int> { fastestTourist });

                CrossBridgeUtilPair(k - 1, tmpStrategy);

                touristCrossed[fastestTourist] = true;
                timeSpentCrossing -= minCrossTime;
                tmpStrategy.RemoveAt(tmpStrategy.Count - 1);
            }

            return minTimeToCross;
        }
    }
}