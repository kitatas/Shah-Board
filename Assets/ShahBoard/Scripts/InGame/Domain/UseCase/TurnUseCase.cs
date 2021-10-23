using System;
using ShahBoard.InGame.Data.Entity;
using UniRx;

namespace ShahBoard.InGame.Domain.UseCase
{
    public sealed class TurnUseCase
    {
        private readonly TurnEntity _turnEntity;
        private readonly ReactiveProperty<PlayerType> _turn;

        public TurnUseCase(TurnEntity turnEntity)
        {
            _turnEntity = turnEntity;
            _turn = new ReactiveProperty<PlayerType>(_turnEntity.Get());
        }

        public IReadOnlyReactiveProperty<PlayerType> turn => _turn;

        public PlayerType GetCurrentTurn() => _turnEntity.Get();

        public PlayerType GetEnemyPlayer()
        {
            return _turnEntity.Get() switch
            {
                PlayerType.Master => PlayerType.Client,
                PlayerType.Client => PlayerType.Master,
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        public void SetNextTurn()
        {
            switch (_turnEntity.Get())
            {
                case PlayerType.Master:
                    _turnEntity.Set(PlayerType.Client);
                    break;
                case PlayerType.Client:
                case PlayerType.None:
                    _turnEntity.Set(PlayerType.Master);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            _turn.Value = _turnEntity.Get();
        }
    }
}