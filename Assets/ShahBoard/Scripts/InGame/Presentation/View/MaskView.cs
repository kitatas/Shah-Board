using DG.Tweening;
using UnityEngine;

namespace ShahBoard.InGame.Presentation.View
{
    public sealed class MaskView : MonoBehaviour
    {
        [SerializeField] private RectTransform maskTop = default;
        [SerializeField] private RectTransform maskBottom = default;

        private const float SHOW_HEIGHT = 55.0f;
        private const float HIDE_HEIGHT = 300.0f;
        private const float ALL_HIDE_HEIGHT = 355.0f;

        public void HideAll(float tweenTime)
        {
            maskBottom
                .DOAnchorPosY(ALL_HIDE_HEIGHT, tweenTime)
                .SetEase(Ease.Linear);

            maskTop
                .DOAnchorPosY(-ALL_HIDE_HEIGHT, tweenTime)
                .SetEase(Ease.Linear);
        }

        public void ShowCenter(float tweenTime)
        {
            maskTop
                .DOAnchorPosY(SHOW_HEIGHT, tweenTime)
                .SetEase(Ease.Linear);

            maskBottom
                .DOAnchorPosY(-SHOW_HEIGHT, tweenTime)
                .SetEase(Ease.Linear);
        }

        public void ShowMasterArea(float tweenTime)
        {
            ShowMasterEditArea(tweenTime);
            HideClientEditArea(tweenTime);
        }

        public void ShowClientArea(float tweenTime)
        {
            ShowClientEditArea(tweenTime);
            HideMasterEditArea(tweenTime);
        }

        private void ShowMasterEditArea(float tweenTime)
        {
            maskBottom
                .DOAnchorPosY(-HIDE_HEIGHT, tweenTime)
                .SetEase(Ease.Linear);
        }

        private void HideMasterEditArea(float tweenTime)
        {
            maskBottom
                .DOAnchorPosY(HIDE_HEIGHT, tweenTime)
                .SetEase(Ease.Linear);
        }

        private void ShowClientEditArea(float tweenTime)
        {
            maskTop
                .DOAnchorPosY(HIDE_HEIGHT, tweenTime)
                .SetEase(Ease.Linear);
        }

        private void HideClientEditArea(float tweenTime)
        {
            maskTop
                .DOAnchorPosY(-HIDE_HEIGHT, tweenTime)
                .SetEase(Ease.Linear);
        }
    }
}