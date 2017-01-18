using System.Collections.Generic;

namespace DronningeProblemet.BlockedFields
{
    public class DefaultBlockedFiels : IBlockedFields
    {
        public List<int[]> GetBlockedFields(int x, int y)
        {
            return new List<int[]>();
        }
    }
}