using ShahBoard.InGame.Presentation.View;

namespace ShahBoard.InGame.Data.Container
{
    public interface IWriteOnlyBoardPlacementContainer
    {
        void Add(BoardPlacementView placementView, PlayerType type);
    }
}