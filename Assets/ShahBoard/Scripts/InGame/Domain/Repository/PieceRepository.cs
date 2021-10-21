using ShahBoard.InGame.Data.DataStore;
using ShahBoard.InGame.Presentation.View;

namespace ShahBoard.InGame.Domain.Repository
{
    public sealed class PieceRepository
    {
        private readonly PieceTable _pieceTable;

        public PieceRepository(PieceTable pieceTable)
        {
            _pieceTable = pieceTable;
        }

        public PieceView GetPiece(int index)
        {
            return _pieceTable.views[index];
        }

        public PieceView FindPiece(PieceType pieceType)
        {
            return _pieceTable.views.Find(x => x.pieceType == pieceType);
        }

        public PieceData FindData(PieceType pieceType)
        {
            return _pieceTable.data.Find(x => x.type == pieceType);
        }
    }
}