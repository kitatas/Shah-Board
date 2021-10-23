using ShahBoard.InGame.Presentation.View;

namespace ShahBoard.InGame.Domain.UseCase
{
    public sealed class MovementUseCase
    {
        private PieceView _selectPiece;
        private PieceView _removePiece;
        private BoardPlacementView _selectPlacement;

        public void Set(PieceView selectPiece, PieceView removePiece, BoardPlacementView selectPlacement)
        {
            _selectPiece = selectPiece;
            _removePiece = removePiece;
            _selectPlacement = selectPlacement;
        }

        public void Move()
        {
            // 削除するコマがある場合
            if (_removePiece != null)
            {
                _removePiece.RemoveDeck();
                _removePiece.gameObject.SetActive(false);
            }

            // コマの移動
            _selectPiece.UpdateCurrentPlacement(_selectPlacement.GetPosition());
            _selectPlacement.SetPlacementPiece(_selectPiece);

            Reset();
        }

        private void Reset()
        {
            _selectPiece = null;
            _removePiece = null;
            _selectPlacement = null;
        }
    }
}