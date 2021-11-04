using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ShahBoard.InGame.Presentation.View
{
    public sealed class PieceDataView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI pieceNameMaster = default;
        [SerializeField] private TextMeshProUGUI pieceNameClient = default;
        [SerializeField] private Image pieceMoveRangeMaster = default;
        [SerializeField] private Image pieceMoveRangeClient = default;

        public void Init()
        {
            pieceNameMaster.text = $"";
            pieceNameClient.text = $"";
            Activate(false);
        }

        public void SetData(string pieceName, Sprite sprite)
        {
            // TODO: 名称変更
            pieceNameMaster.text = $"{pieceName}";
            pieceNameClient.text = $"{pieceName}";
            pieceMoveRangeMaster.sprite = sprite;
            pieceMoveRangeClient.sprite = sprite;
            Activate(true);
        }

        private void Activate(bool value)
        {
            pieceMoveRangeMaster.enabled = value;
            pieceMoveRangeClient.enabled = value;
        }
    }
}