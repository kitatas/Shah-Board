using ShahBoard.InGame.Presentation.View;

namespace ShahBoard.InGame.Domain.UseCase
{
    public interface IBoardPlacementContainerUseCase
    {
        void UpdateEditPlacement(PlayerType playerType, PlacementType placementType);
        BoardPlacementView FindPlacement(PieceView pieceView);
    }
}