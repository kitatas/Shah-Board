using UnityEngine;

namespace ShahBoard.InGame.Domain.UseCase
{
    public sealed class InputUseCase
    {
        public bool isTap => Input.GetMouseButtonDown(0);
        public bool isDrag => Input.GetMouseButton(0);
        public Vector3 tapPosition => Input.mousePosition;
    }
}