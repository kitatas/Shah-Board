using System;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace ShahBoard.Common.Presentation.View
{
    [RequireComponent(typeof(Button))]
    public sealed class BaseButton : MonoBehaviour
    {
        private Button _button;

        public Button button => _button ??= GetComponent<Button>();

        public IObservable<Unit> OnClick() => button.OnClickAsObservable();

        public void SetInteractable(bool value) => button.interactable = value;
    }
}