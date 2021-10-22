namespace ShahBoard.InGame.Data.Entity
{
    public sealed class PlayerStatusEntity
    {
        private readonly PlayerType _playerType;
        private bool _isEditing;

        public PlayerStatusEntity(PlayerType playerType)
        {
            _playerType = playerType;
            _isEditing = true;
        }

        public void SetEditComplete()
        {
            _isEditing = false;
        }

        public bool IsEdit()
        {
            return _isEditing;
        }
    }
}