using ShahBoard.Common.Presentation.Controller;
using UniRx;
using UnityEngine;
using VContainer;

namespace ShahBoard.Common.Presentation.View
{
    [RequireComponent(typeof(BaseButton))]
    public sealed class ButtonLoader : MonoBehaviour
    {
        [SerializeField] private SceneName sceneName = default;

        private SceneLoader _sceneLoader;

        [Inject]
        private void Construct(SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        private void Start()
        {
            GetComponent<BaseButton>().OnClick()
                .Subscribe(_ => _sceneLoader.Load(sceneName))
                .AddTo(this);
        }
    }
}