using System;

namespace Lab07
{

    public class SmellsChecker
    {

        private readonly int smellCount;
        private readonly int[][] customerPreferences;
        private readonly int satisfactionLevel;

        private int[] customerSatisfactionLevel;
        private int customerCount;
        /// <summary>
        ///   
        /// </summary>
        /// <param name="smellCount">Liczba zapachów, którymi dysponuje sklep</param>
        /// <param name="customerPreferences">Preferencje klientów
        /// Każda tablica -- element tablicy tablic -- to preferencje jednego klienta.
        /// Preferencje klienta mają długość smellCount, na i-tej pozycji jest
        ///  1 -- klient preferuje zapach
        ///  0 -- zapach neutralny
        /// -1 -- klient nie lubi zapachu
        /// 
        /// Zapachy numerujemy od 0
        /// </param>
        /// <param name="satisfactionLevel">Oczekiwany poziom satysfakcji</param>
        public SmellsChecker(int smellCount, int[][] customerPreferences, int satisfactionLevel)
        {
            this.smellCount = smellCount;
            this.customerPreferences = customerPreferences;
            this.satisfactionLevel = satisfactionLevel;

            this.customerCount = customerPreferences.GetLength(0);
            this.customerSatisfactionLevel = new int[customerCount];
        }

        /// <summary>
        /// Implementacja etapu 1
        /// </summary>
        /// <returns><c>true</c>, jeśli przypisanie jest możliwe <c>false</c> w p.p.</returns>
        /// <param name="smells">Wyjściowa tablica rozpylonych zapachów realizująca rozwiązanie, jeśli się da. null w p.p. </param>
        public Boolean AssignSmells(out bool[] smells)
        {
            bool[] tmpSmells = new bool[smellCount];
            smells = null;

            bool foundSatisfactoryForAll = AssignSmellsRec(tmpSmells, 0);
            if(foundSatisfactoryForAll)
            {
                smells = tmpSmells;
            }

            return foundSatisfactoryForAll;
        }

        public Boolean AssignSmellsRec(bool[] smells, int level)
        {
            if(level == smellCount)
            {   //check if combination of smells is satisfactory for all
                return checkCanSatisfyCustomers(level);
            }

            bool foundSatisfactory = trySmell(smells, level, true);
            if(foundSatisfactory)
            {
                return true;
            }

            foundSatisfactory = trySmell(smells, level, false);
            if(foundSatisfactory)
            {
                return true;
            }

            return false;
        }

        public bool checkCanSatisfyCustomers(int level)
        {
            for (int customer = 0; customer < customerCount; customer++)
            {
                int tmpSatisfactionLevel = customerSatisfactionLevel[customer];
                for (int smell = level; smell < smellCount; smell++)
                {
                    if (customerPreferences[customer][smell] == 1)
                    {
                        tmpSatisfactionLevel += customerPreferences[customer][smell];
                    }
                }
                if (tmpSatisfactionLevel < satisfactionLevel)
                {   //it is impossible to satisfy customer with given smells and preferences
                    return false;
                }
            }

            //it is hypothetically possible to satisfy every customer
            return true;
        }

        public bool trySmell(bool[] smells, int smell, bool isChoosen)
        {
            smells[smell] = isChoosen;
            //check if current smell choice can lead to solution
            bool canSatisfyCustomers = checkCanSatisfyCustomers(smell);
            if (!canSatisfyCustomers)
            {
                return false;
            }

            if(isChoosen)
            {
                addSmellToCustomerSatisfactionLevel(smell);
            }

            bool isSatisfactory = AssignSmellsRec(smells, smell + 1);
            if (isSatisfactory)
            {
                return true;
            }

            if(isChoosen)
            {
                removeSmellFromCustomerSatisfactionLevel(smell);
            }
            return false;
        }

        public void addSmellToCustomerSatisfactionLevel(int smell)
        {
            for (int customer = 0; customer < customerCount; customer++)
            {
                customerSatisfactionLevel[customer] += customerPreferences[customer][smell];
            }
        }

        public void removeSmellFromCustomerSatisfactionLevel(int smell)
        {
            for (int customer = 0; customer < customerCount; customer++)
            {
                customerSatisfactionLevel[customer] -= customerPreferences[customer][smell];
            }
        }

        /// <summary>
        /// Implementacja etapu 2
        /// </summary>
        /// <returns>Maksymalna liczba klientów, których można usatysfakcjonować</returns>
        /// <param name="smells">Wyjściowa tablica rozpylonych zapachów, realizująca ten poziom satysfakcji</param>
        public int AssignSmellsMaximizeHappyCustomers(out bool[] smells)
        {
            smells = new bool[smellCount];
            return -1;
        }

    }

}

