using System;
using DG.Tweening;
using ShahBoard.Common.Presentation.View;
using UniRx;
using UnityEngine;

namespace ShahBoard.InGame.Presentation.View
{
    public sealed class EditView : MonoBehaviour
    {
        [SerializeField] private Transform mainCamera = default;
        [SerializeField] private MaskView maskView = default;

        [SerializeField] private RectTransform buttonContainerMaster = default;
        [SerializeField] private RectTransform buttonContainerClient = default;

        [SerializeField] private BaseButton editAutoMaster = default;
        [SerializeField] private BaseButton editAutoClient = default;
        [SerializeField] private BaseButton editResetMaster = default;
        [SerializeField] private BaseButton editResetClient = default;
        [SerializeField] private BaseButton editCompleteMaster = default;
        [SerializeField] private BaseButton editCompleteClient = default;

        private readonly Subject<PlayerType> _editAuto = new Subject<PlayerType>();
        public IObservable<PlayerType> OnEditAuto() => _editAuto;

        private readonly Subject<PlayerType> _editReset = new Subject<PlayerType>();
        public IObservable<PlayerType> OnEditReset() => _editReset;

        private readonly Subject<PlayerType> _editComplete = new Subject<PlayerType>();
        public IObservable<PlayerType> OnEditComplete() => _editComplete;

        public const float HIDE_HEIGHT = 80.0f;
        public const float EDIT_HEIGHT = 3.0f;

        public void Init()
        {
            editAutoMaster.OnClick()
                .Subscribe(_ => _editAuto.OnNext(PlayerType.Master))
                .AddTo(this);

            editAutoClient.OnClick()
                .Subscribe(_ => _editAuto.OnNext(PlayerType.Client))
                .AddTo(this);

            editResetMaster.OnClick()
                .Subscribe(_ => _editReset.OnNext(PlayerType.Master))
                .AddTo(this);

            editResetClient.OnClick()
                .Subscribe(_ => _editReset.OnNext(PlayerType.Client))
                .AddTo(this);

            editCompleteMaster.OnClick()
                .Subscribe(_ =>
                {
                    buttonContainerMaster
                        .DOAnchorPosY(-HIDE_HEIGHT, UiConfig.TWEEN_TIME);
                    TweenEditCameraPosition(PlayerType.Client);
                    _editComplete.OnNext(PlayerType.Master);
                })
                .AddTo(this);

            editCompleteClient.OnClick()
                .Subscribe(_ =>
                {
                    buttonContainerClient
                        .DOAnchorPosY(HIDE_HEIGHT, UiConfig.TWEEN_TIME);
                    TweenEditCameraPosition(PlayerType.None);
                    _editComplete.OnNext(PlayerType.Client);
                })
                .AddTo(this);

            maskView.HideAll(0.0f);
        }

        public void SetEditCompleteButton(PlayerType playerType, bool value)
        {
            switch (playerType)
            {
                case PlayerType.Master:
                    editResetMaster.SetInteractable(value);
                    editCompleteMaster.SetInteractable(value);
                    break;
                case PlayerType.Client:
                    editResetClient.SetInteractable(value);
                    editCompleteClient.SetInteractable(value);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(playerType), playerType, null);
            }
        }

        public void TweenEditCameraPosition(PlayerType playerType)
        {
            switch (playerType)
            {
                case PlayerType.None:
                    mainCamera
                        .DOLocalMoveZ(0.0f, UiConfig.TWEEN_TIME)
                        .SetEase(Ease.Linear);
                    maskView.ShowCenter(UiConfig.TWEEN_TIME);
                    break;
                case PlayerType.Master:
                    mainCamera
                        .DOLocalMoveZ(-EDIT_HEIGHT, 0.0f)
                        .SetEase(Ease.Linear);
                    maskView.ShowMasterArea(UiConfig.TWEEN_TIME);
                    break;
                case PlayerType.Client:
                    mainCamera
                        .DOLocalMoveZ(EDIT_HEIGHT, UiConfig.TWEEN_TIME)
                        .SetEase(Ease.Linear);
                    maskView.ShowClientArea(UiConfig.TWEEN_TIME);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(playerType), playerType, null);
            }
        }
    }
}