using System.Threading;
using Cysharp.Threading.Tasks;
using ShahBoard.InGame.Domain.UseCase;

namespace ShahBoard.InGame.Presentation.Controller
{
    public sealed class BattleState : BaseGameState
    {
        private readonly MovementUseCase _movementUseCase;

        public BattleState(MovementUseCase movementUseCase)
        {
            _movementUseCase = movementUseCase;
        }

        public override GameState state => GameState.Battle;

        public override async UniTask InitAsync(CancellationToken token)
        {

        }

        public override async UniTask<GameState> TickAsync(CancellationToken token)
        {
            // TODO: アニメーション
            _movementUseCase.Move();

            return GameState.Judge;
        }
    }
}