using System.Collections.Generic;
using ShahBoard.InGame.Presentation.View;

namespace ShahBoard.InGame.Data.Container
{
    public interface IReadOnlyBoardPlacementContainer
    {
        List<BoardPlacementView> GetAllPlacement();
        List<BoardPlacementView> GetEditPlacement(PlayerType playerType);
    }
}