using System.Collections.Generic;

namespace DronningeProblemet.BlockedFields
{
    public interface IBlockedFields
    {
        List<int[]> GetBlockedFields(int x, int y);
    }
}