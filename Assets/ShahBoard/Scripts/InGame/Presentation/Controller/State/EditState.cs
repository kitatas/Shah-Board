using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using ShahBoard.Common.Domain.UseCase;
using ShahBoard.InGame.Domain.UseCase;
using ShahBoard.InGame.Presentation.View;
using UniRx;

namespace ShahBoard.InGame.Presentation.Controller
{
    public sealed class EditState : BaseGameState
    {
        private readonly IBoardPlacementContainerUseCase _containerUseCase;
        private readonly EditUseCase _editUseCase;
        private readonly InputUseCase _inputUseCase;
        private readonly PieceDataUseCase _dataUseCase;
        private readonly PlayerStatusUseCase _statusUseCase;
        private readonly LanguageUseCase _languageUseCase;
        private readonly IReadOnlySaveUseCase _saveUseCase;
        private readonly EditView _editView;
        private readonly PieceDataView _dataView;

        public EditState(IBoardPlacementContainerUseCase containerUseCase, EditUseCase editUseCase,
            InputUseCase inputUseCase, PieceDataUseCase dataUseCase, PlayerStatusUseCase statusUseCase,
            LanguageUseCase languageUseCase, IReadOnlySaveUseCase saveUseCase, EditView editView,
            PieceDataView dataView)
        {
            _containerUseCase = containerUseCase;
            _editUseCase = editUseCase;
            _inputUseCase = inputUseCase;
            _dataUseCase = dataUseCase;
            _statusUseCase = statusUseCase;
            _languageUseCase = languageUseCase;
            _saveUseCase = saveUseCase;
            _editView = editView;
            _dataView = dataView;
        }

        public override GameState state => GameState.Edit;

        public override async UniTask InitAsync(CancellationToken token)
        {
            var playerTypes = new List<PlayerType>
            {
                PlayerType.Master,
                PlayerType.Client,
            };
            foreach (var playerType in playerTypes)
            {
                _editUseCase.GetEditDeckPieceCount(playerType)
                    .Subscribe(x => _editView.SetEditCompleteButton(playerType, x > DeckConfig.INIT_PIECE_COUNT))
                    .AddTo(_editView);
            }

            _editView.Init();

            _editView.OnEditAuto()
                .Subscribe(x =>
                {
                    _editUseCase.SetEditDeckCount(x, DeckConfig.MAX_PIECE_COUNT);
                    _containerUseCase.SetAllInDeckAuto(x);
                })
                .AddTo(_editView);

            _editView.OnEditReset()
                .Subscribe(x =>
                {
                    _editUseCase.SetEditDeckCount(x, DeckConfig.INIT_PIECE_COUNT);
                    _containerUseCase.RemoveAllInDeck(x);
                })
                .AddTo(_editView);

            _editView.OnEditComplete()
                .Subscribe(x => _statusUseCase.SetEditComplete(x))
                .AddTo(_editView);

            _dataView.Init();
        }

        public override async UniTask<GameState> TickAsync(CancellationToken token)
        {
            _editView.TweenEditCameraPosition(PlayerType.Master);

            while (_statusUseCase.IsEditing())
            {
                // 入力待ち
                await UniTask.WhenAny(
                    UniTask.WaitUntil(() => _inputUseCase.isTap, cancellationToken: token),
                    _editView.OnEditComplete().ToUniTask(true, token)
                );

                var storePiece = _editUseCase.GetEditPiece(_inputUseCase.tapPosition);
                if (storePiece != null)
                {
                    _containerUseCase.UpdateEditPlacement(storePiece.playerType, PlacementType.Valid);
                    var moveRangeSprite = _dataUseCase.GetPieceMoveRangeSprite(storePiece.pieceType);
                    var pieceName = _languageUseCase.Find(_saveUseCase.LoadLanguageType(), storePiece.pieceType).name;
                    _dataView.SetData(pieceName, moveRangeSprite);

                    // 配置結果待ち
                    await UniTask.WaitUntil(() =>
                    {
                        // ドラッグ中は移動
                        if (_inputUseCase.isDrag)
                        {
                            storePiece.SetPosition(_editUseCase.GetEditPosition(_inputUseCase.tapPosition));
                            return false;
                        }

                        return true;
                    }, cancellationToken: token);

                    var placement = _editUseCase.GetValidPlacement(storePiece, _inputUseCase.tapPosition);
                    var piece = _editUseCase.GetPlacementPiece(storePiece, _inputUseCase.tapPosition);

                    // 配置可能なマスの場合
                    if (placement != null || piece != null)
                    {
                        if (placement == null)
                        {
                            placement = _containerUseCase.FindPlacement(piece);
                        }

                        if (piece == null)
                        {
                            piece = placement.GetPlacementPiece();
                        }

                        // storePieceが配置済みの場合
                        if (storePiece.IsInDeck())
                        {
                            var storePlacement = _containerUseCase.FindPlacement(storePiece);

                            // placementにコマが配置されている場合
                            if (piece != null)
                            {
                                piece.UpdateCurrentPlacement(storePlacement.GetPosition());
                                storePlacement.SetPlacementPiece(piece);
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
                            // placementにコマが配置されている場合
                            if (piece != null)
                            {
                                piece.SetInitPosition();
                            }
                            else
                            {
                                _editUseCase.IncreaseEditDeckCount(storePiece.playerType);
                            }
                        }

                        // 配置
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

                    _containerUseCase.UpdateEditPlacement(storePiece.playerType, PlacementType.Invalid);
                }

                _dataView.Init();
            }

            // EditViewのフェードアウト待ち
            await UniTask.Delay(TimeSpan.FromSeconds(UiConfig.TWEEN_TIME), cancellationToken: token);

            return GameState.Select;
        }
    }
}