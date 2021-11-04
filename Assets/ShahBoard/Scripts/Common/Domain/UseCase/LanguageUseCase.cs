using ShahBoard.Common.Data.Entity;
using ShahBoard.Common.Domain.Repository;

namespace ShahBoard.Common.Domain.UseCase
{
    public sealed class LanguageUseCase
    {
        private readonly LanguageRepository _languageRepository;

        public LanguageUseCase(LanguageRepository languageRepository)
        {
            _languageRepository = languageRepository;
        }

        public PieceData Find(LanguageType languageType, PieceType pieceType)
        {
            return _languageRepository
                .Find(languageType).pieceData
                .Find(x => x.type == pieceType);
        }
    }
}