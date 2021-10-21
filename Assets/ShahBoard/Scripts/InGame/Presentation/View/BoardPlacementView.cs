using UnityEngine;

namespace ShahBoard.InGame.Presentation.View
{
    public sealed class BoardPlacementView : MonoBehaviour
    {
        public void Init(Transform parent, Vector3 position)
        {
            transform.parent = parent;
            transform.position = position;
        }
    }
}