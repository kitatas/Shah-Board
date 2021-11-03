using System;
using ShahBoard.Common.Domain.UseCase;
using UnityEngine;
using VContainer;

namespace ShahBoard.Common.Presentation.Controller
{
    public sealed class SeController : BaseSoundController
    {
        private ISeUseCase _seUseCase;

        [Inject]
        private void Construct(ISeUseCase seUseCase)
        {
            _seUseCase = seUseCase;
        }

        public void Play(SeType seType)
        {
            Play(_seUseCase.GetClip(seType));
        }

        public void Play(ButtonType buttonType)
        {
            try
            {
                var clip = _seUseCase.GetClip(buttonType);
                Play(clip);
            }
            catch (Exception e)
            {
                Debug.LogError($"[{nameof(SeController)}] : {e}");
                throw;
            }
        }

        private void Play(AudioClip clip)
        {
            if (clip == null)
            {
                return;
            }

            audioSource.PlayOneShot(clip);
        }
    }
}