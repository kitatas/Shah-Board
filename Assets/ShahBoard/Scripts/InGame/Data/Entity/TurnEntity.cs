namespace ShahBoard.InGame.Data.Entity
{
    public sealed class TurnEntity
    {
        private PlayerType _playerTurn;

        public TurnEntity()
        {
            _playerTurn = PlayerType.None;
        }

        public PlayerType Get() => _playerTurn;

        public void Set(PlayerType type) => _playerTurn = type;
    }
}