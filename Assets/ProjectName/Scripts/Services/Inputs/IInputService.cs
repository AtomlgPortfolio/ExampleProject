using ProjectName.Scripts.Infrastructure.Services;
using UnityEngine;

namespace ProjectName.Scripts.Services.Inputs
{
    public interface IInputService : IService
    {
        Vector2 Axis { get; }

        bool AttackButtonUp();
    }
}