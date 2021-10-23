using System.Threading;
using Cysharp.Threading.Tasks;
using ShahBoard.InGame.Domain.UseCase;
using ShahBoard.InGame.Presentation.View;

namespace ShahBoard.InGame.Presentation.Controller
{
    public sealed class SelectState : BaseGameState
    {
        private readonly IBoardPlacementContainerUseCase _containerUseCase;
        private readonly InputUseCase _inputUseCase;
        private readonly MovementUseCase _movementUseCase;
        private readonly PieceDataUseCase _pieceDataUseCase;
        private readonly SelectUseCase _selectUseCase;
        private readonly TurnUseCase _turnUseCase;

        public SelectState(IBoardPlacementContainerUseCase containerUseCase, InputUseCase inputUseCase,
            MovementUseCase movementUseCase, PieceDataUseCase pieceDataUseCase, SelectUseCase selectUseCase,
            TurnUseCase turnUseCase)
        {
            _containerUseCase = containerUseCase;
            _inputUseCase = inputUseCase;
            _movementUseCase = movementUseCase;
            _pieceDataUseCase = pieceDataUseCase;
            _selectUseCase = selectUseCase;
            _turnUseCase = turnUseCase;
        }

        public override GameState state => GameState.Select;

        public override async UniTask InitAsync(CancellationToken token)
        {

        }

        public override async UniTask<GameState> TickAsync(CancellationToken token)
        {
            _turnUseCase.SetNextTurn();
            // TODO: ターン表示待ち

            var currentTurn = _turnUseCase.GetCurrentTurn();
            UnityEngine.Debug.Log($"[LOG] current turn: {currentTurn}");

            PieceView selectPiece = null;
            while (true)
            {
                // コマの選択
                if (selectPiece == null)
                {
                    // 自分の駒のあるマスの色を更新
                    _containerUseCase.UpdateAllPlacementType(PlacementType.Invalid);
                    _containerUseCase.UpdatePlayerPiecePlacement(currentTurn, PlacementType.Input);

                    await UniTask.WaitUntil(() => _inputUseCase.isTap, cancellationToken: token);
                    selectPiece = _selectUseCase.GetPlayerPiece(_inputUseCase.tapPosition, currentTurn);
                    if (selectPiece == null)
                    {
                        continue;
                    }
                }

                // 移動範囲のマスを更新
                _containerUseCase.UpdateAllPlacementType(PlacementType.Invalid);
                var moveRange = _pieceDataUseCase.GetMoveRangeList(selectPiece);
                _containerUseCase.SetUpMoveRangePlacement(currentTurn, moveRange);

                await UniTask.WaitUntil(() => _inputUseCase.isTap, cancellationToken: token);

                var boardPiece = _selectUseCase.GetBoardPiece(_inputUseCase.tapPosition);
                if (boardPiece != null)
                {
                    // 自分のコマを選択した場合
                    if (boardPiece.playerType == currentTurn)
                    {
                        // 最初に選択したコマと同じ
                        selectPiece = selectPiece == boardPiece ? null : boardPiece;
                    }
                    // 相手のコマを選択した場合
                    else
                    {
                        // 相手のコマを取れる場合
                        var placement = _containerUseCase.FindPlacement(boardPiece);
                        if (placement.IsEqualPlacementType(PlacementType.Valid))
                        {
                            _containerUseCase.FindPlacement(selectPiece).SetPlacementPiece(null);
                            _movementUseCase.Set(selectPiece, boardPiece, placement);
                            break;
                        }
                    }

                    continue;
                }

                var nextPlacement = _selectUseCase.GetValidPlacement(_inputUseCase.tapPosition);
                if (nextPlacement != null && nextPlacement.IsEqualPlacementType(PlacementType.Valid))
                {
                    // コマがない場合
                    var nextPlacementPiece = nextPlacement.GetPlacementPiece();
                    if (nextPlacementPiece == null)
                    {
                        _containerUseCase.FindPlacement(selectPiece).SetPlacementPiece(null);
                        _movementUseCase.Set(selectPiece, null, nextPlacement);
                        break;
                    }

                    // 相手のコマが配置されている場合
                    if (nextPlacementPiece.playerType == _turnUseCase.GetEnemyPlayer())
                    {
                        _containerUseCase.FindPlacement(selectPiece).SetPlacementPiece(null);
                        _movementUseCase.Set(selectPiece, nextPlacementPiece, nextPlacement);
                        break;
                    }
                    else
                    {
                        UnityEngine.Debug.LogError($"[ERROR] my piece is valid placement.");
                    }

                    continue;
                }
            }

            _containerUseCase.UpdateAllPlacementType(PlacementType.Invalid);

            return GameState.Battle;
        }
    }
}