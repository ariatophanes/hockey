using System;
using UnityEngine;

public interface IPhysicalObject
{
    event Action onStop;
    event Action<string> onCollidedWithObject, onCollidedWithTrigger;
    void ApplyForce(Vector2 force);
    void SetPosition(Vector2 pos);
}