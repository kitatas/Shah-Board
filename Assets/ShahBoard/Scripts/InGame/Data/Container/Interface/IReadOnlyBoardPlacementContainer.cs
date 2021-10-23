using System.Collections.Generic;
using ShahBoard.InGame.Presentation.View;

namespace ShahBoard.InGame.Data.Container
{
    public interface IReadOnlyBoardPlacementContainer
    {
        void UpdateEditPlacement(PlayerType playerType, PlacementType placementType);
        List<BoardPlacementView> GetAllPlacement();
        BoardPlacementView FindPlacement(PieceView pieceView);
        List<BoardPlacementView> GetPiecePlacementList(PlayerType playerType);
    }
}