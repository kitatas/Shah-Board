using ShahBoard.InGame.Data.Container;
using ShahBoard.InGame.Presentation.View;
using UnityEngine;

namespace ShahBoard.InGame.Domain.Factory
{
    public sealed class BoardFactory
    {
        private readonly Transform _board;
        private readonly IWriteOnlyBoardPlacementContainer _placementContainer;

        public BoardFactory(Transform board, IWriteOnlyBoardPlacementContainer placementContainer)
        {
            _board = board;
            _placementContainer = placementContainer;
        }

        public void GeneratePlacementObject(BoardPlacementView placementView, Vector3 generatePosition)
        {
            var placementObject = Object.Instantiate(placementView);
            placementObject.Init(_board, generatePosition);
            _placementContainer.Add(placementObject);
        }
    }
}