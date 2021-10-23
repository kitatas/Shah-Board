using ShahBoard.InGame.Presentation.View;
using UnityEngine;

namespace ShahBoard.InGame.Domain.UseCase
{
    public interface IBoardPlacementContainerUseCase
    {
        void UpdateEditPlacement(PlayerType playerType, PlacementType placementType);
        BoardPlacementView FindPlacement(PieceView pieceView);
        void UpdateAllPlacementType(PlacementType placementType);
        void UpdatePlayerPiecePlacement(PlayerType playerType, PlacementType placementType);
        void SetUpMoveRangePlacement(PlayerType playerType, Vector3[] positionList);
    }
}