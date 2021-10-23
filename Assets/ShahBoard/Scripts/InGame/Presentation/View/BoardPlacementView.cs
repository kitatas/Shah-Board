using System;
using UniRx;
using UnityEngine;

namespace ShahBoard.InGame.Presentation.View
{
    [RequireComponent(typeof(MeshRenderer))]
    public sealed class BoardPlacementView : MonoBehaviour
    {
        private ReactiveProperty<PlacementType> _placementType;
        [SerializeField] private PieceView _placementPiece;

        public void Init(Transform parent, Vector3 position, PieceView pieceView)
        {
            transform.parent = parent;
            transform.position = position;

            _placementPiece = pieceView;

            var meshRenderer = GetComponent<MeshRenderer>();
            _placementType = new ReactiveProperty<PlacementType>(PlacementType.Invalid);
            _placementType
                .Subscribe(x =>
                {
                    switch (x)
                    {
                        case PlacementType.Valid:
                            meshRenderer.material.color = Color.green;
                            break;
                        case PlacementType.Invalid:
                            meshRenderer.material.color = Color.white;
                            break;
                        case PlacementType.Input:
                            meshRenderer.material.color = Color.yellow;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException(nameof(x), x, null);
                    }
                })
                .AddTo(this);
        }

        public void UpdatePlacementType(PlacementType type)
        {
            _placementType.Value = type;
        }

        public bool IsEqualPlacementType(PlacementType type)
        {
            return _placementType.Value == type;
        }

        public Vector3 GetPosition()
        {
            return transform.position;
        }

        public PieceView GetPlacementPiece()
        {
            return _placementPiece;
        }

        public void SetPlacementPiece(PieceView pieceView)
        {
            _placementPiece = pieceView;
        }

        public bool IsEqualPosition(Vector3 position)
        {
            return
                Mathf.Approximately(GetPosition().x, position.x) &&
                Mathf.Approximately(GetPosition().z, position.z);
        }
    }
}