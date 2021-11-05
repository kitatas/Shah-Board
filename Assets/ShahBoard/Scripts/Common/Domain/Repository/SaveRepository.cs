using ShahBoard.Common.Data.Entity;

namespace ShahBoard.Common.Domain.Repository
{
    public sealed class SaveRepository
    {
        private static SaveEntity CreateNewData()
        {
            return new SaveEntity
            {
                languageType = LanguageType.Japanese,
            };
        }

        public SaveEntity Load()
        {
            // TODO: セーブデータがない場合
            {
                return CreateNewData();
            }
        }

        public void Save()
        {
            
        }
    }
}