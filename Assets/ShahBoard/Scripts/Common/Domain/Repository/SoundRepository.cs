using ShahBoard.Common.Data.DataStore;
using UnityEngine;

namespace ShahBoard.Common.Domain.Repository
{
    public sealed class SoundRepository
    {
        private readonly BgmTable _bgmTable;
        private readonly SeTable _seTable;

        public SoundRepository(BgmTable bgmTable, SeTable seTable)
        {
            _bgmTable = bgmTable;
            _seTable = seTable;
        }

        public AudioClip Find(BgmType bgmType)
        {
            return _bgmTable.list.Find(x => x.type == bgmType).clip;
        }

        public AudioClip Find(SeType seType)
        {
            return _seTable.list.Find(x => x.type == seType).clip;
        }
    }
}