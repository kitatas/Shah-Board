using System.Threading;
using Cysharp.Threading.Tasks;
using ShahBoard.InGame.Domain.UseCase;
using ShahBoard.InGame.Presentation.View;

namespace ShahBoard.InGame.Presentation.Controller
{
    public sealed class InputState : BaseGameState
    {
        private readonly InputUseCase _inputUseCase;
        private readonly TurnUseCase _turnUseCase;
        private readonly PieceSelectUseCase _selectUseCase;
        private readonly PieceMovementUseCase _movementUseCase;
        private readonly IBoardPlacementContainerUseCase _placementContainerUseCase;

        public InputState(InputUseCase inputUseCase, TurnUseCase turnUseCase, PieceSelectUseCase selectUseCase,
            PieceMovementUseCase movementUseCase, IBoardPlacementContainerUseCase placementContainerUseCase)
        {
            _inputUseCase = inputUseCase;
            _turnUseCase = turnUseCase;
            _selectUseCase = selectUseCase;
            _movementUseCase = movementUseCase;
            _placementContainerUseCase = placementContainerUseCase;
        }

        public override GameState state => GameState.Input;

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
                    _placementContainerUseCase.UpdatePlayerPiecePlacement(currentTurn, PlacementType.Input);

                    await UniTask.WaitUntil(() => _inputUseCase.isTap, cancellationToken: token);
                    selectPiece = _selectUseCase.GetPiece(_inputUseCase.tapPosition, currentTurn);
                    if (selectPiece == null)
                    {
                        continue;
                    }
                }

                // 移動範囲のマスを更新
                _placementContainerUseCase.UpdateAllPlacementType(PlacementType.Invalid);
                var moveRange = _movementUseCase.GetMoveRangeList(selectPiece);
                _placementContainerUseCase.SetUpMoveRangePlacement(currentTurn, moveRange);

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
                        var placement = _placementContainerUseCase.FindPlacement(boardPiece);
                        if (placement.IsEqualPlacementType(PlacementType.Valid))
                        {
                            // 相手のコマを削除
                            boardPiece.RemoveDeck();
                            boardPiece.gameObject.SetActive(false);

                            // コマの移動
                            _placementContainerUseCase.FindPlacement(selectPiece).SetPlacementPiece(null);
                            selectPiece.UpdateCurrentPlacement(placement.GetPosition());
                            placement.SetPlacementPiece(selectPiece);

                            break;
                        }
                    }

                    continue;
                }

                var nextPlacement = _selectUseCase.GetNextPlacement(_inputUseCase.tapPosition);
                if (nextPlacement != null && nextPlacement.IsEqualPlacementType(PlacementType.Valid))
                {
                    // コマがない場合
                    var nextPlacementPiece = nextPlacement.GetPlacementPiece();
                    if (nextPlacementPiece == null)
                    {
                        // コマの移動
                        _placementContainerUseCase.FindPlacement(selectPiece).SetPlacementPiece(null);
                        selectPiece.UpdateCurrentPlacement(nextPlacement.GetPosition());
                        nextPlacement.SetPlacementPiece(selectPiece);

                        break;
                    }

                    // 相手のコマが配置されている場合
                    if (nextPlacementPiece.playerType == _turnUseCase.GetEnemyPlayer())
                    {
                        // 相手のコマを削除
                        nextPlacementPiece.RemoveDeck();
                        nextPlacementPiece.gameObject.SetActive(false);

                        // コマの移動
                        _placementContainerUseCase.FindPlacement(selectPiece).SetPlacementPiece(null);
                        selectPiece.UpdateCurrentPlacement(nextPlacement.GetPosition());
                        nextPlacement.SetPlacementPiece(selectPiece);

                        break;
                    }
                    else
                    {
                        UnityEngine.Debug.LogError($"[ERROR] my piece is valid placement.");
                    }

                    continue;
                }
            }

            _placementContainerUseCase.UpdateAllPlacementType(PlacementType.Invalid);

            return GameState.Move;
        }
    }
}