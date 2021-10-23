using System.Threading;
using Cysharp.Threading.Tasks;
using ShahBoard.InGame.Domain.UseCase;

namespace ShahBoard.InGame.Presentation.Controller
{
    public sealed class JudgeState : BaseGameState
    {
        private readonly IPieceContainerUseCase _pieceContainerUseCase;
        private readonly TurnUseCase _turnUseCase;

        public JudgeState(IPieceContainerUseCase pieceContainerUseCase, TurnUseCase turnUseCase)
        {
            _pieceContainerUseCase = pieceContainerUseCase;
            _turnUseCase = turnUseCase;
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
                return GameState.Result;
            }
            else
            {
                return GameState.Select;
            }
        }
    }
}