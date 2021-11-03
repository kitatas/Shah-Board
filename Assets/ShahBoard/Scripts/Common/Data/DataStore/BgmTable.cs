using System.Collections.Generic;
using UnityEngine;

namespace ShahBoard.Common.Data.DataStore
{
    [CreateAssetMenu(fileName = nameof(BgmTable), menuName = "CommonTable/" + nameof(BgmTable), order = 0)]
    public sealed class BgmTable : ScriptableObject
    {
        [SerializeField] private List<BgmData> dataList = default;

        public List<BgmData> list => dataList;
    }
}