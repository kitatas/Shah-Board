using ShahBoard.InGame.Data.Container;
using ShahBoard.InGame.Presentation.View;
using UnityEngine;

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

        public void UpdateAllPlacementType(PlacementType placementType)
        {
            foreach (var placementView in _placementContainer.GetAllPlacement())
            {
                placementView.UpdatePlacementType(placementType);
            }
        }

        /// <summary>
        /// PlayerのコマがあるマスのPlacementTypeを更新
        /// </summary>
        /// <param name="playerType"></param>
        /// <param name="placementType"></param>
        public void UpdatePlayerPiecePlacement(PlayerType playerType, PlacementType placementType)
        {
            UpdateAllPlacementType(PlacementType.Invalid);

            foreach (var placementView in _placementContainer.GetPiecePlacementList(playerType))
            {
                placementView.UpdatePlacementType(placementType);
            }
        }

        /// <summary>
        /// コマの移動範囲を設定
        /// </summary>
        /// <param name="playerType"></param>
        /// <param name="positionList"></param>
        public void SetUpMoveRangePlacement(PlayerType playerType, Vector3[] positionList)
        {
            for (int i = 0; i < positionList.Length; i++)
            {
                foreach (var placementView in _placementContainer.GetAllPlacement())
                {
                    // 異なる座標
                    if (Mathf.Approximately(positionList[i].x, placementView.GetPosition().x) == false ||
                        Mathf.Approximately(positionList[i].z, placementView.GetPosition().z) == false)
                    {
                        continue;
                    }

                    // 自分のコマが配置されていない場合
                    var placementPiece = placementView.GetPlacementPiece();
                    if (placementPiece == null || placementPiece.playerType != playerType)
                    {
                        placementView.UpdatePlacementType(PlacementType.Valid);
                    }

                    break;
                }
            }
        }
    }
}