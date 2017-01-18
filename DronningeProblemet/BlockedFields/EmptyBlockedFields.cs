using System.Collections.Generic;

namespace DronningeProblemet.BlockedFields
{
    internal class EmptyBlockedFields : IBlockedFields
    {
        public List<int[]> GetBlockedFields(int x, int y)
        {
            return new List<int[]>();
        }
    }
}