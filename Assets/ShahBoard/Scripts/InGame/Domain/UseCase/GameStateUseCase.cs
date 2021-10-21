using ShahBoard.InGame.Data.Entity;
using UniRx;

namespace ShahBoard.InGame.Domain.UseCase
{
    public sealed class GameStateUseCase
    {
        private readonly GameStateEntity _gameStateEntity;
        private readonly ReactiveProperty<GameState> _gameState;

        public GameStateUseCase(GameStateEntity gameStateEntity)
        {
            _gameStateEntity = gameStateEntity;
            _gameState = new ReactiveProperty<GameState>(_gameStateEntity.Get());
        }

        public IReadOnlyReactiveProperty<GameState> gameState => _gameState;

        public void SetState(GameState state)
        {
            _gameStateEntity.Set(state);
            _gameState.Value = _gameStateEntity.Get();
        }

        public bool IsEqual(GameState state)
        {
            return _gameStateEntity.Get() == state;
        }

        public bool IsPlaying()
        {
            return
                IsEqual(GameState.Match) ||
                IsEqual(GameState.Edit) ||
                IsEqual(GameState.Input) ||
                IsEqual(GameState.Move) ||
                IsEqual(GameState.Battle) ||
                IsEqual(GameState.Judge);
        }
    }
}