using System.Collections.Generic;
using ShahBoard.InGame.Presentation.View;

namespace ShahBoard.InGame.Data.Container
{
    public sealed class BoardPlacementContainer : IWriteOnlyBoardPlacementContainer
    {
        private readonly List<BoardPlacementView> _placementViews;

        public BoardPlacementContainer()
        {
            _placementViews = new List<BoardPlacementView>();
        }

        public void Add(BoardPlacementView placementView)
        {
            _placementViews.Add(placementView);
        }
    }
}