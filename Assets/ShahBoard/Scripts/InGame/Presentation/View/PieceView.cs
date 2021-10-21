using UnityEngine;

namespace ShahBoard.InGame.Presentation.View
{
    public sealed class PieceView : MonoBehaviour
    {
        [SerializeField] private PieceType type = default;

        public PieceType pieceType => type;
        public PlayerType playerType { get; private set; }

        public void Init(Vector3 position, PlayerType type)
        {
            transform.position = position;
            playerType = type;
        }
    }
}