using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using ShahBoard.InGame.Domain.UseCase;
using ShahBoard.InGame.Presentation.Controller;
using UniRx;
using VContainer.Unity;

namespace ShahBoard.InGame.Presentation.Presenter
{
    public sealed class StatePresenter : IPostInitializable, IDisposable
    {
        private readonly GameStateUseCase _gameStateUseCase;
        private readonly StateController _stateController;
        private readonly CompositeDisposable _disposable;
        private readonly CancellationTokenSource _tokenSource;

        public StatePresenter(GameStateUseCase gameStateUseCase, StateController stateController)
        {
            _gameStateUseCase = gameStateUseCase;
            _stateController = stateController;
            _disposable = new CompositeDisposable();
            _tokenSource = new CancellationTokenSource();
        }

        public void PostInitialize()
        {
            _stateController.InitAsync(_tokenSource.Token).Forget();

            _gameStateUseCase.gameState
                .Where(x => x != GameState.None)
                .Subscribe(x =>
                {
                    UniTask.Void(async _ =>
                    {
                        var nextState = await _stateController.TickAsync(x, _tokenSource.Token);
                        _gameStateUseCase.SetState(nextState);
                    }, _tokenSource.Token);
                })
                .AddTo(_disposable);
        }

        public void Dispose()
        {
            _disposable?.Dispose();
            _tokenSource?.Cancel();
            _tokenSource?.Dispose();
        }
    }
}