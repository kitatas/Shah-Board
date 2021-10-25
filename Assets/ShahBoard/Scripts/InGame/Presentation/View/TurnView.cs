using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace ShahBoard.InGame.Presentation.View
{
    public sealed class TurnView : MonoBehaviour
    {
        [SerializeField] private Image turnTextBackground = default;
        [SerializeField] private TextMeshProUGUI turnText = default;

        private readonly Vector2 _showSize = new Vector2(400.0f, 150.0f);
        private readonly Vector2 _hideSize = new Vector2(100.0f, 150.0f);
        private readonly float _hideHeight = 70.0f;

        public void Init()
        {
            turnText.rectTransform
                .ObserveEveryValueChanged(v => v.anchoredPosition.y)
                .Subscribe(v =>
                {
                    var y = (_hideHeight - Mathf.Abs(v)) / _hideHeight;
                    turnText.rectTransform.localScale = new Vector3(1.0f, y, 1.0f);
                })
                .AddTo(turnText);

            InitView();
        }

        private void InitView()
        {
            turnTextBackground.rectTransform
                .DOSizeDelta(_hideSize, 0.0f);

            turnText.rectTransform
                .DOAnchorPosY(-_hideHeight, 0.0f);

            turnTextBackground.enabled = false;
            turnText.text = $"";
        }

        public async UniTask ShowAsync(PlayerType playerType, CancellationToken token)
        {
            turnTextBackground.enabled = true;
            turnText.text = $"{playerType} Turn";

            await (
                turnTextBackground.rectTransform
                    .DOSizeDelta(_showSize, UiConfig.TWEEN_TIME)
                    .SetEase(Ease.OutBack)
                    .WithCancellation(token),
                DOTween.Sequence()
                    .Append(turnText.rectTransform
                        .DOAnchorPosY(_hideHeight, UiConfig.TWEEN_TIME / 2.0f)
                        .SetEase(Ease.OutQuad))
                    .Append(turnText.rectTransform
                        .DOAnchorPosY(0.0f, UiConfig.TWEEN_TIME / 2.0f)
                        .SetEase(Ease.OutBack))
                    .WithCancellation(token)
            );

            await UniTask.Delay(TimeSpan.FromSeconds(0.5f), cancellationToken: token);

            await (
                turnTextBackground.rectTransform
                    .DOSizeDelta(_hideSize, UiConfig.TWEEN_TIME)
                    .SetEase(Ease.InBack)
                    .WithCancellation(token),
                turnText.rectTransform
                    .DOAnchorPosY(_hideHeight, UiConfig.TWEEN_TIME)
                    .SetEase(Ease.InBack)
                    .WithCancellation(token)
            );

            InitView();
        }
    }
}