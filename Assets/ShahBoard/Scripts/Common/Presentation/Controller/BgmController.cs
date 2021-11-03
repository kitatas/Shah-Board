using ShahBoard.Common.Domain.UseCase;
using VContainer;

namespace ShahBoard.Common.Presentation.Controller
{
    public sealed class BgmController : BaseSoundController
    {
        private IBgmUseCase _bgmUseCase;

        [Inject]
        private void Construct(IBgmUseCase bgmUseCase)
        {
            _bgmUseCase = bgmUseCase;
        }

        public void Play(BgmType bgmType, bool isLoop = false)
        {
            var clip = _bgmUseCase.GetClip(bgmType);
            if (clip == null || audioSource.clip == clip)
            {
                return;
            }

            audioSource.clip = clip;
            audioSource.loop = isLoop;
            audioSource.Play();
        }

        public void Stop()
        {
            audioSource.Stop();
        }
    }
}