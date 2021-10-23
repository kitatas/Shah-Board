using System;
using System.Collections.Generic;
using ShahBoard.InGame.Presentation.View;
using UnityEngine;

namespace ShahBoard.InGame.Data.Container
{
    public sealed class BoardPlacementContainer : IWriteOnlyBoardPlacementContainer, IReadOnlyBoardPlacementContainer
    {
        private readonly List<BoardPlacementView> _placementViews;
        private readonly List<BoardPlacementView>[] _piecePlacementViews;

        public BoardPlacementContainer()
        {
            _placementViews = new List<BoardPlacementView>();
            _piecePlacementViews = new[]
            {
                new List<BoardPlacementView>(),
                new List<BoardPlacementView>(),
            };
        }

        public void Add(BoardPlacementView placementView, PlayerType type)
        {
            _placementViews.Add(placementView);

            switch (type)
            {
                case PlayerType.None:
                    break;
                case PlayerType.Master:
                case PlayerType.Client:
                    _piecePlacementViews[(int)type - 1].Add(placementView);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }

        public void UpdateEditPlacement(PlayerType playerType, PlacementType placementType)
        {
            foreach (var placementView in _piecePlacementViews[(int)playerType - 1])
            {
                placementView.UpdatePlacementType(placementType);
            }
        }

        public List<BoardPlacementView> GetAllPlacement()
        {
            return _placementViews;
        }

        /// <summary>
        /// pieceViewと同じ位置のマスを取得
        /// </summary>
        /// <param name="pieceView"></param>
        /// <returns></returns>
        public BoardPlacementView FindPlacement(PieceView pieceView)
        {
            return _placementViews
                .Find(v =>
                    Mathf.Approximately(v.GetPosition().x, pieceView.GetInDeckPosition().x) &&
                    Mathf.Approximately(v.GetPosition().z, pieceView.GetInDeckPosition().z));
        }

        /// <summary>
        /// PlayerTypeの駒がある全てのBoardPlacementViewを取得
        /// </summary>
        /// <param name="playerType"></param>
        public List<BoardPlacementView> GetPiecePlacementList(PlayerType playerType)
        {
            return _placementViews
                .FindAll(x => x.GetPlacementPiece() != null && x.GetPlacementPiece().playerType == playerType);
        }
    }
}