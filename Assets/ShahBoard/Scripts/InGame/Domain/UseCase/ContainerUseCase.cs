using ShahBoard.InGame.Data.Container;
using ShahBoard.InGame.Presentation.View;

namespace ShahBoard.InGame.Domain.UseCase
{
    public sealed class ContainerUseCase : IBoardPlacementContainerUseCase
    {
        private readonly IReadOnlyBoardPlacementContainer _placementContainer;

        public ContainerUseCase(IReadOnlyBoardPlacementContainer placementContainer)
        {
            _placementContainer = placementContainer;
        }

        public void UpdateEditPlacement(PlayerType playerType, PlacementType placementType)
        {
            _placementContainer.UpdateEditPlacement(playerType, placementType);
        }

        public BoardPlacementView FindPlacement(PieceView pieceView)
        {
            return _placementContainer.FindPlacement(pieceView);
        }
    }
}