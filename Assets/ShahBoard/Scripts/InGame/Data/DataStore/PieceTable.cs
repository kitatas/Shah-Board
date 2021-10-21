using System.Collections.Generic;
using ShahBoard.InGame.Presentation.View;
using UnityEngine;

namespace ShahBoard.InGame.Data.DataStore
{
    [CreateAssetMenu(fileName = nameof(PieceTable), menuName = "InGameTable/" + nameof(PieceTable), order = 0)]
    public sealed class PieceTable : ScriptableObject
    {
        [SerializeField] private List<PieceData> dataList = default;
        [SerializeField] private List<PieceView> viewList = default;

        public List<PieceData> data => dataList;
        public List<PieceView> views => viewList;
    }
}