using System;
using System.Collections.Generic;
using System.Linq;
using DronningeProblemet.Enums;

namespace DronningeProblemet
{
    public class ChessBoard
    {
        public Field[,] Board;
        /// <summary> Standard Contructor</summary>
        public ChessBoard()
        {
            InitializeBoard();
        }

        /// <summary> Get a copy of the board. </summary>
        /// <returns> Copy of the board.</returns>
        public ChessBoard Copy()
        {
            var board = new ChessBoard();

            for(var x= 0; x<8;x++)
            {
                for(var y = 0; y<8;y++)
                {
                    board.Board[x, y].Blocked = Board[x, y].Blocked;
                    board.Board[x, y].Piece = Board[x, y].Piece;
                }
            }
            return board;
        }

        public ChessBoard TurnedCopy()
        {
            var board = new ChessBoard();

            for (var x = 0; x < 8; x++)
            {
                for (var y = 0; y < 8; y++)
                {
                    board.Board[y, 7-x].Blocked = Board[x, y].Blocked;
                    board.Board[y, 7-x].Piece = Board[x, y].Piece;
                }
            }
            return board;
        }

	    public ChessBoard HorizontalMirrorCopy()
	    {
		    var board = new ChessBoard();

			for (var x = 0; x < 8; x++)
			{
				for (var y = 0; y < 8; y++)
				{
					board.Board[x, Math.Abs(y - 7)].Blocked = Board[x, y].Blocked;
					board.Board[x, Math.Abs(y - 7)].Piece = Board[x, y].Piece;
				}
			}

			return board;
	    }

		public ChessBoard VerticalMirrorCopy()
		{
			var board = new ChessBoard();

			for (var x = 0; x < 8; x++)
			{
				for (var y = 0; y < 8; y++)
				{
					board.Board[Math.Abs(x - 7), y].Blocked = Board[x, y].Blocked;
					board.Board[Math.Abs(x - 7), y].Piece = Board[x, y].Piece;
				}
			}

			return board;
		}

		/// <summary> Calculates if an object is a board, 
		/// if it is, then if it is equal to this board.</summary>
		/// <param name="obj"> The object in question. </param>
		/// <returns>whether an object is a board, if it is, then if it is equal to this board.</returns>
		public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;
            var board = (ChessBoard)obj;


            var boardT1 = board.TurnedCopy();
            var boardT2 = boardT1.TurnedCopy();
            var boardT3 = boardT2.TurnedCopy();

            return CustomEquals(board)
                   || CustomEquals(boardT1)
                   || CustomEquals(boardT2)
                   || CustomEquals(boardT3)
				   //|| CustomEquals(VerticalMirrorCopy())
				   //|| CustomEquals(HorizontalMirrorCopy())
				   ;
        }

        private bool CustomEquals(ChessBoard b1)
        {
            for (var x = 0; x < 8; x++)
            {
                for (var y = 0; y < 8; y++)
                {
                    if (!b1.Board[x, y].Piece.Equals(Board[x, y].Piece))
                        return false;
                    if (b1.Board[x, y].Blocked != Board[x, y].Blocked)
                        return false;
                }
            }
            return true;
        }

        /// <summary>Gets the hashcode based on placement of Queens,
        /// this method should be redone, if any other Chesspiece then Queens is used.</summary>
        /// <returns>Hashcode based on placement of Queens.</returns>
        public override int GetHashCode()
        {

            var h1 = ThisHash();
            var board = TurnedCopy();
            var h2 = board.ThisHash();
            board = board.TurnedCopy();
            var h3 = board.ThisHash();
            board = board.TurnedCopy();
            var h4 = board.ThisHash();

	        var list = new List<int>
	        {
		        h1,
		        h2,
		        h3,
		        h4,
				//HorizontalMirrorCopy().ThisHash(),
				//VerticalMirrorCopy().ThisHash()
			};
            list.Sort();
            return list.Select(x => x.ToString()).Aggregate((x, y) => x.ToString() + y.ToString()).GetHashCode();
        }

