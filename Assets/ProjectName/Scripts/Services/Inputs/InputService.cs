using UnityEngine;

namespace ProjectName.Scripts.Services.Inputs
{
    public abstract class InputService : IInputService
    {
        protected const string Horizontal = "Horizontal";
        protected const string Vertical = "Vertical";
        private const int AttackMouseButton = 0;

        public abstract Vector2 Axis { get; }

        public bool AttackButtonUp() =>
            Input.GetMouseButtonUp(AttackMouseButton) || Input.GetKeyUp(KeyCode.Space);

        protected Vector2 InputAxis() =>
            new Vector2(Input.GetAxis(Horizontal), Input.GetAxis(Vertical));
    }
}