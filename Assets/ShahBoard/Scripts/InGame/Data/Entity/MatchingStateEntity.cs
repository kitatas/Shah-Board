namespace ShahBoard.InGame.Data.Entity
{
    public sealed class MatchingStateEntity
    {
        private MatchingState _matchingState;

        public MatchingStateEntity()
        {
            _matchingState = MatchingState.None;
        }

        public MatchingState Get() => _matchingState;

        public void Set(MatchingState state) => _matchingState = state;
    }
}