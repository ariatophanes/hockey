using System;
using UnityEngine;
using Zenject;

public interface IInputService : ITickable
{
    event EventHandler<OnDragEventArgs> onDrag;
    event EventHandler<OnStartDragEventArgs> onStartDrag;
    event EventHandler<OnEndDragEventArgs> onEndDrag;
}