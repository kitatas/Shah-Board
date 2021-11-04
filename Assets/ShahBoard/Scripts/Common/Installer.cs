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

        protected override void Configure(IContainerBuilder builder)
        {
            // DataStore
            builder.RegisterInstance<LanguageTable>(languageTable);

            // Repository
            builder.Register<LanguageRepository>(Lifetime.Singleton);

            // UseCase
            builder.Register<LanguageUseCase>(Lifetime.Singleton);

            // Controller
            builder.Register<SceneLoader>(Lifetime.Singleton);
        }
    }
}