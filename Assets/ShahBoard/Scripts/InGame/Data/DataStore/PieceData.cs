using UnityEngine;

namespace ShahBoard.InGame.Data.DataStore
{
    [CreateAssetMenu(fileName = nameof(PieceData), menuName = "InGameTable/" + nameof(PieceData), order = 0)]
    public sealed class PieceData : ScriptableObject
    {
        [SerializeField] private PieceType pieceType = default;

        public PieceType type => pieceType;
    }
}