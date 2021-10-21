using ShahBoard.InGame.Domain.UseCase;

namespace ShahBoard.InGame.Presentation.Controller
{
    public abstract class MatchState : BaseGameState
    {
        protected readonly MatchingStateUseCase _matchingStateUseCase;

        protected MatchState(MatchingStateUseCase matchingStateUseCase)
        {
            _matchingStateUseCase = matchingStateUseCase;
        }

        public override GameState state => GameState.Match;
    }
}