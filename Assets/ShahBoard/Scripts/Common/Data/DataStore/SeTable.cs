using System.Collections.Generic;
using UnityEngine;

namespace ShahBoard.Common.Data.DataStore
{
    [CreateAssetMenu(fileName = nameof(SeTable), menuName = "CommonTable/" + nameof(SeTable), order = 0)]
    public sealed class SeTable : ScriptableObject
    {
        [SerializeField] private List<SeData> dataList = default;

        public List<SeData> list => dataList;
    }
}