using UnityEngine;

namespace ShahBoard.Common.Data.DataStore
{
    [CreateAssetMenu(fileName = nameof(BgmData), menuName = "CommonTable/" + nameof(BgmData), order = 0)]
    public sealed class BgmData : ScriptableObject
    {
        [SerializeField] private BgmType bgmType = default;
        [SerializeField] private AudioClip audioClip = default;

        public BgmType type => bgmType;
        public AudioClip clip => audioClip;
    }
}