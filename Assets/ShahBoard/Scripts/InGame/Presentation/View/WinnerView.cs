using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace ShahBoard.InGame.Presentation.View
{
    public sealed class WinnerView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI masterWinnerText = default;
        [SerializeField] private TextMeshProUGUI clientWinnerText = default;

        public async UniTask SetWinnerTextAsync(PlayerType playerType, CancellationToken token)
        {
            switch (playerType)
            {
                case PlayerType.Master:
                    await (
                        masterWinnerText
                            .DOText($"You win!", UiConfig.TWEEN_TIME)
                            .SetEase(Ease.Linear)
                            .WithCancellation(token),
                        clientWinnerText
                            .DOText($"You lose", UiConfig.TWEEN_TIME)
                            .SetEase(Ease.Linear)
                            .WithCancellation(token)
                    );
                    break;
                case PlayerType.Client:
                    await (
                        masterWinnerText
                            .DOText($"You lose", UiConfig.TWEEN_TIME)
                            .SetEase(Ease.Linear)
                            .WithCancellation(token),
                        clientWinnerText
                            .DOText($"You win!", UiConfig.TWEEN_TIME)
                            .SetEase(Ease.Linear)
                            .WithCancellation(token)
                    );
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(playerType), playerType, null);
            }
        }
    }
}