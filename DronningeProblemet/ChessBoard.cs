﻿using System;
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
        public ChessBoard CopyBoard()
        {
            var board = new ChessBoard();

            for(int x= 0; x<8;x++)
            {
                for(int y = 0; y<8;y++)
                {
                    board.Board[x, y].Blocked = Board[x, y].Blocked;
                    board.Board[x, y].Piece = Board[x, y].Piece;
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

            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    if (!board.Board[x, y].Piece.Equals(Board[x, y].Piece))
                        return false;
                    if ((board.Board[x, y].Blocked != Board[x, y].Blocked))
                        return false;
                    if ((board.Board[x, y].Color != Board[x, y].Color))
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
            int ret = 0;
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    if(Board[x,y].Piece.Type == PieceType.Queen)
                    {
                        ret = ret * 8;
                        ret = ret + (x + 1);
                        ret = ret * 8;
                        ret = ret + (y + 1);
                    }
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
            if(Board[x, y].Blocked)
                return false;
            if(Board[x, y].Piece.Type == PieceType.Empty)
            {
                Board[x, y].Piece = piece;
                
                // Set blocked Fields
                List<int[]> fieldsToBlock = piece.BlockedFields.GetBlockedFields(x, y);
                foreach (int[] item in fieldsToBlock.Where(item => Board[item[0], item[1]].Piece.Type == PieceType.Empty))
                {
                    Board[item[0], item[1]].Blocked = true;
                }
                return true;
            }
            return false;
        }

        /// <summary> Determines whether the coords given are blocked. </summary>
        /// <param name="x"> X Coord. </param>
        /// <param name="y"> Y Coord. </param>
        /// <returns> Whether the coords are blocked</returns>
        public bool IsUnblocked(int x, int y)
        {
            return !Board[x, y].Blocked && Board[x, y].Piece.Type == PieceType.Empty;
        }

        /// <summary> Print simple version of the board. </summary>
        public void PrintBoardSimple()
        {
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    string toWrite = "";
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
            for (int x = 0; x < 8; x++)
            {
                for (int y = 0; y < 8; y++)
                {
                    string toWrite = "";
                    switch (Board[x,y].Piece.Type)
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
                            break;
                    }
                    if (Board[x, y].Blocked)
                        toWrite = "X  ";

                    switch (Board[x,y].Color)
                    {
                        case FieldColor.Black:
                            toWrite = "B" + toWrite;
                            break;
                        case FieldColor.White:
                            toWrite = "W" + toWrite;
                            break;
                        default:
                            break;
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

            for (int x = 0; x < 8; x++)
            {
                for(int y = 0; y < 8; y++)
                {
                    if(((x+y) % 2) == 0)
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