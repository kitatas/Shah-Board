using ShahBoard.InGame.Presentation.View;

namespace ShahBoard.InGame.Data.Container
{
    public interface IReadOnlyBoardPlacementContainer
    {
        void UpdateEditPlacement(PlayerType playerType, PlacementType placementType);
        BoardPlacementView FindPlacement(PieceView pieceView);
    }
}