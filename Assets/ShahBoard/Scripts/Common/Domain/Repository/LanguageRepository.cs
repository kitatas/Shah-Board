using System.Collections.Generic;
using ShahBoard.Common.Data.DataStore;
using ShahBoard.Common.Data.Entity;
using UnityEngine;

namespace ShahBoard.Common.Domain.Repository
{
    public sealed class LanguageRepository
    {
        private readonly List<LanguageEntity> _languageEntities;

        public LanguageRepository(LanguageTable languageTable)
        {
            _languageEntities = new List<LanguageEntity>();
            foreach (var textAsset in languageTable.list)
            {
                var data = JsonUtility.FromJson<LanguageEntity>(textAsset.ToString());
                _languageEntities.Add(data);
            }
        }

        public LanguageData Find(LanguageType type)
        {
            return _languageEntities
                .Find(x => x.type == type).data;
        }
    }
}