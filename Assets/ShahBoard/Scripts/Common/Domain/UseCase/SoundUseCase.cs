using System;
using ShahBoard.Common.Domain.Repository;
using UnityEngine;

namespace ShahBoard.Common.Domain.UseCase
{
    public sealed class SoundUseCase : IBgmUseCase, ISeUseCase
    {
        private readonly SoundRepository _soundRepository;

        public SoundUseCase(SoundRepository soundRepository)
        {
            _soundRepository = soundRepository;
        }

        public AudioClip GetClip(BgmType bgmType)
        {
            return _soundRepository.Find(bgmType);
        }

        public AudioClip GetClip(SeType seType)
        {
            return _soundRepository.Find(seType);
        }

        public AudioClip GetClip(ButtonType buttonType)
        {
            switch (buttonType)
            {
                case ButtonType.Decision:
                    return GetClip(SeType.Decision);
                case ButtonType.Cancel:
                    return GetClip(SeType.Cancel);
                default:
                    throw new ArgumentOutOfRangeException(nameof(buttonType), buttonType, null);
            }
        }
    }
}