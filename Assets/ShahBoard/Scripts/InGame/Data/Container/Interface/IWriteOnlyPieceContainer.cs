using ShahBoard.InGame.Presentation.View;

namespace ShahBoard.InGame.Data.Container
{
    public interface IWriteOnlyPieceContainer
    {
        void Add(PlayerType type, PieceView view);
    }
}