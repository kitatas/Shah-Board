namespace ShahBoard.InGame.Data.Entity
{
    public sealed class GameStateEntity
    {
        private GameState _gameState;

        public GameStateEntity()
        {
            _gameState = GameState.Match;
        }

        public GameState Get() => _gameState;

        public void Set(GameState state) => _gameState = state;
    }
}