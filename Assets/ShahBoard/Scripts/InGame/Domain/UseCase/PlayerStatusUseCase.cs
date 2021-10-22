using System;
using ShahBoard.InGame.Data.Entity;

namespace ShahBoard.InGame.Domain.UseCase
{
    public sealed class PlayerStatusUseCase
    {
        private readonly PlayerStatusEntity _masterStatusEntity;
        private readonly PlayerStatusEntity _clientStatusEntity;

        public PlayerStatusUseCase()
        {
            _masterStatusEntity = new PlayerStatusEntity(PlayerType.Master);
            _clientStatusEntity = new PlayerStatusEntity(PlayerType.Client);
        }

        public void SetEditComplete(PlayerType playerType)
        {
            switch (playerType)
            {
                case PlayerType.Master:
                    _masterStatusEntity.SetEditComplete();
                    break;
                case PlayerType.Client:
                    _clientStatusEntity.SetEditComplete();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(playerType), playerType, null);
            }
        }

        public bool IsEditing()
        {
            return _masterStatusEntity.IsEdit() || _clientStatusEntity.IsEdit();
        }
    }
}