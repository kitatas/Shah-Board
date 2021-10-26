using ShahBoard.InGame.Data.Container;
using ShahBoard.InGame.Presentation.View;
using UnityEngine;

namespace ShahBoard.InGame.Domain.Factory
{
    public sealed class PieceFactory
    {
        private readonly Transform _board;
        private readonly IWriteOnlyPieceContainer _pieceContainer;

        public PieceFactory(Transform board, IWriteOnlyPieceContainer pieceContainer)
        {
            _board = board;
            _pieceContainer = pieceContainer;
        }

        public PieceView Generate(PieceView pieceView, Vector3 position, PlayerType type)
        {
            var pieceObject = Object.Instantiate(pieceView, _board);
            pieceObject.Init(position, type);
            _pieceContainer.Add(type, pieceObject);
            return pieceObject;
        }
    }
}