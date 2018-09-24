using System;
using DronningeProblemet.Enums;

namespace DronningeProblemet.BlockedFields
{
    public static class BlockedFieldsFactory
    {
        public static IBlockedFields Get(PieceType type)
        {
            switch (type)
            {
                case PieceType.King:
                    return new DefaultBlockedFiels();
                case PieceType.Rook:
                    return new DefaultBlockedFiels();
                case PieceType.Bishop:
                    return new DefaultBlockedFiels();
                case PieceType.Queen:
                    return new QueenBlockedFields();
                case PieceType.Knight:
                    return new KnightBlockedFields();
                case PieceType.Pawn:
                    return new DefaultBlockedFiels();
                case PieceType.Empty:
                    return new EmptyBlockedFields();
	            default:
		            throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}
