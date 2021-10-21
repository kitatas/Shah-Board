using ShahBoard.InGame.Domain.UseCase;
using ShahBoard.InGame.Presentation.View;
using UniRx;

namespace ShahBoard.InGame.Presentation.Presenter
{
    public sealed class MatchingPresenter
    {
        public MatchingPresenter(MatchingStateUseCase matchingStateUseCase, MatchingStateView matchingStateView)
        {
            matchingStateUseCase.matchingState
                .Subscribe(matchingStateView.Show)
                .AddTo(matchingStateView);
        }
    }
}