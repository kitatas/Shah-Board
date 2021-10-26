using ShahBoard.InGame.Domain.UseCase;
using ShahBoard.InGame.Presentation.View;
using UniRx;
using VContainer.Unity;

namespace ShahBoard.InGame.Presentation.Presenter
{
    public sealed class MatchingPresenter : IPostInitializable
    {
        private readonly MatchingStateUseCase _matchingStateUseCase;
        private readonly MatchingStateView _matchingStateView;

        public MatchingPresenter(MatchingStateUseCase matchingStateUseCase, MatchingStateView matchingStateView)
        {
            _matchingStateUseCase = matchingStateUseCase;
            _matchingStateView = matchingStateView;
        }

        public void PostInitialize()
        {
            _matchingStateUseCase.matchingState
                .Subscribe(_matchingStateView.Show)
                .AddTo(_matchingStateView);
        }
    }
}