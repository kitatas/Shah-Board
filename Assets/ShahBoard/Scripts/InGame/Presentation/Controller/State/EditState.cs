using System.Threading;
using Cysharp.Threading.Tasks;
using ShahBoard.InGame.Domain.UseCase;

namespace ShahBoard.InGame.Presentation.Controller
{
    public sealed class EditState : BaseGameState
    {
        private readonly IBoardPlacementContainerUseCase _placementContainerUseCase;
        private readonly InputUseCase _inputUseCase;
        private readonly PieceEditUseCase _pieceEditUseCase;

        public EditState(IBoardPlacementContainerUseCase placementContainerUseCase, InputUseCase inputUseCase,
            PieceEditUseCase pieceEditUseCase)
        {
            _placementContainerUseCase = placementContainerUseCase;
            _inputUseCase = inputUseCase;
            _pieceEditUseCase = pieceEditUseCase;
        }

        public override GameState state => GameState.Edit;

        public override async UniTask InitAsync(CancellationToken token)
        {

        }

        public override async UniTask<GameState> TickAsync(CancellationToken token)
        {
            // TODO: 2プレイヤーの編成完了ボタンが押されるまで
            while (true)
            {
                // 入力待ち
                await UniTask.WaitUntil(() => _inputUseCase.isTap, cancellationToken: token);

                var storePiece = _pieceEditUseCase.GetEditPiece(_inputUseCase.tapPosition);
                if (storePiece != null)
                {
                    _placementContainerUseCase.UpdateEditPlacement(storePiece.playerType, PlacementType.Valid);

                    // 配置結果待ち
                    await UniTask.WaitUntil(() =>
                    {
                        // ドラッグ中は移動
                        if (_inputUseCase.isDrag)
                        {
                            storePiece.SetPosition(_pieceEditUseCase.GetEditPosition(_inputUseCase.tapPosition));
                            return false;
                        }

                        var placement = _pieceEditUseCase.GetPiecePlacement(storePiece, _inputUseCase.tapPosition);
                        var placementPiece = _pieceEditUseCase.GetPlacementPiece(storePiece, _inputUseCase.tapPosition);

                        // 配置可能なマスの場合
                        if (placement != null || placementPiece != null)
                        {
                            if (placement == null)
                            {
                                placement = _placementContainerUseCase.FindPlacement(placementPiece);
                            }

                            if (placementPiece == null)
                            {
                                placementPiece = placement.GetPlacementPiece();
                            }

                            // storePieceが配置済みの場合
                            if (storePiece.IsInDeck())
                            {
                                var storePlacement = _placementContainerUseCase.FindPlacement(storePiece);

                                // placementに駒が配置されている場合
                                if (placementPiece != null)
                                {
                                    placementPiece.UpdateCurrentPlacement(storePlacement.GetPosition());
                                    storePlacement.SetPlacementPiece(placementPiece);
                                }
                                // placementが空きの場合
                                else
                                {
                                    storePlacement.SetPlacementPiece(null);
                                }
                            }
                            // storePieceが未配置の場合
                            else
                            {
                                // placementに駒が配置されている場合
                                if (placementPiece != null)
                                {
                                    placementPiece.SetInitPosition();
                                }
                            }

                            storePiece.UpdateCurrentPlacement(placement.GetPosition());
                            placement.SetPlacementPiece(storePiece);
                        }
                        else
                        {
                            // storePieceが配置済みの場合
                            if (storePiece.IsInDeck())
                            {
                                storePiece.SetInDeckPosition();
                            }
                            // storePieceが未配置の場合
                            else
                            {
                                storePiece.SetInitPosition();
                            }
                        }

                        return true;
                    }, cancellationToken: token);

                    _placementContainerUseCase.UpdateEditPlacement(storePiece.playerType, PlacementType.Invalid);

                    // TODO: あとで消す
                    break;
                }
            }

            return GameState.Input;
        }
    }
}