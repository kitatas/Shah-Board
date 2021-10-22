using System.Threading;
using Cysharp.Threading.Tasks;
using ShahBoard.InGame.Domain.UseCase;
using ShahBoard.InGame.Presentation.View;
using UniRx;

namespace ShahBoard.InGame.Presentation.Controller
{
    public sealed class EditState : BaseGameState
    {
        private readonly IBoardPlacementContainerUseCase _placementContainerUseCase;
        private readonly InputUseCase _inputUseCase;
        private readonly PieceEditUseCase _pieceEditUseCase;
        private readonly PlayerStatusUseCase _statusUseCase;
        private readonly EditView _editView;

        public EditState(IBoardPlacementContainerUseCase placementContainerUseCase, InputUseCase inputUseCase,
            PieceEditUseCase pieceEditUseCase, PlayerStatusUseCase statusUseCase, EditView editView)
        {
            _placementContainerUseCase = placementContainerUseCase;
            _inputUseCase = inputUseCase;
            _pieceEditUseCase = pieceEditUseCase;
            _statusUseCase = statusUseCase;
            _editView = editView;
        }

        public override GameState state => GameState.Edit;

        public override async UniTask InitAsync(CancellationToken token)
        {
            _editView.Init();

            _editView.OnEditAuto()
                .Subscribe(x =>
                {
                    // 
                    UnityEngine.Debug.Log($"[LOG] push auto: {x}");
                })
                .AddTo(_editView);

            _editView.OnEditReset()
                .Subscribe(x =>
                {
                    // 
                    UnityEngine.Debug.Log($"[LOG] push reset: {x}");
                })
                .AddTo(_editView);

            _editView.OnEditComplete()
                .Subscribe(x =>
                {
                    UnityEngine.Debug.Log($"[LOG] push complete: {x}");
                    _statusUseCase.SetEditComplete(x);
                })
                .AddTo(_editView);
        }

        public override async UniTask<GameState> TickAsync(CancellationToken token)
        {
            while (_statusUseCase.IsEditing())
            {
                // 入力待ち
                await UniTask.WhenAny(
                    UniTask.WaitUntil(() => _inputUseCase.isTap, cancellationToken: token),
                    _editView.OnEditComplete().ToUniTask(true, token)
                );

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
                }
            }

            return GameState.Input;
        }
    }
}