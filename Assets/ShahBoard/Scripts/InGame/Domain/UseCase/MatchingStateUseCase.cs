using ShahBoard.InGame.Data.Entity;
using UniRx;

namespace ShahBoard.InGame.Domain.UseCase
{
    public sealed class MatchingStateUseCase
    {
        private readonly MatchingStateEntity _matchingStateEntity;
        private readonly ReactiveProperty<MatchingState> _matchingState;

        public MatchingStateUseCase(MatchingStateEntity matchingStateEntity)
        {
            _matchingStateEntity = matchingStateEntity;
            _matchingState = new ReactiveProperty<MatchingState>(_matchingStateEntity.Get());
        }

        public IReadOnlyReactiveProperty<MatchingState> matchingState => _matchingState;

        public void SetState(MatchingState state)
        {
            _matchingStateEntity.Set(state);
            _matchingState.Value = _matchingStateEntity.Get();
        }

        public bool IsEqual(MatchingState state)
        {
            return _matchingStateEntity.Get() == state;
        }
    }
}