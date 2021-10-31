using System.Collections.Generic;
using System.Linq;
using ShahBoard.InGame.Domain.Repository;
using ShahBoard.InGame.Presentation.View;
using UnityEngine;

namespace ShahBoard.InGame.Domain.UseCase
{
    public sealed class PieceDataUseCase
    {
        private readonly PieceRepository _pieceRepository;

        public PieceDataUseCase(PieceRepository pieceRepository)
        {
            _pieceRepository = pieceRepository;
        }

        public IEnumerable<Vector3> GetMoveRangeList(PieceView pieceView)
        {
            return _pieceRepository.FindData(pieceView.pieceType).GetMoveRange()
                .Select(v => v + pieceView.GetInDeckPosition());
        }

        public Sprite GetPieceMoveRangeSprite(PieceType pieceType)
        {
            return _pieceRepository.FindData(pieceType).sprite;
        }
    }
}