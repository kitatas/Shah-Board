using ShahBoard.InGame.Data.Container;
using ShahBoard.InGame.Presentation.View;
using UnityEngine;

namespace ShahBoard.InGame.Domain.Factory
{
    public sealed class PieceFactory
    {
        private readonly IWriteOnlyPieceContainer _pieceContainer;

        public PieceFactory(IWriteOnlyPieceContainer pieceContainer)
        {
            _pieceContainer = pieceContainer;
        }

        public void Generate(PieceView pieceView, Vector3 position, PlayerType type)
        {
            var pieceObject = Object.Instantiate(pieceView);
            pieceObject.Init(position, type);
            _pieceContainer.Add(type, pieceObject);
        }
    }
}