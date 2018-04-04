using System;
using System.Collections.Generic;
using System.Linq;

namespace ASD
{
    class VelocityMeasurements
    {
        /// <summary>
        /// Metoda zwraca możliwą minimalną i maksymalną wartość prędkości samochodu w momencie wypadku.
        /// </summary>
        /// <param name="measurements">Tablica zawierające wartości pomiarów urządzenia zainstalowanego w aucie Mateusza</param>
        /// <param name="isBrakingValue">Tablica zwracająca informację dla każdego z pomiarów z tablicy measurements informację bool czy dla sekwencji dającej 
        /// minimalną prędkość wynikową traktować dany pomiar jako hamujący (true) przy przyspieszający (false)</param>
        /// <returns>Struktura Velocities z informacjami o najniższej i najwyższej możliwej prędkości w momencie wypadku</returns>
        /// 
        /// <remarks>
        /// Złożoność pamięciowa algorytmu powinna być nie większa niż O(sumy_wartości_pomiarów).
        /// Złożoność czasowa algorytmu powinna być nie większa niż O(liczby_pomiarów * sumy_wartości_pomiarów).
        /// </remarks>
        public static Velocities FinalVelocities(int[] measurements, out bool[] isBrakingValue)
        {
            isBrakingValue = new bool[measurements.Length];     //initialized to false

            // Uzupełnić
            int sum = measurements.Aggregate(0, (total, x) => total += x);
            if (0 == sum)
                return new Velocities(0, 0);
            
            int minVelocity = Int32.MaxValue;

            int[] subsetSum = new int[sum/2 + 1];
            subsetSum[0] = Int32.MaxValue;      //sentiel TODO:is it essential?
            for (int i = 1; i < subsetSum.Length; i++)
            {
                subsetSum[i] = -1;
            }

            for (int i = 0; i < measurements.Length; i++)
            {
                int val = measurements[i];
                for (int j = 1; j < subsetSum.Length; j++)
                {
                    if(j >= val)
                    {
                        if (subsetSum[j - val] != -1        //check if subsetSum[j - val] can be formed with other measurements
                            && subsetSum[j - val] != i      //check if you haven't used the measurement already to get there
                            && subsetSum[j] == -1)          //don't overwrite if there is already a value
                            subsetSum[j] = i;               //remember what measurement was used to reach that subsum
                    }
                }
            }

            for (int i = subsetSum.Length - 1; i >= 0; i--)
                
            {
                if(subsetSum[i] != -1)
                {
                    minVelocity = i;
                    while (i > 0)
                    {
                        int val = measurements[subsetSum[i]];
                        isBrakingValue[subsetSum[i]] = true;
                        i -= val;
                    }
                    break;
                }
            }

            if (Int32.MaxValue == minVelocity)
                minVelocity = 0;

            minVelocity = Math.Abs(sum - 2 * minVelocity);

            return new Velocities(minVelocity, sum);
        }

        /// <summary>
        /// Metoda zwraca możliwą minimalną i maksymalną wartość prędkości samochodu w trakcie całego okresu trwania podróży.
        /// </summary>
        /// <param name="measurements">Tablica zawierające wartości pomiarów urządzenia zainstalowanego w aucie Mateusza</param>
        /// <param name="isBrakingValue">W tej wersji algorytmu proszę ustawić parametr na null</param>
        /// <returns>Struktura Velocities z informacjami o najniższej i najwyższej możliwej prędkości na trasie</returns>
        /// 
        /// <remarks>
        /// Złożoność pamięciowa algorytmu powinna być nie większa niż O(sumy_wartości_pomiarów).
        /// Złożoność czasowa algorytmu powinna być nie większa niż O(liczby_pomiarów * sumy_wartości_pomiarów).
        /// </remarks>
        public static Velocities JourneyVelocities(int[] measurements, out bool[] isBrakingValue)
        {
            isBrakingValue = null;  // Nie zmieniać !!!

            // Uzupełnić
            int sum = measurements.Aggregate(0, (total, x) => total += x);
            if (0 == sum)
                return new Velocities(0, 0);

            bool[] tmpBool;
            var tmpMeasurements = new Queue<int[]>();
            for (int i = 0; i < measurements.Length; i++)
            {
                int[] subset = new int[i + 1];
                Array.Copy(measurements, subset, i + 1);    //TODO: length can cause problems
                tmpMeasurements.Enqueue(subset);
            }

            Velocities[] velocities = new Velocities[measurements.Length];

            for (int i = 0; i < velocities.Length; i++)
            {
                velocities[i] = FinalVelocities(tmpMeasurements.Dequeue(), out tmpBool);
            }

            int minVal = Int32.MaxValue;

            for (int i = 0; i < velocities.Length; i++)
            {
                if (minVal > velocities[i].minVelocity)
                    minVal = velocities[i].minVelocity;
            }

            return new Velocities(minVal, sum);
        }
    }
}
