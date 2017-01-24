using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DronningeProblemet.BlockedFields
{
	public class KnighBlockedFields : IBlockedFields
	{
		public List<int[]> GetBlockedFields(int x, int y)
		{
			var list = new List<int[]>();

			TryAddToList(new[] { x + 1, y + 2 }, list);
			TryAddToList(new[] { x + 2, y + 1 }, list);

			TryAddToList(new[] { x - 1, y - 2 }, list);
			TryAddToList(new[] { x - 2, y - 1 }, list);

			TryAddToList(new[] { x + 1, y - 2 }, list);
			TryAddToList(new[] { x + 2, y - 1 }, list);

			TryAddToList(new[] { x - 1, y + 2 }, list);
			TryAddToList(new[] { x - 2, y + 1 }, list);

			return list;
		}

		private static void TryAddToList(int[] coord, ICollection<int[]> list)
		{
			if (coord.Any(i => i < 0 || i > 7))
				return;
			list.Add(coord);
		}
	}
}
