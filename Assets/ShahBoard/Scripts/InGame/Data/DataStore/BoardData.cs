using ShahBoard.InGame.Presentation.View;
using UnityEngine;

namespace ShahBoard.InGame.Data.DataStore
{
    [CreateAssetMenu(fileName = nameof(BoardData), menuName = "InGameTable/" + nameof(BoardData), order = 0)]
    public sealed class BoardData : ScriptableObject
    {
        [SerializeField] private BoardPlacementView boardPlacementViewPrefab = default;

        public BoardPlacementView boardPlacementView => boardPlacementViewPrefab;
    }
}