using System;

namespace ASD
{
    class CrossoutChecker
    {
        /// <summary>
        /// Sprawdza, czy podana lista wzorców zawiera wzorzec x
        /// </summary>
        /// <param name="patterns">Lista wzorców</param>
        /// <param name="x">Jedyny znak szukanego wzorca</param>
        /// <returns></returns>
        static bool comparePattern(char[][] patterns, char x)
        {

            foreach (char[] pat in patterns)
            {
                if (pat.Length == 1 && pat[0] == x)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Sprawdza, czy podana lista wzorców zawiera wzorzec xy
        /// </summary>
        /// <param name="patterns">Lista wzorców</param>
        /// <param name="x">Pierwszy znak szukanego wzorca</param>
        /// <param name="y">Drugi znak szukanego wzorca</param>
        /// <returns></returns>
        static bool comparePattern(char[][] patterns, char x, char y)
        {
            foreach (char[] pat in patterns)
            {
                if (pat.GetLength(0) == 2 && pat[0] == x && pat[1] == y)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Metoda sprawdza, czy podany ciąg znaków można sprowadzić do ciągu pustego przez skreślanie zadanych wzorców.
        /// Zakładamy, że każdy wzorzec składa się z jednego lub dwóch znaków!
        /// </summary>
        /// <param name="sequence">Ciąg znaków</param>
        /// <param name="patterns">Lista wzorców</param>
        /// <param name="crossoutsNumber">Minimalna liczba skreśleń gwarantująca sukces lub int.MaxValue, jeżeli się nie da</param>
        /// <returns></returns>
        public static bool Erasable(char[] sequence, char[][] patterns, out int crossoutsNumber)
        {
            int[,] tmp = new int[sequence.Length, sequence.Length];
            for (int i = 0; i < sequence.Length; i++)
                for (int j = 0; j < sequence.Length; j++)
                    tmp[i, j] = int.MaxValue;

            for (int i = 0; i < sequence.Length; i++)
            {
                if (comparePattern(patterns, sequence[i]))
                    tmp[i, i] = 1;
            }
            for (int i = 0; i < sequence.Length - 1; i++)
            {
                if (comparePattern(patterns, sequence[i], sequence[i + 1]))
                    tmp[i, i + 1] = 1;
                else if (tmp[i, i] != int.MaxValue && tmp[i + 1, i + 1] != int.MaxValue)
                    tmp[i, i + 1] = 2;
            }
            for (int i = 2; i < sequence.Length; i++)
                for (int j = 0; j < sequence.Length - i; j++)
                {
                    if (comparePattern(patterns, sequence[j], sequence[j + i]) == true && tmp[j + 1, j + i - 1] != int.MaxValue)
                        tmp[j, j + i] = 1 + tmp[j + 1, j + i - 1];
                    for (int k = j; k < j + i; k++)
                        if (tmp[j, k] != int.MaxValue && tmp[k + 1, j + i] != int.MaxValue && tmp[j, j + i] > tmp[k + 1, j + i] + tmp[j, k])
                            tmp[j, j + i] = tmp[j, k] + tmp[k + 1, j + i];

                }
            crossoutsNumber = tmp[0, sequence.Length - 1];
            if (tmp[0, sequence.Length - 1] != int.MaxValue)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Metoda sprawdza, jaka jest minimalna długość ciągu, który można uzyskać z podanego poprzez skreślanie zadanych wzorców.
        /// Zakładamy, że każdy wzorzec składa się z jednego lub dwóch znaków!
        /// </summary>
        /// <param name="sequence">Ciąg znaków</param>
        /// <param name="patterns">Lista wzorców</param>
        /// <returns></returns>
        public static int MinimumRemainder(char[] sequence, char[][] patterns)
        {
            int[,] tmp = new int[sequence.Length, sequence.Length];
            int[] tmp2 = new int[sequence.Length];
            for (int i = 0; i < sequence.Length; i++)
                for (int j = 0; j < sequence.Length; j++)
                    tmp[i, j] = int.MaxValue;

            for (int i = 0; i < sequence.Length; i++)
            {
                if (comparePattern(patterns, sequence[i]))
                    tmp[i, i] = 1;
            }
            for (int i = 0; i < sequence.Length - 1; i++)
            {
                if (comparePattern(patterns, sequence[i], sequence[i + 1]))
                    tmp[i, i + 1] = 2;
                else if (tmp[i, i] != int.MaxValue && tmp[i + 1, i + 1] != int.MaxValue)
                    tmp[i, i + 1] = 2;
            }
            for (int i = 1; i < sequence.Length; i++)
                for (int j = 0; j < sequence.Length - i; j++)
                {
                    if (comparePattern(patterns, sequence[j], sequence[j + i]) == true && tmp[j + 1, j + i - 1] != int.MaxValue)
                        tmp[j, j + i] = i + 1;
                    else
                    {
                        for (int k = j; k < j + i; k++)
                            if (tmp[j, k] != int.MaxValue && tmp[k + 1, j + i] != int.MaxValue)
                                tmp[j, j + i] = i + 1;
                    }

                }
            for (int i = 0; i < sequence.Length; i++)
                for (int j = 0; j < sequence.Length; j++)
                {

                }
            return 0;
        }

        // można dopisać metody pomocnicze
    }
}
