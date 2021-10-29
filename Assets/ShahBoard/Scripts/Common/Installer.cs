using ShahBoard.Common.Presentation.Controller;
using VContainer;
using VContainer.Unity;

namespace ShahBoard.Common
{
    public sealed class Installer : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<SceneLoader>(Lifetime.Singleton);
        }
    }
}