using UnityEngine;

namespace ProjectName.Scripts.Services.Inputs
{
    public class MobileInputService : InputService
    {
        public override Vector2 Axis => InputAxis();
    }
}