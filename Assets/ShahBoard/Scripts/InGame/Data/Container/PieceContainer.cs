using System.Collections.Generic;
using ShahBoard.InGame.Presentation.View;

namespace ShahBoard.InGame.Data.Container
{
    public sealed class PieceContainer : IWriteOnlyPieceContainer
    {
        private readonly List<PieceView>[] _pieceViews;

        public PieceContainer()
        {
            _pieceViews = new[]
            {
                new List<PieceView>(),
                new List<PieceView>(),
            };
        }

        public void Add(PlayerType type, PieceView view)
        {
            _pieceViews[(int)type - 1].Add(view);
        }
    }
}