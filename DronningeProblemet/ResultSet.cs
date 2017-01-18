using System.Collections.Generic;

namespace DronningeProblemet
{
    /// <summary> Data container for returning the Algorithms findings. </summary>
    public class ResultSet
    {
        public int Queens;
        public HashSet<ChessBoard> Solutions;

        public ResultSet(int queens, HashSet<ChessBoard> solutions)
        {
            Queens = queens;
            Solutions = solutions;
        }
    }
}