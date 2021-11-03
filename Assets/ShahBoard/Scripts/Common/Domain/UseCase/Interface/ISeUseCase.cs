using UnityEngine;

namespace ShahBoard.Common.Domain.UseCase
{
    public interface ISeUseCase
    {
        AudioClip GetClip(SeType seType);
        AudioClip GetClip(ButtonType buttonType);
    }
}