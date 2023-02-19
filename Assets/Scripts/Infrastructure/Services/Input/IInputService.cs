using System;
using UnityEngine;

namespace Diabloid
{
    public interface IInputService
    {
        event Action<Vector3> OnGroundTouched;
        event Action<GameObject> OnEnemyTouched;
    }
}