using ShahBoard.Common.Presentation.View;
using UnityEngine;

namespace ShahBoard.InGame.Presentation.View
{
    public sealed class NextButtonView : MonoBehaviour
    {
        [SerializeField] private BaseButton titleButton = default;

        public void Init()
        {
            Activate(false);
        }

        public void Activate(bool value)
        {
            titleButton.gameObject.SetActive(value);
        }
    }
}