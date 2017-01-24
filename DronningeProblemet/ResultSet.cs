using System.Collections.Generic;

namespace DronningeProblemet
{
    /// <summary> Data container for returning the Algorithms findings. </summary>
    public class ResultSet
    {
        public int Queens;
        public IEnumerable<ChessBoard> Solutions;

        public ResultSet(int queens, IEnumerable<ChessBoard> solutions)
        {
            Queens = queens;
            Solutions = solutions;
        }
    }
}