using System.Collections.Generic;

namespace DronningeProblemet.BlockedFields
{
    public class QueenBlockedFields : IBlockedFields
    {
        public List<int[]> GetBlockedFields(int x, int y)
        {
            var list = new List<int[]>();
            for (var i = 0; i < 8; i++)
            {
                list.Add(new[] { x, i });
                list.Add(new[] { i, y });
            }
            var tx = x;
            var ty = y;
            while (tx < 7 && ty < 7)
            {
                tx++;
                ty++;
                list.Add(new[] { tx, ty });
            }
            tx = x;
            ty = y;
            while (tx > 0 && ty > 0)
            {
                tx--;
                ty--;
                list.Add(new[] { tx, ty });
            }
            tx = x;
            ty = y;
            while (tx < 7 && ty > 0)
            {
                tx++;
                ty--;
                list.Add(new[] { tx, ty });
            }

            tx = x;
            ty = y;
            while (ty < 7 && tx > 0)
            {
                ty++;
                tx--;
                list.Add(new[] { tx, ty });
            }
            list.RemoveAll(o => o[0] == x && o[1] == y);
            return list;
        }
    }
}