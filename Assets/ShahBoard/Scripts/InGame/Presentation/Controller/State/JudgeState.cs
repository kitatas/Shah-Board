using System.Threading;
using Cysharp.Threading.Tasks;

namespace ShahBoard.InGame.Presentation.Controller
{
    public sealed class JudgeState : BaseGameState
    {
        public override GameState state => GameState.Judge;

        public override async UniTask InitAsync(CancellationToken token)
        {

        }

        public override async UniTask<GameState> TickAsync(CancellationToken token)
        {
            // ゲームが決着した場合
            if (true)
            {
                return GameState.Result;
            }
            else
            {
                return GameState.Input;
            }
        }
    }
}