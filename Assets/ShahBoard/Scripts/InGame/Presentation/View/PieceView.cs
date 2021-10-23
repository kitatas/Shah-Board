using UnityEngine;

namespace ShahBoard.InGame.Presentation.View
{
    public sealed class PieceView : MonoBehaviour
    {
        [SerializeField] private PieceType type = default;
        private PieceStatus _status;
        private Vector3 _initPosition;
        private Vector3 _inDeckPosition;

        public PieceType pieceType => type;
        public PlayerType playerType { get; private set; }

        public void Init(Vector3 position, PlayerType type)
        {
            _initPosition = position;
            SetInitPosition();
            playerType = type;

            if (pieceType == PieceType.Emperor)
            {
                _status = PieceStatus.InDeck;
                _inDeckPosition = position;
            }
            else
            {
                _status = PieceStatus.None;
            }
        }

        public void SetInitPosition()
        {
            SetPosition(_initPosition);
            RemoveDeck();
        }

        public void SetInDeckPosition()
        {
            SetPosition(_inDeckPosition);
        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        public bool IsInDeck()
        {
            return _status == PieceStatus.InDeck;
        }

        public void RemoveDeck()
        {
            _status = PieceStatus.None;
        }

        public void UpdateCurrentPlacement(Vector3 position)
        {
            _status = PieceStatus.InDeck;
            _inDeckPosition = position;
            SetInDeckPosition();
        }

        public Vector3 GetInDeckPosition()
        {
            return _inDeckPosition;
        }
    }
}