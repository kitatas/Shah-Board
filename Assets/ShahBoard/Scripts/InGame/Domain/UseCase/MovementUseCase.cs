using System.Linq;
using ShahBoard.InGame.Domain.Repository;
using ShahBoard.InGame.Presentation.View;
using UnityEngine;

namespace ShahBoard.InGame.Domain.UseCase
{
    public sealed class MovementUseCase
    {
        private readonly PieceRepository _repository;

        public MovementUseCase(PieceRepository repository)
        {
            _repository = repository;
        }

        public Vector3[] GetMoveRangeList(PieceView pieceView)
        {
            return _repository.FindData(pieceView.pieceType).GetMoveRange()
                .Select(v => v + pieceView.GetInDeckPosition())
                .ToArray();
        }
    }
}