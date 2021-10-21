using System.Threading;
using Cysharp.Threading.Tasks;

namespace ShahBoard.InGame.Presentation.Controller
{
    public sealed class MoveState : BaseGameState
    {
        public override GameState state => GameState.Move;

        public override async UniTask InitAsync(CancellationToken token)
        {

        }

        public override async UniTask<GameState> TickAsync(CancellationToken token)
        {
            return GameState.Battle;
        }
    }
}