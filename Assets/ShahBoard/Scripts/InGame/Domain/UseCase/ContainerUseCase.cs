using System.Collections.Generic;
using System.Linq;
using ShahBoard.Common;
using ShahBoard.InGame.Data.Container;
using ShahBoard.InGame.Presentation.View;
using UnityEngine;

namespace ShahBoard.InGame.Domain.UseCase
{
    public sealed class ContainerUseCase : IBoardPlacementContainerUseCase, IPieceContainerUseCase
    {
        private readonly IReadOnlyBoardPlacementContainer _placementContainer;
        private readonly IReadOnlyPieceContainer _pieceContainer;

        public ContainerUseCase(IReadOnlyBoardPlacementContainer placementContainer,
            IReadOnlyPieceContainer pieceContainer)
        {
            _placementContainer = placementContainer;
            _pieceContainer = pieceContainer;
        }

        public void UpdateEditPlacement(PlayerType playerType, PlacementType placementType)
        {
            foreach (var placementView in _placementContainer.GetEditPlacement(playerType))
            {
                placementView.UpdatePlacementType(placementType);
            }
        }

        public BoardPlacementView FindPlacement(PieceView pieceView)
        {
            return _placementContainer.GetAllPlacement()
                .Find(v => v.IsEqualPosition(pieceView.GetInDeckPosition()));
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
            foreach (var placementView in _placementContainer.GetAllPlacement())
            {
                var piece = placementView.GetPlacementPiece();
                if (piece != null && piece.playerType == playerType)
                {
                    placementView.UpdatePlacementType(placementType);
                }
            }
        }

        /// <summary>
        /// コマの移動範囲を設定
        /// </summary>
        /// <param name="playerType"></param>
        /// <param name="positionList"></param>
        public void SetUpMoveRangePlacement(PlayerType playerType, IEnumerable<Vector3> positionList)
        {
            foreach (var position in positionList)
            {
                foreach (var placementView in _placementContainer.GetAllPlacement())
                {
                    // 異なる座標
                    if (placementView.IsEqualPosition(position) == false)
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

        public void SetAllInDeckAuto(PlayerType playerType)
        {
            RemoveAllInDeck(playerType);

            var outDeckPieces = _pieceContainer.GetPlayerPiece(playerType)
                .Where(x => x.IsInDeck() == false)
                .ToList();

            foreach (var placement in _placementContainer.GetEditPlacement(playerType))
            {
                var piece = outDeckPieces[Random.Range(0, outDeckPieces.Count)];
                piece.UpdateCurrentPlacement(placement.GetPosition());
                placement.SetPlacementPiece(piece);
                outDeckPieces.Remove(piece);
            }
        }

        public void RemoveAllInDeck(PlayerType playerType)
        {
            // コマを初期位置に
            foreach (var piece in _pieceContainer.GetPlayerPiece(playerType))
            {
                if (piece.IsInDeck() && piece.pieceType != PieceType.Emperor)
                {
                    piece.SetInitPosition();
                }
            }

            // マスに配置されているコマ情報の初期化
            foreach (var placement in _placementContainer.GetEditPlacement(playerType))
            {
                placement.SetPlacementPiece(null);
            }
        }
        
        public bool IsNonePiece(PlayerType playerType)
        {
            var pieces = _pieceContainer.GetPlayerPiece(playerType).FindAll(x => x.IsInDeck());
            return pieces.Count == 1 || pieces.Count(x => x.pieceType == PieceType.Emperor) == 0;
        }
    }
}