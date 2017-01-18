using DronningeProblemet.BlockedFields;
using DronningeProblemet.Enums;

namespace DronningeProblemet
{
    public class ChessPiece
    {
        /// <summary> Type of the ChessPiece </summary>
        public readonly PieceType Type;

        public IBlockedFields BlockedFields;

        public ChessPiece(PieceType type)
        {
            Type = type;
            BlockedFields = BlockedFieldsFactory.Get(type);
        }

        /// <summary> The two Chesspieces are equal if they have same type. </summary>
        /// <param name="obj"> the object in question. </param>
        /// <returns> Whether the 2 pieces are equal. </returns>
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var chessPiece = (ChessPiece)obj;

            return (Type == chessPiece.Type);
        }

        /// <summary> Get hashcode based on type of the piece. </summary>
        /// <returns> Hashcode based on type of the piece.</returns>
        public override int GetHashCode()
        {
            return Type.GetHashCode();
        }
    }
}
