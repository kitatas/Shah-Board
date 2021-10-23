using System.Collections.Generic;
using ShahBoard.InGame.Presentation.View;

namespace ShahBoard.InGame.Data.Container
{
    public interface IReadOnlyPieceContainer
    {
        List<PieceView> GetPlayerPiece(PlayerType type);
    }
}