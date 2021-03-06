using System.Collections.Generic;
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
        void SetUpMoveRangePlacement(PlayerType playerType, IEnumerable<Vector3> positionList);
        void SetAllInDeckAuto(PlayerType playerType);
        void RemoveAllInDeck(PlayerType playerType);
    }
}