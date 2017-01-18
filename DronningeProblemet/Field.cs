using DronningeProblemet.Enums;

namespace DronningeProblemet
{
    public class Field
    {
        /// <summary> Color of the field. </summary>
        public FieldColor Color;

        /// <summary> The Chesspiece on the field. </summary>
        public ChessPiece Piece;

        /// <summary> Whether or not the field is blocked. </summary>
        public bool Blocked;

        /// <summary> Placeholder for empty field. </summary>
        private static ChessPiece p = new ChessPiece(PieceType.Empty);

        /// <summary> Constructor. </summary>
        /// <param name="color"> Color of the Field. </param>
        /// <param name="blocked"> Whether or not the field is blocked, default is false. </param>
        public Field(FieldColor color, bool blocked = false)
        {
            Blocked = blocked;
            Color = color;
            Piece = p;
        }
    }
}
