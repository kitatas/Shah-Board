using UnityEngine;

namespace ShahBoard.Common.Domain.UseCase
{
    public interface IBgmUseCase
    {
        AudioClip GetClip(BgmType bgmType);
    }
}