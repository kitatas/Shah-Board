using System;
using TMPro;
using UnityEngine;

namespace ShahBoard.InGame.Presentation.View
{
    public sealed class MatchingStateView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI matchingStateText = default;

        public void Show(MatchingState state)
        {
            switch (state)
            {
                case MatchingState.None:
                    matchingStateText.text = $"";
                    break;
                case MatchingState.Connect:
                    // matchingStateText.text = $"対戦の準備中です。";
                    matchingStateText.text = $"connect";
                    break;
                case MatchingState.Matching:
                    // matchingStateText.text = $"対戦相手を探しています";
                    matchingStateText.text = $"matching";
                    break;
                case MatchingState.Matched:
                    // matchingStateText.text = $"対戦相手が見つかりました";
                    matchingStateText.text = $"matched";
                    break;
                case MatchingState.Ready:
                    // matchingStateText.text = $"まもなく開始します";
                    matchingStateText.text = $"ready";
                    break;
                case MatchingState.Disconnect:
                    // matchingStateText.text = $"通信が切断されました";
                    matchingStateText.text = $"disconnect";
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }
    }
}