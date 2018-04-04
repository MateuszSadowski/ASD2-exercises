
using System;

namespace ASD
{

class ChangeMaking
    {

    /// <summary>
    /// Metoda wyznacza rozwiązanie problemu wydawania reszty przy pomocy minimalnej liczby monet
    /// bez ograniczeń na liczbę monet danego rodzaju
    /// </summary>
    /// <param name="amount">Kwota reszty do wydania</param>
    /// <param name="coins">Dostępne nominały monet</param>
    /// <param name="change">Liczby monet danego nominału użytych przy wydawaniu reszty</param>
    /// <returns>Minimalna liczba monet potrzebnych do wydania reszty</returns>
    /// <remarks>
    /// coins[i]  - nominał monety i-tego rodzaju
    /// change[i] - liczba monet i-tego rodzaju (nominału) użyta w rozwiązaniu
    /// Jeśli dostepnymi monetami nie da się wydać danej kwoty to metochange = null,
    /// a metoda również zwraca null
    ///
    /// Wskazówka/wymaganie:
    /// Dodatkowa uzyta pamięć powinna (musi) być proporcjonalna do wartości amount ( czyli rzędu o(amount) )
    /// </remarks>
    public static int? NoLimitsDynamic(int amount, int[] coins, out int[] change)
        {
            int[] arrAmount = new int[amount + 1];
            int[] arrCoins = new int[amount + 1];

            arrAmount[0] = 0;
            for (int k = 1; k < arrAmount.Length; k++)
            {
                arrAmount[k] = Int32.MaxValue;
            }

            for (int k = 1; k < arrCoins.Length; k++)
            {
                arrCoins[k] = -1;
            }
             
            for (int j = 0; j < coins.Length; j++)
            {
                for (int k = 1; k < arrAmount.Length; k++)
                {
                    if (k >= coins[j])
                    {
                        if(arrAmount[k - coins[j]] != Int32.MaxValue && arrAmount[k] > 1 + arrAmount[k - coins[j]])
                        {
                            arrAmount[k] = 1 + arrAmount[k - coins[j]];
                            arrCoins[k] = j;
                        }
                    }
                }
            }

            if(-1 == arrCoins[amount])
            {
                change = null;  // zmienić
                return null;      // zmienić
            }

            int i = amount;
            int[] arrTmpChange = new int[coins.Length];
            while(i != 0)
            {
                arrTmpChange[arrCoins[i]] += 1;
                i = i - coins[arrCoins[i]];
            }
            change = arrTmpChange;

            return arrAmount[amount];
        }


    /// <summary>
    /// Metoda wyznacza rozwiązanie problemu wydawania reszty przy pomocy minimalnej liczby monet
    /// z uwzględnieniem ograniczeń na liczbę monet danego rodzaju
    /// </summary>
    /// <param name="amount">Kwota reszty do wydania</param>
    /// <param name="coins">Dostępne nominały monet</param>
    /// <param name="limits">Liczba dostępnych monet danego nomimału</param>
    /// <param name="change">Liczby monet danego nominału użytych przy wydawaniu reszty</param>
    /// <returns>Minimalna liczba monet potrzebnych do wydania reszty</returns>
    /// <remarks>
    /// coins[i]  - nominał monety i-tego rodzaju
    /// limits[i] - dostepna liczba monet i-tego rodzaju (nominału)
    /// change[i] - liczba monet i-tego rodzaju (nominału) użyta w rozwiązaniu
    /// Jeśli dostepnymi monetami nie da się wydać danej kwoty to change = null,
    /// a metoda również zwraca null
    ///
    /// Wskazówka/wymaganie:
    /// Dodatkowa uzyta pamięć powinna (musi) być proporcjonalna do wartości iloczynu amount*(liczba rodzajów monet)
    /// ( czyli rzędu o(amount*(liczba rodzajów monet)) )
    /// </remarks>
    public static int? Dynamic(int amount, int[] coins, int[] limits, out int[] change)
        {
        change = null;  // zmienić
        return -1;      // zmienić
        }

    }

}
