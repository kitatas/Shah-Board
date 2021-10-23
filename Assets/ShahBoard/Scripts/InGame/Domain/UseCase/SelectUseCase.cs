using ShahBoard.InGame.Presentation.View;
using UnityEngine;

namespace ShahBoard.InGame.Domain.UseCase
{
    public sealed class SelectUseCase
    {
        private readonly Camera _camera;

        public SelectUseCase(Camera camera)
        {
            _camera = camera;
        }

        /// <summary>
        /// 有効なマスを取得
        /// </summary>
        /// <param name="tapPosition"></param>
        /// <returns></returns>
        public BoardPlacementView GetValidPlacement(Vector3 tapPosition)
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

        /// <summary>
        /// 盤上のPlayerTypeのコマを取得
        /// </summary>
        /// <param name="tapPosition"></param>
        /// <param name="playerType"></param>
        /// <returns></returns>
        public PieceView GetPlayerPiece(Vector3 tapPosition, PlayerType playerType)
        {
            var piece = GetBoardPiece(tapPosition);
            if (piece != null && piece.playerType == playerType)
            {
                return piece;
            }

            return null;
        }
    }
}