using System;
using UnityEngine;

public interface IThrowService : IDisableableService
{
    event Action<Vector3> onThrow;
}