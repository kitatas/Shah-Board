using UnityEngine.SceneManagement;

namespace ShahBoard.Common.Presentation.Controller
{
    public sealed class SceneLoader
    {
        public void Load(SceneName sceneName)
        {
            // TODO: トランジション
            SceneManager.LoadScene(sceneName.ToString());
        }
    }
}