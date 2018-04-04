
using System;

namespace ASD
{
    class SpecialNumbers
    {
        const int mod = 10000;

        // funkcja rekurencyjna
        // n cyfr
        public static int SpecialNumbersRec(int n)
        {
            // ZMIEN
            // mozesz dopisac swoja rekurencyjna funkcje pomocnicza
            if (0 >= n)
                return 0;
            else if (1 == n)
                return 9;

            return WorkRec(-1, n);
        }

        private static int WorkRec(int prevDigit, int n)
        {
            int counter = 0;

            for (int i = (prevDigit == -1 ? 9 : prevDigit); i > 0; i--)
            {
                if (-1 == prevDigit)
                {
                    if (1 == n)  //last digit
                        counter = (counter + 1) % mod;
                    else
                        counter =  (counter + WorkRec(i, n - 1)) % mod;
                }
                else if (i == prevDigit || 1 == (prevDigit + i) % 2)
                {
                    if (1 == n)  //last digit
                        counter = (counter + 1) % mod;
                    else
                        counter = (counter + WorkRec(i, n - 1)) % mod;
                }
            }

            return counter;
        }

        // programowanie dynamiczne
        // n cyfr
        public static int SpecialNumbersDP(int n)
        {
            if (0 >= n)
                return 0;
            else if (1 == n)
                return 9;

            int[,] countArr = new int[9,n - 1];

            //fill the first column
            FillFirstColumn(n, countArr);

            if (2 == n)
                return SumResult(n, countArr);


            for (int j = 1; j < n - 1; j++)
            {
                for (int prevDigit = 1; prevDigit <= 9; prevDigit++)
                {
                    for (int k = 1; k <= prevDigit; k++)
                    {
                        if (k == prevDigit || 1 == (prevDigit + k) % 2)
                            countArr[prevDigit - 1, j] = (countArr[prevDigit - 1, j] + countArr[k - 1, j - 1]) % mod;
                    }
                }
            }

            return SumResult(n, countArr);
        }

        private static void FillFirstColumn(int n, int[,] arr)
        {
            for (int prevDigit = 1; prevDigit <= 9; prevDigit++)
            {
                for (int j = 1; j <= prevDigit; j++)
                {
                    if (j == prevDigit || 1 == (prevDigit + j) % 2)
                    {
                        arr[prevDigit - 1, 0] += 1;
                    }
                }
            }
        }

        private static int SumResult(int n, int[,] arr)
        {
            int result = 0;
            for (int i = 0; i < 9; i++)
            {
                result = (result + arr[i, n - 2]) % mod;
            }
            return result;
        }

        // programowanie dynamiczne
        // n cyfr
        // req - tablica z wymaganiami, jezeli req[i, j] == 0 to znaczy, ze  i + 1 nie moze stac PRZED j + 1
        public static int SpecialNumbersDP(int n, bool[,] req)
        {
            if (0 >= n)
                return 0;
            else if (1 == n)
                return 9;

            int[,] countArr = new int[9, n - 1];

            //fill the first column
            FillFirstColumnReq(n, countArr, req);

            if (2 == n)
                return SumResult(n, countArr);


            for (int j = 1; j < n - 1; j++)
            {
                for (int prevDigit = 1; prevDigit <= 9; prevDigit++)
                {
                    for (int k = 1; k <= prevDigit; k++)
                    {
                        if (req[prevDigit - 1, k - 1])
                            countArr[prevDigit - 1, j] = (countArr[prevDigit - 1, j] + countArr[k - 1, j - 1]) % mod;
                    }
                }
            }

            return SumResult(n, countArr);
        }

        private static void FillFirstColumnReq(int n, int[,] arr, bool[,] req)
        {
            for (int prevDigit = 1; prevDigit <= 9; prevDigit++)
            {
                for (int j = 1; j <= prevDigit; j++)
                {
                    if (req[prevDigit - 1, j - 1])
                    {
                        arr[prevDigit - 1, 0] += 1;
                    }
                }
            }
        }

    }//class SpecialNumbers

}//namespace ASD