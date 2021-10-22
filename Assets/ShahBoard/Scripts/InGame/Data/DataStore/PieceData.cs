using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;

namespace ShahBoard.InGame.Data.DataStore
{
    [CreateAssetMenu(fileName = nameof(PieceData), menuName = "InGameTable/" + nameof(PieceData), order = 0)]
    public sealed class PieceData : SerializedScriptableObject
    {
        [SerializeField] private PieceType pieceType = default;

        [SerializeField]
        [Header(nameof(moveRange))]
        [TableMatrix(SquareCells = true, HideColumnIndices = true, HideRowIndices = true,
            DrawElementMethod = nameof(DrawPlacement))]
        private bool[,] moveRange = new bool[5, 5];

        public PieceType type => pieceType;

        public Vector3[] GetMoveRange()
        {
            var ranges = new List<Vector3>();
            for (int i = 0; i < moveRange.GetLength(0); i++)
            {
                for (int j = 0; j < moveRange.GetLength(1); j++)
                {
                    if (i == 2 && j == 2)
                    {
                        continue;
                    }

                    if (moveRange[i, j])
                    {
                        ranges.Add(new Vector3(i - 2.0f, 0.0f, 2.0f - j));
                    }
                }
            }

            return ranges.ToArray();
        }

#if UNITY_EDITOR
        private static bool DrawPlacement(Rect rect, bool value)
        {
            if (Event.current.type == EventType.MouseDown && rect.Contains(Event.current.mousePosition))
            {
                value = !value;
                GUI.changed = true;
                Event.current.Use();
            }

            UnityEditor.EditorGUI.DrawRect(rect.Padding(1),
                value ? new Color(0.5f, 1.0f, 0.5f, 0.5f) : new Color(0.0f, 0.0f, 0.0f, 0.5f));

            return value;
        }
#endif
    }
}