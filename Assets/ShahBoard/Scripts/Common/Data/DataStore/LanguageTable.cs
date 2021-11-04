using System.Collections.Generic;
using UnityEngine;

namespace ShahBoard.Common.Data.DataStore
{
    [CreateAssetMenu(fileName = nameof(LanguageTable), menuName = "CommonTable/" + nameof(LanguageTable), order = 0)]
    public sealed class LanguageTable : ScriptableObject
    {
        [SerializeField] private List<TextAsset> dataList = default;

        public List<TextAsset> list => dataList;
    }
}