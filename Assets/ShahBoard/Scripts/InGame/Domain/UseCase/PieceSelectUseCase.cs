using ShahBoard.InGame.Presentation.View;
using UnityEngine;

namespace ShahBoard.InGame.Domain.UseCase
{
    public sealed class PieceSelectUseCase
    {
        private readonly Camera _camera;

        public PieceSelectUseCase(Camera camera)
        {
            _camera = camera;
        }

        public PieceView GetPiece(Vector3 tapPosition, PlayerType playerType)
        {
            var ray = _camera.ScreenPointToRay(tapPosition);
            if (Physics.Raycast(ray, out var hit))
            {
                var piece = hit.collider.gameObject.GetComponent<PieceView>();
                if (piece != null && piece.playerType == playerType && piece.IsInDeck())
                {
                    return piece;
                }
            }

            return null;
        }

        public BoardPlacementView GetNextPlacement(Vector3 tapPosition)
        {
            var ray = _camera.ScreenPointToRay(tapPosition);
            if (Physics.Raycast(ray, out var hit))
            {
                var placement = hit.collider.gameObject.GetComponent<BoardPlacementView>();
                if (placement != null && placement.IsEqualPlacementType(PlacementType.Valid))
                {
                    return placement;
                }
            }

            return null;
        }

        /// <summary>
        /// 盤上のコマを取得
        /// </summary>
        /// <param name="tapPosition"></param>
        /// <returns></returns>
        public PieceView GetBoardPiece(Vector3 tapPosition)
        {
            var ray = _camera.ScreenPointToRay(tapPosition);
            if (Physics.Raycast(ray, out var hit))
            {
                var piece = hit.collider.gameObject.GetComponent<PieceView>();
                if (piece != null && piece.IsInDeck())
                {
                    return piece;
                }
            }

            return null;
        }
    }
}