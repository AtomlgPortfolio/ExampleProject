using System;

namespace ProjectName.Scripts.Logic
{
    public interface IHealth
    {
        float Value { get; set; }
        float MaxValue { get; set; }
        event Action Changed;
        void TakeDamage(float damage);
    }
}