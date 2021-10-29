using System.Threading;
using Cysharp.Threading.Tasks;
using ShahBoard.InGame.Presentation.View;

namespace ShahBoard.InGame.Presentation.Controller
{
    public sealed class ResultState : BaseGameState
    {
        private readonly NextButtonView _nextButtonView;

        public ResultState(NextButtonView nextButtonView)
        {
            _nextButtonView = nextButtonView;
        }

        public override GameState state => GameState.Result;

        public override async UniTask InitAsync(CancellationToken token)
        {
            _nextButtonView.Init();
        }

        public override async UniTask<GameState> TickAsync(CancellationToken token)
        {
            _nextButtonView.Activate(true);
            return GameState.None;
        }
    }
}