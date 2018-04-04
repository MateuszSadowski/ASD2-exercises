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
            isBrakingValue = null;

            // Uzupełnić

            return new Velocities(-1, -1);
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
             
            return new Velocities(-1, -1);
        }
    }
}
