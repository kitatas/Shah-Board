using ShahBoard.InGame.Presentation.View;
using UnityEngine;

namespace ShahBoard.InGame.Domain.UseCase
{
    public sealed class EditUseCase
    {
        private readonly Camera _camera;
        private const float EDIT_HEIGHT = 5.0f;

        public EditUseCase(Camera camera)
        {
            _camera = camera;
        }

        /// <summary>
        /// 編成可能なコマを取得
        /// </summary>
        /// <param name="touchPosition"></param>
        /// <returns></returns>
        public PieceView GetEditPiece(Vector3 touchPosition)
        {
            var ray = _camera.ScreenPointToRay(touchPosition);
            if (Physics.Raycast(ray, out var hit))
            {
                var piece = hit.collider.gameObject.GetComponent<PieceView>();
                if (piece != null && piece.pieceType != PieceType.Emperor)
                {
                    return piece;
                }
            }

            return null;
        }

        public Vector3 GetEditPosition(Vector3 touchPosition)
        {
            var position = _camera.ScreenToWorldPoint(touchPosition);
            position.y = EDIT_HEIGHT;
            return position;
        }

        /// <summary>
        /// 配置可能マスを取得
        /// </summary>
        /// <param name="view"></param>
        /// <param name="tapPosition"></param>
        /// <returns></returns>
        public BoardPlacementView GetValidPlacement(PieceView view, Vector3 tapPosition)
        {
            var position = _camera.ScreenToWorldPoint(tapPosition);
            position.y = EDIT_HEIGHT;
            var ray = new Ray(position, -view.transform.up);
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
        /// 皇帝以外の配置済みのコマを取得
        /// </summary>
        /// <param name="view"></param>
        /// <param name="tapPosition"></param>
        /// <returns></returns>
        public PieceView GetPlacementPiece(PieceView view, Vector3 tapPosition)
        {
            var position = _camera.ScreenToWorldPoint(tapPosition);
            position.y = EDIT_HEIGHT;
            var ray = new Ray(position, -view.transform.up);
            if (Physics.Raycast(ray, out var hit))
            {
                var piece = hit.collider.gameObject.GetComponent<PieceView>();
                if (piece != null && piece.pieceType != PieceType.Emperor && piece.IsInDeck())
                {
                    return piece;
                }
            }

            return null;
        }
    }
}