using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace ShahBoard.InGame.Presentation.Controller
{
    public sealed class StateController
    {
        private readonly List<BaseGameState> _states;

        public StateController(MatchState matchState, EditState editState, SelectState selectState,
            MoveState moveState, BattleState battleState, JudgeState judgeState, ResultState resultState)
        {
            _states = new List<BaseGameState>
            {
                matchState,
                editState,
                selectState,
                moveState,
                battleState,
                judgeState,
                resultState,
            };
        }

        public async UniTaskVoid InitAsync(CancellationToken token)
        {
            foreach (var state in _states)
            {
                state.InitAsync(token).Forget();
            }
        }

        public async UniTask<GameState> TickAsync(GameState state, CancellationToken token)
        {
            var currentState = _states.Find(x => x.state == state);
            if (currentState == null)
            {
                UnityEngine.Debug.LogError($"[ERROR] no state");
                return GameState.None;
            }

            // TODO: delete
            {
                UnityEngine.Debug.Log($"[LOG] current state: {currentState.state}");
                await UniTask.Delay(TimeSpan.FromSeconds(1.0f), cancellationToken: token);
            }

            return await currentState.TickAsync(token);
        }
    }
}