using System;
using System.Collections.Generic;
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
            var queen = new ChessPiece(PieceType.Queen);
            
            // Array for 64 boards
            var boards = new ChessBoard[8, 8];

            var solutionSet = new HashSet<ChessBoard>();

            // First queen can be in 64 places, place one in each and insert them into the HashSet.
            for (var x = 0; x < 8; x++)
            {
                for (var y = 0; y < 8; y++)
                {
                    boards[x, y] = new ChessBoard();
                    boards[x, y].TryPlacePiece(x, y, queen);
                    solutionSet.Add(boards[x, y]);
                }
            }

            var numberOfQueens = 1;
            while (true)
            {
                var tempSolutionSet = new HashSet<ChessBoard>();
                foreach (var board in solutionSet)
                {
                    for (var x = 0; x < 8; x++)
                    {
                        for (var y = 0; y < 8; y++)
                        {
                            // If coords are empty and unblocked, place a queen and add them to the temporary Solutionset.
	                        if (!board.Board[x, y].IsFree)
								continue;
	                        var newBoard = board.Copy();

	                        if (newBoard.TryPlacePiece(x, y, queen))
		                        tempSolutionSet.Add(newBoard);
	                        else
		                        log("ERROR: Could not place piece");
                        }
                    }
                }
                if (tempSolutionSet.Count == 0)
                    return new ResultSet(numberOfQueens, solutionSet);
                numberOfQueens++;
                solutionSet = tempSolutionSet;
                log(tempSolutionSet.Count + " Solutions with " + numberOfQueens + " Queens");
            }
        }
    }
}
