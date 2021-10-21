using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using ShahBoard.InGame.Domain.UseCase;

namespace ShahBoard.InGame.Presentation.Controller
{
    public sealed class LocalMatchState : MatchState
    {
        public LocalMatchState(MatchingStateUseCase matchingStateUseCase) : base(matchingStateUseCase)
        {
        }

        public override async UniTask InitAsync(CancellationToken token)
        {
            
        }

        public override async UniTask<GameState> TickAsync(CancellationToken token)
        {
            _matchingStateUseCase.SetState(MatchingState.Connect);
            await UniTask.Delay(TimeSpan.FromSeconds(1.0f), cancellationToken: token);
            
            _matchingStateUseCase.SetState(MatchingState.Ready);
            await UniTask.Delay(TimeSpan.FromSeconds(1.0f), cancellationToken: token);

            _matchingStateUseCase.SetState(MatchingState.None);
            await UniTask.Delay(TimeSpan.FromSeconds(1.0f), cancellationToken: token);

            return GameState.Edit;
        }
    }
}