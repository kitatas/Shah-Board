using System.Threading;
using Cysharp.Threading.Tasks;

namespace ShahBoard.InGame.Presentation.Controller
{
    public sealed class EditState : BaseGameState
    {
        public override GameState state => GameState.Edit;

        public override async UniTask InitAsync(CancellationToken token)
        {

        }

        public override async UniTask<GameState> TickAsync(CancellationToken token)
        {
            return GameState.Input;
        }
    }
}