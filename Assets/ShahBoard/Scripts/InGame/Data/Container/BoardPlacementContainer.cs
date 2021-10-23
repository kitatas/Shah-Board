using System;
using System.Collections.Generic;
using ShahBoard.InGame.Presentation.View;

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

        public List<BoardPlacementView> GetAllPlacement()
        {
            return _placementViews;
        }

        public List<BoardPlacementView> GetEditPlacement(PlayerType playerType)
        {
            return _piecePlacementViews[(int)playerType - 1];
        }
    }
}