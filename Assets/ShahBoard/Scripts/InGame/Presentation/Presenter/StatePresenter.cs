using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using ShahBoard.InGame.Domain.UseCase;
using ShahBoard.InGame.Presentation.Controller;
using UniRx;

namespace ShahBoard.InGame.Presentation.Presenter
{
    public sealed class StatePresenter : IDisposable
    {
        private readonly CompositeDisposable _disposable;
        private readonly CancellationTokenSource _tokenSource;

        public StatePresenter(GameStateUseCase gameStateUseCase, StateController stateController)
        {
            _disposable = new CompositeDisposable();
            _tokenSource = new CancellationTokenSource();

            stateController.InitAsync(_tokenSource.Token).Forget();

            gameStateUseCase.gameState
                .Where(x => x != GameState.None)
                .Subscribe(x =>
                {
                    UniTask.Void(async _ =>
                    {
                        var nextState = await stateController.TickAsync(x, _tokenSource.Token);
                        gameStateUseCase.SetState(nextState);
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