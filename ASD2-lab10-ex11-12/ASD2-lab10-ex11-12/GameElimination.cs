namespace GameElimination
{
    using System;

    using ASD.Graphs;

    public partial class Program
    {
        /// <summary>
        /// Procedura określająca czy drużyna jest wyeliminowana z rozgrywek
        /// </summary>
        /// <param name="teamId">indeks drużyny do sprawdzenia</param>
        /// <param name="teams">lista zespołów</param>
        /// <param name="predictedResults">wyniki gwarantujące zwycięstwo sprawdzanej drużyny</param>
        /// <returns></returns>
        public static bool IsTeamEliminated(int teamId, Team[] teams, out int[,] predictedResults)
        {
            predictedResults = null;
            
            return true;
        }

        internal static Graph BuildSeekFlowGraph(int teamId, Team[] teams)
        {
            int teamsCount = teams.Length;
            int pairLayerCount = (int)GetBinCoeff(teamsCount - 1, 2);
            int teamLayerCount = teamsCount - 1;
            int verticesCount = 2 + pairLayerCount + teamLayerCount;

            Graph h = new AdjacencyListsGraph<SimpleAdjacencyList>(true, verticesCount);    //Directed

            // start - 0
            // target - 1
            // team pairs - 2 => 2 + pairLayerCount
            // teams - 2 + pairLayerCount => verticesCount
            var matchesToPlayBetweenTeams = new int[pairLayerCount];
            for (int i = 0; i < pairLayerCount + 1; i++)
                if (i != teamId)    //If not input team
                    for (int j = i + 1; j < teamsCount; j++)
                        if (j != teamId) //if not corresponding to input team
                            matchesToPlayBetweenTeams[i] = teams[i].NumberOfGamesToPlayByTeam[j];   //Number of games to play by team pairs

            for (int i = 0; i < pairLayerCount; i++)
                if (i != teamId)
                    for (int j = i + 1; j < teamsCount; j++)
                        h.AddEdge(0, i, matchesToPlayBetweenTeams[i]);

        }

        internal static long GetBinCoeff(long N, long K)
        {
            // This function gets the total number of unique combinations based upon N and K.
            // N is the total number of items.
            // K is the size of the group.
            // Total number of unique combinations = N! / ( K! (N - K)! ).
            // This function is less efficient, but is more likely to not overflow when N and K are large.
            // Taken from:  http://blog.plover.com/math/choose.html
            //
            long r = 1;
            long d;
            if (K > N) return 0;
            for (d = 1; d <= K; d++)
            {
                r *= N--;
                r /= d;
            }
            return r;
        }
    }
}
