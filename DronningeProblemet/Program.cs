using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using DronningeProblemet.Enums;

namespace DronningeProblemet
{
    public class Program
    {
	    private static void Main(string[] args)
        {
            Console.WriteLine("Solution = " + Algorithm(Console.WriteLine).Queens + " Queens");
            Console.ReadLine();
        }

        public static ResultSet Algorithm(Action<string> log)
        {
            // One queen is initialized and will be the only queen needed (She can be in many places at once :D), also to save memory.
            var chessPiece = new ChessPiece(PieceType.Queen);
            
            // Array for 64 boards
            var boards = new ChessBoard[8, 8];

            var solutionSet = GetInitialBoardCollection(boards, chessPiece);

	        var nrOfPieces = 1;
            while (true)
            {
	            var solutionSetCount = solutionSet.Count;
	            log($"Start foreeach with solutionSet Size: {solutionSetCount}");
	            var count = 0;

				var tempSolutionSet = new ConcurrentDictionary<int, ChessBoard>();
				Parallel.ForEach(solutionSet, (board) =>
	            {
					for (var x = 0; x < 8; x++)
					{
						for (var y = 0; y < 8; y++)
						{
							// If coords are empty and unblocked, place a queen and add them to the temporary Solutionset.
							if (!board.Value.Board[x, y].IsFree)
								continue;
							var newBoard = board.Value.Copy();

							if (newBoard.TryPlacePiece(x, y, chessPiece))
								tempSolutionSet.TryAdd(newBoard.GetHashCode(), newBoard);
							else
								log("ERROR: Could not place piece");
						}
					}
					count++;
					if (count % 1000 == 0)
						log($"{count}/{solutionSetCount} done");
				});
				

				if (tempSolutionSet.Count == 0)
                    return new ResultSet(nrOfPieces, solutionSet.Select(x => x.Value));
                nrOfPieces++;
                solutionSet = tempSolutionSet;
                log($"{tempSolutionSet.Count} Solutions with {nrOfPieces} {chessPiece.Type}s");
            }
        }

	    private static ConcurrentDictionary<int, ChessBoard> GetInitialBoardCollection(ChessBoard[,] boards, ChessPiece chessPiece)
	    {
		    var solutionSet = new ConcurrentDictionary<int, ChessBoard>();

		    // First queen can be in 64 places, place one in each and insert them into the HashSet.
		    for (var x = 0; x < 8; x++)
		    {
			    for (var y = 0; y < 8; y++)
			    {
				    boards[x, y] = new ChessBoard();
				    boards[x, y].TryPlacePiece(x, y, chessPiece);
				    solutionSet.TryAdd(boards[x, y].GetHashCode(), boards[x, y]);
			    }
		    }
		    return solutionSet;
	    }
    }
}
