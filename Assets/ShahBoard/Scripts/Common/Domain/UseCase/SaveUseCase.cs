using ShahBoard.Common.Domain.Repository;

namespace ShahBoard.Common.Domain.UseCase
{
    public sealed class SaveUseCase : IReadOnlySaveUseCase
    {
        private readonly SaveRepository _saveRepository;

        public SaveUseCase(SaveRepository saveRepository)
        {
            _saveRepository = saveRepository;
        }

        public LanguageType LoadLanguageType()
        {
            return _saveRepository.Load().languageType;
        }
    }
}