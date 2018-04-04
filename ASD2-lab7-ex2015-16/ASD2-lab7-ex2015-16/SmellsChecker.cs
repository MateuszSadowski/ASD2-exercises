using System;

namespace Lab07
{

    public class SmellsChecker
    {

        private readonly int smellCount;
        private readonly int[][] customerPreferences;
        private readonly int satisfactionLevel;

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
            //check if all customers can be hypothetically satisfied according to smells and their preferences
            if(!checkCanSatisfyCustomers())
            {
                return false;
            }

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
                return trySatisfyCustomers(smells);
            }

            for (int i = level; i < smellCount; i++)
            {
                smells[i] = true;
                bool isSatisfactory = AssignSmellsRec(smells, level + 1);

                if(isSatisfactory)
                {
                    return true;
                }

                smells[i] = false;
                isSatisfactory = AssignSmellsRec(smells, level + 1);

                if (isSatisfactory)
                {
                    return true;
                }
            }

            return false;
        }

        public bool trySatisfyCustomers(bool[] smells)
        {
            for (int customer = 0; customer < customerPreferences.GetLength(0); customer++)
            {
                int tmpSatisfactionLevel = 0;
                for (int smell = 0; smell < smellCount; smell++)
                {
                    if (smells[smell])
                    {
                        tmpSatisfactionLevel += customerPreferences[customer][smell];
                    }
                }
                if (tmpSatisfactionLevel < satisfactionLevel)
                {   //if not satisfactory for ANY customer, indicate failure
                    return false;
                }
            }

            //is satisfactory for EVERY customer
            return true;
        }

        public bool checkCanSatisfyCustomers()
        {
            for (int customer = 0; customer < customerPreferences.GetLength(0); customer++)
            {
                int tmpSatisfactionLevel = 0;
                for (int smell = 0; smell < smellCount; smell++)
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

