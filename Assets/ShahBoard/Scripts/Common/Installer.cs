using ShahBoard.Common.Data.DataStore;
using ShahBoard.Common.Domain.Repository;
using ShahBoard.Common.Domain.UseCase;
using ShahBoard.Common.Presentation.Controller;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace ShahBoard.Common
{
    public sealed class Installer : LifetimeScope
    {
        [SerializeField] private LanguageTable languageTable = default;
        [SerializeField] private BgmTable bgmTable = default;
        [SerializeField] private SeTable seTable = default;

        protected override void Configure(IContainerBuilder builder)
        {
            // DataStore
            builder.RegisterInstance<LanguageTable>(languageTable);
            builder.RegisterInstance<BgmTable>(bgmTable);
            builder.RegisterInstance<SeTable>(seTable);

            // Repository
            builder.Register<LanguageRepository>(Lifetime.Singleton);
            builder.Register<SaveRepository>(Lifetime.Singleton);
            builder.Register<SoundRepository>(Lifetime.Singleton);

            // UseCase
            builder.Register<LanguageUseCase>(Lifetime.Singleton);
            builder.Register<SaveUseCase>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<SoundUseCase>(Lifetime.Singleton).AsImplementedInterfaces();

            // Controller
            builder.Register<SceneLoader>(Lifetime.Singleton);
            builder.Register<BgmController>(Lifetime.Singleton);
            builder.Register<SeController>(Lifetime.Singleton);
        }
    }
}