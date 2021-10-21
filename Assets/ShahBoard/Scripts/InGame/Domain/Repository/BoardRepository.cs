using ShahBoard.InGame.Data.DataStore;
using ShahBoard.InGame.Presentation.View;

namespace ShahBoard.InGame.Domain.Repository
{
    public sealed class BoardRepository
    {
        private readonly BoardData _boardData;

        public BoardRepository(BoardData boardData)
        {
            _boardData = boardData;
        }

        public BoardPlacementView GetPlacement()
        {
            return _boardData.boardPlacementView;
        }
    }
}