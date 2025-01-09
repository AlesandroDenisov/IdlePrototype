using IdleArcade.Core;
using IdleArcade.Services;
using UnityEngine;

namespace IdleArcade.Services.Input
{
    public interface IInputService : IService
    {
        Vector2 Axis { get; }

        //bool IsAttackButtonUp();
    }
}