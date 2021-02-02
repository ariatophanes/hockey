using System;
using UnityEngine;

public class TouchInputService : IInputService
{

    public event EventHandler<OnDragEventArgs> onDrag;
    public event EventHandler<OnStartDragEventArgs> onStartDrag;
    public event EventHandler<OnEndDragEventArgs> onEndDrag;
    
    private bool drag = false;
    private Vector3 startPos;

    public void Tick()
    {
        StartDragProcessing();

        EndDragProcessing();

        DragProcessing();
    }

    private void DragProcessing()
    {
        if (drag && Input.GetMouseButton(0))
        {
            Vector3 d = startPos - Input.mousePosition;
            onDrag?.Invoke(this, new OnDragEventArgs() {delta = d});
        }
    }

    private void EndDragProcessing()
    {
        if (!Input.GetMouseButtonUp(0)) return;

        drag = false;
        onEndDrag?.Invoke(this, new OnEndDragEventArgs(){pointerPos = Input.mousePosition});
    }

    private void StartDragProcessing()
    {
        if (Input.GetMouseButton(0) && !drag)
        {
            drag = true;
            onStartDrag?.Invoke(this, new OnStartDragEventArgs(){pointerPos = Input.mousePosition});
            startPos = Input.mousePosition;
        }
    }
}