using System.Threading;
using Cysharp.Threading.Tasks;
using ShahBoard.InGame.Domain.UseCase;
using ShahBoard.InGame.Presentation.View;

namespace ShahBoard.InGame.Presentation.Controller
{
    public sealed class JudgeState : BaseGameState
    {
        private readonly IPieceContainerUseCase _pieceContainerUseCase;
        private readonly TurnUseCase _turnUseCase;
        private readonly WinnerView _winnerView;

        public JudgeState(IPieceContainerUseCase pieceContainerUseCase, TurnUseCase turnUseCase, WinnerView winnerView)
        {
            _pieceContainerUseCase = pieceContainerUseCase;
            _turnUseCase = turnUseCase;
            _winnerView = winnerView;
        }

        public override GameState state => GameState.Judge;

        public override async UniTask InitAsync(CancellationToken token)
        {

        }

        public override async UniTask<GameState> TickAsync(CancellationToken token)
        {
            // コマが1つ または 皇帝が取られた場合
            if (_pieceContainerUseCase.IsNonePiece(_turnUseCase.GetEnemyPlayer()))
            {
                await _winnerView.SetWinnerTextAsync(_turnUseCase.GetCurrentTurn(), token);
                return GameState.Result;
            }
            else
            {
                return GameState.Select;
            }
        }
    }
}