        public int ThisHash()
        {
            var ret = 0;
            for (var x = 0; x < 8; x++)
            {
                for (var y = 0; y < 8; y++)
                {
                    if (Board[x, y].Piece.Type == PieceType.Empty)
                        continue;
                    ret = ret * 8;
                    ret = ret + (x + 1);
                    ret = ret * 8;
                    ret = ret + (y + 1);
                }
            }
            return ret;

        }

        /// <summary> Try to place a Chesspiece on the given x,y coord, 
        /// returns true if successful.</summary>
        /// <param name="x">x part of the coord.</param>
        /// <param name="y">y part of the coord.</param>
        /// <param name="piece">The ChessPiece.</param>
        /// <returns>Whether the piece gets placed.</returns>
        public bool TryPlacePiece(int x, int y, ChessPiece piece)
        {
            if(Board[x, y].Blocked || Board[x, y].Piece.Type != PieceType.Empty)
                return false;

            Board[x, y].Piece = piece;
                
            // Set blocked Fields
            var fieldsToBlock = piece.BlockedFields.GetBlockedFields(x, y);
            foreach (var item in fieldsToBlock.Where(item => Board[item[0], item[1]].Piece.Type == PieceType.Empty))
            {
                Board[item[0], item[1]].Blocked = true;
            }
            return true;
        }

        /// <summary> Print simple version of the board. </summary>
        public void PrintBoardSimple()
        {
            for (var x = 0; x < 8; x++)
            {
                for (var y = 0; y < 8; y++)
                {
                    var toWrite = "";
                    switch (Board[x, y].Piece.Type)
                    {
                        case PieceType.King:
                            toWrite = "K  ";
                            break;
                        case PieceType.Rook:
                            toWrite = "R  ";
                            break;
                        case PieceType.Bishop:
                            toWrite = "B  ";
                            break;
                        case PieceType.Queen:
                            toWrite = "Q  ";
                            break;
                        case PieceType.Knight:
                            toWrite = "C  ";
                            break;
                        case PieceType.Pawn:
                            toWrite = "P  ";
                            break;
                        case PieceType.Empty:
                            toWrite = "E  ";
                            break;
	                    default:
		                    throw new ArgumentOutOfRangeException();
                    }
	                if (Board[x, y].Blocked)
		                toWrite = "X  ";


	                Console.Write(toWrite);
                }
	            Console.WriteLine();
	            Console.WriteLine();
            }
        }

	    /// <summary> Print the board. </summary>
	    public void PrintBoard()
	    {
		    for (var x = 0; x < 8; x++)
		    {
			    for (var y = 0; y < 8; y++)
			    {
				    var toWrite = "";

					switch (Board[x, y].Piece.Type)
				    {
					    case PieceType.King:
						    toWrite = "K  ";
						    break;
					    case PieceType.Rook:
						    toWrite = "R  ";
						    break;
					    case PieceType.Bishop:
						    toWrite = "B  ";
						    break;
					    case PieceType.Queen:
						    toWrite = "Q  ";
						    break;
					    case PieceType.Knight:
						    toWrite = "C  ";
						    break;
					    case PieceType.Pawn:
						    toWrite = "P  ";
						    break;
					    case PieceType.Empty:
						    toWrite = "E  ";
						    break;
					    default:
						    throw new ArgumentOutOfRangeException();
				    }
				    if (Board[x, y].Blocked)
					    toWrite = "X  ";

				    switch (Board[x, y].Color)
				    {
					    case FieldColor.Black:
						    toWrite = "B" + toWrite;
						    break;
					    case FieldColor.White:
						    toWrite = "W" + toWrite;
						    break;
					    default:
						    throw new ArgumentOutOfRangeException();
				    }
				    Console.Write(toWrite);
			    }
			    Console.WriteLine();
			    Console.WriteLine();
		    }
	    }

	    /// <summary> Initialize the board. </summary>
	    private void InitializeBoard()
	    {
		    Board = new Field[8, 8];

		    for (var x = 0; x < 8; x++)
		    {
			    for (var y = 0; y < 8; y++)
			    {
				    if (((x + y)%2) == 0)
				    {
					    Board[x, y] = new Field(FieldColor.Black);
				    }
				    else
				    {
					    Board[x, y] = new Field(FieldColor.White);
				    }
			    }
		    }
	    }
    }
}
