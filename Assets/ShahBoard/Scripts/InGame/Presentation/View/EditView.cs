using System;
using DG.Tweening;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace ShahBoard.InGame.Presentation.View
{
    public sealed class EditView : MonoBehaviour
    {
        [SerializeField] private RectTransform buttonContainerMaster = default;
        [SerializeField] private RectTransform buttonContainerClient = default;
        
        [SerializeField] private Button editAutoMaster = default;
        [SerializeField] private Button editAutoClient = default;
        [SerializeField] private Button editResetMaster = default;
        [SerializeField] private Button editResetClient = default;
        [SerializeField] private Button editCompleteMaster = default;
        [SerializeField] private Button editCompleteClient = default;

        private readonly Subject<PlayerType> _editAuto = new Subject<PlayerType>();
        public IObservable<PlayerType> OnEditAuto() => _editAuto;

        private readonly Subject<PlayerType> _editReset = new Subject<PlayerType>();
        public IObservable<PlayerType> OnEditReset() => _editReset;

        private readonly Subject<PlayerType> _editComplete = new Subject<PlayerType>();
        public IObservable<PlayerType> OnEditComplete() => _editComplete;

        public const float HIDE_HEIGHT = 80.0f;
        
        public void Init()
        {
            editAutoMaster
                .OnClickAsObservable()
                .Subscribe(_ => _editAuto.OnNext(PlayerType.Master))
                .AddTo(this);

            editAutoClient
                .OnClickAsObservable()
                .Subscribe(_ => _editAuto.OnNext(PlayerType.Client))
                .AddTo(this);

            editResetMaster
                .OnClickAsObservable()
                .Subscribe(_ => _editReset.OnNext(PlayerType.Master))
                .AddTo(this);

            editResetClient
                .OnClickAsObservable()
                .Subscribe(_ => _editReset.OnNext(PlayerType.Client))
                .AddTo(this);

            editCompleteMaster
                .OnClickAsObservable()
                .Subscribe(_ =>
                {
                    buttonContainerMaster
                        .DOAnchorPosY(-HIDE_HEIGHT, UiConfig.TWEEN_TIME);
                    _editComplete.OnNext(PlayerType.Master);
                })
                .AddTo(this);

            editCompleteClient
                .OnClickAsObservable()
                .Subscribe(_ =>
                {
                    buttonContainerClient
                        .DOAnchorPosY(HIDE_HEIGHT, UiConfig.TWEEN_TIME);
                    _editComplete.OnNext(PlayerType.Client);
                })
                .AddTo(this);
        }
    }
}