
using System;

namespace ASD
{

partial class Lab02
    {

    // zwraca dominujacy element (gdy takiego nie ma zwraca null)
    // w parametrze max ustawia liczbe wystapien dominujacego elementu (gdy takiego nie ma ustawia 0)
    // wymagana zlozonosc o(n*n)
    static public Elem FindWinnerSimply(Elem[] el, out int max)
        {
            int domValue = el.Length / 2;

            for (int i = 0; i < el.Length; i++)
            {
                int numOfOccur = 0;
                for (int j = 0; j < el.Length; j++)
                {
                    if (i == j)
                        continue;

                    if (el[i].Compare(el[j]))
                        numOfOccur += 1;
                }

                if(numOfOccur >= domValue)      //Dlaczego tutaj >= ??
                {
                    max = numOfOccur + 1;
                    return el[i];
                }
            }

            max = 0;
            return null;
        }

    // zwraca dominujacy element (gdy takiego nie ma zwraca null)
    // w parametrze max ustawia liczbe wystapien dominujacego elementu (gdy takiego nie ma ustawia 0)
    // wymagana zlozonosc o(n*log(n))
    static public Elem FindWinnerFast(Elem[] el, out int max)
        {
            if (0 != el.Length)
            {
                
            }

            max = 0;
            return null;
        }

    static private Elem FindWinnerRec(Elem[] el, out int max, int begin, int end)
        {
            int length = end - begin + 1;

            if(length / 2 > 1)
            {
                FindWinnerRec(el, out max, begin, length / 2);
                FindWinnerRec(el, out max, length / 2 + 1, end);
            }



            max = 0;
            return null;
        }

    private static int CountElement(Elem[] el, int begin, int end, Elem element)
        {
            return -1;
        }


    //
    // tu mozna dopisac prywatne funkcje pomocnicze
    //

    }

}  // namespace ASD
