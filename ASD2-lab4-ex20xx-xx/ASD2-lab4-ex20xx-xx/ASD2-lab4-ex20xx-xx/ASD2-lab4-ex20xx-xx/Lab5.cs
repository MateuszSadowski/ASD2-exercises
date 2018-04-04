
// tu można dodać using

using System;
namespace Square
{
    partial class Lab5
    {
        /// <summary>
        /// Funkcja obliczająca najmniejszą możliwą liczbę działek, które należy kupić aby zainwestować amount kwadratów.
        /// Metoda powinna wykorzystywać następujace twierdzenie Lagrang'a:
        /// Dla każdego naturalnego x istnieją naturalne a,b,c,d takie, że x = a*a + b*b + c*c + d*d
        /// Uwaga: liczby naturalne zawierają zero
        /// </summary>
        /// <param name="amount">kwota, którą bohater chce wydać</param>
        /// <param name="areas">wynikowa tablica z powierzchniami kupionych działek</param>
        /// <returns>liczba kupionych działek</returns>
        static int CertificateNumberLagrange(int amount, out int[] areas)
        {
            int total = 5;           
            int[] i = new int[4];        

            for(i[0] = 0; i[0]*i[0] <= amount; i[0]++)
                for(i[1] = 0; i[1]*i[1] <= amount; i[1]++)
                    for(i[2] = 0; i[2]*i[2] <= amount; i[2]++)
                        for(i[3] = 0; i[3]*i[3] <= amount; i[3]++)
                            if (i[0] * i[0] + i[1] * i[1] + i[2] * i[2] + i[3] * i[3] == amount)
                            {
                                total = Convert.ToInt32(i[0] != 0) + Convert.ToInt32(i[1] != 0) + Convert.ToInt32(i[2] != 0) + Convert.ToInt32(i[3] != 0);
                                int[] t = new int[total];

                                for (int j = 0; j < total; j++)
                                    t[j] = i[3 - j];
                                areas = t;
                                return total;
                            }

            areas = new int[0];
            return total;            
        }

        /// <summary>
        /// Funkcja obliczająca najmniejszą możliwą liczbę działek, które należy kupić aby zainwestować amount kwadratów.
        /// Metoda powinna wykorzystywać programowanie dynamiczne.
        /// Należy stworzyć tablicę results taką, że results[i] zawiera wielkości działek, które należy kupić aby wydać i kwadratów.
        /// Tablicę należy budować od i = 1 aż do i = amount.
        /// </summary>
        /// <param name="amount">kwota, którą bohater chce wydać</param>
        /// <param name="areas">wynikowa tablica z powierzchniami kupionych działek</param>
        /// <returns>liczba kupionych działek</returns>
        static int CertificateNumberDynamicPrograming(int amount, out int[] areas)
        {
            int[] arr = new int[amount + 1];
            int[] tmpAreas = new int[amount + 1];
            areas = new int[amount];

            for (int i = 1; i < arr.Length; i++)
            {
                arr[i] = Int32.MaxValue;
                tmpAreas[i] = -1;
            }

            int max = Convert.ToInt32(Math.Floor(Math.Sqrt(amount))) + 1;
            for (int i = 1; i < max; i++)
            {
                int p = i * i;
                for (int j = 1; j < arr.Length; j++)
                {
                    if (j < p)
                        continue;

                    if (arr[j] > 1 + arr[j - p])
                    {
                        arr[j] = 1 + arr[j - p];
                        tmpAreas[j] = i;
                    }
                }

            }

            int k = tmpAreas.Length - 1, ind = 0;
            while(k > 0)
            {
                areas[ind++] = tmpAreas[k];
                k -= tmpAreas[k] * tmpAreas[k];
            }

            return arr[amount];   
        }
    }
}
